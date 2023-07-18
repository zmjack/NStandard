﻿#if NET6_0_OR_GREATER
using System;
using System.ComponentModel;
using System.Linq;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class DateOnlyExtensions
    {
        /// <summary>
        /// Gets a past day for the specified day of week.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="dayOfWeek"></param>
        /// <param name="includeCurrentDay"></param>
        /// <returns></returns>
        public static DateOnly PastDay(this DateOnly @this, DayOfWeek dayOfWeek, bool includeCurrentDay = false)
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
        public static DateOnly FutureDay(this DateOnly @this, DayOfWeek dayOfWeek, bool includeCurrentDay = false)
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
        public static int WeekInMonth(this DateOnly @this, DayOfWeek weekStart = DayOfWeek.Sunday)
        {
            var day1 = new DateOnly(@this.Year, @this.Month, 1);
            var week0 = PastDay(day1, weekStart, true);

            if (week0.Month == @this.Month) week0 = week0.AddDays(-7);
            return (PastDay(@this, weekStart, true).DayNumber - week0.DayNumber) / 7;
        }

        /// <summary>
        /// Gets the number of weeks in a year for the specified date. 
        /// </summary>
        /// <param name="this"></param>
        /// <param name="weekStart"></param>
        /// <returns></returns>
        public static int Week(this DateOnly @this, DayOfWeek weekStart = DayOfWeek.Sunday)
        {
            var day1 = new DateOnly(@this.Year, 1, 1);
            var week0 = PastDay(day1, weekStart, true);

            if (week0.Year == @this.Year) week0 = week0.AddDays(-7);
            return (PastDay(@this, weekStart, true).DayNumber - week0.DayNumber) / 7;
        }

        /// <summary>
        /// Get the start point of the sepecified year.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateOnly StartOfYear(this DateOnly @this) => new(@this.Year, 1, 1);

        /// <summary>
        /// Get the start point of the sepecified month.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateOnly StartOfMonth(this DateOnly @this) => new(@this.Year, @this.Month, 1);

        /// <summary>
        /// Get the end point of the sepecified year.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateOnly EndOfYear(this DateOnly @this) => new(@this.Year, 12, 31);

        /// <summary>
        /// Get the end point of the sepecified month.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateOnly EndOfMonth(this DateOnly @this)
        {
            if (new[] { 1, 3, 5, 7, 8, 10, 12 }.Contains(@this.Month))
                return new DateOnly(@this.Year, @this.Month, 31);
            else if (new[] { 4, 6, 9, 11 }.Contains(@this.Month))
                return new DateOnly(@this.Year, @this.Month, 30);
            else
            {
                if (DateTime.IsLeapYear(@this.Year))
                    return new DateOnly(@this.Year, @this.Month, 29);
                else return new DateOnly(@this.Year, @this.Month, 28);
            }
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> that adds the specified number of complete years to the value of this instance.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="value"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static DateOnly AddDays(this DateOnly @this, int value, DayMode mode)
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
        /// Returns a new <see cref="DateOnly"/> that adds the specified number of complete years to the value of this instance.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static DateOnly AddYears(DateOnly @this, int value)
        {
            var target = @this.AddYears(value);
            if (value > 0 && target.Day < @this.Day) return target.AddDays(1);
            else return target;
        }

        /// <summary>
        /// Returns a new <see cref="DateOnly"/> that adds the specified number of complete months to the value of this instance.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static DateOnly AddMonths(this DateOnly @this, int value)
        {
            var target = @this.AddMonths(value);
            if (value > 0 && target.Day < @this.Day) return target.AddDays(1);
            else return target;
        }

        /// <summary>
        /// Returns a new <see cref="DateOnly"/> that adds the specified diff-number of years to the value of this instance.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateOnly AddTotalYears(this DateOnly @this, double value)
        {
            var integer = (int)value;
            var fractional = value - integer;
            var start = AddYears(@this, integer);

            if (fractional == 0) return start;

            double offsetDays;
            if (value >= 0)
            {
                var end = AddYears(@this, integer + 1);
                offsetDays = (end.DayNumber - start.DayNumber) * fractional;
            }
            else
            {
                var end = AddYears(@this, integer - 1);
                offsetDays = (start.DayNumber - end.DayNumber) * fractional;
            }

            return start.AddDays((int)offsetDays);
        }

        /// <summary>
        /// Returns a new <see cref="DateOnly"/> that adds the specified diff-number of months to the value of this instance.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateOnly AddTotalMonths(this DateOnly @this, double value)
        {
            var integer = (int)value;
            var fractional = value - integer;
            var start = AddMonths(@this, integer);

            if (fractional == 0) return start;

            double offsetDays;
            if (value >= 0)
            {
                var end = AddMonths(@this, integer + 1);
                offsetDays = (end.DayNumber - start.DayNumber) * fractional;
            }
            else
            {
                var end = AddMonths(@this, integer - 1);
                offsetDays = (start.DayNumber - end.DayNumber) * fractional;
            }

            return start.AddDays((int)offsetDays);
        }

        private static int CastCycleDays(int days, bool isBackward)
        {
            days %= 7;
            if (isBackward) return days > 0 ? days - 7 : days;
            else return days < 0 ? days + 7 : days;
        }

    }
}
#endif
