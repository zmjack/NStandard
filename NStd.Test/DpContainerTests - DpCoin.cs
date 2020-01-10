using NLinq;
using System.Linq;
using Xunit;

namespace NStd.Test
{
    public partial class DpContainerTests
    {
        public class DpCoin : DpContainer<int, DpCoin.Result>
        {
            public class Result
            {
                public int CoinCount;
                public int[] Coins;
            }

            public int[] CoinValues { get; private set; }

            public DpCoin(int[] coinValues)
            {
                CoinValues = coinValues;
            }

            public override Result StateTransfer(int n)
            {
                // dp(n) = 0                        if  n=0
                // dp(n) = min{v| d(n-v) + 1}       if  n-v>=0, v={CoinValues}

                if (n == 0) return new Result { CoinCount = 0, Coins = new int[0] };

                var take_v = CoinValues
                    .Where(v => n - v >= 0)
                    .WhereMin(v => this[n - v].CoinCount)
                    .First();
                var preResult = this[n - take_v];

                return new Result
                {
                    CoinCount = preResult.CoinCount + 1,
                    Coins = preResult.Coins.Concat(new[] { take_v }).ToArray(),
                };
            }
        }

        [Fact]
        public void DpCoinTest()
        {
            var dpCoin = new DpCoin(new[] { 1, 2, 4, 5 });
            var result8 = dpCoin[8];

            Assert.Equal(2, result8.CoinCount);
            Assert.Equal(new[] { 4, 4 }, result8.Coins);
        }

    }
}
