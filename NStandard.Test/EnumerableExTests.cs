using System.Linq;
using Xunit;

namespace NStandard.Test
{
    public class EnumerableExTests
    {
        [Fact]
        public void Test1()
        {
            var s = new string(EnumerableEx.Concat("123", "456", "789").ToArray());
            Assert.Equal("123456789", s);
        }

    }
}
