using System;
using System.Threading;
using Xunit;

namespace NStandard.Test
{
    public class DateTimeExTests
    {
        [Fact]
        public void YearDiffTest()
        {
            Assert.Equal(0, DateTimeEx.YearDiff(new DateTime(2012, 4, 16), new DateTime(2013, 4, 15)));
            Assert.Equal(1, DateTimeEx.YearDiff(new DateTime(2012, 4, 16), new DateTime(2013, 4, 16)));
            Assert.Equal(0, DateTimeEx.YearDiff(new DateTime(2000, 2, 29), new DateTime(2001, 2, 28)));
            Assert.Equal(1, DateTimeEx.YearDiff(new DateTime(2000, 2, 29), new DateTime(2001, 3, 1)));

            Assert.Equal(0, DateTimeEx.YearDiff(new DateTime(2013, 4, 15), new DateTime(2012, 4, 16)));
            Assert.Equal(-1, DateTimeEx.YearDiff(new DateTime(2013, 4, 16), new DateTime(2012, 4, 16)));
            Assert.Equal(0, DateTimeEx.YearDiff(new DateTime(2001, 2, 28), new DateTime(2000, 2, 29)));
            Assert.Equal(-1, DateTimeEx.YearDiff(new DateTime(2001, 3, 1), new DateTime(2000, 2, 29)));
        }

        [Fact]
        public void MonthDiffTest()
        {
            Assert.Equal(11, DateTimeEx.MonthDiff(new DateTime(2012, 4, 16), new DateTime(2013, 4, 15)));
            Assert.Equal(12, DateTimeEx.MonthDiff(new DateTime(2012, 4, 16), new DateTime(2013, 4, 16)));
            Assert.Equal(35, DateTimeEx.MonthDiff(new DateTime(2000, 2, 29), new DateTime(2003, 2, 28)));
            Assert.Equal(36, DateTimeEx.MonthDiff(new DateTime(2000, 2, 29), new DateTime(2003, 3, 1)));

            Assert.Equal(-11, DateTimeEx.MonthDiff(new DateTime(2013, 4, 15), new DateTime(2012, 4, 16)));
            Assert.Equal(-12, DateTimeEx.MonthDiff(new DateTime(2013, 4, 16), new DateTime(2012, 4, 16)));
            Assert.Equal(-35, DateTimeEx.MonthDiff(new DateTime(2003, 2, 28), new DateTime(2000, 2, 29)));
            Assert.Equal(-36, DateTimeEx.MonthDiff(new DateTime(2003, 3, 1), new DateTime(2000, 2, 29)));
        }

        [Fact]
        public void ExactYearDiffTest()
        {
            Assert.Equal(29d * 24 / (366 * 24), DateTimeEx.ExactYearDiff(new DateTime(2020, 2, 1), new DateTime(2020, 3, 1)), 2);
            Assert.Equal((29d * 24 - 3d) / (366 * 24), DateTimeEx.ExactYearDiff(new DateTime(2020, 2, 2, 15, 0, 0), new DateTime(2020, 3, 2, 12, 0, 0)));
            Assert.Equal((29d * 24 + 3d) / (366 * 24), DateTimeEx.ExactYearDiff(new DateTime(2020, 2, 2, 15, 0, 0), new DateTime(2020, 3, 2, 18, 0, 0)));
            Assert.Equal((43d * 24) / (366 * 24), DateTimeEx.ExactYearDiff(new DateTime(2020, 2, 1), new DateTime(2020, 3, 15)));
        }

        [Fact]
        public void ExactMonthDiffTest()
        {
            Assert.Equal(1d, DateTimeEx.ExactMonthDiff(new DateTime(2020, 2, 1), new DateTime(2020, 3, 1)));
            Assert.Equal(1d - 3d / (29 * 24), DateTimeEx.ExactMonthDiff(new DateTime(2020, 2, 2, 15, 0, 0), new DateTime(2020, 3, 2, 12, 0, 0)));
            Assert.Equal(1d + 3d / (31 * 24), DateTimeEx.ExactMonthDiff(new DateTime(2020, 2, 2, 15, 0, 0), new DateTime(2020, 3, 2, 18, 0, 0)));
            Assert.Equal(1d + 14d / 31, DateTimeEx.ExactMonthDiff(new DateTime(2020, 2, 1), new DateTime(2020, 3, 15)));
        }

        [Fact]
        public void TestUnixTimestamp()
        {
            var dt = new DateTime(1970, 1, 1, 16, 0, 0, DateTimeKind.Utc);

            Assert.Equal(57600, dt.ToUnixTimeSeconds());
            Assert.Equal(57600000, dt.ToUnixTimeMilliseconds());

            Assert.Equal(dt, DateTimeEx.FromUnixTimeSeconds(57600));
            Assert.Equal(dt, DateTimeEx.FromUnixTimeMilliseconds(57600_000));

            Assert.Equal(new DateTime(2018, 10, 31, 15, 55, 17), DateTimeEx.FromUnixTimeSeconds(1540972517).ToLocalTime());
        }

    }
}
