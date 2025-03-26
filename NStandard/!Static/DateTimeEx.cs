using System.ComponentModel;
using System.Globalization;

namespace NStandard.Static;

public static class DateTimeEx
{
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
    /// The number of complete years in the period. [ Similar as DATEDIF(*, *, "Y") function in Excel. ]
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static int Years(DateTime start, DateTime end)
    {
        if (start.Kind != DateTimeKind.Unspecified && end.Kind != DateTimeKind.Unspecified && start.Kind != end.Kind)
            throw new ArgumentException($"The kind of {nameof(start)} and {nameof(end)} must be the same.");

        var offset = end.Year - start.Year;
        var target = DateTimeExtensions.AddYears(start, offset);

        if (end >= start) return end >= target ? offset : offset - 1;
        else return end <= target ? offset : offset + 1;
    }

    /// <summary>
    /// The number of complete months in the period, similar as DATEDIF(*, *, "M") function in Excel, but more accurate.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static int Months(DateTime start, DateTime end)
    {
        if (start.Kind != DateTimeKind.Unspecified && end.Kind != DateTimeKind.Unspecified && start.Kind != end.Kind)
            throw new ArgumentException($"The kind of {nameof(start)} and {nameof(end)} must be the same.");

        var offset = (end.Year - start.Year) * 12 + end.Month - start.Month;
        var target = DateTimeExtensions.AddMonths(start, offset);

        if (end >= start) return end >= target ? offset : offset - 1;
        else return end <= target ? offset : offset + 1;
    }

    /// <summary>
    /// The number of complete years in the period, expressed in whole and fractional year. [ Similar as DATEDIF(*, *, "Y") function in Excel. ]
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static double TotalYears(DateTime start, DateTime end)
    {
        var integer = Years(start, end);
        var targetStart = DateTimeExtensions.AddYears(start, integer);

        if (end >= start)
        {
            var targetEnd = DateTimeExtensions.AddYears(start, integer + 1);
            var fractional = (end - targetStart).TotalDays / (targetEnd - targetStart).TotalDays;
            return integer + fractional;
        }
        else
        {
            var targetEnd = DateTimeExtensions.AddYears(start, integer - 1);
            var fractional = (targetStart - end).TotalDays / (targetStart - targetEnd).TotalDays;
            return integer - fractional;
        }
    }

    /// <summary>
    /// The number of complete months in the period, expressed in whole and fractional month. [ similar as DATEDIF(*, *, "M") function in Excel. ]
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static double TotalMonths(DateTime start, DateTime end)
    {
        var integer = Months(start, end);
        var targetStart = DateTimeExtensions.AddMonths(start, integer);

        if (end >= start)
        {
            var targetEnd = DateTimeExtensions.AddMonths(start, integer + 1);
            var fractional = (end - targetStart).TotalDays / (targetEnd - targetStart).TotalDays;
            return integer + fractional;
        }
        else
        {
            var targetEnd = DateTimeExtensions.AddMonths(start, integer - 1);
            var fractional = (targetStart - end).TotalDays / (targetStart - targetEnd).TotalDays;
            return integer - fractional;
        }
    }

    /// <summary>
    /// Gets a DateTime for the specified week of year.
    /// <para>[BUG] If <paramref name="weekStart"/> is not <see cref="DayOfWeek.Sunday"/>, the return value may be wrong.</para>
    /// <para>Please use <see cref="FromWeekOfYear(int, int, DateTimeKind, CalendarWeekRule, DayOfWeek)"/> instead.</para>
    /// </summary>
    /// <param name="year"></param>
    /// <param name="week"></param>
    /// <param name="kind"></param>
    /// <param name="weekStart"></param>
    /// <returns></returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use FromWeek(year, week, kind, CalendarWeekRule.FirstFullWeek, firstDayOfWeek) instead.", true)]
    public static DateTime ParseFromWeek(int year, int week, DateTimeKind kind, DayOfWeek weekStart = DayOfWeek.Sunday)
    {
        var day1 = new DateTime(year, 1, 1, 0, 0, 0, kind);
        var week0 = DateTimeExtensions.PastDay(day1, weekStart, true);
        if (week0.Year == year) week0 = week0.AddDays(-7);
        return week0.AddDays(week * 7);
    }

