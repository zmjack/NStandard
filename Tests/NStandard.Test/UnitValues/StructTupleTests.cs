using Xunit;

namespace NStandard.Test
{
    public class StructTupleTests
    {
        [Fact]
        public void StructTupleTest()
        {
            var tuple = StructTuple.Create("1", "2");
            (string a, string b) = tuple;

            Assert.Equal(a, tuple.Item1);
            Assert.Equal(b, tuple.Item2);
        }
    }
}
