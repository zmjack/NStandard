using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NStandard
{
    public static class Range
    {
        public static IEnumerable<DateTime> Create(DateTime start, int length, DateRangeType type)
        {
            if (type is DateRangeType.Unset) throw new ArgumentException("Please specify the DateRangeType.", nameof(type));

            var value = start;
            for (int i = 0; i < length; i++)
            {
                yield return value;

                switch (type)
                {
                    case DateRangeType.Year: value = value.AddYears(1); break;
                    case DateRangeType.Month: value = value.AddMonths(1); break;
                    case DateRangeType.Day: value = value.AddDays(1); break;
                    case DateRangeType.Hour: value = value.AddHours(1); break;
                    case DateRangeType.Minute: value = value.AddMinutes(1); break;
                    case DateRangeType.Second: value = value.AddSeconds(1); break;
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

    }
}
