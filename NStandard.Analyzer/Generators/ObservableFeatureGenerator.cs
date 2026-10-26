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
public class ObservableFeatureGenerator : IIncrementalGenerator
{
    public const string FeatureAttributeName = "NStandard.ComponentModel.ObservableFeatureAttribute";
    private readonly TypeDetector _typeDetector = new();

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        AnalyzerDebugger.DebugAvailable();
        var provider = context.SyntaxProvider
            .ForAttributeWithMetadataName(FeatureAttributeName,
                static (node, _) => node is ClassDeclarationSyntax,
                static (ctx, _) => (ctx.TargetNode as ClassDeclarationSyntax)!
            );
        var compilation = context.CompilationProvider.Combine(provider.Collect());
        context.RegisterSourceOutput(compilation, Execute);
    }

    public void Execute(SourceProductionContext context, (Compilation, ImmutableArray<ClassDeclarationSyntax>) tuple)
    {
        var (compilation, nodes) = tuple;
        if (nodes.Length == 0) return;

        var usings = new HashSet<string>()
        {
            "System",
            "System.ComponentModel",
            "System.Collections.Generic"
        };

        foreach (var classDeclaration in nodes)
        {
            var semantic = compilation.GetSemanticModel(classDeclaration.SyntaxTree);
            var symbol = _typeDetector.GetSymbol(compilation, classDeclaration);

            var isChanging = false;
            var isChanged = false;
            foreach (var type in classDeclaration.BaseList!.Types)
            {
                var convertedType = semantic.GetTypeInfo(type.Type).ConvertedType!;
                var typeString = convertedType.ToDisplayString();
                if (typeString == "System.ComponentModel.INotifyPropertyChanged")
                {
                    isChanged = true;
                }
                if (typeString == "System.ComponentModel.INotifyPropertyChanging")
                {
                    isChanging = true;
                }
            }
            if (!isChanging && !isChanged)
            {
                context.Report(ErrorKind.NotPropertyChangedOrPropertyChanging, classDeclaration);
                continue;
            }

            var properties = classDeclaration.DescendantNodes().OfType<PropertyDeclarationSyntax>();
            var collector = new PropertyDependencyCollector();
            var dependencies = collector.Collect(semantic, classDeclaration);
            var reversedDependencies = collector.ReverseDependecies(dependencies);
            var code = new StringBuilder();

            foreach (var dep in dependencies)
            {
                var propType = semantic.GetTypeInfo(dep.Key.Type);
                var propNamespaces = propType.ConvertedType!.GetUsingNamespaces();
                foreach (var _ns in propNamespaces)
                {
                    usings.Add(_ns.ToDisplayString());
                }

                if (reversedDependencies.TryGetValue(dep.Key, out var notifications))
                {
                    var propertyName = dep.Key.Identifier.ValueText;
                    var backingName = $"backing_{propertyName}";

                    code.AppendLine($"""
                    private {dep.Key.Type} {backingName};
                    public partial {dep.Key.Type} {propertyName}
                    {"{"}
                        get => {backingName};
                        set
                        {"{"}
                            if (!EqualityComparer<{dep.Key.Type}>.Default.Equals({backingName}, value))
                            {"{"}
                    """);
                    if (isChanging)
                    {
                        code.AppendLine($"""
                                    PropertyChanging?.Invoke(this, new PropertyChangingEventArgs("{propertyName}"));
                        """);
                        foreach (var notification in notifications)
                        {
                            var name = notification.Identifier.ValueText;
                            code.AppendLine($"""
                                        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs("{name}"));
                            """);
                        }
                    }

                    code.AppendLine($"""
                                {backingName} = value;
                    """);

                    if (isChanged)
                    {
                        code.AppendLine($"""
                                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("{propertyName}"));
                        """);
                        foreach (var notification in notifications)
                        {
                            var name = notification.Identifier.ValueText;
                            code.AppendLine($"""
                                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("{name}"));
                            """);
                        }
                    }

                    code.AppendLine($"""
                            {"}"}
                        {"}"}
                    {"}"}
                    """);
                }
            }

            var builder = new StringBuilder();
            builder.AppendLine("// <auto-generated/>");
            foreach (var ns in usings.OrderBy(x => x))
            {
                builder.AppendLine($"using {ns};");
            }
            builder.AppendLine();

            var baseList = new List<string>();
            if (isChanging) baseList.Add("INotifyPropertyChanging");
            if (isChanged) baseList.Add("INotifyPropertyChanged");

            builder.AppendLine(symbol.Format(code.ToString(), $": {string.Join(", ", baseList)}"));
            context.AddSource($"{symbol}.g.cs", builder.ToString());
        }
    }
}
