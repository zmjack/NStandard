using Xunit;

namespace NStandard.Collections.Test
{
    public class CounterTests
    {
        [Fact]
        public void ParseTest()
        {
            var counter = Counter.Parse("aba");
            Assert.Equal(2, counter['a']);
            Assert.Equal(1, counter['b']);
        }

        [Fact]
        public void ElementTest()
        {
            var counter = Counter.Parse("aba");
            Assert.Equal(new[] { 'a', 'a', 'b' }, counter.Elements());
        }

        [Fact]
        public void AddingTest1()
        {
            var counter = Counter.Parse("aba") + Counter.Parse("babc");
            Assert.Equal(3, counter['a']);
            Assert.Equal(3, counter['b']);
            Assert.Equal(1, counter['c']);
        }

        [Fact]
        public void AddingTest2()
        {
            var counter = Counter.Parse("babc") + Counter.Parse("aba");
            Assert.Equal(3, counter['a']);
            Assert.Equal(3, counter['b']);
            Assert.Equal(1, counter['c']);
        }

        [Fact]
        public void SubtractTest1()
        {
            var counter = Counter.Parse("aba") - Counter.Parse("babc");
            Assert.Equal(1, counter['a']);
            Assert.Equal(-1, counter['b']);
            Assert.Equal(-1, counter['c']);
        }

        [Fact]
        public void SubtractTest2()
        {
            var counter = Counter.Parse("babc") - Counter.Parse("aba");
            Assert.Equal(-1, counter['a']);
            Assert.Equal(1, counter['b']);
            Assert.Equal(1, counter['c']);
        }

    }
}
