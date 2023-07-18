using System;
using System.Collections.Generic;

namespace NStandard
{
    public static class RangeEx
    {
#if NET6_0_OR_GREATER
        public static IEnumerable<DateOnly> Create(DateOnly start, int length, DateRangeType type)
        {
            if (type is DateRangeType.Unset) throw new ArgumentException($"Please specify the {nameof(DateRangeType)}.", nameof(type));

            var value = start;
            for (int i = 0; i < length; i++)
            {
                yield return value;
                switch (type)
                {
                    case DateRangeType.Year: value = value.AddYears(1); break;
                    case DateRangeType.Month: value = value.AddMonths(1); break;
                    case DateRangeType.Day: value = value.AddDays(1); break;
                }
            }
        }

        public static IEnumerable<DateOnly> CreateRange(DateOnly start, DateOnly end, DateRangeType type)
        {
            if (type is DateRangeType.Unset) throw new ArgumentException($"Please specify the {nameof(DateRangeType)}.", nameof(type));

            var value = start;
            while (value <= end)
            {
                yield return value;
                switch (type)
                {
                    case DateRangeType.Year: value = value.AddYears(1); break;
                    case DateRangeType.Month: value = value.AddMonths(1); break;
                    case DateRangeType.Day: value = value.AddDays(1); break;
                }
            }
        }
#endif

        public static IEnumerable<DateTime> Create(DateTime start, int length, DateTimeRangeType type)
        {
            if (type is DateTimeRangeType.Unset) throw new ArgumentException($"Please specify the {nameof(DateTimeRangeType)}.", nameof(type));

            var value = start;
            for (int i = 0; i < length; i++)
            {
                yield return value;
                switch (type)
                {
                    case DateTimeRangeType.Year: value = value.AddYears(1); break;
                    case DateTimeRangeType.Month: value = value.AddMonths(1); break;
                    case DateTimeRangeType.Day: value = value.AddDays(1); break;
                    case DateTimeRangeType.Hour: value = value.AddHours(1); break;
                    case DateTimeRangeType.Minute: value = value.AddMinutes(1); break;
                    case DateTimeRangeType.Second: value = value.AddSeconds(1); break;
                }
            }
        }

        public static IEnumerable<DateTime> CreateRange(DateTime start, DateTime end, DateTimeRangeType type)
        {
            if (type is DateTimeRangeType.Unset) throw new ArgumentException($"Please specify the {nameof(DateTimeRangeType)}.", nameof(type));

            var value = start;
            while (value <= end)
            {
                yield return value;
                switch (type)
                {
                    case DateTimeRangeType.Year: value = value.AddYears(1); break;
                    case DateTimeRangeType.Month: value = value.AddMonths(1); break;
                    case DateTimeRangeType.Day: value = value.AddDays(1); break;
                    case DateTimeRangeType.Hour: value = value.AddHours(1); break;
                    case DateTimeRangeType.Minute: value = value.AddMinutes(1); break;
                    case DateTimeRangeType.Second: value = value.AddSeconds(1); break;
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
}
