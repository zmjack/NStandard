using Xunit;

namespace NStandard.Test
{
    public class VariantStringTests
    {
        [Fact]
        public void Test1()
        {
            Assert.True(new VariantString("true"));
            Assert.False(new VariantString("false"));
        }

    }
}
