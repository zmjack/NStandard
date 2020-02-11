using System;
using Xunit;

namespace NStandard.Test
{
    public class EnumOptionTests
    {
        private enum ETest { Default = 16 }

        [Fact]
        public void Test1()
        {
            var a0 = new EnumOption(typeof(ETest), nameof(ETest.Default));
            var a1 = new EnumOption(typeof(ETest), nameof(ETest.Default));
            Assert.Equal(a0, a1);
        }

        [Fact]
        public void Test2()
        {
            var a0 = new EnumOption(typeof(ETest), nameof(ETest.Default));
            var b0 = new EnumOption<ETest, int>(nameof(ETest.Default));
            var b1 = new EnumOption<ETest, int>(16);
            Assert.Equal(a0, b0);
            Assert.Equal(b0, a0);
            Assert.Equal(b0, b1);
            Assert.Throws<InvalidOperationException>(() => new EnumOption<ETest, byte>(16));
        }

    }
}
