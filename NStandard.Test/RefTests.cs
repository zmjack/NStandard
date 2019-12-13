using Xunit;

namespace NStandard.Test
{
    public class RefTests
    {
        [Fact]
        public void RRTest()
        {
            Ref<int> ra = Ref.Bind(0);
            Ref<int> rb = Ref.Bind(0);

            Assert.True(ra.Value == rb.Value);
            Assert.False(ra.Value != rb.Value);

            Assert.False(ra.RefValue == rb.RefValue);
            Assert.True(ra.RefValue != rb.RefValue);

            Assert.False(ra == rb);         // Compare as Ref<int>
            Assert.True(ra != rb);          // Compare as Ref<int>

            Assert.True(ra.Equals(rb));
        }

        [Fact]
        public void RITest()
        {
            Ref<int> ri = Ref.Bind(0);
            int i = 0;

            Assert.True(ri.Value == i);
            Assert.True(i == ri.Value);
            Assert.False(ri.Value != i);
            Assert.False(i != ri.Value);

            Assert.False(ri.RefValue == (object)i);
            Assert.False((object)i == ri.RefValue);
            Assert.True(ri.RefValue != (object)i);
            Assert.True((object)i != ri.RefValue);

            Assert.False(ri == i);           // Compare as Ref<int> (left-side)
            Assert.True(ri != i);            // Compare as Ref<int> (left-side)

            Assert.True(i == ri);            // Compare as int
            Assert.False(i != ri);           // Compare as int

            Assert.True(ri.Equals(i));
            Assert.True(i.Equals(ri));
        }

        [Fact]
        public void RDTest()
        {
            Ref<double> rd = Ref.Bind(0d);
            int i = 0;

            Assert.True(rd.Value == i);
            Assert.True(i == rd.Value);
            Assert.False(rd.Value != i);
            Assert.False(i != rd.Value);

            Assert.False(rd.RefValue == (object)i);
            Assert.False((object)i == rd.RefValue);
            Assert.True(rd.RefValue != (object)i);
            Assert.True((object)i != rd.RefValue);

            Assert.False(rd == i);           // Compare as Ref<double> (left-side)
            Assert.True(rd != i);            // Compare as Ref<double> (left-side)

            Assert.True(i == rd);            // Compare as double
            Assert.False(i != rd);           // Compare as double

            Assert.False(rd.Equals(i));
            Assert.False(i.Equals(rd));
        }

    }
}
