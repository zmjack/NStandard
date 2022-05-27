using System;
using System.ComponentModel;
using System.Linq;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XDateTimeOffset
    {
        /// <summary>
        /// Gets a past day for the specified day of week.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="dayOfWeek"></param>
        /// <param name="includeCurrentDay"></param>
        /// <returns></returns>
        public static DateTimeOffset PastDay(this DateTimeOffset @this, DayOfWeek dayOfWeek, bool includeCurrentDay = false)
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
        public static DateTimeOffset FutureDay(this DateTimeOffset @this, DayOfWeek dayOfWeek, bool includeCurrentDay = false)
        {
            var days = dayOfWeek - @this.DayOfWeek;

            if (!includeCurrentDay && days == 0) return @this.AddDays(7);
            else return @this.AddDays(CastCycleDays(days, false));
        }

        /// <summary>
        /// Gets the number of weeks in a month for the specified date.
        /// (eg. If define Sunday as the fisrt day of the week, its first appearance means week 1, before is week 0.)
        /// </summary>
        /// <param name="this"></param>
        /// <param name="weekStart"></param>
        /// <returns></returns>
        public static int WeekInMonth(this DateTimeOffset @this, DayOfWeek weekStart = DayOfWeek.Sunday)
        {
            var day1 = new DateTimeOffset(@this.Year, @this.Month, 1, 0, 0, 0, @this.Offset);
            var week0 = PastDay(day1, weekStart, true);

            if (week0.Month == @this.Month) week0 = week0.AddDays(-7);
            return (PastDay(@this, weekStart, true) - week0).Days / 7;
        }

        /// <summary>
        /// Gets the number of weeks in a year for the specified date. 
        /// </summary>
        /// <param name="this"></param>
        /// <param name="weekStart"></param>
        /// <returns></returns>
        public static int Week(this DateTimeOffset @this, DayOfWeek weekStart = DayOfWeek.Sunday)
        {
            var day1 = new DateTimeOffset(@this.Year, 1, 1, 0, 0, 0, @this.Offset);
            var week0 = PastDay(day1, weekStart, true);

            if (week0.Year == @this.Year) week0 = week0.AddDays(-7);
            return (PastDay(@this, weekStart, true) - week0).Days / 7;
        }

#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET46_OR_GREATER
#else
        /// <summary>
        /// Returns the number of milliseconds that have elapsed since 1970-01-01T00:00:00.000Z.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static long ToUnixTimeMilliseconds(this DateTimeOffset @this)
        {
            long num = @this.UtcDateTime.Ticks / 10000;
            return num - 62135596800000L;
        }

        /// <summary>
        /// Returns the number of seconds that have elapsed since 1970-01-01T00:00:00Z.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static long ToUnixTimeSeconds(this DateTimeOffset @this)
        {
            long num = @this.UtcDateTime.Ticks / 10000000;
            return num - 62135596800L;
        }
#endif

        /// <summary>
        /// Get the start point of the sepecified month.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTimeOffset StartOfYear(this DateTimeOffset @this) => new(@this.Year, 1, 1, 0, 0, 0, 0, @this.Offset);

        /// <summary>
        /// Get the start point of the sepecified month.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTimeOffset StartOfMonth(this DateTimeOffset @this) => new(@this.Year, @this.Month, 1, 0, 0, 0, 0, @this.Offset);

        /// <summary>
        /// Get the start point of the sepecified day.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTimeOffset StartOfDay(this DateTimeOffset @this) => @this.Date;

        /// <summary>
        /// Get the start point of the sepecified hour.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTimeOffset StartOfHour(this DateTimeOffset @this) => new(@this.Year, @this.Month, @this.Day, @this.Hour, 0, 0, 0, @this.Offset);

        /// <summary>
        /// Get the start point of the sepecified minute.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTimeOffset StartOfMinute(this DateTimeOffset @this) => new(@this.Year, @this.Month, @this.Day, @this.Hour, @this.Minute, 0, 0, @this.Offset);

        /// <summary>
        /// Get the start point of the sepecified second.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTimeOffset StartOfSecond(this DateTimeOffset @this) => new(@this.Year, @this.Month, @this.Day, @this.Hour, @this.Minute, @this.Second, 0, @this.Offset);

        /// <summary>
        /// Get the end point of the sepecified month.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTimeOffset EndOfYear(this DateTimeOffset @this) => new(@this.Year, 12, 31, 23, 59, 59, 999, @this.Offset);

        /// <summary>
        /// Get the end point of the sepecified month.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTimeOffset EndOfMonth(this DateTimeOffset @this)
        {
            if (new[] { 1, 3, 5, 7, 8, 10, 12 }.Contains(@this.Month))
                return new DateTimeOffset(@this.Year, @this.Month, 31, 23, 59, 59, 999, @this.Offset);
            else if (new[] { 4, 6, 9, 11 }.Contains(@this.Month))
                return new DateTimeOffset(@this.Year, @this.Month, 30, 23, 59, 59, 999, @this.Offset);
            else
            {
                if (DateTime.IsLeapYear(@this.Year))
                    return new DateTimeOffset(@this.Year, @this.Month, 29, 23, 59, 59, 999, @this.Offset);
                else return new DateTimeOffset(@this.Year, @this.Month, 28, 23, 59, 59, 999, @this.Offset);
            }
        }

        /// <summary>
        /// Get the end point of the sepecified day.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTimeOffset EndOfDay(this DateTimeOffset @this) => new(@this.Year, @this.Month, @this.Day, 23, 59, 59, 999, @this.Offset);

        /// <summary>
        /// Get the end point of the sepecified hour.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTimeOffset EndOfHour(this DateTimeOffset @this) => new(@this.Year, @this.Month, @this.Day, @this.Hour, 59, 59, 999, @this.Offset);

        /// <summary>
        /// Get the end point of the sepecified minute.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTimeOffset EndOfMinute(this DateTimeOffset @this) => new(@this.Year, @this.Month, @this.Day, @this.Hour, @this.Minute, 59, 999, @this.Offset);

        /// <summary>
        /// Get the end point of the sepecified second.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTimeOffset EndOfSecond(this DateTimeOffset @this) => new(@this.Year, @this.Month, @this.Day, @this.Hour, @this.Minute, @this.Second, 999, @this.Offset);

        /// <summary>
        /// Returns a new <see cref="DateTimeOffset"/> that adds the specified number of complete years to the value of this instance.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="value"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static DateTimeOffset AddDays(this DateTimeOffset @this, int value, DayMode mode)
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

        internal static DateTimeOffset AddYears(DateTimeOffset @this, int value)
        {
            var target = @this.AddYears(value);
            if (value > 0 && target.Day < @this.Day) return target.AddDays(1);
            else return target;
        }

        internal static DateTimeOffset AddMonths(DateTimeOffset @this, int value)
        {
            var target = @this.AddMonths(value);
            if (value > 0 && target.Day < @this.Day) target = target.AddDays(1);
            return target;
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> that adds the specified diff-number of years to the value of this instance.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTimeOffset AddTotalYears(this DateTimeOffset @this, double value)
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
        public static DateTimeOffset AddTotalMonths(this DateTimeOffset @this, double value)
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
        /// Gets the number of milliseconds elapsed from <see cref="DateTimeOffset.MinValue"/>.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static double ElapsedMilliseconds(this DateTimeOffset @this) => (@this - DateTimeOffset.MinValue).TotalMilliseconds;

        /// <summary>
        /// Gets the number of seconds elapsed from <see cref="DateTimeOffset.MinValue"/>.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static double ElapsedSeconds(this DateTimeOffset @this) => (@this - DateTimeOffset.MinValue).TotalSeconds;

        /// <summary>
        /// Gets the number of minutes elapsed from <see cref="DateTimeOffset.MinValue"/>.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static double ElapsedMinutes(this DateTimeOffset @this) => (@this - DateTimeOffset.MinValue).TotalMinutes;

        /// <summary>
        /// Gets the number of hours elapsed from <see cref="DateTimeOffset.MinValue"/>.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static double ElapsedHours(this DateTimeOffset @this) => (@this - DateTimeOffset.MinValue).TotalHours;

        /// <summary>
        /// Gets the number of days elapsed from <see cref="DateTimeOffset.MinValue"/>.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static double ElapsedDays(this DateTimeOffset @this) => (@this - DateTimeOffset.MinValue).TotalDays;

        /// <summary>
        /// Gets the number of months elapsed from <see cref="DateTimeOffset.MinValue"/>.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static double ElapsedMonths(this DateTimeOffset @this) => DateTimeOffsetEx.TotalMonths(DateTimeOffset.MinValue, @this);

        /// <summary>
        /// Gets the number of years elapsed from <see cref="DateTimeOffset.MinValue"/>.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static double ElapsedYears(this DateTimeOffset @this) => DateTimeOffsetEx.TotalYears(DateTimeOffset.MinValue, @this);

        private static int CastCycleDays(int days, bool isBackward)
        {
            days %= 7;
            if (isBackward) return days > 0 ? days - 7 : days;
            else return days < 0 ? days + 7 : days;
        }

    }
}
