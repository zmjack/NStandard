using Xunit;

namespace NStandard.Test
{
    public class ValTests
    {
        [Fact]
        public void StructTest()
        {
            var vals = new Val<int>[] { 8, 8 };

            Assert.Equal(vals[0], vals[1]);
            Assert.NotSame(vals[0], vals[1]);

            Assert.Equal(vals[0].Any, vals[1].Any);
            Assert.NotSame(vals[0].Any, vals[1].Any);
        }

        [Fact]
        public void ClassTest()
        {
            var str = "str";
            var vals = new Val<string>[] { str, str };

            Assert.Equal(vals[0], vals[1]);
            Assert.NotSame(vals[0], vals[1]);

            Assert.Equal(vals[0].Any, vals[1].Any);
            Assert.Same(vals[0].Any, vals[1].Any);
        }

    }
}
