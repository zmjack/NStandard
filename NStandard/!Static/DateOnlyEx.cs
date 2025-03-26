#if NET6_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace NStandard.Static;

public static class DateOnlyEx
{
    /// <summary>
    /// Gets a System.DateOnly object that is set to the current date on this computer.
    /// </summary>
    /// <returns></returns>
    public static DateOnly Today => DateOnly.FromDateTime(DateTime.Now);

    /// <summary>
    /// The number of complete years in the period. [ Similar as DATEDIF(*, *, "Y") function in Excel. ]
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static int Years(DateOnly start, DateOnly end)
    {
        var offset = end.Year - start.Year;
        var target = DateOnlyExtensions.AddYears(start, offset);

        if (end >= start) return end >= target ? offset : offset - 1;
        else return end <= target ? offset : offset + 1;
    }

    /// <summary>
    /// The number of complete months in the period, similar as DATEDIF(*, *, "M") function in Excel, but more accurate.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static int Months(DateOnly start, DateOnly end)
    {
        var offset = (end.Year - start.Year) * 12 + end.Month - start.Month;
        var target = DateOnlyExtensions.AddMonths(start, offset);

        if (end >= start) return end >= target ? offset : offset - 1;
        else return end <= target ? offset : offset + 1;
    }

    /// <summary>
    /// The number of complete years in the period, expressed in whole and fractional year. [ Similar as DATEDIF(*, *, "Y") function in Excel. ]
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static double TotalYears(DateOnly start, DateOnly end)
    {
        var integer = Years(start, end);
        var targetStart = DateOnlyExtensions.AddYears(start, integer);

        if (end >= start)
        {
            var targetEnd = DateOnlyExtensions.AddYears(start, integer + 1);
            var fractional = (double)(end.DayNumber - targetStart.DayNumber) / (targetEnd.DayNumber - targetStart.DayNumber);
            return integer + fractional;
        }
        else
        {
            var targetEnd = DateOnlyExtensions.AddYears(start, integer - 1);
            var fractional = (double)(targetStart.DayNumber - end.DayNumber) / (targetStart.DayNumber - targetEnd.DayNumber);
            return integer - fractional;
        }
    }

    /// <summary>
    /// The number of complete months in the period, expressed in whole and fractional month. [ similar as DATEDIF(*, *, "M") function in Excel. ]
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static double TotalMonths(DateOnly start, DateOnly end)
    {
        var integer = Months(start, end);
        var targetStart = DateOnlyExtensions.AddMonths(start, integer);

        if (end >= start)
        {
            var targetEnd = DateOnlyExtensions.AddMonths(start, integer + 1);
            var fractional = (double)(end.DayNumber - targetStart.DayNumber) / (targetEnd.DayNumber - targetStart.DayNumber);
            return integer + fractional;
        }
        else
        {
            var targetEnd = DateOnlyExtensions.AddMonths(start, integer - 1);
            var fractional = (double)(targetStart.DayNumber - end.DayNumber) / (targetStart.DayNumber - targetEnd.DayNumber);
            return integer - fractional;
        }
    }

    /// <summary>
    /// Gets a DateOnly for the specified week of year.
    /// <para>[BUG] If <paramref name="weekStart"/> is not <see cref="DayOfWeek.Sunday"/>, the return value may be wrong.</para>
    /// <para>Please use <see cref="FromWeekOfYear(int, int, CalendarWeekRule, DayOfWeek)"/> instead.</para>
    /// </summary>
    /// <param name="year"></param>
    /// <param name="week"></param>
    /// <param name="weekStart"></param>
    /// <returns></returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use FromWeek(year, week, CalendarWeekRule.FirstFullWeek, firstDayOfWeek) instead.", true)]
    public static DateOnly ParseFromWeek(int year, int week, DayOfWeek weekStart = DayOfWeek.Sunday)
    {
        var day1 = new DateOnly(year, 1, 1);
        var week0 = DateOnlyExtensions.PastDay(day1, weekStart, true);
        if (week0.Year == year) week0 = week0.AddDays(-7);
        return week0.AddDays(week * 7);
    }

    /// <summary>
    /// Gets a DateOnly for the specified week of year.
    /// <para>[BUG] If <paramref name="weekStart"/> is not <see cref="DayOfWeek.Sunday"/>, the return value may be wrong.</para>
    /// <para>Please use <see cref="FromWeekOfYear(int, int, CalendarWeekRule, DayOfWeek)"/> instead.</para>
    /// </summary>
    /// <param name="year"></param>
    /// <param name="week"></param>
    /// <param name="weekStart"></param>
    /// <returns></returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use FromWeek(year, week, CalendarWeekRule.FirstFullWeek, firstDayOfWeek) instead.", true)]
    public static DateOnly FromWeek(int year, int week, DayOfWeek weekStart = DayOfWeek.Sunday)
    {
        var day1 = new DateOnly(year, 1, 1);
        var week0 = DateOnlyExtensions.PastDay(day1, weekStart, true);
        if (week0.Year == year) week0 = week0.AddDays(-7);
        return week0.AddDays(week * 7);
    }

    /// <summary>
    /// Gets a DateOnly for the specified iso-week of year.
    /// </summary>
    /// <param name="pair"></param>
    /// <returns></returns>
    public static DateOnly FromWeekOfYear(YearWeekPair pair)
    {
        return FromWeekOfYear(pair.Year, pair.Week, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
    }
    /// <summary>
    /// Gets a DateOnly for the specified week of year.
    /// </summary>
    /// <param name="pair"></param>
    /// <param name="rule"></param>
    /// <param name="firstDayOfWeek"></param>
    /// <returns></returns>
    public static DateOnly FromWeekOfYear(YearWeekPair pair, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
    {
        return FromWeekOfYear(pair.Year, pair.Week, rule, firstDayOfWeek);
    }

    /// <summary>
    /// Gets a DateOnly for the specified iso-week of year.
    /// </summary>
    /// <param name="year"></param>
    /// <param name="week"></param>
    /// <returns></returns>
    public static DateOnly FromWeekOfYear(int year, int week)
    {
        return FromWeekOfYear(year, week, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
    }
    /// <summary>
    /// Gets a DateOnly for the specified week of year.
    /// </summary>
    /// <param name="year"></param>
    /// <param name="week"></param>
    /// <param name="rule"></param>
    /// <param name="firstDayOfWeek"></param>
    /// <returns></returns>
    public static DateOnly FromWeekOfYear(int year, int week, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
    {
        var firstDayOfYear = new DateOnly(year, 1, 1);
        var pair = firstDayOfYear.WeekOfYear(rule, firstDayOfWeek);
        var offsetWeek = pair.Year == year ? week - 1 : week;
        var target = firstDayOfYear.AddDays(7 * offsetWeek);
        return target.StartOfWeek(firstDayOfWeek);
    }
}
#endif
