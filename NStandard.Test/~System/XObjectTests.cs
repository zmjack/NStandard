using System;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace NStandard.Test
{
    public class XObjectTests
    {
        [Fact]
        public void ForTest()
        {
            var items = new[] { "a12", "_34", "$56" };
            var result = items.Select(x => x.Project(new Regex(@"[a-zA-Z]+(\d+)")) ?? x.Project(new Regex(@"_(\d+)")) ?? "Unknown");

            Assert.Equal(new[] { "12", "34", "Unknown" }, result);
        }

        [Fact]
        public void NForTest()
        {
            Func<decimal, decimal> dx(Func<decimal, decimal> func)
            {
                decimal deltaX = 0.000_000_000_000_1m;
                return x => (func(x + deltaX) - func(x)) / deltaX;
            }

            Func<decimal, decimal> func = x => x * x * x * x;   // f(x) = x^4
            {
                var func_dx = dx(func);                 // f'(x)  = 4 * x^3
                var func_d2x = dx(dx(func));            // f''(x) = 12 * x^2
                Assert.Equal(32, (int)func_dx(2));      // f'(2)  = 4 * 2^3  = 32
                Assert.Equal(48, (int)func_d2x(2));      // f''(x) = 12 * 2^2 = 48
            }
            {
                var func_dx = func.For(dx);             // f'(x)  = 4 * x^3
                var func_d2x = func.NFor(dx, 2);        // f''(x) = 12 * x^2
                Assert.Equal(32, (int)func_dx(2));      // f'(2)  = 4 * 2^3  = 32
                Assert.Equal(48, (int)func_d2x(2));     // f''(x) = 12 * 2^2 = 48
            }
        }

        [Fact]
        public void ReturnTest()
        {
            var s = "a";
            var result = s.Return(x => int.Parse(x), x => -1);

            Assert.Equal(-1, result);
        }

        [Fact]
        public void DumpTest()
        {
            using (var agent = new ConsoleAgent())
            {
                new object[] { "1", new { a = 1, b = new { a = 1, b = 2 } } }.Dump();

                var output = agent.ReadAllText();

                Assert.Equal(@"<object[]>
[
    <string>1,
    {
        a: <int>1,
        {
            a: <int>1,
            b: <int>2,
        },
    },
]
", output);
            }

        }

    }
}
