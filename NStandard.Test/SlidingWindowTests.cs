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
            var result = SlidingWindow.Slide(numbers, 2).Max(w => w.Sum());
            Assert.Equal(700, result);
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

                Assert.Equal(0 + 11 * i, window[-1]);
                Assert.Equal(numbers[i], window[0]);

                list1.Add(window.ToArray());
                list2.Add(window[-1..1]);
            }

            var excepted = new[]
            {
                new [] { default, 11 },
                new [] { 11, 22 },
                new [] { 22, 33 },
            };

            Assert.Equal(excepted, list1.ToArray());
            Assert.Equal(excepted, list2.ToArray());
        }

    }
}
