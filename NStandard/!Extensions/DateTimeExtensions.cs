using NStandard.Static;
using System.ComponentModel;
using System.Globalization;

namespace NStandard;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class DateTimeExtensions
{
    /// <summary>
    /// Gets a compatible instance in microseconds.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static DateTime ToFixed(this DateTime @this)
    {
        var n100 = @this.Ticks % 10;
        if (n100 < 5) return @this.AddTicks(-n100);
        else if (n100 > 5) return @this.AddTicks(10 - n100);
        else
        {
            var us = @this.Ticks / 10 % 10;
            if ((us & 1) == 0)
                return @this.AddTicks(-n100);
            else return @this.AddTicks(10 - n100);
        }
    }

    /// <summary>
    /// Gets a past day for the specified day of week.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="dayOfWeek"></param>
    /// <param name="includeCurrentDay"></param>
    /// <returns></returns>
    public static DateTime PastDay(this DateTime @this, DayOfWeek dayOfWeek, bool includeCurrentDay = false)
    {
        var days = dayOfWeek - @this.DayOfWeek;
        if (!includeCurrentDay && days == 0) return @this.AddDays(-7);
        else return @this.AddDays(CastCycleDays(days, true));
    }

    /// <summary>
    /// Gets a future day for the specified day of week.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="dayOfWeek"></param>
    /// <param name="includeCurrentDay"></param>
    /// <returns></returns>
    public static DateTime FutureDay(this DateTime @this, DayOfWeek dayOfWeek, bool includeCurrentDay = false)
    {
        var days = dayOfWeek - @this.DayOfWeek;

        if (!includeCurrentDay && days == 0) return @this.AddDays(7);
        else return @this.AddDays(CastCycleDays(days, false));
    }

    /// <summary>
    /// Gets the number of weeks in a month for the specified date.
    /// (eg. If define Sunday as the first day of the week, its first appearance means week 1, before is week 0.)
    /// </summary>
    /// <param name="this"></param>
    /// <param name="weekStart"></param>
    /// <returns></returns>
    public static int WeekInMonth(this DateTime @this, DayOfWeek weekStart = DayOfWeek.Sunday)
    {
        var day1 = new DateTime(@this.Year, @this.Month, 1, 0, 0, 0, @this.Kind);
        var week0 = PastDay(day1, weekStart, true);

        if (week0.Month == @this.Month) week0 = week0.AddDays(-7);
        return (PastDay(@this, weekStart, true) - week0).Days / 7;
    }

    /// <summary>
    /// Gets the number of weeks in a year for the specified date.
    /// <para>[BUG] If <paramref name="weekStart"/> is not <see cref="DayOfWeek.Sunday"/>, the return value may be wrong.</para>
    /// <para>Please use <see cref="WeekOfYear(DateTime, CalendarWeekRule, DayOfWeek)"/> instead.</para>
    /// </summary>
    /// <param name="this"></param>
    /// <param name="weekStart"></param>
    /// <returns></returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use WeekOfYear(@this, CalendarWeekRule.FirstFullWeek, DayOfWeek.Sunday) instead.", true)]
    public static int Week(this DateTime @this, DayOfWeek weekStart = DayOfWeek.Sunday)
    {
        var day1 = new DateTime(@this.Year, 1, 1, 0, 0, 0, @this.Kind);
        var week0 = PastDay(day1, weekStart, true);

        if (week0.Year == @this.Year) week0 = week0.AddDays(-7);
        return (PastDay(@this, weekStart, true) - week0).Days / 7;
    }

