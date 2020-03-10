using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NStandard.Test
{
    public class DateTimeExTests
    {
        [Fact]
        public void GetCompleteYearsTest()
        {
            var birthday = new DateTime(2012, 4, 16);
            var date1 = new DateTime(2013, 4, 15);
            var date2 = new DateTime(2013, 4, 16);
            Assert.Equal(0, DateTimeEx.YearDiff(birthday, date1));
            Assert.Equal(1, DateTimeEx.YearDiff(birthday, date2));
        }

        [Fact]
        public void GetCompleteMonthsTest()
        {
            var birthday = new DateTime(2012, 4, 16);
            var date1 = new DateTime(2013, 4, 15);
            var date2 = new DateTime(2013, 4, 16);
            Assert.Equal(11, DateTimeEx.MonthDiff(birthday, date1));
            Assert.Equal(12, DateTimeEx.MonthDiff(birthday, date2));
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

    }
}
