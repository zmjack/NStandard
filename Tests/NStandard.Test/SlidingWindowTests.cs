using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NStandard.Test
{
    public class SlidingWindowTests
    {
        [Fact]
        public void Test1()
        {
            var numbers = new[] { 100, 200, 300, 400 };
            var result = SlidingWindow.Slide(numbers, 3).Select(x => x.ToArray());
            Assert.Equal(new[]
            {
                new[] { 100, 200, 300 },
                new[] { 200, 300, 400 },
            }, result);
        }

        [Fact]
        public void Test2()
        {
            var window = new SlidingWindow<int>(2);
            var list1 = new List<int[]>();
            var list2 = new List<int[]>();

            var numbers = new[] { 11, 22, 33 };
            for (int i = 0; i < 3; i++)
            {
                window.Fill(numbers[i]);

                Assert.Equal(0 + 11 * i, window[0]);
                Assert.Equal(11 + 11 * i, window[1]);

                list1.Add(window.ToArray());
                list2.Add(window[0..2]);
            }

            var excepted = new[]
            {
                new [] { default, 11 },
                new [] { 11, 22  },
                new [] { 22, 33 },
            };

            Assert.Equal(excepted, list1.ToArray());
            Assert.Equal(excepted, list2.ToArray());
        }

        [Fact]
        public void Test3()
        {
            var numbers = new[] { 100, 200, 300, 400 };
            var result = SlidingWindow.Slide(numbers, -1, 1).Select(x => x[0]);
            Assert.Equal(new[] { 200, 300 }, result);
        }

    }
}