    /// <summary>
    /// Gets the iso-week number of the year for the specified date.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static YearWeekPair WeekOfYear(this DateTime @this)
    {
        return WeekOfYear(@this, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
    }
    /// <summary>
    /// Gets the week number of the year for the specified date.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="rule"></param>
    /// <param name="firstDayOfWeek"></param>
    /// <returns></returns>
    public static YearWeekPair WeekOfYear(this DateTime @this, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
    {
        //TODO: optimize
        var week = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(@this, rule, firstDayOfWeek);
        if (rule == CalendarWeekRule.FirstDay)
        {
            return new(@this.Year, week);
        }
        else
        {
            if (@this.Month == 1 && week > 6)
            {
                return new(@this.Year - 1, week);
            }
            else return new(@this.Year, week);
        }
    }

    public static DateTime StartOfWeek(this DateTime @this)
    {
        return StartOfWeek(@this, DayOfWeek.Monday);
    }
    public static DateTime EndOfWeek(this DateTime @this)
    {
        return EndOfWeek(@this, DayOfWeek.Monday);
    }
    public static DateTime StartOfWeek(this DateTime @this, DayOfWeek firstDayOfWeek)
    {
        var offset = @this.DayOfWeek - firstDayOfWeek;
        if (offset < 0) offset += 7;
        return @this.AddDays(-offset);
    }
    public static DateTime EndOfWeek(this DateTime @this, DayOfWeek firstDayOfWeek)
    {
        var offset = @this.DayOfWeek - firstDayOfWeek;
        if (offset < 0) offset += 7;
        return @this.AddDays(6 - offset);
    }

    /// <summary>
    /// Gets the quarter of the specified date.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use Quarter(@this) instead.")]
    public static int Season(this DateTime @this)
    {
        return Quarter(@this);
    }
    /// <summary>
    /// Gets the quarter of the specified date.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static int Quarter(this DateTime @this)
    {
        return @this.Month switch
        {
            >= 1 and <= 3 => 1,
            >= 4 and <= 6 => 2,
            >= 7 and <= 9 => 3,
            >= 10 and <= 12 => 4,
            _ => throw new NotImplementedException(),
        };
    }

    /// <summary>
    /// Gets the first day of the quarter for the specified date.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use StartOfQuarter(@this) instead.")]
    public static DateTime StartOfSeason(this DateTime @this)
    {
        return StartOfQuarter(@this);
    }
    /// <summary>
    /// Gets the first day of the quarter for the specified date.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static DateTime StartOfQuarter(this DateTime @this)
    {
        return @this.Month switch
        {
            >= 1 and <= 3 => new(@this.Year, 1, 1, 0, 0, 0, 0, @this.Kind),
            >= 4 and <= 6 => new(@this.Year, 4, 1, 0, 0, 0, 0, @this.Kind),
            >= 7 and <= 9 => new(@this.Year, 7, 1, 0, 0, 0, 0, @this.Kind),
            >= 10 and <= 12 => new(@this.Year, 10, 1, 0, 0, 0, 0, @this.Kind),
            _ => throw new NotImplementedException(),
        };
    }

    /// <summary>
    /// Gets the last day of the quarter for the specified date.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use EndOfQuarter(@this) instead.")]
    public static DateTime EndOfSeason(this DateTime @this)
    {
        return EndOfQuarter(@this);
    }
    /// <summary>
    /// Gets the last day of the quarter for the specified date.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static DateTime EndOfQuarter(this DateTime @this)
    {
        return @this.Month switch
        {
            >= 1 and <= 3 => new(@this.Year, 3, 31, 59, 59, 59, 59, @this.Kind),
            >= 4 and <= 6 => new(@this.Year, 6, 30, 59, 59, 59, 59, @this.Kind),
            >= 7 and <= 9 => new(@this.Year, 9, 30, 59, 59, 59, 59, @this.Kind),
            >= 10 and <= 12 => new(@this.Year, 12, 31, 59, 59, 59, 59, @this.Kind),
            _ => throw new NotImplementedException(),
        };
    }

    /// <summary>
    /// Gets the start point of the specified year.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static DateTime StartOfYear(this DateTime @this) => new(@this.Year, 1, 1, 0, 0, 0, 0, @this.Kind);

    /// <summary>
    /// Gets the start point of the specified month.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static DateTime StartOfMonth(this DateTime @this) => new(@this.Year, @this.Month, 1, 0, 0, 0, 0, @this.Kind);

    /// <summary>
    /// Gets the start point of the specified day.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static DateTime StartOfDay(this DateTime @this) => @this.Date;

    /// <summary>
    /// Gets the start point of the specified hour.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static DateTime StartOfHour(this DateTime @this) => new(@this.Year, @this.Month, @this.Day, @this.Hour, 0, 0, 0, @this.Kind);

    /// <summary>
    /// Gets the start point of the specified minute.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static DateTime StartOfMinute(this DateTime @this) => new(@this.Year, @this.Month, @this.Day, @this.Hour, @this.Minute, 0, 0, @this.Kind);

    /// <summary>
    /// Gets the start point of the specified second.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static DateTime StartOfSecond(this DateTime @this) => new(@this.Year, @this.Month, @this.Day, @this.Hour, @this.Minute, @this.Second, 0, @this.Kind);

    /// <summary>
    /// Gets the end point of the specified year.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static DateTime EndOfYear(this DateTime @this) => new(@this.Year, 12, 31, 23, 59, 59, 999, @this.Kind);

    /// <summary>
    /// Gets the end point of the specified month.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static DateTime EndOfMonth(this DateTime @this)
    {
        if (new[] { 1, 3, 5, 7, 8, 10, 12 }.Contains(@this.Month))
            return new DateTime(@this.Year, @this.Month, 31, 23, 59, 59, 999, @this.Kind);
        else if (new[] { 4, 6, 9, 11 }.Contains(@this.Month))
            return new DateTime(@this.Year, @this.Month, 30, 23, 59, 59, 999, @this.Kind);
        else
        {
            if (DateTime.IsLeapYear(@this.Year))
                return new DateTime(@this.Year, @this.Month, 29, 23, 59, 59, 999, @this.Kind);
            else return new DateTime(@this.Year, @this.Month, 28, 23, 59, 59, 999, @this.Kind);
        }
    }

    /// <summary>
    /// Gets the end point of the specified day.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static DateTime EndOfDay(this DateTime @this) => new(@this.Year, @this.Month, @this.Day, 23, 59, 59, 999, @this.Kind);

    /// <summary>
    /// Gets the end point of the specified hour.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static DateTime EndOfHour(this DateTime @this) => new(@this.Year, @this.Month, @this.Day, @this.Hour, 59, 59, 999, @this.Kind);

    /// <summary>
    /// Gets the end point of the specified minute.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static DateTime EndOfMinute(this DateTime @this) => new(@this.Year, @this.Month, @this.Day, @this.Hour, @this.Minute, 59, 999, @this.Kind);

    /// <summary>
    /// Gets the end point of the specified second.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static DateTime EndOfSecond(this DateTime @this) => new(@this.Year, @this.Month, @this.Day, @this.Hour, @this.Minute, @this.Second, 999, @this.Kind);

    /// <summary>
    /// Returns a new <see cref="DateTime"/> that adds the specified number of complete years to the value of this instance.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="value"></param>
    /// <param name="mode"></param>
    /// <returns></returns>
    public static DateTime AddDays(this DateTime @this, int value, DayMode mode)
    {
        if (value == 0) return @this;

        int days;
        int week;
        int mod;

        switch (mode)
        {
            default:
            case DayMode.Undefined: return @this.AddDays(value);

            case DayMode.Weekday:
                if (value > 0)
                {
                    // Set to Monday
                    if (@this.DayOfWeek == DayOfWeek.Saturday)
                    {
                        @this = @this.AddDays(2);
                        value--;
                    }
                    else if (@this.DayOfWeek == DayOfWeek.Sunday)
                    {
                        @this = @this.AddDays(1);
                        value--;
                    }
                }
                else
                {
                    // Set to Friday
                    if (@this.DayOfWeek == DayOfWeek.Saturday)
                    {
                        @this = @this.AddDays(-1);
                        value++;
                    }
                    else if (@this.DayOfWeek == DayOfWeek.Sunday)
                    {
                        @this = @this.AddDays(-2);
                        value++;
                    }
                }

                if (value == 0) return @this;

                days = 5;
                week = value / days;
                mod = value % days;

                if (value > 0)
                {
                    if (mod >= DayOfWeek.Saturday - @this.DayOfWeek) return @this.AddDays(week * 7 + mod + 2);
                    else return @this.AddDays(week * 7 + mod);
                }
                else
                {
                    if (mod <= DayOfWeek.Sunday - @this.DayOfWeek) return @this.AddDays(week * 7 + mod - 2);
                    else return @this.AddDays(week * 7 + mod);
                }

            case DayMode.Weekend:
                if (value > 0)
                {
                    // Set to Saturday
                    if (@this.DayOfWeek is >= DayOfWeek.Monday and <= DayOfWeek.Friday)
                    {
                        @this = @this.AddDays(DayOfWeek.Saturday - @this.DayOfWeek);
                        value--;
                    }
                }
                else
                {
                    // Set to Sunday
                    if (@this.DayOfWeek is >= DayOfWeek.Monday and <= DayOfWeek.Friday)
                    {
                        @this = @this.AddDays(DayOfWeek.Sunday - @this.DayOfWeek);
                        value++;
                    }
                }

                if (value == 0) return @this;

                days = 2;
                week = value / days;
                mod = value % days;

                if (value > 0)
                {
                    if (@this.DayOfWeek == DayOfWeek.Sunday && mod == 1) return @this.AddDays(week * 7 + mod + 5);
                    else return @this.AddDays(week * 7 + mod);
                }
                else
                {
                    if (@this.DayOfWeek == DayOfWeek.Saturday && mod == -1) return @this.AddDays(week * 7 + mod - 5);
                    else return @this.AddDays(week * 7 + mod);
                }
        }
    }

    /// <summary>
    /// Returns a new <see cref="DateTime"/> that adds the specified number of complete years to the value of this instance.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    internal static DateTime AddYears(DateTime @this, int value)
    {
        var target = @this.AddYears(value);
        if (value > 0 && target.Day < @this.Day) return target.AddDays(1);
        else return target;
    }

    /// <summary>
    /// Returns a new <see cref="DateTime"/> that adds the specified number of complete months to the value of this instance.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    internal static DateTime AddMonths(this DateTime @this, int value)
    {
        var target = @this.AddMonths(value);
        if (value > 0 && target.Day < @this.Day) return target.AddDays(1);
        else return target;
    }

    /// <summary>
    /// Returns a new <see cref="DateTime"/> that adds the specified diff-number of years to the value of this instance.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static DateTime AddTotalYears(this DateTime @this, double value)
    {
        var integer = (int)value;
        var fractional = value - integer;
        var start = AddYears(@this, integer);

        if (fractional == 0) return start;

        double offsetDays;
        if (value >= 0)
        {
            var end = AddYears(@this, integer + 1);
            offsetDays = (end - start).TotalDays * fractional;
        }
        else
        {
            var end = AddYears(@this, integer - 1);
            offsetDays = (start - end).TotalDays * fractional;
        }

        return start.AddDays(offsetDays);
    }

    /// <summary>
    /// Returns a new <see cref="DateTime"/> that adds the specified diff-number of months to the value of this instance.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static DateTime AddTotalMonths(this DateTime @this, double value)
    {
        var integer = (int)value;
        var fractional = value - integer;
        var start = AddMonths(@this, integer);

        if (fractional == 0) return start;

        double offsetDays;
        if (value >= 0)
        {
            var end = AddMonths(@this, integer + 1);
            offsetDays = (end - start).TotalDays * fractional;
        }
        else
        {
            var end = AddMonths(@this, integer - 1);
            offsetDays = (start - end).TotalDays * fractional;
        }

        return start.AddDays(offsetDays);
    }

    /// <summary>
    /// Gets the number of milliseconds elapsed from <see cref="DateTime.MinValue"/>.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static double ElapsedMilliseconds(this DateTime @this) => (@this - DateTime.MinValue).TotalMilliseconds;

    /// <summary>
    /// Gets the number of seconds elapsed from <see cref="DateTime.MinValue"/>.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static double ElapsedSeconds(this DateTime @this) => (@this - DateTime.MinValue).TotalSeconds;

    /// <summary>
    /// Gets the number of minutes elapsed from <see cref="DateTime.MinValue"/>.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static double ElapsedMinutes(this DateTime @this) => (@this - DateTime.MinValue).TotalMinutes;

    /// <summary>
    /// Gets the number of hours elapsed from <see cref="DateTime.MinValue"/>.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static double ElapsedHours(this DateTime @this) => (@this - DateTime.MinValue).TotalHours;

    /// <summary>
    /// Gets the number of days elapsed from <see cref="DateTime.MinValue"/>.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static double ElapsedDays(this DateTime @this) => (@this - DateTime.MinValue).TotalDays;

    /// <summary>
    /// Gets the number of months elapsed from <see cref="DateTime.MinValue"/>.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static double ElapsedMonths(this DateTime @this) => DateTimeEx.TotalMonths(DateTime.MinValue, @this);

    /// <summary>
    /// Gets the number of years elapsed from <see cref="DateTime.MinValue"/>.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static double ElapsedYears(this DateTime @this) => DateTimeEx.TotalYears(DateTime.MinValue, @this);

    private static int CastCycleDays(int days, bool isBackward)
    {
        days %= 7;
        if (isBackward) return days > 0 ? days - 7 : days;
        else return days < 0 ? days + 7 : days;
    }

    /// <summary>
    /// Returns the number of milliseconds that have elapsed since 1970-01-01T00:00:00.000Z.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static long ToUnixTimeMilliseconds(this DateTime @this)
    {
        long num = @this.ToUniversalTime().Ticks / 10000;
        return num - 62135596800000L;
    }

    /// <summary>
    /// Returns the number of seconds that have elapsed since 1970-01-01T00:00:00Z.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static long ToUnixTimeSeconds(this DateTime @this)
    {
        long num = @this.ToUniversalTime().Ticks / 10000000;
        return num - 62135596800L;
    }

}
