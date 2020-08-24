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
        public void DpFibTest1()
        {
            var fib = new DpFib();
            Assert.Equal(1836311903, fib[45]);
        }

        public int BadFib(int n)
        {
            if (n == 0 || n == 1) return 1;
            return BadFib(n - 1) + BadFib(n - 2);
        }

        public int Fib(DefaultDpContainer<int, int> dp, int n)
        {
            if (n == 0 || n == 1) return 1;
            return dp[n - 1] + dp[n - 2];
        }

        [Fact]
        public void DpFibTest2()
        {
            var fib = DpContainer.Create<int, int>(Fib);
            Assert.Equal(1836311903, fib[45]);
        }

    }
}
