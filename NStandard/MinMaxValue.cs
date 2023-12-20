using System.ComponentModel;

namespace NStandard;

public struct MinMaxValue<T>(T min, T max)
{
    public T Min { get; set; } = min;
    public T Max { get; set; } = max;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void Deconstruct(out T min, out T max)
    {
        min = Min;
        max = Max;
    }
}
