using Microsoft.CodeAnalysis;

namespace NStandard.Analyzer
{
    public static class Errors
    {
        public static readonly DiagnosticDescriptor NeedPartialKeyword = new(
            "NA001",
            "NA001",
            "Target must be marked with the `partial` keyword",
            "Generator",
            DiagnosticSeverity.Error,
            true
        );

        public static readonly DiagnosticDescriptor NeedNumberType = new(
            "NA002",
            "NA002",
            "Target must be a number type",
            "Generator",
            DiagnosticSeverity.Error,
            true
        );
    }
}
