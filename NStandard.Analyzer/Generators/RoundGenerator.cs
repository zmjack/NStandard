using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NStandard.Analyzer.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace NStandard.Analyzer.Generators;

[Generator]
public class RoundGenerator : IIncrementalGenerator
{
    public const string FeatureAttributeName = "NStandard.Design.RoundFeatureAttribute";
    public const string TargetAttributeName = "NStandard.Design.RoundAttribute";
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

    private class Info
    {
        public TypeSymbol DeclarationType { get; set; }
        public string Modifiers { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public int Digist { get; set; }
        public MidpointRounding Mode { get; set; }
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
        var list = new List<Info>();

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
                var attributes = prop.AttributeLists.SelectMany(x => x.Attributes);
                foreach (var attr in attributes)
                {
                    var attrType = semantic.GetTypeInfo(attr);
                    if (attrType.ConvertedType!.ToDisplayString() == TargetAttributeName)
                    {
                        var propType = semantic.GetTypeInfo(prop.Type);
                        var propNamespaces = propType.ConvertedType!.GetUsingNamespaces();
                        foreach (var _ns in propNamespaces)
                        {
                            usings.Add(_ns.ToDisplayString());
                        }

                        var propKeyword = propType.ConvertedType!.ToString();
                        if (propKeyword
                            is not "float" and not "float?"
                            and not "double" and not "double?"
                            and not "decimal" and not "decimal?")
                        {
                            context.Report(ErrorKind.NotNumberType, prop);
                            continue;
                        }

                        var roundArgument = attr.ArgumentList!.Arguments[0];
                        var digist = int.Parse(roundArgument.ToString());
                        var mode = MidpointRounding.ToEven;
                        if (attr.ArgumentList!.Arguments.Count == 2)
                        {
                            var modeArgument = attr.ArgumentList!.Arguments[1];
                            var memberAccess = (modeArgument.Expression as MemberAccessExpressionSyntax)!;
                            var identifierName = (memberAccess.Name as IdentifierNameSyntax)!;
                            var valueText = identifierName.Identifier.ValueText;
                            mode = (MidpointRounding)Enum.Parse(typeof(MidpointRounding), valueText);
                        }
                        list.Add(new()
                        {
                            DeclarationType = declarationType,
                            Modifiers = prop.Modifiers.ToString(),
                            Type = propKeyword,
                            Name = prop.Identifier.Text,
                            Digist = digist,
                            Mode = mode,
                        });
                        break;
                    }
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
            foreach (var prop in g)
            {
                var backingName = $"backing_{prop.Name}";
                var math = prop.Type switch
                {
                    "float" or "float?" => "MathF",
                    "double" or "double?" => "Math",
                    "decimal" or "decimal?" => "decimal",
                    _ => throw new NotSupportedException(),
                };
                var nullable = prop.Type.EndsWith("?");

                code.AppendLine($"""
                private {prop.Type} {backingName};
                {prop.Modifiers} {prop.Type} {prop.Name}
                {"{"}
                    get => {backingName};
                """);

                if (prop.Mode == MidpointRounding.ToEven)
                {
                    if (nullable)
                    {
                        code.AppendLine($"""
                            set => {backingName} = value.HasValue ? {math}.Round(value.Value, {prop.Digist}) : null;
                        """);
                    }
                    else
                    {
                        code.AppendLine($"""
                            set => {backingName} = {math}.Round(value, {prop.Digist});
                        """);
                    }
                }
                else
                {
                    if (nullable)
                    {
                        code.AppendLine($"""
                            set => {backingName} = value.HasValue ? {math}.Round(value.Value, {prop.Digist}, MidpointRounding.{prop.Mode}) : null;
                        """);
                    }
                    else
                    {
                        code.AppendLine($"""
                            set => {backingName} = {math}.Round(value, {prop.Digist}, MidpointRounding.{prop.Mode});
                        """);
                    }
                }
                code.AppendLine($"""
                {"}"}
                """);
            }

            builder.AppendLine(g.Key.Format(code.ToString()));
            context.AddSource($"{g.Key}.g.cs", builder.ToString());
        }
    }
}
