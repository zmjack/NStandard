﻿using System;
using Xunit;

namespace NStandard.Test
{
    public class XDateTimeTests
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
            /* 2012 - 04
             * 
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
        }

    }
}
