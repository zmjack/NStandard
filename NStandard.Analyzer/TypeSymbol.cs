using NStandard.Analyzer.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypeSharp;

namespace NStandard.Analyzer;

public class TypeSymbol(string ns, string name, bool isValueType)
{
    public TypeSymbol? Parent { get; set; }
    public string Namespace { get; set; } = ns;
    public string Name { get; set; } = name;
    public bool IsValueType { get; set; } = isValueType;

    public override string ToString()
    {
        IEnumerable<TypeSymbol> types =
        [
            ..GetParents().Reverse(),
            this,
        ];
        return string.Join(".", types.Select(x => x.Name));
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

        sb.AppendLine($"namespace {Namespace}\r\n{"{"}");
        indent++;

        IEnumerable<TypeSymbol> types =
        [
            ..GetParents().Reverse(),
            this,
        ];
        foreach (var parent in types)
        {
            sb.AppendLine($"{indent}public partial {(parent.IsValueType ? "struct" : "class")} {parent.Name}");
            sb.AppendLine($"{indent}{"{"}");
            indent++;
        }
        foreach (var line in code.GetLines())
        {
            sb.AppendLine($"{indent}{line}");
        }
        foreach (var parent in types)
        {
            indent--;
            sb.AppendLine($"{indent}{"}"}");
        }
        sb.AppendLine($"{"}"}");

        return sb.ToString();
    }
}
