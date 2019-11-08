using System;
using System.Collections.Generic;

namespace NStandard
{
    public static class DateTimeEx
    {
        /// <summary>
        /// Gets the DateTime(UTC) of UnixMinValue.
        /// </summary>
        /// <returns></returns>
        public static DateTime UnixMinValue() => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Converts the sepecified Unix TimeStamp(seconds) to DateTime(UTC).
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static DateTime FromUnixSeconds(long seconds)
            => FromUnixMilliseconds(seconds * 1000);

        /// <summary>
        /// Converts the sepecified Unix TimeStamp(milliseconds) to DateTime(UTC).
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static DateTime FromUnixMilliseconds(long milliseconds)
            => new DateTime(milliseconds * 10000 + 621355968000000000, DateTimeKind.Utc);

        /// <summary>
        /// Gets the Unix Timestamp(milliseconds) of the specified DateTime(UTC).
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static long ToUnixTimeMilliseconds(DateTime @this)
            => (@this.ToUniversalTime().Ticks - 621355968000000000) / 10000;

        /// <summary>
        /// Gets the Unix Timestamp(seconds) of the specified DateTime(UTC).
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static long ToUnixTimeSeconds(DateTime @this)
            => ToUnixTimeMilliseconds(@this) / 1000;

        /// <summary>
        /// Gets the range of months.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> GetMonths(DateTime startDate, DateTime endDate)
        {
            startDate = new DateTime(startDate.Year, startDate.Month, 1);
            endDate = new DateTime(endDate.Year, endDate.Month, 1);

            for (var dt = startDate; dt <= endDate; dt = dt.AddMonths(1))
                yield return dt;
        }

        /// <summary>
        /// Gets the range of days.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> GetDays(DateTime startDate, DateTime endDate)
        {
            startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day);
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day);

            for (var dt = startDate; dt <= endDate; dt = dt.AddDays(1))
                yield return dt;
        }

        /// <summary>
        /// The number of complete years in the period.
        /// Same as DATEDIF(*, *, "Y") function in Excel.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static int GetCompleteYears(DateTime startDate, DateTime endDate)
        {
            startDate = startDate.Date;
            endDate = endDate.Date;

            var passedYears = endDate.Year - startDate.Year;

            if (endDate.AddYears(-passedYears) >= startDate)
                return passedYears;
            else return passedYears - 1;
        }

        /// <summary>
        /// The number of complete months in the period.
        /// Same as DATEDIF(*, *, "M") function in Excel.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static int GetCompleteMonths(DateTime startDate, DateTime endDate)
        {
            startDate = startDate.Date;
            endDate = endDate.Date;

            var passedYears = endDate.Year - startDate.Year;
            var passedMonths = endDate.Month - startDate.Month;

            if (endDate.AddYears(passedYears).AddMonths(passedMonths) > startDate)
                return passedYears * 12 + passedMonths;
            else return passedYears * 12 + passedMonths - 1;
        }

        /// <summary>
        /// The number of days in the period.
        /// Same as DATEDIF(*, *, "D") function in Excel.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static int GetCompleteDays(DateTime startDate, DateTime endDate)
        {
            startDate = startDate.Date;
            endDate = endDate.Date;
            return (int)(endDate - startDate).TotalDays;
        }

        /// <summary>
        /// Gets a DateTime for the specified week of year.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="week"></param>
        /// <param name="weekStart"></param>
        /// <returns></returns>
        public static DateTime ParseFromWeek(int year, int week, DayOfWeek weekStart = DayOfWeek.Sunday)
        {
            var day1 = new DateTime(year, 1, 1);
            var week0 = XDateTime.PastDay(day1, weekStart, true);

            if (week0.Year == year)
                week0 = week0.AddDays(-7);

            return week0.AddDays(week * 7);
        }

        ///// <summary>
        ///// [NOT RECOMMEND] The difference between the days in start_date and end_date. The months and years of the dates are ignored.
        ///// Same as DATEDIF(*, *, "MD") function in Excel.
        ///// </summary>
        ///// <param name="startDate"></param>
        ///// <param name="endDate"></param>
        ///// <returns></returns>
        //public static int GetDiffDaysIgnoreYearMonth(DateTime startDate, DateTime endDate)
        //{
        //    startDate = startDate.Date;
        //    endDate = endDate.Date;

        //    if (startDate.Day > endDate.Day)
        //    {
        //        var prevMonth = endDate.AddMonths(-1);
        //        var offsetDays = startDate.Day - DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);
        //        return (int)(endDate - prevMonth.AddDays(offsetDays)).TotalDays;
        //    }
        //    else return endDate.Day - startDate.Day;
        //}

        ///// <summary>
        ///// The difference between the months in start_date and end_date. The days and years of the dates are ignored.
        ///// Same as DATEDIF(*, *, "YM") function in Excel.
        ///// </summary>
        ///// <param name="startDate"></param>
        ///// <param name="endDate"></param>
        ///// <returns></returns>
        //public static int GetDiffMonthsIgnoreYear(DateTime startDate, DateTime endDate)
        //{
        //    startDate = startDate.Date;
        //    endDate = endDate.Date;

        //    if (startDate.Month == endDate.Month)
        //        return endDate.Day >= startDate.Day ? 0 : 11;
        //    else
        //    {
        //        int passedMonths;
        //        if (startDate.Month > endDate.Month)
        //            passedMonths = endDate.Month + 12 - startDate.Month;
        //        else passedMonths = endDate.Month - startDate.Month;

        //        if (endDate.Day >= startDate.Day)
        //            return passedMonths;
        //        else return passedMonths - 1;
        //    }
        //}

        ///// <summary>
        ///// The difference between the days of start_date and end_date. The years of the dates are ignored.
        ///// Same as DATEDIF(*, *, "YD") function in Excel.
        ///// </summary>
        ///// <param name="startDate"></param>
        ///// <param name="endDate"></param>
        ///// <returns></returns>
        //public static int GetDiffDaysIgnoreYear(DateTime startDate, DateTime endDate)
        //{

        //}

    }
}
