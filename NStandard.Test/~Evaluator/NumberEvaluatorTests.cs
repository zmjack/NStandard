using System;
using Xunit;

namespace NStandard.Test
{
    public class NumberEvaluatorTests
    {
        [Fact]
        public void NormalTest1()
        {
            double excepted = 20120416;
            var exp = "(20120416)";
            var actual = Evaluator.Numerical.Eval(exp);
            Assert.Equal(excepted, actual);
        }

        [Fact]
        public void NormalTest2()
        {
            double excepted = 1 + (2 * 3 - 4 * (5 + 6)) + 7;
            var exp = "1 + (2 * 3 - 4 * (5 + 6)) + 7";
            var actual = Evaluator.Numerical.Eval(exp);
            Assert.Equal(excepted, actual);
        }

        [Fact]
        public void NormalTest3()
        {
            double excepted = 3 + 5 * 8.1 - 6;
            var exp = "3 + 5 * 8.1 - 6";
            var actual = Evaluator.Numerical.Eval(exp);
            Assert.Equal(excepted, actual);
        }

        [Fact]
        public void NormalParameterTest()
        {
            var exp = "1 + (2 * $a - 4 * ($b + 6)) + 7";
            var tree = Evaluator.Numerical.Build(exp, out _);

            Assert.Equal("((1 + ((2 * a) - (4 * (b + 6)))) + 7)", tree.ToString());

            var del = Evaluator.Numerical.Compile<Func<double, double, double>>(exp);
            Assert.Equal(1 + (2 * 3 - 4 * (5 + 6)) + 7, del(3, 5));
            Assert.Equal(1 + (2 * 4 - 4 * (6 + 6)) + 7, del(4, 6));
        }

        [Fact]
        public void ArgumentNumberErrorTest()
        {
            var exp = "$a + $b";
            Assert.Throws<ArgumentException>(() => Evaluator.Numerical.Eval(exp));
        }

        [Fact]
        public void InvalidExpStringErrorTest()
        {
            var exp = "1 # 2";
            Assert.Throws<ArgumentException>(() => Evaluator.Numerical.Eval(exp));
        }

        [Fact]
        public void UnopenedBracketErrorTest()
        {
            var exp = "1 + 2 + (3 + 4))";
            Assert.Throws<ArgumentException>(() => Evaluator.Numerical.Eval(exp));
        }

        [Fact]
        public void UnclosedBracketErrorTest()
        {
            var exp = "1 + (2 + (3 + 4)";
            Assert.Throws<ArgumentException>(() => Evaluator.Numerical.Eval(exp));
        }

        [Fact]
        public void Eval1000Test()
        {
            double excepted = 1 + (2 * 3 - 4 * (5 + 6)) + 7;
            var exp = "1 + (2 * 3 - 4 * (5 + 6)) + 7";
            for (int i = 0; i < 1000; i++)
            {
                var actual = Evaluator.Numerical.Eval(exp);
                Assert.Equal(excepted, actual);
            }
        }

        [Fact]
        public void Compiled1000Test()
        {
            double excepted = 1 + (2 * 3 - 4 * (5 + 6)) + 7;
            var exp = "1 + (2 * 3 - 4 * (5 + 6)) + 7";
            var func = Evaluator.Numerical.Compile<Func<double>>(exp);
            for (int i = 0; i < 1000; i++)
            {
                var actual = func();
                Assert.Equal(excepted, actual);
            }
        }

    }
}
