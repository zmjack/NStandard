namespace NStandard.Data;

public record struct Moment
{
    public MomentType Type { get; set; }
    public long Value { get; set; }

#if NET6_0_OR_GREATER
    public static Moment From(DateOnly date, MomentType type)
    {
        return From(new DateTime(date.Year, date.Month, date.Day, 0, 0, 0), type);
    }
#endif

    public static Moment From(DateTime time, MomentType type)
    {
        long value = type switch
        {
            MomentType.Undefined => throw new NotSupportedException($"{type} is not supported."),
            MomentType.Nanosecond => throw new NotSupportedException($"{type} is not supported."),
            MomentType.Microsecond => throw new NotSupportedException($"{type} is not supported."),
            MomentType.Millisecond => (long)time.ElapsedMilliseconds(),
            MomentType.Second => (long)time.ElapsedSeconds(),
            MomentType.Minute => (long)time.ElapsedMinutes(),
            MomentType.Hour => (long)time.ElapsedHours(),
            MomentType.Day => (long)time.ElapsedDays(),
            MomentType.Week => (long)time.ElapsedDays() / 7,
            MomentType.Month => (long)time.ElapsedMonths(),
            MomentType.Quarter => (long)time.ElapsedMonths() / 3,
            MomentType.Year => (long)time.ElapsedYears(),
            _ => throw new NotImplementedException(),
        };

        return new()
        {
            Type = type,
            Value = value,
        };
    }

#if NET6_0_OR_GREATER
    public DateOnly ToDateOnly()
    {
        return DateOnly.FromDateTime(ToDateTime());
    }
#endif
    public DateTime ToDateTime()
    {
        var time = Type switch
        {
            MomentType.Undefined => throw new NotSupportedException($"{Type} is not supported."),
            MomentType.Nanosecond => throw new NotSupportedException($"{Type} is not supported."),
            MomentType.Microsecond => throw new NotSupportedException($"{Type} is not supported."),
            MomentType.Millisecond => DateTime.MinValue.AddMilliseconds(Value),
            MomentType.Second => DateTime.MinValue.AddSeconds(Value),
            MomentType.Minute => DateTime.MinValue.AddMinutes(Value),
            MomentType.Hour => DateTime.MinValue.AddHours(Value),
            MomentType.Day => DateTime.MinValue.AddDays(Value),
            MomentType.Week => DateTime.MinValue.AddDays(Value * 7),
            MomentType.Month => DateTime.MinValue.AddMonths((int)Value),
            MomentType.Quarter => DateTime.MinValue.AddMonths((int)Value * 3),
            MomentType.Year => DateTime.MinValue.AddYears((int)Value),
            _ => throw new NotImplementedException(),
        };
        return time;
    }
}
