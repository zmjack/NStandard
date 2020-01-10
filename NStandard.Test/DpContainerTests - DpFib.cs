using Xunit;

namespace NStandard.Test
{
    public partial class DpContainerTests
    {
        public class DpFib : DpContainer<int, int>
        {
            public override int StateTransfer(int n)
            {
                if (n == 0 || n == 1) return 1;
                return this[n - 1] + this[n - 2];
            }
        }

        [Fact]
        public void DpFibTest()
        {
            Assert.Equal(1836311903, new DpFib()[45]);
        }

    }
}
