using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NStandard.Analyzer.Extensions;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace NStandard.Analyzer.Generators;

[Generator]
public class FieldFeatureGenerator : IIncrementalGenerator
{
    public const string FeatureAttributeName = "NStandard.Design.FieldFeatureAttribute";
    public const string FieldBackendAttributeName = "NStandard.Design.FieldBackendAttribute";
    public const string FixedArrayAttributeName = "NStandard.Design.FixedArrayAttribute";
    private readonly TypeDetector _typeDetector = new();

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        AnalyzerDebugger.DebugAvailable();
        var provider = context.SyntaxProvider
            .ForAttributeWithMetadataName(FeatureAttributeName,
                static (node, _) => node is TypeDeclarationSyntax,
                static (ctx, _) => (ctx.TargetNode as TypeDeclarationSyntax)!
            );
        var compilation = context.CompilationProvider.Combine(provider.Collect());
        context.RegisterSourceOutput(compilation, Execute);
    }

    private enum PropertyType
    {
        Unknown,
        FieldBackend,
        FixedArray,
    }

    private class Property
    {
        public TypeSymbol DeclarationType { get; set; }
        public string Modifiers { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public PropertyType TypeKind { get; set; }
        public object? Tag { get; set; }
    }

    public void Execute(SourceProductionContext context, (Compilation, ImmutableArray<TypeDeclarationSyntax>) tuple)
    {
        var (compilation, nodes) = tuple;
        if (nodes.Length == 0) return;

        var usings = new HashSet<string>()
        {
            "System",
            "System.Runtime.CompilerServices",
        };
        var list = new List<Property>();

        foreach (var typeDeclaration in nodes)
        {
            if (!typeDeclaration.Modifiers.ContainsToken(SyntaxModifier.partial))
            {
                context.Report(ErrorKind.MissingPartialKeyword, typeDeclaration);
                continue;
            }

            var semantic = compilation.GetSemanticModel(typeDeclaration.SyntaxTree);
            var declarationType = _typeDetector.GetSymbol(compilation, typeDeclaration);
            var props = typeDeclaration.ChildNodes().OfType<PropertyDeclarationSyntax>();
            foreach (var prop in props)
            {
                var isTarget = false;
                var attributes = prop.AttributeLists.SelectMany(x => x.Attributes);
                foreach (var attr in attributes)
                {
                    var attrType = semantic.GetTypeInfo(attr);
                    var attrName = attrType.ConvertedType?.ToDisplayString();
                    var typeKind = attrName switch
                    {
                        FieldBackendAttributeName => PropertyType.FieldBackend,
                        FixedArrayAttributeName => PropertyType.FixedArray,
                        _ => PropertyType.Unknown
                    };

                    if (typeKind != PropertyType.Unknown)
                    {
                        object? tag = null;
                        if (typeKind == PropertyType.FixedArray)
                        {
                            if (!prop.Modifiers.ContainsToken(SyntaxModifier.partial))
                            {
                                context.Report(ErrorKind.MissingPartialKeyword, prop);
                                continue;
                            }
                            if (prop.Type.Kind() != SyntaxKind.ArrayType)
                            {
                                context.Report(ErrorKind.NotArray, prop);
                                continue;
                            }
                            var arg0 = attr.ArgumentList!.Arguments[0];
                            tag = int.Parse(arg0.ToString());
                        }

                        var propType = semantic.GetTypeInfo(prop.Type);
                        var propNamespaces = propType.ConvertedType!.GetUsingNamespaces();
                        foreach (var _ns in propNamespaces)
                        {
                            usings.Add(_ns.ToDisplayString());
                        }

                        list.Add(new()
                        {
                            DeclarationType = declarationType,
                            Modifiers = prop.Modifiers.ToString(),
                            Type = prop.Type!.ToString(),
                            Name = prop.Identifier.Text,
                            TypeKind = typeKind,
                            Tag = tag,
                        });
                        isTarget = true;
                        break;
                    }
                }

                if (!isTarget)
                {
                    //TODO: Collect InvocationExpressionSyntax in get/set accessors
                    //which invokes GetValue/SetValue methods
                    //var accessorList = prop.AccessorList;
                    //foreach (var accessor in accessorList!.Accessors)
                    //{
                    //}
                }
            }
        }

        foreach (var g in list.GroupBy(x => x.DeclarationType))
        {
            var builder = new StringBuilder();
            builder.AppendLine("// <auto-generated/>");
            foreach (var ns in usings.OrderBy(x => x))
            {
                builder.AppendLine($"using {ns};");
            }
            builder.AppendLine();

            var code = new StringBuilder();
            code.AppendLine($"""
            public struct Fields
            {"{"}
            """);
            foreach (var prop in g)
            {
                code.AppendLine($"""
                    public {prop.Type} {prop.Name} {"{"} get; set; {"}"}
                """);
            }
            code.AppendLine($"""
            {"}"}
            private Fields fields;
            """);
            code.AppendLine();

            {
                var props = g.Where(x => x.TypeKind == PropertyType.FieldBackend);
                code.AppendLine($"""
                public dynamic GetValue([CallerMemberName] string name = "")
                {"{"}
                    return name switch
                    {"{"}
                """);
                foreach (var prop in props)
                {
                    code.AppendLine($"""
                            nameof({prop.Name}) => fields.{prop.Name},
                    """);
                }
                code.AppendLine($"""
                        _ => throw new NotImplementedException($"FieldContainer.GetValue: {"{"}name{"}"} is not implemented."),
                    {"}"};
                {"}"}
                """);

                code.AppendLine($"""
                public void SetValue(dynamic value, [CallerMemberName] string name = "")
                {"{"}
                    switch (name)
                    {"{"}
                """);
                foreach (var prop in props)
                {
                    var property = $"{prop.Name}Property";
                    code.AppendLine($"""
                            case nameof({prop.Name}): fields.{prop.Name} = value; break;
                    """);
                }
                code.AppendLine($"""
                        default: throw new NotImplementedException($"FieldContainer.SetValue: {"{"}name{"}"} is not implemented.");
                    {"}"}
                {"}"}
                """);
                code.AppendLine();
            }
            {
                var props = g.Where(x => x.TypeKind == PropertyType.FixedArray);
                foreach (var prop in props)
                {
                    var fixedSize = (int)prop.Tag!;
                    code.AppendLine($"""
                    {string.Join(" ", prop.Modifiers)} {prop.Type} {prop.Name}
                    {"{"}
                        get
                        {"{"}
                            if (fields.{prop.Name} is null) fields.{prop.Name} = new int[{fixedSize}];
                            return fields.{prop.Name};
                        {"}"}
                        set
                        {"{"}
                            if (fields.{prop.Name} is null) fields.{prop.Name} = new int[{fixedSize}];
                            if (value.Length != {fixedSize}) throw new ArgumentException($"The length of {"{"}nameof({prop.Name}){"}"} must be {fixedSize}.");

                            int i = 0;
                            foreach (var element in value)
                            {"{"}
                                fields.{prop.Name}[i++] = element;
                            {"}"}
                        {"}"}
                    {"}"}
                    """);
                }
            }

            builder.AppendLine(g.Key.Format(code.ToString()));
            context.AddSource($"{g.Key}.g.cs", builder.ToString());
        }
    }
}
