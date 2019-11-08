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
            var result = items.Select(x => x.For(new Func<string, string>[]
            {
                _ => _.Project(new Regex(@"[a-zA-Z]+(\d+)")),
                _ => _.Project(new Regex(@"_(\d+)")),
            }) ?? "Unknown");

            Assert.Equal(new[] { "12", "34", "Unknown" }, result);
        }
    }
}
