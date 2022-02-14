using Xunit;

namespace NStandard.Test
{
    public class RefTests
    {
        [Fact]
        public void NewTest()
        {
            var refs = new[] { Ref.New<int>(), Ref.New<int>() };
            refs[0].Struct = 8;
            refs[1].Struct = 8;
            Assert.Equal(refs[0], refs[1]);
            Assert.NotSame(refs[0], refs[1]);
            Assert.Equal(refs[0].Struct, refs[1].Struct);
        }

        [Fact]
        public void CloneTest()
        {
            int eight = 8;
            var refs = new[] { Ref.Clone(eight), Ref.Clone(eight) };
            Assert.Equal(refs[0], refs[1]);
            Assert.NotSame(refs[0], refs[1]);
            Assert.Equal(refs[0].Struct, refs[1].Struct);
        }

        [Fact]
        public void ConvertTest()
        {
            var refs = new Ref<int>[] { 8, 8 };
            Assert.Equal(refs[0], refs[1]);
            Assert.NotSame(refs[0], refs[1]);
            Assert.Equal(refs[0].Struct, refs[1].Struct);
        }

        [Fact]
        public void SameTest()
        {
            var ref1 = Ref.Clone(8);
            var ref2 = ref1;
            Assert.Equal(ref1, ref2);
            Assert.Same(ref1, ref2);
            Assert.Equal(ref1.Struct, ref2.Struct);
        }

    }
}
