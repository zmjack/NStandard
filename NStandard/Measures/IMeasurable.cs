namespace NStandard.Measures;

[Obsolete("Use IMeasurable instead.")]
public interface IMeasurable<TValue>
{
    TValue Value { get; set; }
}

public interface IAdditionMeasurable<TSelf> where TSelf : IMeasurable, IAdditionMeasurable<TSelf>
{
#if NET7_0_OR_GREATER
    static abstract bool ForceAggregate { get; }
#endif
    bool CanAggregate(TSelf other);
}

public interface IMeasurable
{
#if NET7_0_OR_GREATER
    static abstract string Measure { get; }
#endif
    decimal Value { get; set; }
}
