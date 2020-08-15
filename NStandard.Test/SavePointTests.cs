using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NStandard.Test
{
    public class SavePointTests
    {
        [Fact]
        public void Test1()
        {
            var point = new SavePoint<int>(2);

            var list1 = new List<int[]>();
            var list2 = new List<int[]>();

            var numbers = new[] { 11, 22, 33 };
            for (int i = 0; i < 3; i++)
            {
                point.Save(numbers[i]);

                Assert.Equal(0 + 11 * i, point[-1]);
                Assert.Equal(numbers[i], point[0]);

                list1.Add(point.ToArray());
                list2.Add(point[-1..1]);
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
