using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace NStandard.Test
{
    public class ObjectExtensionsTests
    {
        [Fact]
        public void PipeClassTest()
        {
            var aclass = new IntClass { Value = 1 };
            aclass.Pipe(x => x.Value = 2);
            Assert.Equal(2, aclass.Value);
        }

        [Fact]
        public void PipeStructTest()
        {
            var astruct = new IntStrcut { Value = 1 };
            astruct.Pipe(x => x.Value = 2);
            Assert.Equal(1, astruct.Value);

            astruct.Value = 2;
            Assert.Equal(2, astruct.Value);
        }

        [Fact]
        public void ExtractTest()
        {
            var items = new[] { "a12", "_34", "$56" };
            var result = items.Select(x => x.Extract(new Regex(@"[a-zA-Z]+(\d+)")).FirstOrDefault() ?? x.Extract(new Regex(@"_(\d+)")).FirstOrDefault() ?? "Unknown");

            Assert.Equal(new[] { "12", "34", "Unknown" }, result);

            var content = "[abc]-[123]-[a12]";
            var s = content.Extract(new Regex(@"\[a(.+?)\]")).ToArray();

        }

        private readonly UnaryFunc<Func<decimal, decimal>> d = func =>
        {
            decimal deltaX = 0.000_000_000_000_1m;
            return x => (func(x + deltaX) - func(x)) / deltaX;
        };

        [Fact]
        public void HigherTest1()
        {
            static decimal f(decimal x) => x * x * x * x;   // f  (x) = x^4
            var d1 = d.Higher(1);   // d1 = d(f)            // f' (x) = 4  * x^3
            var d2 = d.Higher(2);   // d2 = d(d(f))         // f''(x) = 12 * x^2

            Assert.Equal(32, (int)d(f)(2));
            Assert.Equal(32, (int)d1(f)(2));

            Assert.Equal(48, (int)d(d(f))(2));
            Assert.Equal(48, (int)d2(f)(2));
        }

        [Fact]
        public void HigherTest2()
        {
            var md5 = MD5.Create();
            var computeMD5 = new UnaryFunc<byte[]>(md5.ComputeHash);

            var result1 = ((Func<byte[], byte[]>)md5.ComputeHash).Higher(3)(Encoding.Default.GetBytes("NStandard"));
            var result2 = computeMD5.Higher(3)(Encoding.Default.GetBytes("NStandard"));
            var result3 = computeMD5(computeMD5(computeMD5(Encoding.Default.GetBytes("NStandard"))));

            Assert.Equal(result1, result2);
            Assert.Equal(result2, result3);
        }
    }
}
