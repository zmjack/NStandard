using System.Linq;
using System;
using Xunit;

namespace NStandard.Test
{
    public class AnyTests
    {
        [Fact]
        public void StrcutCastTest()
        {
            // Hex: 0x3c75c28f
            // Dec: 1014350479
            // Bin: 00111100 01110101 11000010 10001111
            Assert.Equal(0x3c75c28f, Any.Struct.Cast<int>(0.015F));
            Assert.Equal(0.015F, Any.Struct.Cast<float>(1014350479));
        }

        [Fact]
        public void ZipTest()
        {
            var starts = new[] { new DateTime(2018, 7, 15), new DateTime(2018, 8, 15), new DateTime(2019, 1, 1) };
            var ends = new[] { new DateTime(2018, 8, 15), new DateTime(2018, 9, 15) };
            var zip = Any.Zip(starts, ends);

            foreach (var (start, end) in zip)
            {
                Assert.Equal(31, (end - start).TotalDays);
            }

            Assert.Equal(62, zip.Sum(x => (x.Item2 - x.Item1).TotalDays));
            Assert.Equal(62, Any.Zip(starts, ends, (a, b) => b - a).Sum(x => x.TotalDays));
        }
    }
}
