using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
namespace NStandard.Analyzer.Extensions;

internal static class SourceProductionContextExtensions
{
    private static void Report(SourceProductionContext @this, ErrorKind kind, Location location)
    {
        var descriptor = kind switch
        {
            ErrorKind.MissingPartialKeyword => Errors.MissingPartialKeyword,
            ErrorKind.NotNumberType => Errors.NotNumberType,
            ErrorKind.NotArray => Errors.NotArray,
            ErrorKind.NotPropertyChangedOrPropertyChanging => Errors.NotPropertyChangedOrPropertyChanging,
            _ => Errors.Default,
        };
        var diagnostic = Diagnostic.Create(descriptor, location);
        @this.ReportDiagnostic(diagnostic);
    }

    internal static void Report(this SourceProductionContext @this, ErrorKind kind, BaseTypeDeclarationSyntax syntax)
    {
        Report(@this, kind, syntax.Identifier.GetLocation());
    }

    internal static void Report(this SourceProductionContext @this, ErrorKind kind, PropertyDeclarationSyntax syntax)
    {
        Report(@this, kind, syntax.Identifier.GetLocation());
    }
}
