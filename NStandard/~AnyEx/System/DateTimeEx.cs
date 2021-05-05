using System;
using System.Collections.Generic;
using System.Globalization;

namespace NStandard
{
    public static class DateTimeEx
    {
        public static NowScope BeginNowScope() => new();
        public static NowScope BeginNowScope(Func<DateTime, DateTime> store) => new(store);

        public static NowScopeAccessor NowScopes => NowScopeAccessor.Instance;

        /// <summary>
        /// Gets the DateTime(UTC) of UnixMinValue.
        /// </summary>
        /// <returns></returns>
        public static readonly DateTime UnixMinValue = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Converts a Unix time expressed as the number of milliseconds that have elapsed
        ///     since 1970-01-01T00:00:00Z to a <see cref="DateTime"/> value.
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static DateTime FromUnixTimeMilliseconds(long milliseconds) => new(DateTimeOffsetEx.FromUnixTimeMilliseconds(milliseconds).Ticks, DateTimeKind.Utc);

        /// <summary>
        /// Converts a Unix time expressed as the number of seconds that have elapsed since
        ///     1970-01-01T00:00:00Z to a <see cref="DateTime"/> value.
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static DateTime FromUnixTimeSeconds(long seconds) => new(DateTimeOffsetEx.FromUnixTimeSeconds(seconds).Ticks, DateTimeKind.Utc);

        /// <summary>
        /// Gets the range of months.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> GetMonths(DateTime start, DateTime end)
        {
            if (start.Kind != DateTimeKind.Unspecified && end.Kind != DateTimeKind.Unspecified && start.Kind != end.Kind)
                throw new ArgumentException($"The kind of {nameof(start)} and {nameof(end)} must be the same.");

            start = new DateTime(start.Year, start.Month, 1, 0, 0, 0, start.Kind);
            end = new DateTime(end.Year, end.Month, 1);
            for (var dt = start; dt <= end; dt = dt.AddMonths(1)) yield return dt;
        }

        /// <summary>
        /// Gets the range of days.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> GetDays(DateTime start, DateTime end)
        {
            if (start.Kind != DateTimeKind.Unspecified && end.Kind != DateTimeKind.Unspecified && start.Kind != end.Kind)
                throw new ArgumentException($"The kind of {nameof(start)} and {nameof(end)} must be the same.");

            start = new DateTime(start.Year, start.Month, start.Day, 0, 0, 0, start.Kind);
            end = new DateTime(end.Year, end.Month, end.Day);
            for (var dt = start; dt <= end; dt = dt.AddDays(1)) yield return dt;
        }

        private static int PrivateYearDiff(DateTime start, DateTime end)
        {
            var factor = start > end ? -1 : 1;
            if (factor == -1) Any.Swap(ref start, ref end);

            var passedYears = end.Year - start.Year;
            var target = start.AddCompleteYears(passedYears);
            return factor * (target > end ? passedYears - 1 : passedYears);
        }

        private static int PrivateMonthDiff(DateTime start, DateTime end)
        {
            var factor = start > end ? -1 : 1;
            if (factor == -1) Any.Swap(ref start, ref end);

            var passedYears = end.Year - start.Year;
            var passedMonths = end.Month - start.Month;
            var target = start.AddCompleteMonths(passedYears * 12 + passedMonths);
            return factor * (target > end ? passedYears * 12 + passedMonths - 1 : passedYears * 12 + passedMonths);
        }

        /// <summary>
        /// The number of complete years in the period, similar as DATEDIF(*, *, "Y") function in Excel.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int YearDiff(DateTime start, DateTime end) => MonthDiff(start, end) / 12;

        /// <summary>
        /// The number of complete months in the period, similar as DATEDIF(*, *, "M") function in Excel.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int MonthDiff(DateTime start, DateTime end)
        {
            if (start.Kind != DateTimeKind.Unspecified && end.Kind != DateTimeKind.Unspecified && start.Kind != end.Kind)
                throw new ArgumentException($"The kind of {nameof(start)} and {nameof(end)} must be the same.");
            return PrivateMonthDiff(start, end);
        }

        /// <summary>
        /// The number of complete years in the period, return a double value.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static double ExactYearDiff(DateTime start, DateTime end)
        {
            if (start.Kind != DateTimeKind.Unspecified && end.Kind != DateTimeKind.Unspecified && start.Kind != end.Kind)
                throw new ArgumentException($"The kind of {nameof(start)} and {nameof(end)} must be the same.");

            var diff = PrivateYearDiff(start, end);
            var endStart = start.AddCompleteYears(diff);
            var endEnd = endStart.AddCompleteYears(1);
            return diff + (end - endStart).TotalDays / (endEnd - endStart).TotalDays;
        }

        /// <summary>
        /// The number of complete months in the period, return a double value.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static double ExactMonthDiff(DateTime start, DateTime end)
        {
            if (start.Kind != DateTimeKind.Unspecified && end.Kind != DateTimeKind.Unspecified && start.Kind != end.Kind)
                throw new ArgumentException($"The kind of {nameof(start)} and {nameof(end)} must be the same.");

            var diff = PrivateMonthDiff(start, end);
            var endStart = start.AddCompleteMonths(diff);
            var endEnd = endStart.AddCompleteMonths(1);
            return diff + (end - endStart).TotalDays / (endEnd - endStart).TotalDays;
        }

        /// <summary>
        /// Gets a DateTime for the specified week of year.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="week"></param>
        /// <param name="weekStart"></param>
        /// <returns></returns>
        [Obsolete("This method will be removed in the future. Please use ParseFromWeek(int year, int week, DateTimeKind kind, DayOfWeek weekStart = DayOfWeek.Sunday) instead.")]
        public static DateTime ParseFromWeek(int year, int week, DayOfWeek weekStart = DayOfWeek.Sunday) => ParseFromWeek(year, week, DateTimeKind.Unspecified, weekStart);

        /// <summary>
        /// Gets a DateTime for the specified week of year.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="week"></param>
        /// <param name="kind"></param>
        /// <param name="weekStart"></param>
        /// <returns></returns>
        public static DateTime ParseFromWeek(int year, int week, DateTimeKind kind, DayOfWeek weekStart = DayOfWeek.Sunday)
        {
            var day1 = new DateTime(year, 1, 1, 0, 0, 0, kind);
            var week0 = XDateTime.PastDay(day1, weekStart, true);
            if (week0.Year == year) week0 = week0.AddDays(-7);
            return week0.AddDays(week * 7);
        }

        /// <summary>
        /// Converts the specified string representation of a date and time to its System.DateTime
        ///     equivalent using the specified format and culture-specific format information.
        ///     The format of the string representation must match the specified format exactly.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime ParseExtract(string s, string format)
        {
            return DateTime.ParseExact(s, format, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts the specified string representation of a date and time to its System.DateTime
        ///     equivalent using the specified format, culture-specific format information, and
        ///     style. The format of the string representation must match the specified format
        ///     exactly. The method returns a value that indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="format"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryParseExtract(string s, string format, out DateTime result)
        {
            return DateTime.TryParseExact(s, format, CultureInfo.CurrentCulture, DateTimeStyles.None, out result);
        }

        /// <summary>
        /// Returns the number of days in the specified month and year.
        /// If the specified year is a leap year, return 366, else return 365.
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static int DaysInYear(int year) => DateTime.IsLeapYear(year) ? 366 : 365;

    }
}
