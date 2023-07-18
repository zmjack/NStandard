using System;
using System.Globalization;

namespace NStandard
{
    public static class DateTimeOffsetEx
    {
        /// <summary>
        /// Gets the DateTimeOffset(UTC) of UnixMinValue.
        /// </summary>
        /// <returns></returns>
        public static readonly DateTimeOffset UnixMinValue = new(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);

        /// <summary>
        /// Converts a Unix time expressed as the number of milliseconds that have elapsed
        ///     since 1970-01-01T00:00:00Z to a <see cref="DateTimeOffset"/> value.
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static DateTimeOffset FromUnixTimeMilliseconds(long milliseconds)
        {
#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_3_OR_GREATER || NET46_OR_GREATER
            return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds);
#else
            if (milliseconds < -62135596800000L || milliseconds > 253402300799999L)
            {
                throw new ArgumentOutOfRangeException("milliseconds", SR.Format(SR.ArgumentOutOfRange_Range, -62135596800000L, 253402300799999L));
            }
            long ticks = milliseconds * 10000 + 621355968000000000L;
            return new DateTimeOffset(ticks, TimeSpan.Zero);
#endif
        }

        /// <summary>
        /// Converts a Unix time expressed as the number of seconds that have elapsed since
        ///     1970-01-01T00:00:00Z to a <see cref="DateTimeOffset"/> value.
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static DateTimeOffset FromUnixTimeSeconds(long seconds)
        {
#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_3_OR_GREATER || NET46_OR_GREATER
            return DateTimeOffset.FromUnixTimeSeconds(seconds);
#else
            if (seconds < -62135596800L || seconds > 253402300799L)
            {
                throw new ArgumentOutOfRangeException("seconds", SR.Format(SR.ArgumentOutOfRange_Range, -62135596800L, 253402300799L));
            }
            long ticks = seconds * 10000000 + 621355968000000000L;
            return new DateTimeOffset(ticks, TimeSpan.Zero);
#endif
        }

        /// <summary>
        /// The number of complete years in the period. [ Similar as DATEDIF(*, *, "Y") function in Excel. ]
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int Years(DateTimeOffset start, DateTimeOffset end)
        {
            var offset = end.Year - start.Year;
            var target = DateTimeOffsetExtensions.AddYears(start, offset);

            if (end >= start) return end >= target ? offset : offset - 1;
            else return end <= target ? offset : offset + 1;
        }

        /// <summary>
        /// The number of complete months in the period, similar as DATEDIF(*, *, "M") function in Excel.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int Months(DateTimeOffset start, DateTimeOffset end)
        {
            var offset = (end.Year - start.Year) * 12 + end.Month - start.Month;
            var target = DateTimeOffsetExtensions.AddMonths(start, offset);

            if (end >= start) return end >= target ? offset : offset - 1;
            else return end <= target ? offset : offset + 1;
        }

        /// <summary>
        /// The number of complete years in the period, expressed in whole and fractional year. [ Similar as DATEDIF(*, *, "Y") function in Excel. ]
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static double TotalYears(DateTimeOffset start, DateTimeOffset end)
        {
            var integer = Years(start, end);
            var targetStart = DateTimeOffsetExtensions.AddYears(start, integer);

            if (end >= start)
            {
                var targetEnd = DateTimeOffsetExtensions.AddYears(start, integer + 1);
                var fractional = (end - targetStart).TotalDays / (targetEnd - targetStart).TotalDays;
                return integer + fractional;
            }
            else
            {
                var targetEnd = DateTimeOffsetExtensions.AddYears(start, integer - 1);
                var fractional = (targetStart - end).TotalDays / (targetStart - targetEnd).TotalDays;
                return integer - fractional;
            }
        }

        /// <summary>
        /// The number of complete months in the period, return a double value.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static double TotalMonths(DateTimeOffset start, DateTimeOffset end)
        {
            var integer = Months(start, end);
            var targetStart = DateTimeOffsetExtensions.AddMonths(start, integer);

            if (end >= start)
            {
                var targetEnd = DateTimeOffsetExtensions.AddMonths(start, integer + 1);
                var fractional = (end - targetStart).TotalDays / (targetEnd - targetStart).TotalDays;
                return integer + fractional;
            }
            else
            {
                var targetEnd = DateTimeOffsetExtensions.AddMonths(start, integer - 1);
                var fractional = (targetStart - end).TotalDays / (targetStart - targetEnd).TotalDays;
                return integer - fractional;
            }
        }

        /// <summary>
        /// Gets a DateTimeOffset for the specified week of year.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="week"></param>
        /// <param name="offset"></param>
        /// <param name="weekStart"></param>
        /// <returns></returns>
        public static DateTimeOffset ParseFromWeek(int year, int week, TimeSpan offset, DayOfWeek weekStart = DayOfWeek.Sunday)
        {
            var day1 = new DateTimeOffset(year, 1, 1, 0, 0, 0, offset);
            var week0 = DateTimeOffsetExtensions.PastDay(day1, weekStart, true);
            if (week0.Year == year) week0 = week0.AddDays(-7);
            return week0.AddDays(week * 7);
        }

        /// <summary>
        /// Converts the specified string representation of a date and time to its System.DateTimeOffset
        ///     equivalent using the specified format and culture-specific format information.
        ///     The format of the string representation must match the specified format exactly.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTimeOffset ParseExtract(string s, string format)
        {
            return DateTimeOffset.ParseExact(s, format, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts the specified string representation of a date and time to its System.DateTimeOffset
        ///     equivalent using the specified format, culture-specific format information, and
        ///     style. The format of the string representation must match the specified format
        ///     exactly. The method returns a value that indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="format"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryParseExtract(string s, string format, out DateTimeOffset result)
        {
            return DateTimeOffset.TryParseExact(s, format, CultureInfo.CurrentCulture, DateTimeStyles.None, out result);
        }

    }
}
