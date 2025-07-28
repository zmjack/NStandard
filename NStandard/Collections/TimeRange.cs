namespace NStandard.Collections;

#if NET6_0_OR_GREATER
public struct TimeRange(TimeOnly start, TimeOnly end)
{
    public TimeOnly Start { get; set; } = start;
    public TimeOnly End { get; set; } = end;

    public readonly bool Contains(TimeOnly time)
    {
        if (Start > End)
        {
            return Start <= time || time <= End;
        }
        else
        {
            return Start <= time && time <= End;
        }
    }

    public readonly bool Contains(TimeSpan timeSpan)
    {
        return Contains(new TimeOnly(timeSpan.Ticks));
    }

    public readonly TimeSpan Interval => End - Start;
}
#endif
