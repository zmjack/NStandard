using System.ComponentModel;

namespace NStandard.ValueTuples;

public static class MinMaxPair
{
    public static MinMaxPair<T> Create<T>(T min, T max)
    {
        return new MinMaxPair<T>(min, max);
    }
}

public struct MinMaxPair<T>(T min, T max)
{
    public T Min { get; set; } = min;
    public T Max { get; set; } = max;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void Deconstruct(out T min, out T max)
    {
        min = Min;
        max = Max;
    }

#if NET5_0_OR_GREATER
    public static implicit operator MinMaxPair<T>((T Min, T Max) tuple)
    {
        return new MinMaxPair<T>
        {
            Min = tuple.Min,
            Max = tuple.Max,
        };
    }
#endif
}
