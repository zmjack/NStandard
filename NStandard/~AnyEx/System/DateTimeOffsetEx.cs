using System;
using System.Collections.Generic;
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
#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET46_OR_GREATER
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
#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET46_OR_GREATER
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
        /// Gets the range of months.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static IEnumerable<DateTimeOffset> GetMonths(DateTimeOffset start, DateTimeOffset end)
        {
            if (start.Offset != end.Offset) throw new ArgumentException($"The offset of {nameof(start)} and {nameof(end)} must be the same.");

            start = new DateTimeOffset(start.Year, start.Month, 1, 0, 0, 0, start.Offset);
            end = new DateTimeOffset(end.Year, end.Month, 1, 0, 0, 0, start.Offset);
            for (var dt = start; dt <= end; dt = dt.AddMonths(1)) yield return dt;
        }

        /// <summary>
        /// Gets the range of days.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static IEnumerable<DateTimeOffset> GetDays(DateTimeOffset start, DateTimeOffset end)
        {
            if (start.Offset != end.Offset) throw new ArgumentException($"The offset of {nameof(start)} and {nameof(end)} must be the same.");

            start = new DateTimeOffset(start.Year, start.Month, start.Day, 0, 0, 0, start.Offset);
            end = new DateTimeOffset(end.Year, end.Month, end.Day, 0, 0, 0, start.Offset);
            for (var dt = start; dt <= end; dt = dt.AddDays(1)) yield return dt;
        }

        private static int PrivateYearDiff(DateTimeOffset start, DateTimeOffset end)
        {
            var factor = start > end ? -1 : 1;
            if (factor == -1) Any.Swap(ref start, ref end);

            var passedYears = end.Year - start.Year;
            var target = start.AddYearDiff(passedYears);
            return factor * (target > end ? passedYears - 1 : passedYears);
        }

        private static int PrivateMonthDiff(DateTimeOffset start, DateTimeOffset end)
        {
            var factor = start > end ? -1 : 1;
            if (factor == -1) Any.Swap(ref start, ref end);

            var passedYears = end.Year - start.Year;
            var passedMonths = end.Month - start.Month;
            var target = start.AddMonthDiff(passedYears * 12 + passedMonths);
            return factor * (target > end ? passedYears * 12 + passedMonths - 1 : passedYears * 12 + passedMonths);
        }

        /// <summary>
        /// The number of complete years in the period, similar as DATEDIF(*, *, "Y") function in Excel.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int YearDiff(DateTimeOffset start, DateTimeOffset end) => MonthDiff(start, end) / 12;

        /// <summary>
        /// The number of complete months in the period, similar as DATEDIF(*, *, "M") function in Excel.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int MonthDiff(DateTimeOffset start, DateTimeOffset end)
        {
            if (start.Offset != end.Offset) throw new ArgumentException($"The offset of {nameof(start)} and {nameof(end)} must be the same.");
            return PrivateMonthDiff(start, end);
        }

        /// <summary>
        /// The number of complete years in the period, return a double value.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static double ExactYearDiff(DateTimeOffset start, DateTimeOffset end)
        {
            if (start.Offset != end.Offset) throw new ArgumentException($"The offset of {nameof(start)} and {nameof(end)} must be the same.");

            var diff = PrivateYearDiff(start, end);
            var endStart = start.AddYearDiff(diff);
            var endEnd = endStart.AddYearDiff(1);
            return diff + (end - endStart).TotalDays / (endEnd - endStart).TotalDays;
        }

        /// <summary>
        /// The number of complete months in the period, return a double value.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static double ExactMonthDiff(DateTimeOffset start, DateTimeOffset end)
        {
            if (start.Offset != end.Offset) throw new ArgumentException($"The offset of {nameof(start)} and {nameof(end)} must be the same.");

            var diff = PrivateMonthDiff(start, end);
            var endStart = start.AddMonthDiff(diff);
            var endEnd = endStart.AddMonthDiff(1);
            return diff + (end - endStart).TotalDays / (endEnd - endStart).TotalDays;
        }

        /// <summary>
        /// Gets a DateTimeOffset for the specified week of year.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="week"></param>
        /// <param name="weekStart"></param>
        /// <returns></returns>
        [Obsolete("This method will be removed in the future. Please use ParseFromWeek(int year, int week, TimeSpan offset, DayOfWeek weekStart = DayOfWeek.Sunday) instead.")]
        public static DateTimeOffset ParseFromWeek(int year, int week, DayOfWeek weekStart = DayOfWeek.Sunday) => ParseFromWeek(year, week, TimeSpan.Zero, weekStart);

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
            var week0 = XDateTimeOffset.PastDay(day1, weekStart, true);
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
