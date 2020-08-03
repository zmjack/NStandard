using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Xunit;

namespace NStandard.Test
{
    public class XObjectTests
    {
        [Fact]
        public void ExtractTest()
        {
            var items = new[] { "a12", "_34", "$56" };
            var result = items.Select(x => x.Extract(new Regex(@"[a-zA-Z]+(\d+)")).FirstOrDefault() ?? x.Extract(new Regex(@"_(\d+)")).FirstOrDefault() ?? "Unknown");

            Assert.Equal(new[] { "12", "34", "Unknown" }, result);

            var content = "[abc]-[123]-[a12]";
            var s = content.Extract(new Regex(@"\[a(.+?)\]")).ToArray();

        }

        [Fact]
        public void ForUntilTest()
        {
            var i = 1;
            var ret = i.ForUntil(i => i * 2, i => i > 1023);
            Assert.Equal(1024, ret);
        }

        private readonly SingleOpFunc<Func<decimal, decimal>> d = func =>
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
            var computeMD5 = new SingleOpFunc<byte[]>(x => md5.ComputeHash(x));

            var result1 = computeMD5.Higher(3)("NStandard".Bytes());
            var result2 = computeMD5(computeMD5(computeMD5("NStandard".Bytes())));

            Assert.Equal(result1, result2);
        }

        [Fact]
        public void AsTest()
        {
            // Hex: 0x3c75c28f
            // Dec: ‭‭1014350479‬‬
            // Bin: 00111100 01110101 11000010 10001111
            Assert.Equal(0x3c75c28f, 0.015F.MemoryAs<int>());
            Assert.Equal(0.015F, 1014350479.MemoryAs<float>());
        }

    }
}
