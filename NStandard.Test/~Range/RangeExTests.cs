using System;
using System.Linq;
using Xunit;

namespace NStandard.Test
{
    public class RangeExTests
    {
        [Fact]
        public void IntTest1()
        {
            var range = RangeEx.Create(95, 6).ToArray();
            Assert.Equal(new[] { 95, 96, 97, 98, 99, 100 }, range);
        }

        [Fact]
        public void IntTest2()
        {
            var range = RangeEx.Create(95, 6, prev => prev / 2).ToArray();
            Assert.Equal(new[] { 95, 47, 23, 11, 5, 2 }, range);
        }

        [Fact]
        public void DateTimeYearTest()
        {
            var start = new DateTime(2000, 1, 1);
            var range = RangeEx.Create(start, 3, DateRangeType.Year).ToArray();
            Assert.Equal(new[]
            {
                new DateTime(2000, 1, 1, 0, 0, 0),
                new DateTime(2001, 1, 1, 0, 0, 0),
                new DateTime(2002, 1, 1, 0, 0, 0),
            }, range);
        }

        [Fact]
        public void DateTimeMonthTest()
        {
            var start = new DateTime(2000, 1, 1);
            var range = RangeEx.Create(start, 3, DateRangeType.Month).ToArray();
            Assert.Equal(new[]
            {
                new DateTime(2000, 1, 1, 0, 0, 0),
                new DateTime(2000, 2, 1, 0, 0, 0),
                new DateTime(2000, 3, 1, 0, 0, 0),
            }, range);
        }

        [Fact]
        public void DateTimeDayTest()
        {
            var start = new DateTime(2000, 1, 1);
            var range = RangeEx.Create(start, 3, DateRangeType.Day).ToArray();
            Assert.Equal(new[]
            {
                new DateTime(2000, 1, 1, 0, 0, 0),
                new DateTime(2000, 1, 2, 0, 0, 0),
                new DateTime(2000, 1, 3, 0, 0, 0),
            }, range);
        }

        [Fact]
        public void DateTimeHourTest()
        {
            var start = new DateTime(2000, 1, 1);
            var range = RangeEx.Create(start, 3, DateRangeType.Hour).ToArray();
            Assert.Equal(new[]
            {
                new DateTime(2000, 1, 1, 0, 0, 0),
                new DateTime(2000, 1, 1, 1, 0, 0),
                new DateTime(2000, 1, 1, 2, 0, 0),
            }, range);
        }

        [Fact]
        public void DateTimeMinuteTest()
        {
            var start = new DateTime(2000, 1, 1);
            var range = RangeEx.Create(start, 3, DateRangeType.Minute).ToArray();
            Assert.Equal(new[]
            {
                new DateTime(2000, 1, 1, 0, 0, 0),
                new DateTime(2000, 1, 1, 0, 1, 0),
                new DateTime(2000, 1, 1, 0, 2, 0),
            }, range);
        }

        [Fact]
        public void DateTimeSecondTest()
        {
            var start = new DateTime(2000, 1, 1);
            var range = RangeEx.Create(start, 3, DateRangeType.Second).ToArray();
            Assert.Equal(new[]
            {
                new DateTime(2000, 1, 1, 0, 0, 0),
                new DateTime(2000, 1, 1, 0, 0, 1),
                new DateTime(2000, 1, 1, 0, 0, 2),
            }, range);
        }

    }
}
