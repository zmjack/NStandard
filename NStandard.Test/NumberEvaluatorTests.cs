using Xunit;

namespace NStandard.Test
{
    public class NumberEvaluatorTests
    {
        [Fact]
        public void Test1()
        {
            var exp = "3 + 5 * 8.1 - 6";
            Assert.Equal(3 + 5 * 8.1 - 6, Evaluator.Numerical.Eval(exp));
        }

    }
}
