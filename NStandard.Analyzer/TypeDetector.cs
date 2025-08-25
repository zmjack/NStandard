using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NStandard.Analyzer;

public class TypeDetector()
{
    private readonly List<TypeSymbol> _list = [];

    public TypeSymbol GetSymbol(Compilation compilation, TypeDeclarationSyntax syntax)
    {
        TypeSymbol? parent = syntax.Parent switch
        {
            TypeDeclarationSyntax typeDecl => GetSymbol(compilation, typeDecl),
            _ => null,
        };
        string? ns = syntax.Parent switch
        {
            TypeDeclarationSyntax typeDecl => parent!.Namespace,
            NamespaceDeclarationSyntax nsDecl => nsDecl.Name.ToString(),
            FileScopedNamespaceDeclarationSyntax fsnsDecl => fsnsDecl.Name.ToString(),
            CompilationUnitSyntax cus => null,
            _ => throw new NotSupportedException($"Unsupported parent syntax: {syntax.Parent?.GetType().FullName}"),
        };

        var name = syntax.Identifier.Text;
        var find = _list.Find(x => x.Namespace == ns && x.Parent == parent && x.Name == name);
        if (find is not null) return find;

        var modifiers = syntax.Modifiers.Select(x => x.ValueText).ToArray();
        var isValueType = syntax is StructDeclarationSyntax;
        var symbol = new TypeSymbol(ns, modifiers, name, isValueType)
        {
            Parent = parent,
        };
        _list.Add(symbol);

        return symbol;
    }

    public TypeSymbol GetSymbol(string? ns, TypeDeclarationSyntax syntax)
    {
        var name = syntax.Identifier.Text;
        TypeSymbol? parent = syntax.Parent switch
        {
            TypeDeclarationSyntax typeDecl => GetSymbol(ns, typeDecl),
            NamespaceDeclarationSyntax nsDecl => null,
            FileScopedNamespaceDeclarationSyntax fsnsDecl => null,
            _ => throw new NotSupportedException($"Unsupported parent syntax: {syntax.Parent?.GetType().FullName}"),
        };

        var find = _list.Find(x => x.Namespace == ns && x.Parent == parent && x.Name == name);
        if (find is not null) return find;

        var modifiers = syntax.Modifiers.Select(x => x.ValueText).ToArray();
        var isValueType = syntax is StructDeclarationSyntax;
        var symbol = new TypeSymbol(ns, modifiers, name, isValueType)
        {
            Parent = parent,
        };
        _list.Add(symbol);

        return symbol;
    }
}
