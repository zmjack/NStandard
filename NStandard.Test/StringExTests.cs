using System;
using Xunit;

namespace NStandard.Test
{
    public class StringExTests
    {
        public class Simple
        {
            public int A { get; set; }
            public string B { get; set; }
            public string C { get; set; }
            public string D;
        }

        [Fact]
        public void CamelCaseTest()
        {
            Assert.Equal("", StringEx.CamelCase(""));
            Assert.Equal("cpKey", StringEx.CamelCase("CPKey"));
            Assert.Equal("mySQL", StringEx.CamelCase("MySQL"));
            Assert.Equal("gate2Name", StringEx.CamelCase("gate2Name"));
            Assert.Equal("dawnxV2", StringEx.CamelCase("DAWNXV2"));
            Assert.Throws<ArgumentException>(() => StringEx.CamelCase("Exception\0"));
        }

        [Fact]
        public void KebabCaseTest()
        {
            Assert.Equal("", StringEx.KebabCase(""));
            Assert.Equal("cp-key", StringEx.KebabCase("CPKey"));
            Assert.Equal("my-sql", StringEx.KebabCase("MySQL"));
            Assert.Equal("gate2-name", StringEx.KebabCase("gate2Name"));
            Assert.Equal("dawnx-v2", StringEx.KebabCase("DAWNXV2"));
            Assert.Throws<ArgumentException>(() => StringEx.KebabCase("Exception\0"));
        }

        [Fact]
        public void CommonStartsTest()
        {
            Assert.Equal("AB", StringEx.CommonStarts("ABC", "AB123", "ABC23"));
        }

        [Fact]
        public void ExtractTest()
        {
            var simple = new Simple();
            StringEx.Extract("1||3.1|3.2|45", simple, x => $"{x.A}?|{x.B}?|{x.C}|{x.D}?");

            Assert.Equal(1, simple.A);
            Assert.Equal("", simple.B);
            Assert.Equal("3.1|3.2", simple.C);
            Assert.Equal("45", simple.D);
        }

    }
}
