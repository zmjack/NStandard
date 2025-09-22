using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace NStandard.Analyzer.Extensions;

public static class ITypeSymbolExtensions
{
    public static IEnumerable<INamespaceSymbol> GetUsingNamespaces(this ITypeSymbol @this)
    {
        if (@this is INamedTypeSymbol namedType)
        {
            var ns = namedType.ContainingNamespace;
            if (ns is not null) yield return ns;
        }
        else if (@this is IArrayTypeSymbol arrayType)
        {
            var ns = arrayType.ElementType.ContainingNamespace;
            if (ns is not null) yield return ns;
        }
        else if (@this is IPointerTypeSymbol pointerType)
        {
            var ns = pointerType.PointedAtType.ContainingNamespace;
            if (ns is not null) yield return ns;
        }
        else if (@this is IFunctionPointerTypeSymbol functionPointer)
        {
            var ns = functionPointer.Signature.ReturnType.ContainingNamespace;
            if (ns is not null) yield return ns;
        }
        else
        {
            throw new NotSupportedException($"Unsupported type symbol: {@this.GetType().Name}");
        }
    }
}
