using System;

namespace NStandard.Measures;

[Obsolete("Use IMeasurable instead.")]
public interface IMeasurable<TValue>
{
    TValue Value { get; set; }
}

public interface IMeasurable
{
    string? Measure { get; }
    decimal Value { get; set; }
}
