using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NStandard.Threading
{
    public class Cron
    {
        public CronFieldType SecondType { get; set; }
        public int[] Seconds { get; set; } = new int[0];

        public CronFieldType MinuteType { get; set; }
        public int[] Minutes { get; set; } = new int[0];

        public CronFieldType HourType { get; set; }
        public int[] Hours { get; set; } = new int[0];

        public CronFieldType DayType { get; set; }
        public int[] Days { get; set; } = new int[0];

        public CronFieldType MonthType { get; set; }
        public int[] Months { get; set; } = new int[0];

        public CronFieldType DayOfWeekType { get; set; }
        public DayOfWeek[] DayOfWeeks { get; set; } = new DayOfWeek[0];

        public CronFieldType YearType { get; set; }
        public int[] Years { get; set; } = new int[0];

        public DateTime? GetNextTime(DateTime current)
        {
            if (SecondType == CronFieldType.Specified && !Seconds.Any()) throw new ArgumentException("Seconds are Empty.");
            if (MinuteType == CronFieldType.Specified && !Minutes.Any()) throw new ArgumentException("Minutes are Empty.");
            if (HourType == CronFieldType.Specified && !Hours.Any()) throw new ArgumentException("Hours are Empty.");
            if (DayType == CronFieldType.Specified && !Days.Any()) throw new ArgumentException("Days are Empty.");
            if (MonthType == CronFieldType.Specified && !Months.Any()) throw new ArgumentException("Months are Empty.");
            if (DayOfWeekType == CronFieldType.Specified && !DayOfWeeks.Any()) throw new ArgumentException("DayOfWeeks are Empty.");
            if (YearType == CronFieldType.Specified && !Years.Any()) throw new ArgumentException("Years are Empty.");

            return NextSecond(current.Year, current.Month, current.Day, current.Hour, current.Minute, current.Second);
        }

        protected DateTime? NextSecond(int year, int month, int day, int hour, int minute, int second)
        {
            switch (SecondType)
            {
                case CronFieldType.Any:
                    if (second < 59)
                        return new DateTime(year, month, day, hour, minute, second + 1);
                    else return NextMinute(year, month, day, hour, minute, 0);

                case CronFieldType.Specified:
                    var values = Seconds.Where(x => x > second);
                    if (values.Any())
                        return new DateTime(year, month, day, hour, minute, values.First());
                    else return NextMinute(year, month, day, hour, minute, Seconds.First());

                default: throw new NotImplementedException();
            }
        }

        protected DateTime? NextMinute(int year, int month, int day, int hour, int minute, int second)
        {
            switch (MinuteType)
            {
                case CronFieldType.Any:
                    if (minute < 59)
                        return new DateTime(year, month, day, hour, minute + 1, second);
                    else return NextHour(year, month, day, hour, 0, second);

                case CronFieldType.Specified:
                    var values = Minutes.Where(x => x > minute);
                    if (values.Any())
                        return new DateTime(year, month, day, hour, values.First(), second);
                    else return NextHour(year, month, day, hour, Minutes.First(), second);

                default: throw new NotImplementedException();
            }
        }

        protected DateTime? NextHour(int year, int month, int day, int hour, int minute, int second)
        {
            switch (HourType)
            {
                case CronFieldType.Any:
                    if (hour < 23)
                        return new DateTime(year, month, day, hour + 1, minute, second);
                    else return NextDay(year, month, day, 0, minute, second);

                case CronFieldType.Specified:
                    var values = Hours.Where(x => x > hour);
                    if (values.Any())
                        return new DateTime(year, month, day, values.First(), minute, second);
                    else return NextDay(year, month, day, Hours.First(), minute, second);

                default: throw new NotImplementedException();
            }
        }

        protected DateTime? NextDay(int year, int month, int day, int hour, int minute, int second)
        {
            var daysInMonth = DateTime.DaysInMonth(year, month);

            switch (DayType)
            {
                case CronFieldType.Any:
                    {
                        var nextDay = day + 1;

                        if (nextDay <= daysInMonth)
                            return new DateTime(year, month, nextDay, hour, minute, second);
                        else return NextMonth(year, month, 1, hour, minute, second);
                    }

                case CronFieldType.Specified:
                    var values = Days.Where(x => x > day);
                    if (values.Any())
                    {
                        var nextDay = values.First();

                        if (nextDay <= daysInMonth)
                            return new DateTime(year, month, nextDay, hour, minute, second);
                        else return NextMonth(year, month, nextDay, hour, minute, second);
                    }
                    else return NextMonth(year, month, Days.First(), hour, minute, second);

                default: throw new NotImplementedException();
            }
        }

        protected DateTime? NextMonth(int year, int month, int day, int hour, int minute, int second)
        {
            switch (MonthType)
            {
                case CronFieldType.Any:
                    if (month < 12)
                    {
                        var nextMonth = month + 1;
                        var daysInNextMonth = DateTime.DaysInMonth(year, nextMonth);

                        if (day <= daysInNextMonth)
                            return new DateTime(year, nextMonth, day, hour, minute, second);
                        return NextMonth(year, nextMonth, day, hour, minute, second);
                    }
                    else
                    {
                        int nextYear = 0, nextMonth = 1;
                        switch (YearType)
                        {
                            case CronFieldType.Any:
                                nextYear = year + 1;
                                break;

                            case CronFieldType.Specified:
                                var yearValues = Years.Where(x => x > year);
                                if (yearValues.Any())
                                {
                                    nextYear = yearValues.First();
                                }
                                break;

                            default: throw new NotImplementedException();
                        }
                        var daysInNextMonth = DateTime.DaysInMonth(nextYear, nextMonth);

                        if (day <= daysInNextMonth)
                            return new DateTime(nextYear, nextMonth, day, hour, minute, second);
                        return NextMonth(nextYear, nextMonth, day, hour, minute, second);
                    }

                case CronFieldType.Specified:
                    var values = Months.Where(x => x > month);
                    if (values.Any())
                    {
                        var nextMonth = values.First();
                        var daysInNextMonth = DateTime.DaysInMonth(year, nextMonth);

                        if (day <= daysInNextMonth)
                            return new DateTime(year, nextMonth, day, hour, minute, second);
                        return NextMonth(year, nextMonth, day, hour, minute, second);
                    }
                    else
                    {
                        int nextYear = 0, nextMonth = Months.First();
                        switch (YearType)
                        {
                            case CronFieldType.Any:
                                nextYear = year + 1;
                                break;

                            case CronFieldType.Specified:
                                var yearValues = Years.Where(x => x > year);
                                if (yearValues.Any())
                                {
                                    nextYear = yearValues.First();
                                }
                                break;

                            default: throw new NotImplementedException();
                        }
                        var daysInNextMonth = DateTime.DaysInMonth(nextYear, nextMonth);

                        if (day <= daysInNextMonth)
                            return new DateTime(nextYear, nextMonth, day, hour, minute, second);
                        return NextMonth(nextYear, nextMonth, day, hour, minute, second);
                    }

                default: throw new NotImplementedException();
            }
        }

    }
}
