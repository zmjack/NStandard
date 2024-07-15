using System;

namespace NStandard.Measures;

[Obsolete("Use IMeasurable instead.")]
public interface IMeasurable<TValue>
{
    TValue Value { get; set; }
}

public interface IMeasurable
{
    decimal Value { get; set; }
    string Measure { get; }
}
