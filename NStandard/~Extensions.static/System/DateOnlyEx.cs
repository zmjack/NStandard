﻿#if NET6_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Globalization;

namespace NStandard
{
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
        /// </summary>
        /// <param name="year"></param>
        /// <param name="week"></param>
        /// <param name="weekStart"></param>
        /// <returns></returns>
        public static DateOnly ParseFromWeek(int year, int week, DayOfWeek weekStart = DayOfWeek.Sunday)
        {
            var day1 = new DateOnly(year, 1, 1);
            var week0 = DateOnlyExtensions.PastDay(day1, weekStart, true);
            if (week0.Year == year) week0 = week0.AddDays(-7);
            return week0.AddDays(week * 7);
        }

    }
}
#endif
