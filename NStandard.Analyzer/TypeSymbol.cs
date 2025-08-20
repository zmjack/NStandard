using Microsoft.CodeAnalysis;
using NStandard.Analyzer.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypeSharp;

namespace NStandard.Analyzer;

public class TypeSymbol(string? ns, string[] modifiers, string name, bool isValueType)
{
    public TypeSymbol? Parent { get; set; }
    public string[] Modifiers { get; set; } = modifiers;
    public string? Namespace { get; set; } = ns;
    public string Name { get; set; } = name;
    public bool IsValueType { get; set; } = isValueType;

    public override string ToString()
    {
        IEnumerable<TypeSymbol> types =
        [
            ..GetParents().Reverse(),
            this,
        ];

        var typeName = string.Join(".", types.Select(x => x.Name));
        return ns is not null ? $"{ns}.{typeName}" : typeName;
    }

    public string GetSimplifiedName(string ns)
    {
        var fullName = ToString();
        if (fullName.StartsWith(ns)) return fullName.Substring(ns.Length + 1);
        else return fullName;
    }

    public IEnumerable<TypeSymbol> GetParents()
    {
        var current = Parent;
        while (current is not null)
        {
            yield return current;
            current = current.Parent;
        }
    }

    public string Format(string code)
    {
        var sb = new StringBuilder();
        var indent = new Indent(0, 4);

        if (Namespace is not null)
        {
            sb.AppendLine($"namespace {Namespace}\r\n{"{"}");
            indent++;
        }

        IEnumerable<TypeSymbol> symbols =
        [
            ..GetParents().Reverse(),
            this,
        ];
        foreach (var symbol in symbols)
        {
            sb.AppendLine($"{indent}{string.Join(" ", symbol.Modifiers)} {(symbol.IsValueType ? "struct" : "class")} {symbol.Name}");
            sb.AppendLine($"{indent}{"{"}");
            indent++;
        }
        foreach (var line in code.GetLines())
        {
            sb.AppendLine($"{indent}{line}");
        }
        foreach (var symbol in symbols)
        {
            indent--;
            sb.AppendLine($"{indent}{"}"}");
        }

        if (Namespace is not null)
        {
            sb.AppendLine($"{"}"}");
        }

        return sb.ToString();
    }
}
