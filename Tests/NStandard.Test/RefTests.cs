using Xunit;

namespace NStandard.Test
{
    public class RefTests
    {
        [Fact]
        public void NewTest()
        {
            var refs = new[] { Ref.New<int>(), Ref.New<int>() };
            refs[0].Ref = 8;
            refs[1].Ref = 8;
            Assert.Equal(refs[0], refs[1]);
            Assert.NotSame(refs[0], refs[1]);
            Assert.Equal(refs[0].Ref, refs[1].Ref);
        }

        [Fact]
        public void CloneTest()
        {
            int eight = 8;
            var refs = new[] { Ref.Clone(eight), Ref.Clone(eight) };
            Assert.Equal(refs[0], refs[1]);
            Assert.NotSame(refs[0], refs[1]);
            Assert.Equal(refs[0].Ref, refs[1].Ref);
        }

        [Fact]
        public void ConvertTest()
        {
            var refs = new StructRef<int>[] { 8, 8 };
            Assert.Equal(refs[0], refs[1]);
            Assert.NotSame(refs[0], refs[1]);
            Assert.Equal(refs[0].Ref, refs[1].Ref);
        }

        [Fact]
        public void SameTest()
        {
            var ref1 = Ref.Clone(8);
            var ref2 = ref1;
            Assert.Equal(ref1, ref2);
            Assert.Same(ref1, ref2);
            Assert.Equal(ref1.Ref, ref2.Ref);
        }

    }
}
