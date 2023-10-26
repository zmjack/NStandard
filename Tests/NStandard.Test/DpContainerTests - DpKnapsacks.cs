using LinqSharp;
using System;
using System.Linq;
using Xunit;

namespace NStandard.Test;

public partial class DpContainerTests
{
    public class DpKnapsack : DpContainer<(int toGoods, int residualWeight), DpKnapsack.Result>
    {
        public class Result
        {
            public int TotalValue;
            public int[] GoodWeights;
        }

        public (int Weight, int Value)[] Goods { get; private set; }

        public DpKnapsack((int Weight, int Value)[] goods)
        {
            Goods = goods;
        }

        public Result this[int residualWeight]
            => this[(Goods.Length - 1, residualWeight)];

        public override Result StateTransfer((int toGoods, int residualWeight) param)
        {
            // define: i as toGoods, j as knapsackWeight
            // dp(i, j) = 0                                         if  i=0, j<w[0]
            // dp(i, j) = v[0]                                      if  i=0, j>=w[0]
            // dp(i, j) = dp(i-1, j)                                if  i>0, j-w[i]<0
            // dp(i, j) = max(dp(i-1, j), dp(i-1, j-w[i]) + v[i])   if  i>0, j-w[i]>=0

            int i = param.toGoods, j = param.residualWeight;

            if (i == 0 && j < Goods[0].Weight)
                return new Result { TotalValue = 0, GoodWeights = Array.Empty<int>() };
            if (i == 0 && j >= Goods[0].Weight)
                return new Result { TotalValue = Goods[0].Value, GoodWeights = new[] { Goods[0].Weight } };
            if (i > 0 && j - Goods[i].Weight < 0) return this[(i - 1, j)];

            var (scheme, totalValue) = new[]
            {
                (Scheme: 0,  TotalValue: this[(i - 1, j)].TotalValue),
                (Scheme: 1, TotalValue: this[(i - 1, j - Goods[i].Weight)].TotalValue + Goods[i].Value),
            }.WhereMax(x => x.TotalValue).First();

            if (scheme == 1)
            {
                return new Result
                {
                    TotalValue = totalValue,
                    GoodWeights = this[(i - 1, j - Goods[i].Weight)].GoodWeights
                        .Concat(new[] { Goods[i].Weight }).ToArray(),
                };
            }
            else return this[(i - 1, j)];
        }
    }

    [Fact]
    public void DpKnapsackTest()
    {
        var dpKnapsack = new DpKnapsack(new[] { (10, 60), (20, 100), (30, 120) });
        var result30 = dpKnapsack[30];
        var result50 = dpKnapsack[50];

        Assert.Equal(160, result30.TotalValue);
        Assert.Equal(new[] { 10, 20 }, result30.GoodWeights);

        Assert.Equal(220, result50.TotalValue);
        Assert.Equal(new[] { 20, 30 }, result50.GoodWeights);
    }

}
