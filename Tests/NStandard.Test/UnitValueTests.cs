using NStandard.Dev;
using Xunit;

namespace NStandard.Test
{
    public class UnitValueTests
    {
        [Fact]
        public void Test1()
        {
            return;

            var a = UnitValue.Create(4, "m");
            var b = UnitValue.Create(8, "m");
            var time = UnitValue.Create(2, "s");

            Assert.Equal("12 m", (a + b).ToString());
            Assert.Equal("32 m * m", (a * b).ToString());
            Assert.Equal("4 m / s", (b / time).ToString());
        }

    }
}
