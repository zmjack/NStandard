using Xunit;

namespace NStandard.Algorithms.Test
{
    public class KmpTest
    {
        [Fact]
        public void Test1()
        {
            var kmp = new Kmp("ABCDABD");
            Assert.Equal(1, kmp.Count("BBC ABCDAB ABCDABCDABDE"));

            var kmp2 = new Kmp("AA");
            Assert.Equal(3, kmp2.Count("AAAA", true));
            Assert.Equal(2, kmp2.Count("AAAA", false));

            var kmp3 = new Kmp("A");
            Assert.Equal(1, kmp3.Count("A"));
        }
    }
}
