using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace NStandard.Analyzer.Generators;

[Generator]
public class MeasureGenerator : IIncrementalGenerator
{
    public const string FeatureAttributeName = "NStandard.Measures.MeasureAttribute";
    private readonly TypeDetector _typeDetector = new();

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
#if DEBUG
        //if (!System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Launch();
#endif
        var provider = context.SyntaxProvider
            .ForAttributeWithMetadataName(FeatureAttributeName,
                static (node, _) => node is TypeDeclarationSyntax,
                static (ctx, _) => (ctx.TargetNode as TypeDeclarationSyntax)!
            );
        var compilation = context.CompilationProvider.Combine(provider.Collect());
        context.RegisterSourceOutput(compilation, Execute);
    }

    [DebuggerDisplay("{Symbol}")]
    private class GraphNode
    {
        public CustomTypeSymbol Symbol { get; set; }
        public List<GraphLine> Lines = [];

        public GraphNode(CustomTypeSymbol symbol)
        {
            Symbol = symbol;
        }
    }

    [DebuggerDisplay("{Node} = {Coef}")]
    private class GraphLine
    {
        public GraphNode Node { get; set; }
        public decimal Coef { get; set; }
    }

    private class Graph : IEnumerable<GraphNode>
    {
        private readonly List<GraphNode> _list = [];

        private GraphNode GetOrCreate(CustomTypeSymbol symbol)
        {
            var node = _list.Find(x => x.Symbol == symbol);
            if (node is null)
            {
                node = new GraphNode(symbol);
                _list.Add(node);
            }
            return node;
        }

        public void Add(CustomTypeSymbol symbol)
        {
            GetOrCreate(symbol);
        }

        public void Add(CustomTypeSymbol start, CustomTypeSymbol end, decimal coef)
        {
            var node = GetOrCreate(start);
            var target = GetOrCreate(end);

            var found = node.Lines.Any(x => x.Node == target);
            if (found) return;

            node.Lines.Add(new GraphLine
            {
                Node = target,
                Coef = coef,
            });

            target.Lines.Add(new GraphLine
            {
                Node = node,
                Coef = 1 / coef,
            });
        }

        public GraphLine[] GetDirectLines(GraphNode node)
        {
            List<GraphNode> visited = [];

            IEnumerable<GraphLine> GetPaths(GraphNode node, decimal coef)
            {
                if (!visited.Exists(x => x == node))
                {
                    visited.Add(node);
                    foreach (var line in node.Lines)
                    {
                        if (visited.Exists(x => x == line.Node)) continue;

                        var nextCoef = coef * line.Coef;
                        yield return new GraphLine
                        {
                            Node = line.Node,
                            Coef = coef * line.Coef,
                        };

                        foreach (var subNode in GetPaths(line.Node, nextCoef))
                        {
                            yield return subNode;
                        }
                    }
                }
            }

            return GetPaths(node, 1).ToArray();
        }

        public IEnumerator<GraphNode> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }

    private class CustomTypeSymbol : TypeSymbol
    {
        public CustomTypeSymbol(string ns, string name, bool isValueType) : base(ns, ["public"], name, isValueType)
        {
        }

        public string Measure { get; set; }
        public string[] DefinedIdentifiers { get; set; }
    }

    [DebuggerDisplay("{Symbol} = {Coef} * {CoefSymbol}")]
    private class Edge
    {
        public CustomTypeSymbol Symbol { get; set; }
        public CustomTypeSymbol CoefSymbol { get; set; }
        public int Coef { get; set; }
    }

    public void Execute(SourceProductionContext context, (Compilation, ImmutableArray<TypeDeclarationSyntax>) tuple)
    {
        var (compilation, nodes) = tuple;
        if (nodes.Length == 0) return;

        var symbolList = new List<CustomTypeSymbol>();
        CustomTypeSymbol GetSymbol(string ns, string name, bool isValueType)
        {
            var find = symbolList.Find(x => x.Namespace == ns && x.Name == name);
            if (find is not null) return find;

            var symbol = new CustomTypeSymbol(ns, name, isValueType);
            symbolList.Add(symbol);
            return symbol;
        }

        var edgeList = new List<Edge>();
        foreach (var typeDeclaration in nodes)
        {
            var semantic = compilation.GetSemanticModel(typeDeclaration.SyntaxTree);
            var tsymbol = _typeDetector.GetSymbol(compilation, typeDeclaration);
            var name = typeDeclaration.Identifier.Text;
            var attributes = typeDeclaration.AttributeLists.SelectMany(x => x.Attributes);
            CustomTypeSymbol symbol = GetSymbol(tsymbol.Namespace!, name, true);

            var definedlist = new List<string>();
            var props = typeDeclaration.ChildNodes().OfType<PropertyDeclarationSyntax>();
            foreach (var prop in props)
            {
                var _name = prop.Identifier.Text;
                if (_name == "ForceAggregate") definedlist.Add(_name);
            }
            var methods = typeDeclaration.ChildNodes().OfType<MethodDeclarationSyntax>();
            foreach (var method in methods)
            {
                var _name = method.Identifier.Text;
                if (_name == "CanAggregate") definedlist.Add(_name);
            }
            symbol.DefinedIdentifiers = definedlist.ToArray();

            foreach (var attr in attributes)
            {
                var info = semantic.GetTypeInfo(attr);
                if (!info.ConvertedType!.ToString().StartsWith(FeatureAttributeName)) continue;

                var parentKind = typeDeclaration.Parent!.Kind();
                if (parentKind != SyntaxKind.NamespaceDeclaration && parentKind != SyntaxKind.FileScopedNamespaceDeclaration)
                {
                    var discriptor = new DiagnosticDescriptor(
                        "ERR003",
                        "Unsupported Location",
                        "Nested types are not supported.",
                        "Generator",
                        DiagnosticSeverity.Error,
                        true
                    );
                    var error = Diagnostic.Create(discriptor, attr.GetLocation());
                    context.ReportDiagnostic(error);
                    continue;
                }

                var typeArguments = (info.ConvertedType as INamedTypeSymbol)!.TypeArguments;
                if (typeArguments.Any())
                {
                    // argument[0]
                    var arg0 = attr.ArgumentList!.Arguments[0];
                    var kind = arg0.Expression.Kind();
                    if (kind == SyntaxKind.NumericLiteralExpression)
                    {
                        var coefType = typeArguments[0];
                        var coef = int.Parse(arg0.ToString());
                        var coefSymbol = GetSymbol(coefType.ContainingNamespace.ToString(), coefType.Name, true);
                        edgeList.Add(new()
                        {
                            Symbol = symbol,
                            CoefSymbol = coefSymbol,
                            Coef = coef,
                        });
                    }
                    else
                    {
                        var discriptor = new DiagnosticDescriptor(
                            "ERR001",
                            "Expression Error",
                            "Only constant number is supported.",
                            "Generator",
                            DiagnosticSeverity.Error,
                            true
                        );
                        var error = Diagnostic.Create(discriptor, arg0.GetLocation());
                        context.ReportDiagnostic(error);
                    }
                }
                else
                {
                    var arg0 = attr.ArgumentList!.Arguments[0];
                    var kind = arg0.Expression.Kind();
                    if (kind == SyntaxKind.StringLiteralExpression)
                    {
                        symbol.Measure = arg0.ToString();
                    }
                    else
                    {
                        var discriptor = new DiagnosticDescriptor(
                            "ERR002",
                            "Expression Error",
                            "Only constant string is supported.",
                            "Generator",
                            DiagnosticSeverity.Error,
                            true
                        );
                        var error = Diagnostic.Create(discriptor, arg0.GetLocation());
                        context.ReportDiagnostic(error);
                    }
                }
            }
        }

        if (edgeList.Count == 0) return;

        var graph = new Graph();
        foreach (var edge in edgeList)
        {
            graph.Add(edge.Symbol, edge.CoefSymbol, edge.Coef);
        }

        List<string> names = [];
        foreach (var node in graph)
        {
            var lines = graph.GetDirectLines(node);
            var symbol = node.Symbol;
            var fileName = names.Count(name => symbol.ToString().ToLower() == name.ToLower()) switch
            {
                0 => $"{node.Symbol}",
                int c => $"{node.Symbol}_{c}",
            };
            names.Add(symbol.ToString());

            var builder = new StringBuilder();

            builder.AppendLine($"""
            // <auto-generated/>
            using System;
            using NStandard.Measures;

            namespace {symbol.Namespace}
            {"{"}
                public partial struct {symbol.Name} : IMeasurable, IAdditionMeasurable<{symbol.Name}>
                {"{"}
                    public static string Measure {"{"} get; {"}"} = {(symbol.Measure is null ? "\"\"" : $"{symbol.Measure}")};        
                    public decimal Value {"{"} get; set; {"}"}

                    {(!symbol.DefinedIdentifiers.Contains("ForceAggregate") ? "public static bool ForceAggregate => true;" : "// ForceAggregate")}
                    {(!symbol.DefinedIdentifiers.Contains("CanAggregate") ? $"public bool CanAggregate({symbol.Name} other) => true;" : "// CanAggregate")}

                    public {symbol.Name}(decimal value) => Value = value;
                    public {symbol.Name}(short value) => Value = (decimal)value;
                    public {symbol.Name}(int value) => Value = (decimal)value;
                    public {symbol.Name}(long value) => Value = (decimal)value;
                    public {symbol.Name}(ushort value) => Value = (decimal)value;
                    public {symbol.Name}(uint value) => Value = (decimal)value;
                    public {symbol.Name}(ulong value) => Value = (decimal)value;
                    public {symbol.Name}(float value) => Value = (decimal)value;
                    public {symbol.Name}(double value) => Value = (decimal)value;

                    public static {symbol.Name} operator +({symbol.Name} left, {symbol.Name} right) => new(left.Value + right.Value);
                    public static {symbol.Name} operator -({symbol.Name} left, {symbol.Name} right) => new(left.Value - right.Value);
                    public static {symbol.Name} operator *({symbol.Name} left, decimal right) => new(left.Value * right);
                    public static {symbol.Name} operator /({symbol.Name} left, decimal right) => new(left.Value / right);
                    public static decimal operator /({symbol.Name} left, {symbol.Name} right) => left.Value / right.Value;

                    public static bool operator ==({symbol.Name} left, {symbol.Name} right) => left.Value == right.Value;
                    public static bool operator !=({symbol.Name} left, {symbol.Name} right) => left.Value != right.Value;
                    public static bool operator <({symbol.Name} left, {symbol.Name} right) => left.Value < right.Value;
                    public static bool operator <=({symbol.Name} left, {symbol.Name} right) => left.Value <= right.Value;
                    public static bool operator >({symbol.Name} left, {symbol.Name} right) => left.Value > right.Value;
                    public static bool operator >=({symbol.Name} left, {symbol.Name} right) => left.Value >= right.Value;
    
                    public static implicit operator {symbol.Name}(decimal value) => new(value);
                    public static implicit operator {symbol.Name}(short value) => new((decimal)value);
                    public static implicit operator {symbol.Name}(int value) => new((decimal)value);
                    public static implicit operator {symbol.Name}(long value) => new((decimal)value);
                    public static implicit operator {symbol.Name}(ushort value) => new((decimal)value);
                    public static implicit operator {symbol.Name}(uint value) => new((decimal)value);
                    public static implicit operator {symbol.Name}(ulong value) => new((decimal)value);
                    public static implicit operator {symbol.Name}(float value) => new((decimal)value);
                    public static implicit operator {symbol.Name}(double value) => new((decimal)value);
    
                    public override bool Equals(object obj)
                    {"{"}
                        if (obj is not {symbol.Name} other) return false;
                        return Value == other.Value;
                    {"}"}

                    public override int GetHashCode() => (int)(Value % int.MaxValue);
                    public override string ToString() => $"{"{"}Value{"}"} {"{"}Measure{"}"}";
            """);

            if (lines.Any()) builder.AppendLine();

            foreach (var x in lines)
            {
                var retName = x.Node.Symbol.GetSimplifiedName(symbol.Namespace);
                builder.AppendLine($"""
                    public static implicit operator {retName}({symbol.Name} @this) => new(@this.Value * {x.Coef}m);
            """);
            }

            builder.AppendLine($"""
                {"}"}
            {"}"}
            """);
            context.AddSource($"{fileName}.g.cs", builder.ToString());
        }
    }
}
