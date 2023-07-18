using System;
using Xunit;

namespace NStandard.Test
{
    public class DateTimeExtensionsTests
    {
        [Fact]
        public void BeginningOrEndTest()
        {
            var today = new DateTime(2012, 4, 16, 22, 23, 24);

            Assert.Equal(new DateTime(2012, 1, 1, 0, 0, 0, 0), today.StartOfYear());
            Assert.Equal(new DateTime(2012, 4, 1, 0, 0, 0, 0), today.StartOfMonth());
            Assert.Equal(new DateTime(2012, 4, 16, 0, 0, 0, 0), today.StartOfDay());
            Assert.Equal(new DateTime(2012, 12, 31, 23, 59, 59, 999), today.EndOfYear());
            Assert.Equal(new DateTime(2012, 4, 30, 23, 59, 59, 999), today.EndOfMonth());
            Assert.Equal(new DateTime(2012, 4, 16, 23, 59, 59, 999), today.EndOfDay());

            Assert.Equal(new DateTime(2000, 2, 29, 23, 59, 59, 999), new DateTime(2000, 2, 16).EndOfMonth());
            Assert.Equal(new DateTime(2001, 2, 28, 23, 59, 59, 999), new DateTime(2001, 2, 16).EndOfMonth());
        }

        [Fact]
        public void Test1()
        {
            /*  2012 - 04
             *  Su  Mo  Tu  We  Th  Fr  Sa
             *                           1
             *   2   3   4   5   6   7   8
             *   9  10  11  12  13  14  15
             * (16) 17  18  19  20  21  22
             *  23  24  25  26  27  28  29
             *  30
             */

            var today = new DateTime(2012, 4, 16, 22, 23, 24);
            Assert.Equal("2012/1/1 0:00:00", today.StartOfYear().ToString());
            Assert.Equal("2012/4/1 0:00:00", today.StartOfMonth().ToString());
            Assert.Equal("2012/4/16 0:00:00", today.StartOfDay().ToString());

            Assert.Equal("2012/12/31 23:59:59", today.EndOfYear().ToString());
            Assert.Equal("2012/4/30 23:59:59", today.EndOfMonth().ToString());
            Assert.Equal("2012/4/16 23:59:59", today.EndOfDay().ToString());

            Assert.Equal("2012/4/9 22:23:24", today.PastDay(DayOfWeek.Monday, false).ToString());
            Assert.Equal("2012/4/16 22:23:24", today.PastDay(DayOfWeek.Monday, true).ToString());
            Assert.Equal("2012/4/23 22:23:24", today.FutureDay(DayOfWeek.Monday, false).ToString());
            Assert.Equal("2012/4/16 22:23:24", today.FutureDay(DayOfWeek.Monday, true).ToString());

            Assert.Equal("2012/4/15 22:23:24", today.PastDay(DayOfWeek.Sunday, false).ToString());
            Assert.Equal("2012/4/15 22:23:24", today.PastDay(DayOfWeek.Sunday, true).ToString());
            Assert.Equal("2012/4/22 22:23:24", today.FutureDay(DayOfWeek.Sunday, false).ToString());
            Assert.Equal("2012/4/22 22:23:24", today.FutureDay(DayOfWeek.Sunday, true).ToString());

            Assert.Equal(2, today.WeekInMonth(DayOfWeek.Friday));
            Assert.Equal(3, today.WeekInMonth(DayOfWeek.Sunday));
        }

        [Fact]
        public void WeekTest()
        {
            Assert.Equal(0, new DateTime(2017, 1, 1).Week(DayOfWeek.Monday));
            Assert.Equal(1, new DateTime(2018, 1, 1).Week(DayOfWeek.Monday));

            /*  2020 - 01
             *  Su  Mo  Tu  We  Th  Fr  Sa
             *             ( 1)  2   3   4
             * ( 5)  6   7   8   9  10  11
             *  12 (13) 14  15  16  17  18
             *  19  20  21  22  23  24 (25)
             *  26  27  28  29  30  31
             */
            Assert.Equal(0, new DateTime(2020, 1, 1).Week());
            Assert.Equal(1, new DateTime(2020, 1, 5).Week());
            Assert.Equal(2, new DateTime(2020, 1, 13).Week());
            Assert.Equal(3, new DateTime(2020, 1, 25).Week());
        }

