using System.Linq;
using Xunit;

namespace NStandard.Test
{
    public class XIEnumerableTests
    {
        [Fact]
        public void GetWindowsTest1()
        {
            var numbers = new[] { 100, 200, 300, 400 };
            var result2 = numbers.GetSlidingWindows(2).Max(x => x.Values.Sum());
            Assert.Equal(700, result2);
        }

    }
}
