using Microsoft.CodeAnalysis;

namespace NStandard.Analyzer;

public enum ErrorKind
{
    Other,
    MissingPartialKeyword,
    NotNumberType,
    NotArray,
}

internal static class Errors
{
    internal static readonly DiagnosticDescriptor MissingPartialKeyword = new("NS001",
        "The `partial` keyword is missing",
        "The target must be marked with the `partial` keyword",
        "Design", DiagnosticSeverity.Error, true
    );
    internal static readonly DiagnosticDescriptor NotNumberType = new("NS002",
        "The target is not a number",
        "The target must be a number",
        "Design", DiagnosticSeverity.Error, true
    );
    internal static readonly DiagnosticDescriptor NotArray = new("NS003",
        "The target is not an array",
        "The target must be an array",
        "Design", DiagnosticSeverity.Error, true
    );
}