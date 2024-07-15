using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NStandard.Analyzer;

[Generator]
public class MeasureGenerator : ISourceGenerator
{
    public const string InterfaceName = "NStandard.Measures.IMeasurable";
    public const string MeasureAttributeName = "NStandard.Measures.MeasureAttribute";

    private class TypeSymbol
    {
        public string Namespace { get; set; }
        public string Name { get; set; }

        public override string ToString() => $"{Namespace}.{Name}";

        public string GetSimplifiedName(string ns)
        {
            var fullName = ToString();
            if (fullName.StartsWith(ns)) return fullName.Substring(ns.Length + 1);
            else return fullName;
        }
    }

    [DebuggerDisplay("{Symbol} = {Coef} * {CoefSymbol} : {ValueType}")]
    private class Edge
    {
        public TypeSymbol Symbol { get; set; }
        public TypeSymbol? CoefSymbol { get; set; }
        public int Coef { get; set; }
        public string ValueType => "decimal";
    }

    private class Snippet
    {
        public TypeSymbol CoefSymbol { get; set; }
        public decimal Coef { get; set; }
    }

    void AttachSnippetFirst(LinkedList<Snippet> snippetList, LinkedListNode<Edge> node, decimal coef)
    {
        var prev = node.Previous;
        if (prev is not null)
        {
            var nextCoef = coef / prev.Value.Coef;
            snippetList.AddFirst(new Snippet()
            {
                CoefSymbol = prev.Value.Symbol,
                Coef = nextCoef,
            });
            AttachSnippetFirst(snippetList, prev, nextCoef);
        }
    }

    void AttachSnippet(LinkedList<Snippet> snippetList, LinkedListNode<Edge> node, decimal coef)
    {
        if (node.Value.CoefSymbol is not null)
        {
            var nextCoef = coef * node.Value.Coef;
            snippetList.AddLast(new Snippet()
            {
                CoefSymbol = node.Value.CoefSymbol,
                Coef = nextCoef,
            });

            AttachSnippet(snippetList, node.Next, nextCoef);
        }
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
                    if (_struct.BaseList is null) continue;

                    var measurable = (
                        from type in _struct.BaseList!.Types
                        let info = semantic.GetTypeInfo(type.Type)
                        where info.ConvertedType!.ToString().StartsWith(InterfaceName)
                        select info
                    ).FirstOrDefault();

                    if (measurable.Type is null) continue;

                    var name = _struct.Identifier.Text;
                    var attributes = _struct.AttributeLists.SelectMany(x => x.Attributes);

                    foreach (var attr in attributes)
                    {
                        var info = semantic.GetTypeInfo(attr);
                        if (!info.ConvertedType!.ToString().StartsWith(MeasureAttributeName)) continue;

                        // argument[0]
                        var coef = int.Parse(attr.ArgumentList!.Arguments.ToString());
                        var coefType = (info.ConvertedType as INamedTypeSymbol)!.TypeArguments[0];

                        edgeList.Add(new()
                        {
                            Symbol = GetSymbol(ns.Name.ToString(), name),
                            CoefSymbol = GetSymbol(coefType.ContainingNamespace.ToString(), coefType.Name),
                            Coef = coef,
                        });
                    }
                }
            }
        }

        if (edgeList.Count == 0) return;

        void AttachFirst(LinkedList<Edge> list, TypeSymbol symbol)
        {
            var targets = edgeList.Where(x => x.CoefSymbol == symbol).ToArray();
            if (targets.Length == 1)
            {
                var target = targets.First();
                list.AddFirst(target);
                edgeList.Remove(target);
                AttachFirst(list, target.Symbol);
            }
            else
            {
                foreach (var target in targets)
                {
                    edgeList.Remove(target);
                }
            }
        }
        void AttachLast(LinkedList<Edge> list, TypeSymbol symbol)
        {
            var targets = edgeList.Where(x => x.Symbol == symbol).ToArray();
            if (targets.Length == 1)
            {
                var target = targets.First();
                list.AddLast(target);
                edgeList.Remove(target);
                AttachLast(list, target.CoefSymbol!);
            }
            else
            {
                foreach (var target in targets)
                {
                    edgeList.Remove(target);
                }
            }
        }
        LinkedList<Edge> TakeChain()
        {
            var lastIndex = edgeList.Count - 1;
            var list = new LinkedList<Edge>();
            var last = edgeList[lastIndex];

            list.AddLast(last);
            edgeList.RemoveAt(lastIndex);

            AttachFirst(list, last.Symbol);
            AttachLast(list, last.CoefSymbol!);

            list.AddLast(new Edge()
            {
                Symbol = list.Last.Value.CoefSymbol!,
                CoefSymbol = null,
            });

            return list;
        }

        var chains = new List<LinkedList<Edge>>();
        while (edgeList.Count > 0)
        {
            chains.Add(TakeChain());
        }

        foreach (var chain in chains)
        {
            foreach (var edge in chain)
            {
                var symbol = edge.Symbol;
                var snippetList = new LinkedList<Snippet>();

                var current = chain.Find(edge);
                AttachSnippetFirst(snippetList, current, 1);
                AttachSnippet(snippetList, current, 1);

                var source =
$"""
// <auto-generated/>
using System;

namespace {symbol.Namespace};

public partial struct {symbol.Name}
{"{"}
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
    
    public override bool Equals(object obj)
    {"{"}
        if (obj is not {symbol.Name} other) return false;
        return Value == other.Value;
    {"}"}

    public override int GetHashCode() => (int)(Value % int.MaxValue);
    public override string ToString() => $"{"{"}Value{"}"} {"{"}Measure{"}"}";

    public static implicit operator {symbol.Name}(decimal value) => new(value);
    public static implicit operator {symbol.Name}(short value) => new((decimal)value);
    public static implicit operator {symbol.Name}(int value) => new((decimal)value);
    public static implicit operator {symbol.Name}(long value) => new((decimal)value);
    public static implicit operator {symbol.Name}(ushort value) => new((decimal)value);
    public static implicit operator {symbol.Name}(uint value) => new((decimal)value);
    public static implicit operator {symbol.Name}(ulong value) => new((decimal)value);
    public static implicit operator {symbol.Name}(float value) => new((decimal)value);
    public static implicit operator {symbol.Name}(double value) => new((decimal)value);

{string.Join("\r\n",
from x in snippetList
select
$"""
    public static implicit operator {x.CoefSymbol.GetSimplifiedName(symbol.Namespace)}({symbol.Name} @this) => new(@this.Value * {x.Coef}m);
"""
)}
{"}"}
""";
                context.AddSource($"{symbol}.g.cs", source);
            }
        }
    }

    public void Initialize(GeneratorInitializationContext context)
    {
        // if (!Debugger.IsAttached) Debugger.Launch();
    }
}