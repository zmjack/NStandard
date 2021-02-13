using System.Linq;
using Xunit;

namespace NStandard.Schema.Test
{
    public class LoopTests
    {
        [Fact]
        public void Test1()
        {
            var iteratorValues = Loop.Create(2, 3).ToArray();
            Assert.Equal(new[]
            {
                new int?[] { 0, 0 }, new int?[] { 0, 1 }, new int?[] { 0, 2 },
                new int?[] { 1, 0 }, new int?[] { 1, 1 }, new int?[] { 1, 2 },
            }, iteratorValues);
        }

        [Fact]
        public void Test2()
        {
            var iteratorValues = Loop.Create(new[] { 1, 2 }, new[] { 3, 4 }).ToArray();
            Assert.Equal(new[]
            {
                new int?[] { 1, 3 }, new int?[] { 1, 4 },
                new int?[] { 2, 3 }, new int?[] { 2, 4 },
            }, iteratorValues);
        }

    }
}
