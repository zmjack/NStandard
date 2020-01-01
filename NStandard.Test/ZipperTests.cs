using System;
using System.Linq;
using Xunit;

namespace NStandard.Test
{
    public class ZipperTests
    {
        public void ZipperTest()
        {
            var starts = new[] { new DateTime(2018, 6, 15), new DateTime(2018, 12, 31), new DateTime(2019, 1, 1) };
            var ends = new[] { new DateTime(2018, 7, 15), new DateTime(2019, 1, 1) };

            Assert.Equal(31, Zipper.Create(starts, ends).Sum(x => (x.Item2 - x.Item1).TotalDays));
            Assert.Equal(31, Zipper.Create(starts, ends, (a, b) => b - a).Sum(x => x.TotalDays));
        }
    }
}
