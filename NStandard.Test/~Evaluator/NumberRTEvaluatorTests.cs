using System;
using System.Collections.Generic;
using Xunit;

namespace NStandard.Test
{
    public class NumberRTEvaluatorTests
    {
        public class MyEvaluator : NumericalRTEvaluator
        {
            protected override Dictionary<string, int> OpLevels { get; } = new Dictionary<string, int>
            {
                ["*"] = 3,
                ["/"] = 3,
                ["%"] = 3,
                ["+"] = 4,
                ["-"] = 4,
                ["**"] = 2,
                ["//"] = 2,
            };

            protected override Dictionary<string, BinaryOpFunc<double>> OpFunctions { get; } = new Dictionary<string, BinaryOpFunc<double>>
            {
                ["*"] = (left, right) => left * right,
                ["/"] = (left, right) => left / right,
                ["%"] = (left, right) => left % right,
                ["+"] = (left, right) => left + right,
                ["-"] = (left, right) => left - right,
                ["**"] = (left, right) => Math.Pow(left, right),
                ["//"] = (left, right) => Math.Floor(left / right),
            };
        }

        [Fact]
        public void NormalTest1()
        {
            var evaluator = new MyEvaluator();
            Assert.Equal(20, evaluator.Eval("(8 - 5 % 7) + 3 ** 2 + 8"));
            Assert.Equal(-5, evaluator.Eval("-9 // 2"));
            Assert.Throws<ArgumentException>(() => evaluator.Eval("(8 mod 5 % 7) + 3 ** 2 + 8"));
        }

    }
}
