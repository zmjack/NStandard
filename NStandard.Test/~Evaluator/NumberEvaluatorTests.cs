using System;
using Xunit;

namespace NStandard.Test
{
    public class NumberEvaluatorTests
    {
        [Fact]
        public void RT_Test1()
        {
            double excepted = 6280752;
            var exp = "(6280752)";
            var actual = Evaluator.NumericalRT.Eval(exp);
            Assert.Equal(excepted, actual);
        }

        [Fact]
        public void RT_SimpleTest1()
        {
            double excepted = 3 + 5 * 8.1 - 6;
            var exp = "3 + 5 * 8.1 - 6";
            var actual = Evaluator.NumericalRT.Eval(exp);
            Assert.Equal(excepted, actual);
        }

        [Fact]
        public void RT_SimpleTest2()
        {
            double excepted = 3 + 5 * 8.1 - 6;
            double[] operands = { 3, 5, 8.1, 6 };
            string[] ops = { "+", "*", "-" };
            var actual = Evaluator.NumericalRT.Eval(operands, ops);
            Assert.Equal(excepted, actual);
        }

        [Fact]
        public void RT_BracketTest1()
        {
            double excepted = 1 + (2 * 3 - 4 * (5 + 6)) + 7;
            double[] operands = { 1, default, 2, 3, 4, default, 5, 6, default, default, 7 };
            string[] ops = { "+", "(", "*", "-", "*", "(", "+", ")", ")", "+" };
            var actual = Evaluator.NumericalRT.Eval(operands, ops);
            Assert.Equal(excepted, actual);
        }

        [Fact]
        public void RT_BracketTest2()
        {
            double excepted = 1 + (2 * 3 - 4 * (5 + 6)) + 7;
            var exp = "1 + (2 * 3 - 4 * (5 + 6)) + 7";
            var actual = Evaluator.NumericalRT.Eval(exp);
            Assert.Equal(excepted, actual);
        }

        [Fact]
        public void RT_PerfermaceTest()
        {
            double excepted = 1 + (2 * 3 - 4 * (5 + 6)) + 7;
            var exp = "1 + (2 * 3 - 4 * (5 + 6)) + 7";
            for (int i = 0; i < 1000; i++)
            {
                var actual = Evaluator.NumericalRT.Eval(exp);
                Assert.Equal(excepted, actual);
            }
        }

        [Fact]
        public void CP_ParameterTest()
        {
            var exp = "1 + (2 * $a - 4 * ($b + 6)) + 7";
            var tree = Evaluator.Numerical.Build(exp, out _);

            Assert.Equal("((1 + ((2 * a) - (4 * (b + 6)))) + 7)", tree.ToString());

            var del = Evaluator.Numerical.Compile<Func<double, double, double>>(exp);
            Assert.Equal(1 + (2 * 3 - 4 * (5 + 6)) + 7, del(3, 5));
            Assert.Equal(1 + (2 * 4 - 4 * (6 + 6)) + 7, del(4, 6));
        }

        [Fact]
        public void CP_PerfermaceTest1_StaticInvoke()
        {
            double excepted = 1 + (2 * 3 - 4 * (5 + 6)) + 7;
            var exp = "1 + (2 * 3 - 4 * (5 + 6)) + 7";
            var del = Evaluator.Numerical.Compile<Func<double>>(exp);
            for (int i = 0; i < 1000; i++)
            {
                var actual = del();
                Assert.Equal(excepted, actual);
            }
        }

        [Fact]
        public void CP_PerfermaceTest2_DynamicInvoke()
        {
            double excepted = 1 + (2 * 3 - 4 * (5 + 6)) + 7;
            var exp = "1 + (2 * 3 - 4 * (5 + 6)) + 7";
            var del = Evaluator.Numerical.Compile(exp);
            for (int i = 0; i < 1000; i++)
            {
                var actual = del.DynamicInvoke();
                Assert.Equal(excepted, actual);
            }
        }

    }
}
