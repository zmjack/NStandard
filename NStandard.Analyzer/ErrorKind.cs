using Microsoft.CodeAnalysis;

namespace NStandard.Analyzer;

public enum ErrorKind
{
    Default,
    MissingPartialKeyword,
    NotNumberType,
    NotArray,
    NotPropertyChangedOrPropertyChanging,
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
    internal static readonly DiagnosticDescriptor NotPropertyChangedOrPropertyChanging = new("NS004",
        "The type declaration does not implement INotifyPropertyChanged or INotifyPropertyChanging",
        "The type declaration must implement INotifyPropertyChanged or INotifyPropertyChanging",
        "Design", DiagnosticSeverity.Error, true
    );
    internal static readonly DiagnosticDescriptor Default = new("NS000",
        "An unknown error occurred",
        "An unknown error occurred",
        "Design", DiagnosticSeverity.Error, true
    );
}