using System;

namespace NStandard.Measures;

[Obsolete("Use IMeasurable instead.")]
public interface IMeasurable<TValue>
{
    TValue Value { get; set; }
}

public interface IAdditionMeasurable
{
}

public interface IMeasurable
{
#if NET7_0_OR_GREATER
    static abstract string Measure { get; }
#endif
    decimal Value { get; set; }
}
