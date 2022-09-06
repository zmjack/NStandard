using Xunit;

namespace NStandard.Test
{
    public class AnyTests
    {
        [Fact]
        public void StrcutCastTest()
        {
            // Hex: 0x3c75c28f
            // Dec: 1014350479
            // Bin: 00111100 01110101 11000010 10001111
            Assert.Equal(0x3c75c28f, Any.Struct.Cast<int>(0.015F));
            Assert.Equal(0.015F, Any.Struct.Cast<float>(1014350479));
        }

    }
}