        [Fact]
        public void AddTotalYearsTest()
        {
            Assert.Equal(new DateTime(2001, 3, 1), new DateTime(2000, 2, 29).AddTotalYears(1));
            Assert.Equal(new DateTime(2000, 3, 1), new DateTime(2001, 3, 1).AddTotalYears(-1));

            Assert.Equal(new DateTime(1999, 2, 28), new DateTime(2000, 2, 29).AddTotalYears(-1));
            Assert.Equal(new DateTime(2000, 2, 28), new DateTime(1999, 2, 28).AddTotalYears(1));
        }

        [Fact]
        public void AddTotalMonthsTest()
        {
            Assert.Equal(new DateTime(2000, 5, 1), DateTimeExtensions.AddTotalMonths(new DateTime(2000, 3, 31), 1));
            Assert.Equal(new DateTime(2000, 4, 1), DateTimeExtensions.AddTotalMonths(new DateTime(2000, 5, 1), -1));

            Assert.Equal(new DateTime(2000, 2, 29), DateTimeExtensions.AddTotalMonths(new DateTime(2000, 3, 31), -1));
            Assert.Equal(new DateTime(2000, 3, 29), DateTimeExtensions.AddTotalMonths(new DateTime(2000, 2, 29), 1));
        }

        [Fact]
        public void GetElapsedTest()
        {
            Assert.Equal(63470131200000, new DateTime(2012, 4, 16).ElapsedMilliseconds());
            Assert.Equal(63470131200, new DateTime(2012, 4, 16).ElapsedSeconds());
            Assert.Equal(1057835520, new DateTime(2012, 4, 16).ElapsedMinutes());
            Assert.Equal(17630592, new DateTime(2012, 4, 16).ElapsedHours());
            Assert.Equal(734608, new DateTime(2012, 4, 16).ElapsedDays());
            Assert.Equal(24135.5, new DateTime(2012, 4, 16).ElapsedMonths());
            Assert.Equal(2011.2896174863388, new DateTime(2012, 4, 16).ElapsedYears());
        }

