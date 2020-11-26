using System;
using System.Threading;
using Xunit;

namespace NStandard.Test
{
    public class DateTimeExTests
    {
        [Fact]
        public void CompleteYearsTest()
        {
            var birthday = new DateTime(2012, 4, 16);
            Assert.Equal(0, DateTimeEx.YearDiff(birthday, new DateTime(2013, 4, 15)));
            Assert.Equal(1, DateTimeEx.YearDiff(birthday, new DateTime(2013, 4, 16)));

            Assert.Equal(0, DateTimeEx.YearDiff(new DateTime(2000, 2, 29), new DateTime(2001, 2, 28)));
            Assert.Equal(1, DateTimeEx.YearDiff(new DateTime(2000, 2, 29), new DateTime(2001, 3, 1)));
        }

        [Fact]
        public void CompleteMonthsTest()
        {
            var birthday = new DateTime(2012, 4, 16);
            Assert.Equal(11, DateTimeEx.MonthDiff(birthday, new DateTime(2013, 4, 15)));
            Assert.Equal(12, DateTimeEx.MonthDiff(birthday, new DateTime(2013, 4, 16)));

            Assert.Equal(35, DateTimeEx.MonthDiff(new DateTime(2000, 2, 29), new DateTime(2003, 2, 28)));
            Assert.Equal(36, DateTimeEx.MonthDiff(new DateTime(2000, 2, 29), new DateTime(2003, 3, 1)));
        }

        [Fact]
        public void TestUnixTimestamp()
        {
            var dt = new DateTime(1970, 1, 1, 16, 0, 0, DateTimeKind.Utc);

            Assert.Equal(57600, dt.UnixTimeSeconds());
            Assert.Equal(57600000, dt.UnixTimeMilliseconds());

            Assert.Equal(dt, DateTimeEx.FromUnixSeconds(57600));
            Assert.Equal(dt, DateTimeEx.FromUnixMilliseconds(57600_000));

            Assert.Equal(new DateTime(2018, 10, 31, 15, 55, 17),
                DateTimeEx.FromUnixSeconds(1540972517).ToLocalTime());
        }

        [Fact]
        public void ScopedNowTest0()
        {
            using (new NowScope())
            {
                var beforeNow = NowScope.Current.Now;
                Thread.Sleep(1000);
                var afterNow = NowScope.Current.Now;
                Assert.Equal(beforeNow, afterNow);
            }

            using (new NowScope(now => now.StartOfDay()))
            {
                var beforeNow = NowScope.Current.Now;
                Thread.Sleep(1000);
                var afterNow = NowScope.Current.Now;
                Assert.Equal(beforeNow, afterNow);
            }
        }

        [Fact]
        public void ScopedNowTest1()
        {
            using (DateTimeEx.BeginNowScope())
            {
                var beforeNow = DateTimeEx.ScopedNow;
                Thread.Sleep(1000);
                var afterNow = DateTimeEx.ScopedNow;
                Assert.Equal(beforeNow, afterNow);
            }

            using (DateTimeEx.BeginNowScope(now => now.StartOfDay()))
            {
                var beforeNow = DateTimeEx.ScopedNow;
                Thread.Sleep(1000);
                var afterNow = DateTimeEx.ScopedNow;
                Assert.Equal(beforeNow, afterNow);
            }
        }

        [Fact]
        public void ScopedNowTest2()
        {
            var beforeNow = DateTimeEx.ScopedNow;
            Thread.Sleep(1000);
            var afterNow = DateTimeEx.ScopedNow;
            Assert.NotEqual(beforeNow, afterNow);
        }

    }
}
