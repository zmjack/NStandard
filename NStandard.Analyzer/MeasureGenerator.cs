using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace NStandard.Analyzer;

[Generator]
public class MeasureGenerator : ISourceGenerator
{
    public const string MeasureAttributeName = "NStandard.Measures.MeasureAttribute";

    [DebuggerDisplay("{Symbol}")]
    private class GraphNode
    {
        public TypeSymbol Symbol { get; set; }
        public List<GraphLine> Lines = [];

        public GraphNode(TypeSymbol symbol)
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

        private GraphNode GetOrCreate(TypeSymbol symbol)
        {
            var node = _list.Find(x => x.Symbol == symbol);
            if (node is null)
            {
                node = new GraphNode(symbol);
                _list.Add(node);
            }
            return node;
        }

        public void Add(TypeSymbol symbol)
        {
            GetOrCreate(symbol);
        }

        public void Add(TypeSymbol start, TypeSymbol end, decimal coef)
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

    private class TypeSymbol
    {
        public string Namespace { get; set; }
        public string Name { get; set; }
        public string Measure { get; set; }

        public override string ToString() => $"{Namespace}.{Name}";

        public string GetSimplifiedName(string ns)
        {
            var fullName = ToString();
            if (fullName.StartsWith(ns)) return fullName.Substring(ns.Length + 1);
            else return fullName;
        }
    }

    [DebuggerDisplay("{Symbol} = {Coef} * {CoefSymbol}")]
    private class Edge
    {
        public TypeSymbol Symbol { get; set; }
        public TypeSymbol CoefSymbol { get; set; }
        public int Coef { get; set; }
    }

    [DebuggerDisplay("{CoefSymbol} : {Coef}")]
    private class WeightedEdge
    {
        public TypeSymbol? CoefSymbol { get; set; }
        public decimal Coef { get; set; }
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var symbolList = new List<TypeSymbol>();
        TypeSymbol GetSymbol(string ns, string name)
        {
            var find = symbolList.Find(x => x.Namespace == ns && x.Name == name);
            if (find is not null) return find;

            var symbol = new TypeSymbol
            {
                Namespace = ns,
                Name = name,
            };
            symbolList.Add(symbol);
            return symbol;
        }

        var syntaxTrees = context.Compilation.SyntaxTrees;
        var edgeList = new List<Edge>();
        foreach (var tree in syntaxTrees)
        {
            var semantic = context.Compilation.GetSemanticModel(tree);
            var namespaces = tree.GetRoot()
                .DescendantNodesAndSelf()
                .OfType<BaseNamespaceDeclarationSyntax>();

            if (!namespaces.Any()) continue;

            foreach (var ns in namespaces)
            {
                var nsName = ns.Name.ToString();
                var structs = ns.DescendantNodesAndSelf().OfType<StructDeclarationSyntax>();

                foreach (var _struct in structs)
                {
                    var name = _struct.Identifier.Text;
                    var attributes = _struct.AttributeLists.SelectMany(x => x.Attributes);
                    TypeSymbol? symbol = null;

                    foreach (var attr in attributes)
                    {
                        var info = semantic.GetTypeInfo(attr);
                        if (!info.ConvertedType!.ToString().StartsWith(MeasureAttributeName)) continue;

                        symbol ??= GetSymbol(ns.Name.ToString(), name);
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
                                var coefSymbol = GetSymbol(coefType.ContainingNamespace.ToString(), coefType.Name);
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
            }
        }

        if (edgeList.Count == 0) return;

        var graph = new Graph();
        foreach (var edge in edgeList)
        {
            graph.Add(edge.Symbol, edge.CoefSymbol, edge.Coef);
        }

        foreach (var node in graph)
        {
            var lines = graph.GetDirectLines(node);
            var symbol = node.Symbol;
            var builder = new StringBuilder();

            builder.AppendLine(
$"""
// <auto-generated/>
using System;
using NStandard.Measures;

namespace {symbol.Namespace}
{"{"}
    public partial struct {symbol.Name} : IMeasurable
    {"{"}
        public string Measure => {(symbol.Measure is null ? "null" : $"{symbol.Measure}")};
        public decimal Value {"{"} get; set; {"}"}

        #region Core
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
        #endregion

        #region Converting
"""
            );

            foreach (var x in lines)
            {
                builder.AppendLine(
$"""
        public static implicit operator {x.Node.Symbol.GetSimplifiedName(symbol.Namespace)}({symbol.Name} @this) => new(@this.Value * {x.Coef}m);
"""
                );
            }
            builder.AppendLine(
$"""
        #endregion
    {"}"}
{"}"}
"""
            );

            var source = builder.ToString();
            context.AddSource($"{symbol}.g.cs", source);
        }
    }

    public void Initialize(GeneratorInitializationContext context)
    {
#if DEBUG
        // if (!Debugger.IsAttached) Debugger.Launch();
#endif
    }
}