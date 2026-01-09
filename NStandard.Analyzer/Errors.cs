using Microsoft.CodeAnalysis;

namespace NStandard.Analyzer
{
    public static class Errors
    {
        public static readonly DiagnosticDescriptor FieldFeatureTargetNeedPartialKeyword = new(
            "NA001",
            "NA001",
            "Targets marked with FieldFeature must be modified with the `partial` keyword",
            "Generator",
            DiagnosticSeverity.Error,
            true
        );
    }
}
