using Microsoft.CodeAnalysis;

namespace NStandard.Analyzer
{
    public static class Errors
    {
        public static readonly DiagnosticDescriptor FieldFeatureDisabled = new DiagnosticDescriptor(
            "NA001",
            "NA001",
            "Declared class or struct need to be marked with FieldFeature",
            "Generator",
            DiagnosticSeverity.Error,
            true
        );
    }
}
