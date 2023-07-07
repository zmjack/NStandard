using Xunit;

namespace NStandard.Test
{
    public class VariantTests
    {
        [Fact]
        public void Test1()
        {
            Assert.True(new Variant("true"));
            Assert.False(new Variant("false"));
        }

    }
}
