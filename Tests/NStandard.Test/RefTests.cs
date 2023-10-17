using Xunit;

namespace NStandard.Test
{
    public class RefTests
    {
        [Fact]
        public void StructTest()
        {
            var refs = new Ref<int>[] { 8, 8 };

            Assert.NotEqual(refs[0], refs[1]);
            Assert.NotSame(refs[0], refs[1]);

            Assert.Equal(refs[0].Any, refs[1].Any);
            Assert.NotSame(refs[0].Any, refs[1].Any);
        }

        [Fact]
        public void ClassTest()
        {
            var str = "str";
            var refs = new Ref<string>[] { str, str };

            Assert.NotEqual(refs[0], refs[1]);
            Assert.NotSame(refs[0], refs[1]);

            Assert.Equal(refs[0].Any, refs[1].Any);
            Assert.Same(refs[0].Any, refs[1].Any);
        }

    }
}
