using System;
using System.Linq;
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

        [Fact]
        public void FlatTest()
        {
            var d2 = new string[2, 2] { { "0", "1" }, { "2", "3" } };
            Assert.Equal(new[] { "0", "1", "2", "3" }, Any.Flat<string>(d2));
        }

        [Fact]
        public void FlatManyTest()
        {
            var d2 = new string[2, 2] { { "0", "1" }, { "2", "3" } };
            Assert.Equal(new[] { "0", "1", "2", "3", "0", "1", "2", "3" }, Any.Flat<string>(d2, d2));
        }

        [Fact]
        public void FlatDeepTest()
        {
            var array = new object[]
            {
                new[] { "0", "1" },
                new object[]
                {
                    "2",
                    new object[]
                    {
                        "3",
                        new string[]
                        {
                            "4", "5"
                        }
                    }
                }
            };
            Assert.Equal(new[] { "0", "1", "2", "3", "4", "5" }, Any.Flat<string>(array));
        }

        [Fact]
        public unsafe void FlatUnmanagedTest()
        {
            var d2 = new int[2, 2] { { 0, 1 }, { 2, 3 } };
            var slength = d2.GetSequenceLength();

            fixed (int* pd2 = d2)
            {
                Assert.Equal(new[] { 0, 1, 2, 3 }, Any.Flat(pd2, slength));
            }
        }

        [Fact]
        public unsafe void FlatUnmanagedManyTest()
        {
            var d2 = new int[2, 2] { { 0, 1 }, { 2, 3 } };
            var slength = d2.GetSequenceLength();

            fixed (int* pd2 = d2)
            {
                Assert.Equal(new[] { 0, 1, 2, 3, 0, 1, 2, 3 }, Any.Flat(new[] { pd2, pd2 }, new[] { slength, slength }));
            }
        }

        [Fact]
        public void ReDimReduceTest()
        {
            var d2 = new int[3, 3]
            {
                { 0, 1, 2 },
                { 3, 4, 5 },
                { 6, 7, 8 }
            };
            Any.ReDim(ref d2, 2, 2);

            Assert.Equal(new int[2, 2]
            {
                { 0, 1 },
                { 3, 4 },
            }, d2);
        }

        [Fact]
        public void ReDimExpandTest()
        {
            var d2 = new int[3, 3]
            {
                { 0, 1, 2 },
                { 3, 4, 5 },
                { 6, 7, 8 }
            };
            Any.ReDim(ref d2, 4, 4);

            Assert.Equal(new int[4, 4]
            {
                { 0, 1, 2, 0 },
                { 3, 4, 5, 0 },
                { 6, 7, 8, 0 },
                { 0, 0, 0, 0 },
            }, d2);
        }
    }
}
