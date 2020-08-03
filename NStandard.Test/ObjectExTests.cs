using Xunit;

namespace NStandard.Test
{
    public class ObjectExTests
    {
        private class Cls
        {
            public int A { get; set; }
            public int B { get; set; }
        }

        [Fact]
        public void Test1()
        {
            var a = new Cls { A = 123, B = 234 };

            ObjectEx.AcceptPropValues(a, new { A = 111 });
            Assert.Equal(111, a.A);
            Assert.Equal(234, a.B);
        }

    }
}
