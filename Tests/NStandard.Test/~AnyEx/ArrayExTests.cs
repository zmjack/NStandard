using System;
using Xunit;

namespace NStandard.Test
{
    public class ArrayExTests
    {
        [Fact]
        public void AssignTest1()
        {
            var d2 = new int[3, 3]
            {
                { 0, 1, 2 },
                { 3, 4, 5 },
                { 6, 7, 8 }
            };
            var values = new[] { 10, 11, 12 };
            ArrayEx.Assign(d2, values);

            Assert.Equal(new int[3, 3]
            {
                { 10, 11, 12 },
                { 3, 4, 5 },
                { 6, 7, 8 }
            }, d2);
        }

        [Fact]
        public void AssignTest2()
        {
            var d2 = new int[3, 3]
            {
                { 0, 1, 2 },
                { 3, 4, 5 },
                { 6, 7, 8 }
            };
            var values = new[] { 13, 14, 15 };
            ArrayEx.Assign(d2, 3, values, 0, values.Length);

            Assert.Equal(new int[3, 3]
            {
                { 0, 1, 2 },
                { 13, 14, 15 },
                { 6, 7, 8 }
            }, d2);
        }

        [Fact]
        public unsafe void AssignUnmanagedTest1()
        {
            var d2 = new int[3, 3]
            {
                { 0, 1, 2 },
                { 3, 4, 5 },
                { 6, 7, 8 }
            };
            var values = new[] { 10, 11, 12 };

            fixed (int* pd2 = d2)
            fixed (int* pvalues = values)
            {
                ArrayEx.Assign(pd2, pvalues, values.Length);
            }

            Assert.Equal(new int[3, 3]
            {
                { 10, 11, 12 },
                { 3, 4, 5 },
                { 6, 7, 8 }
            }, d2);
        }

        [Fact]
        public unsafe void AssignUnmanagedTest2()
        {
            var d2 = new int[3, 3]
            {
                { 0, 1, 2 },
                { 3, 4, 5 },
                { 6, 7, 8 }
            };
            var values = new[] { 13, 14, 15 };

            fixed (int* pd2 = d2)
            fixed (int* pvalues = values)
            {
                ArrayEx.Assign(pd2, 3, pvalues, 0, values.Length);
            }

            Assert.Equal(new int[3, 3]
            {
                { 0, 1, 2 },
                { 13, 14, 15 },
                { 6, 7, 8 }
            }, d2);
        }

        [Fact]
        public void AssignInsufficientElementsTest()
        {
            var d2 = new int[3, 3]
            {
                { 0, 1, 2 },
                { 3, 4, 5 },
                { 6, 7, 8 }
            };
            Assert.Throws<ArgumentException>(() => ArrayEx.Assign(d2, new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }));
        }

        [Fact]
        public void AssignOverflowTest()
        {
            var d2 = new int[3, 3]
            {
                { 0, 1, 2 },
                { 3, 4, 5 },
                { 6, 7, 8 }
            };
            Assert.Throws<ArgumentException>(() => ArrayEx.Assign(d2, 6, new[] { 11, 22, 33 }, 1, 3));
        }

    }
}
