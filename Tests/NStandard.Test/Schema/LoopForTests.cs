using System.Linq;
using Xunit;

namespace NStandard.Schema.Test
{
    public class LoopForTests
    {
        [Fact]
        public void Test1()
        {
            var lfor = new LoopFor<int>(new[] { 1, 3, 5 });
            var array = lfor.ToArray();
            Assert.Equal(new[] { 1, 3, 5 }, array);
        }

        [Fact]
        public void Test2()
        {
            var lfor = new LoopFor<int>(() => 1, i => i < 7, i => i += 2);
            var array = lfor.ToArray();
            Assert.Equal(new[] { 1, 3, 5 }, array);
        }
    }
}
