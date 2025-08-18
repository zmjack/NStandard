using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;

namespace NStandard.Analyzer;

public class TypeDetector()
{
    private readonly List<TypeSymbol> _list = [];

    public TypeSymbol GetSymbol(string ns, TypeDeclarationSyntax syntax)
    {
        var name = syntax.Identifier.Text;

        var find = _list.Find(x => x.Namespace == ns && x.Name == name);
        if (find is not null) return find;

        var isValueType = syntax is StructDeclarationSyntax;
        var symbol = new TypeSymbol(ns, name, isValueType);
        _list.Add(symbol);

        if (syntax.Parent is TypeDeclarationSyntax parent)
            symbol.Parent = GetSymbol(ns, parent);
        else if (syntax.Parent is NamespaceDeclarationSyntax nsDecl)
            symbol.Parent = null;
        else if (syntax.Parent is FileScopedNamespaceDeclarationSyntax fsnsDecl)
            symbol.Parent = null;
        else throw new NotSupportedException($"Unsupported parent syntax: {syntax.Parent?.GetType().FullName}");

        return symbol;
    }
}
