namespace NStandard;

public static class RangeEx
{
#if NET6_0_OR_GREATER
    public static IEnumerable<DateOnly> Create(DateOnly start, int length, DateOnlyType type)
    {
        if (type is DateOnlyType.Unspecified) throw new ArgumentException($"Please specify the {nameof(DateOnlyType)}.", nameof(type));

        var value = start;
        for (int i = 0; i < length; i++)
        {
            yield return value;
            switch (type)
            {
                case DateOnlyType.Year: value = value.AddYears(1); break;
                case DateOnlyType.Month: value = value.AddMonths(1); break;
                case DateOnlyType.Day: value = value.AddDays(1); break;
            }
        }
    }

    public static IEnumerable<DateOnly> CreateRange(DateOnly start, DateOnly end, DateOnlyType type)
    {
        if (type is DateOnlyType.Unspecified) throw new ArgumentException($"Please specify the {nameof(DateOnlyType)}.", nameof(type));

        var value = start;
        while (value <= end)
        {
            yield return value;
            switch (type)
            {
                case DateOnlyType.Year: value = value.AddYears(1); break;
                case DateOnlyType.Month: value = value.AddMonths(1); break;
                case DateOnlyType.Day: value = value.AddDays(1); break;
            }
        }
    }
#endif

    public static IEnumerable<DateTime> Create(DateTime start, int length, DateTimeType type)
    {
        if (type is DateTimeType.Unspecified) throw new ArgumentException($"Please specify the {nameof(DateTimeType)}.", nameof(type));

        var value = start;
        for (int i = 0; i < length; i++)
        {
            yield return value;
            switch (type)
            {
                case DateTimeType.Year: value = value.AddYears(1); break;
                case DateTimeType.Month: value = value.AddMonths(1); break;
                case DateTimeType.Day: value = value.AddDays(1); break;
                case DateTimeType.Hour: value = value.AddHours(1); break;
                case DateTimeType.Minute: value = value.AddMinutes(1); break;
                case DateTimeType.Second: value = value.AddSeconds(1); break;
            }
        }
    }

    public static IEnumerable<DateTime> CreateRange(DateTime start, DateTime end, DateTimeType type)
    {
        if (type is DateTimeType.Unspecified) throw new ArgumentException($"Please specify the {nameof(DateTimeType)}.", nameof(type));

        var value = start;
        while (value <= end)
        {
            yield return value;
            switch (type)
            {
                case DateTimeType.Year: value = value.AddYears(1); break;
                case DateTimeType.Month: value = value.AddMonths(1); break;
                case DateTimeType.Day: value = value.AddDays(1); break;
                case DateTimeType.Hour: value = value.AddHours(1); break;
                case DateTimeType.Minute: value = value.AddMinutes(1); break;
                case DateTimeType.Second: value = value.AddSeconds(1); break;
            }
        }
    }

    public static IEnumerable<int> Create(int start, int length)
    {
        for (int i = 0; i < length; i++) yield return start + i;
    }

    public static IEnumerable<int> Create(int start, int length, Func<int, int> iterator)
    {
        var value = start;
        for (int i = 0; i < length; i++)
        {
            yield return value;
            value = iterator(value);
        }
    }

    public static IEnumerable<int> CreateRange(int start, int end)
    {
        var value = start;
        while (value <= end)
        {
            yield return value;
            value++;
        }
    }

    public static IEnumerable<int> CreateRange(int start, int end, Func<int, int> iterator)
    {
        var value = start;
        while (value <= end)
        {
            yield return value;
            value = iterator(value);
        }
    }

}
