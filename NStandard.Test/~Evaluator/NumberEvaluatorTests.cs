using Xunit;

namespace NStandard.Test
{
    public class NumberEvaluatorTests
    {
        [Fact]
        public void SimpleTest1()
        {
            var excepted = 3 + 5 * 8.1 - 6;
            var exp = "3 + 5 * 8.1 - 6";
            var actual = Evaluator.Numerical.Eval(exp);
            Assert.Equal(excepted, actual);
        }

        [Fact]
        public void SimpleTest2()
        {
            var excepted = 3 + 5 * 8.1 - 6;
            double[] operands = { 3, 5, 8.1, 6 };
            string[] ops = { "+", "*", "-" };
            var actual = Evaluator.Numerical.Eval(operands, ops);
            Assert.Equal(excepted, actual);
        }

        [Fact]
        public void BracketTest1()
        {
            var excepted = 1 + (2 * 3 - 4 * (5 + 6)) + 7;
            double[] operands = { 1, default, 2, 3, 4, default, 5, 6, default, default, 7 };
            string[] ops = { "+", "(", "*", "-", "*", "(", "+", ")", ")", "+" };
            var actual = Evaluator.Numerical.Eval(operands, ops);
            Assert.Equal(excepted, actual);
        }

        [Fact]
        public void BracketTest2()
        {
            var excepted = 1 + (2 * 3 - 4 * (5 + 6)) + 7;
            var exp = "1 + (2 * 3 - 4 * (5 + 6)) + 7";
            var actual = Evaluator.Numerical.Eval(exp);
            Assert.Equal(excepted, actual);
        }

    }
}
