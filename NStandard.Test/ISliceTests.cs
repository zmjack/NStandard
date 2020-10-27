using System;
using Xunit;

namespace NStandard.Test
{
    public class ISliceTests
    {
        public class SliceCount : ISliceCount<int[]>
        {
            public int[] Numbers = { 0, 1, 2, 3 };

            public int Count => Numbers.Length;

            public int[] Slice(int start, int length)
            {
                var slice = new int[length];
                Array.Copy(Numbers, start, slice, 0, length);
                return slice;
            }
        }

        public class SliceLength : ISliceLength<int[]>
        {
            public int[] Numbers = { 0, 1, 2, 3 };
            public int Length => Numbers.Length;

            public int[] Slice(int start, int length)
            {
                var slice = new int[length];
                Array.Copy(Numbers, start, slice, 0, length);
                return slice;
            }
        }

        [Fact]
        public void SliceCountTest()
        {
            var slice = new SliceCount();
            var result = slice[1..^1];
            Assert.Equal(new[] { 1, 2 }, result);
        }

        [Fact]
        public void SliceLengthTest()
        {
            var slice = new SliceLength();
            var result = slice[1..^1];
            Assert.Equal(new[] { 1, 2 }, result);
        }

    }
}
