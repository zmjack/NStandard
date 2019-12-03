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
        public void ReturnTest()
        {
            var s = "a";
            var result = s.Return(x => int.Parse(x), x => -1);

            Assert.Equal(-1, result);

        }

    }
}
