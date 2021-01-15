using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;

namespace NStandard.Evaluators.Test
{
    public class CustomEvaluatorTests
    {
        public class MyEvaluator : NumericalEvaluator
        {
            private static readonly MethodInfo MathPowMethod = typeof(Math).GetMethod("Pow");
            private static readonly MethodInfo MathFloorMethod = typeof(Math).GetMethod("Floor", new[] { typeof(double) });

            protected override Dictionary<string, int> OpLevels { get; } = new Dictionary<string, int>
            {
                ["**"] = 2,
                ["//"] = 2,
                ["*"] = 3,
                ["/"] = 3,
                ["%"] = 3,
                ["+"] = 4,
                ["-"] = 4,
            };

            protected override Dictionary<string, BinaryOpFunc<Expression>> OpFunctions { get; } = new Dictionary<string, BinaryOpFunc<Expression>>
            {
                ["**"] = (left, right) => Expression.Call(MathPowMethod, left, right),
                ["//"] = (left, right) => Expression.Call(MathFloorMethod, Expression.Divide(left, right)),
                ["*"] = (left, right) => Expression.MultiplyChecked(left, right),
                ["/"] = (left, right) => Expression.Divide(left, right),
                ["%"] = (left, right) => Expression.Modulo(left, right),
                ["+"] = (left, right) => Expression.AddChecked(left, right),
                ["-"] = (left, right) => Expression.SubtractChecked(left, right),
            };
        }

        private readonly MyEvaluator MyEvaluatorInstance = new MyEvaluator();

        [Fact]
        public void NormalTest1()
        {
            var expected = (8 - 5 % 7) + Math.Pow(3, 2) + 8;
            Assert.Equal(expected, MyEvaluatorInstance.Eval("(8 - 5 % 7) + 3 ** 2 + 8"));
        }

        [Fact]
        public void NormalTest2()
        {
            var expected = Math.Floor(-9d / 2);
            Assert.Equal(expected, MyEvaluatorInstance.Eval("-9 // 2"));
        }

        [Fact]
        public void NormalTest3()
        {
            var evaluator = new MyEvaluator();
            Assert.Throws<ArgumentException>(() => MyEvaluatorInstance.Eval("(8 mod 5 % 7) + 3 ** 2 + 8"));
        }

    }
}
