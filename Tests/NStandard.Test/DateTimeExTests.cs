using System;
using Xunit;

namespace NStandard.Test
{
    public class DateTimeExTests
    {
        [Fact]
        public void TotalYearDiffTest()
        {
            static void InnerTest(DateTime start, DateTime end, double diff)
            {
                Assert.Equal(diff, DateTimeEx.TotalYearDiff(start, end));
                Assert.Equal(end, start.AddTotalYearDiff(diff));
            }

            InnerTest(new DateTime(2020, 2, 1), new DateTime(2020, 3, 1), 29d / 366);
            InnerTest(new DateTime(2020, 2, 1), new DateTime(2020, 3, 15), 43d / 366);
            InnerTest(new DateTime(2020, 2, 2, 15, 0, 0), new DateTime(2020, 3, 2, 12, 0, 0), (29d * 24 - 3d) / (366 * 24));
            InnerTest(new DateTime(2020, 2, 2, 15, 0, 0), new DateTime(2020, 3, 2, 18, 0, 0), (29d * 24 + 3d) / (366 * 24));

            InnerTest(new DateTime(2020, 3, 1), new DateTime(2020, 2, 1), -29d / 366);
            InnerTest(new DateTime(2020, 3, 15), new DateTime(2020, 2, 1), -43d / 366);
            InnerTest(new DateTime(2020, 3, 2, 12, 0, 0), new DateTime(2020, 2, 2, 15, 0, 0), -(29d * 24 - 3d) / (366 * 24));
            InnerTest(new DateTime(2020, 3, 2, 18, 0, 0), new DateTime(2020, 2, 2, 15, 0, 0), -(29d * 24 + 3d) / (366 * 24));

            // Special Test
            InnerTest(new DateTime(2000, 7, 15), new DateTime(2000, 8, 16), 32d / 365);
            InnerTest(new DateTime(2000, 8, 16), new DateTime(2000, 7, 15), -32d / 366);
        }

        [Fact]
        public void TotalMonthDiffTest()
        {
            static void InnerTest(DateTime start, DateTime end, double diff)
            {
                Assert.Equal(diff, DateTimeEx.TotalMonthDiff(start, end));
                Assert.Equal(end, start.AddTotalMonthDiff(diff));
            }

            InnerTest(new DateTime(2020, 2, 1), new DateTime(2020, 3, 1), 1d);
            InnerTest(new DateTime(2020, 2, 1), new DateTime(2020, 3, 15), 1d + 14d / 31);
            InnerTest(new DateTime(2020, 2, 2, 15, 0, 0), new DateTime(2020, 3, 2, 12, 0, 0), 1d - 3d / (29 * 24));
            InnerTest(new DateTime(2020, 2, 2, 15, 0, 0), new DateTime(2020, 3, 2, 18, 0, 0), 1d + 3d / (31 * 24));

            InnerTest(new DateTime(2020, 3, 1), new DateTime(2020, 2, 1), -1d);
            InnerTest(new DateTime(2020, 3, 15), new DateTime(2020, 2, 1), -(1d + 14d / 31));
            InnerTest(new DateTime(2020, 3, 2, 12, 0, 0), new DateTime(2020, 2, 2, 15, 0, 0), -(1d - 3d / (29 * 24)));
            InnerTest(new DateTime(2020, 3, 2, 18, 0, 0), new DateTime(2020, 2, 2, 15, 0, 0), -(1d + 3d / (31 * 24)));

            // Special Test
            InnerTest(new DateTime(2000, 7, 15), new DateTime(2000, 8, 16), 1d + 1d / 31);
            InnerTest(new DateTime(2000, 8, 16), new DateTime(2000, 7, 15), -(1d + 1d / 30));
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