    /// <summary>
    /// Gets a DateTime for the specified week of year.
    /// <para>[BUG] If <paramref name="weekStart"/> is not <see cref="DayOfWeek.Sunday"/>, the return value may be wrong.</para>
    /// <para>Please use <see cref="FromWeekOfYear(int, int, DateTimeKind, CalendarWeekRule, DayOfWeek)"/> instead.</para>
    /// </summary>
    /// <param name="year"></param>
    /// <param name="week"></param>
    /// <param name="kind"></param>
    /// <param name="weekStart"></param>
    /// <returns></returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use FromWeek(year, week, kind, CalendarWeekRule.FirstFullWeek, firstDayOfWeek) instead.", true)]
    public static DateTime FromWeek(int year, int week, DateTimeKind kind, DayOfWeek weekStart = DayOfWeek.Sunday)
    {
        var day1 = new DateTime(year, 1, 1, 0, 0, 0, kind);
        var week0 = DateTimeExtensions.PastDay(day1, weekStart, true);
        if (week0.Year == year) week0 = week0.AddDays(-7);
        return week0.AddDays(week * 7);
    }

    /// <summary>
    /// Gets a DateTime for the specified iso-week of year.
    /// </summary>
    /// <param name="pair"></param>
    /// <param name="kind"></param>
    /// <returns></returns>
    public static DateTime FromWeekOfYear(YearWeekPair pair, DateTimeKind kind)
    {
        return FromWeekOfYear(pair.Year, pair.Week, kind, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
    }
    /// <summary>
    /// Gets a DateTime for the specified week of year.
    /// </summary>
    /// <param name="pair"></param>
    /// <param name="kind"></param>
    /// <param name="rule"></param>
    /// <param name="firstDayOfWeek"></param>
    /// <returns></returns>
    public static DateTime FromWeekOfYear(YearWeekPair pair, DateTimeKind kind, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
    {
        return FromWeekOfYear(pair.Year, pair.Week, kind, rule, firstDayOfWeek);
    }

    /// <summary>
    /// Gets a DateTime for the specified iso-week of year.
    /// </summary>
    /// <param name="year"></param>
    /// <param name="week"></param>
    /// <param name="kind"></param>
    /// <returns></returns>
    public static DateTime FromWeekOfYear(int year, int week, DateTimeKind kind)
    {
        return FromWeekOfYear(year, week, kind, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
    }
    /// <summary>
    /// Gets a DateTime for the specified week of year.
    /// </summary>
    /// <param name="year"></param>
    /// <param name="week"></param>
    /// <param name="kind"></param>
    /// <param name="rule"></param>
    /// <param name="firstDayOfWeek"></param>
    /// <returns></returns>
    public static DateTime FromWeekOfYear(int year, int week, DateTimeKind kind, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
    {
        var firstDayOfYear = new DateTime(year, 1, 1, 0, 0, 0, kind);
        var pair = firstDayOfYear.WeekOfYear(rule, firstDayOfWeek);
        var offsetWeek = pair.Year == year ? week - 1 : week;
        var target = firstDayOfYear.AddDays(7 * offsetWeek);
        return target.StartOfWeek(firstDayOfWeek);
    }

    /// <summary>
    /// Converts the specified string representation of a date and time to its System.DateTime
    ///     equivalent using the specified format and culture-specific format information.
    ///     The format of the string representation must match the specified format exactly.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    public static DateTime ParseExact(string s, string format)
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
    public static bool TryParseExact(string s, string format, out DateTime result)
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