        [Fact]
        public void ToUnixTimeMillisecondsTest()
        {
            var now = DateTime.Now;
            var nowOffset = DateTimeOffset.Now;
            Assert.Equal(nowOffset.ToUnixTimeSeconds(), now.ToUnixTimeSeconds());
            Assert.Equal(nowOffset.ToUnixTimeMilliseconds(), now.ToUnixTimeMilliseconds());

            Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(63470131200), DateTimeOffsetEx.FromUnixTimeSeconds(63470131200));
            Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(63470131200000), DateTimeOffsetEx.FromUnixTimeMilliseconds(63470131200000));

            Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(63470131200), DateTimeEx.FromUnixTimeSeconds(63470131200));
            Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(63470131200000), DateTimeEx.FromUnixTimeMilliseconds(63470131200000));
        }

        [Fact]
        public void AddWeekdayDays_FutureTest()
        {
            // Monday start
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(0, DayMode.Weekday), new DateTime(2018, 1, 1));
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(1, DayMode.Weekday), new DateTime(2018, 1, 2));
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(2, DayMode.Weekday), new DateTime(2018, 1, 3));
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(3, DayMode.Weekday), new DateTime(2018, 1, 4));
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(4, DayMode.Weekday), new DateTime(2018, 1, 5));
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(5, DayMode.Weekday), new DateTime(2018, 1, 8));
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(6, DayMode.Weekday), new DateTime(2018, 1, 9));
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(7, DayMode.Weekday), new DateTime(2018, 1, 10));
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(8, DayMode.Weekday), new DateTime(2018, 1, 11));
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(9, DayMode.Weekday), new DateTime(2018, 1, 12));

            // Friday start
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(0, DayMode.Weekday), new DateTime(2021, 1, 1));
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(1, DayMode.Weekday), new DateTime(2021, 1, 4));
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(2, DayMode.Weekday), new DateTime(2021, 1, 5));
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(3, DayMode.Weekday), new DateTime(2021, 1, 6));
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(4, DayMode.Weekday), new DateTime(2021, 1, 7));
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(5, DayMode.Weekday), new DateTime(2021, 1, 8));
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(6, DayMode.Weekday), new DateTime(2021, 1, 11));
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(7, DayMode.Weekday), new DateTime(2021, 1, 12));
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(8, DayMode.Weekday), new DateTime(2021, 1, 13));
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(9, DayMode.Weekday), new DateTime(2021, 1, 14));

            // Saturday start
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(0, DayMode.Weekday), new DateTime(2022, 1, 1));
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(1, DayMode.Weekday), new DateTime(2022, 1, 3));
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(2, DayMode.Weekday), new DateTime(2022, 1, 4));
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(3, DayMode.Weekday), new DateTime(2022, 1, 5));
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(4, DayMode.Weekday), new DateTime(2022, 1, 6));
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(5, DayMode.Weekday), new DateTime(2022, 1, 7));
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(6, DayMode.Weekday), new DateTime(2022, 1, 10));
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(7, DayMode.Weekday), new DateTime(2022, 1, 11));
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(8, DayMode.Weekday), new DateTime(2022, 1, 12));
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(9, DayMode.Weekday), new DateTime(2022, 1, 13));

            // Sunday start
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(0, DayMode.Weekday), new DateTime(2017, 1, 1));
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(1, DayMode.Weekday), new DateTime(2017, 1, 2));
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(2, DayMode.Weekday), new DateTime(2017, 1, 3));
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(3, DayMode.Weekday), new DateTime(2017, 1, 4));
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(4, DayMode.Weekday), new DateTime(2017, 1, 5));
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(5, DayMode.Weekday), new DateTime(2017, 1, 6));
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(6, DayMode.Weekday), new DateTime(2017, 1, 9));
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(7, DayMode.Weekday), new DateTime(2017, 1, 10));
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(8, DayMode.Weekday), new DateTime(2017, 1, 11));
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(9, DayMode.Weekday), new DateTime(2017, 1, 12));

            // Monday start
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(-0, DayMode.Weekday), new DateTime(2018, 1, 1));
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(-1, DayMode.Weekday), new DateTime(2017, 12, 29));
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(-2, DayMode.Weekday), new DateTime(2017, 12, 28));
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(-3, DayMode.Weekday), new DateTime(2017, 12, 27));
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(-4, DayMode.Weekday), new DateTime(2017, 12, 26));
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(-5, DayMode.Weekday), new DateTime(2017, 12, 25));
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(-6, DayMode.Weekday), new DateTime(2017, 12, 22));
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(-7, DayMode.Weekday), new DateTime(2017, 12, 21));
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(-8, DayMode.Weekday), new DateTime(2017, 12, 20));
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(-9, DayMode.Weekday), new DateTime(2017, 12, 19));

            // Friday start                               
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(-0, DayMode.Weekday), new DateTime(2021, 1, 1));
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(-1, DayMode.Weekday), new DateTime(2020, 12, 31));
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(-2, DayMode.Weekday), new DateTime(2020, 12, 30));
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(-3, DayMode.Weekday), new DateTime(2020, 12, 29));
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(-4, DayMode.Weekday), new DateTime(2020, 12, 28));
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(-5, DayMode.Weekday), new DateTime(2020, 12, 25));
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(-6, DayMode.Weekday), new DateTime(2020, 12, 24));
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(-7, DayMode.Weekday), new DateTime(2020, 12, 23));
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(-8, DayMode.Weekday), new DateTime(2020, 12, 22));
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(-9, DayMode.Weekday), new DateTime(2020, 12, 21));

            // Saturday start                             
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(-0, DayMode.Weekday), new DateTime(2022, 1, 1));
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(-1, DayMode.Weekday), new DateTime(2021, 12, 31));
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(-2, DayMode.Weekday), new DateTime(2021, 12, 30));
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(-3, DayMode.Weekday), new DateTime(2021, 12, 29));
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(-4, DayMode.Weekday), new DateTime(2021, 12, 28));
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(-5, DayMode.Weekday), new DateTime(2021, 12, 27));
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(-6, DayMode.Weekday), new DateTime(2021, 12, 24));
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(-7, DayMode.Weekday), new DateTime(2021, 12, 23));
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(-8, DayMode.Weekday), new DateTime(2021, 12, 22));
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(-9, DayMode.Weekday), new DateTime(2021, 12, 21));

            // Sunday start                               
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(-0, DayMode.Weekday), new DateTime(2017, 1, 1));
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(-1, DayMode.Weekday), new DateTime(2016, 12, 30));
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(-2, DayMode.Weekday), new DateTime(2016, 12, 29));
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(-3, DayMode.Weekday), new DateTime(2016, 12, 28));
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(-4, DayMode.Weekday), new DateTime(2016, 12, 27));
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(-5, DayMode.Weekday), new DateTime(2016, 12, 26));
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(-6, DayMode.Weekday), new DateTime(2016, 12, 23));
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(-7, DayMode.Weekday), new DateTime(2016, 12, 22));
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(-8, DayMode.Weekday), new DateTime(2016, 12, 21));
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(-9, DayMode.Weekday), new DateTime(2016, 12, 20));
        }

        [Fact]
        public void AddWeekdayDays_PastTest()
        {
            // Monday start
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(0, DayMode.Weekend), new DateTime(2018, 1, 1));
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(1, DayMode.Weekend), new DateTime(2018, 1, 6));
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(2, DayMode.Weekend), new DateTime(2018, 1, 7));
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(3, DayMode.Weekend), new DateTime(2018, 1, 13));

            // Friday start                                                 
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(0, DayMode.Weekend), new DateTime(2021, 1, 1));
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(1, DayMode.Weekend), new DateTime(2021, 1, 2));
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(2, DayMode.Weekend), new DateTime(2021, 1, 3));
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(3, DayMode.Weekend), new DateTime(2021, 1, 9));

            // Saturday start                                               
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(0, DayMode.Weekend), new DateTime(2022, 1, 1));
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(1, DayMode.Weekend), new DateTime(2022, 1, 2));
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(2, DayMode.Weekend), new DateTime(2022, 1, 8));
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(3, DayMode.Weekend), new DateTime(2022, 1, 9));

            // Sunday start                                                 
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(0, DayMode.Weekend), new DateTime(2017, 1, 1));
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(1, DayMode.Weekend), new DateTime(2017, 1, 7));
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(2, DayMode.Weekend), new DateTime(2017, 1, 8));
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(3, DayMode.Weekend), new DateTime(2017, 1, 14));

            // Monday start                                         
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(-0, DayMode.Weekend), new DateTime(2018, 1, 1));
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(-1, DayMode.Weekend), new DateTime(2017, 12, 31));
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(-2, DayMode.Weekend), new DateTime(2017, 12, 30));
            Assert.Equal(new DateTime(2018, 1, 1).AddDays(-3, DayMode.Weekend), new DateTime(2017, 12, 24));

            // Friday start                                     
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(-0, DayMode.Weekend), new DateTime(2021, 1, 1));
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(-1, DayMode.Weekend), new DateTime(2020, 12, 27));
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(-2, DayMode.Weekend), new DateTime(2020, 12, 26));
            Assert.Equal(new DateTime(2021, 1, 1).AddDays(-3, DayMode.Weekend), new DateTime(2020, 12, 20));

            // Saturday start                                      
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(-0, DayMode.Weekend), new DateTime(2022, 1, 1));
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(-1, DayMode.Weekend), new DateTime(2021, 12, 26));
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(-2, DayMode.Weekend), new DateTime(2021, 12, 25));
            Assert.Equal(new DateTime(2022, 1, 1).AddDays(-3, DayMode.Weekend), new DateTime(2021, 12, 19));

            // Sunday start                                        
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(-0, DayMode.Weekend), new DateTime(2017, 1, 1));
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(-1, DayMode.Weekend), new DateTime(2016, 12, 31));
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(-2, DayMode.Weekend), new DateTime(2016, 12, 25));
            Assert.Equal(new DateTime(2017, 1, 1).AddDays(-3, DayMode.Weekend), new DateTime(2016, 12, 24));
        }

    }
}
