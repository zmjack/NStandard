using Def;
using NStandard.Flows;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace NStandard.Test
{
    public class StringExtensionsTests
    {
        [Fact]
        public void CommonTest()
        {
            var ds = "123";

            Assert.Equal("123123123", ds.Repeat(3));
            Assert.Equal("12", ds.Slice(0, -1));
            Assert.Equal("1", ds.Slice(0, 1));
            Assert.Equal("23", ds.Substring(1));
            Assert.Equal("23", ds.Slice(-2));
            Assert.Throws<ArgumentOutOfRangeException>(() => ds.Slice(3, 2));

            Assert.Equal('1', ds.CharAt(0));
            Assert.Equal('3', ds.CharAt(-1));

            Assert.Equal("zmjack", "zmjack".Center(5));
            Assert.Equal("zmjack", "zmjack".Center(6));
            Assert.Equal(" zmjack", "zmjack".Center(7));
            Assert.Equal(" zmjack ", "zmjack".Center(8));
        }

        [Fact]
        public void GetBytesTest1()
        {
            var str = "你好";
            Assert.Equal(Encoding.UTF8.GetBytes(str), str.Pipe(Encoding.UTF8.GetBytes));
            Assert.Equal(Encoding.UTF8.GetBytes(str), str.Pipe(Encoding.GetEncoding("utf-8").GetBytes));
            Assert.NotEqual(Encoding.ASCII.GetBytes(str), str.Pipe(Encoding.UTF8.GetBytes));
            Assert.NotEqual(Encoding.ASCII.GetBytes(str), str.Pipe(Encoding.GetEncoding("utf-8").GetBytes));
        }

        [Fact]
        public void GetBytesTest2()
        {
            var hexString = "0c66182ec710840065ebaa47c5e6ce90";
            var hexString_Base64 = "MGM2NjE4MmVjNzEwODQwMDY1ZWJhYTQ3YzVlNmNlOTA=";
            var hexString_Bytes = new byte[]
            {
                0x0C, 0x66, 0x18, 0x2E, 0xC7, 0x10, 0x84, 0x00, 0x65, 0xEB, 0xAA, 0x47, 0xC5, 0xE6, 0xCE, 0x90
            };
            Assert.Equal(hexString_Bytes, hexString.Pipe(StringFlow.BytesFromHexString));
            Assert.Equal(hexString, hexString_Bytes.Pipe(BytesFlow.HexString));
            Assert.Equal(hexString, hexString_Base64.Pipe(StringFlow.BytesFromBase64).Pipe(Encoding.Default.GetString));
        }

        [Fact]
        public void NormalizeNewLineTest()
        {
            Assert.Equal($"123{Environment.NewLine}456", "123\r456".NormalizeNewLine());
            Assert.Equal($"123{Environment.NewLine}456", "123\n456".NormalizeNewLine());
            Assert.Equal($"123{Environment.NewLine}456", "123\r\n456".NormalizeNewLine());
            Assert.Equal($"123{Environment.NewLine}{Environment.NewLine}456", "123\n\r456".NormalizeNewLine());
            Assert.Equal($"123{Environment.NewLine}{Environment.NewLine}456", "123\n\r\n456".NormalizeNewLine());
            Assert.Equal($"123{Environment.NewLine}{Environment.NewLine}456", "123\r\r\n456".NormalizeNewLine());
            Assert.Equal($"123{Environment.NewLine}{Environment.NewLine}456", "123\r\n\r456".NormalizeNewLine());
            Assert.Equal($"123{Environment.NewLine}{Environment.NewLine}456", "123\r\n\n456".NormalizeNewLine());
        }

        [Fact]
        public void GetLinesTest()
        {
            string nullString = null;
            Assert.Equal(Array.Empty<string>(), nullString.GetLines());
            Assert.Equal(new[] { "1", "2" }, $"1{Environment.NewLine}2".GetLines());
            Assert.Equal(new[] { "1", "2" }, $"1{Environment.NewLine}2{Environment.NewLine}".GetLines());
            Assert.Equal(new[] { "1", " " }, $"1{Environment.NewLine} ".GetLines());
            Assert.Equal(new[] { "", "1", " " }, $"{Environment.NewLine}1{Environment.NewLine} ".GetLines());

            Assert.Equal(Array.Empty<string>(), nullString.GetLines());
            Assert.Equal(new[] { "1", "2" }, $"1{ControlChars.Lf}2".GetLines(true));
            Assert.Equal(new[] { "1", "2" }, $"1{ControlChars.CrLf}2".GetLines(true));
        }

        [Fact]
        public void GetPureLinesTest()
        {
            string nullString = null;
            Assert.Equal(Array.Empty<string>(), nullString.GetPureLines());
            Assert.Equal(new[] { "1", "2" }, $"1{Environment.NewLine}2".GetPureLines());
            Assert.Equal(new[] { "1", "2" }, $"1{Environment.NewLine}2{Environment.NewLine}".GetPureLines());
            Assert.Equal(new[] { "1" }, $"1{Environment.NewLine} ".GetPureLines());
            Assert.Equal(new[] { "1" }, $"{Environment.NewLine}1{Environment.NewLine} ".GetPureLines());
        }

        [Fact]
        public void UniqueTest()
        {
            Assert.Null(((string)null).Unique());
            Assert.Equal("123 456 7890", "  123  456    7890 ".Unique());
            Assert.Equal("123 456 7890", "  123  456 7890".Unique());
            Assert.Equal("123 456 7890", "  123\r\n456 7890".Unique());
            Assert.Equal("123 456 7890", "  123\r\n 456 7890".Unique());
        }

        [Fact]
        public void ExtractTest()
        {
            var regex = new Regex(@"^(.+?)\s*(?:\(\d+\))?$");
            Assert.Equal("zmjack", "zmjack".Extract(regex).First());
            Assert.Equal("zmjack", "zmjack(1)".Extract(regex).First());
            Assert.Equal("zmjack", "zmjack (1)".Extract(regex).First());
            Assert.Equal("ja", "zmjack".Extract(new Regex(@"(ja)"), "$1").First());

            Assert.Equal("zmjack", "zmjack".ExtractFirst(regex));
            Assert.Equal("zmjack", "zmjack(1)".ExtractFirst(regex));
            Assert.Equal("zmjack", "zmjack (1)".ExtractFirst(regex));
            Assert.Equal("ja", "zmjack".ExtractFirst(new Regex(@"(ja)"), "$1"));
        }

        [Fact]
        public void ProjectToArrayTest1()
        {
            Assert.Throws<ArgumentNullException>(() => "ABC".Resolve(new Regex(@"^$")));
        }

        [Fact]
        public void ProjectToArrayTest2()
        {
            var result = "A|1|11|B|2|22".Resolve(new Regex(@"(?:(?:^|\|)(.+?\|.+?\|.+?)(?=\||$))*"));
            Assert.Equal(new string[][]
            {
                new [] { "A|1|11|B|2|22" },
                new [] { "A|1|11", "B|2|22" },
            }, result);
        }

        [Fact]
        public void ProjectToArrayTest3()
        {
            var declRegex = new Regex(@"(.+)? (.+?)\((?:(?:\[In\] )?(.+?) (.+?)(?:, |(?=\))))*\);");

            {
                var result = "void F(short a, [In] int b);".Resolve(declRegex);
                Assert.Equal(new string[][]
                {
                    new[] { "void F(short a, [In] int b);" },
                    new[] { "void" },
                    new[] { "F" },
                    new[] { "short", "int" },
                    new[] { "a", "b" },
                }, result);
            }

            {
                var result = "void F();".Resolve(declRegex);
                Assert.Equal(new string[][]
                {
                    new[] { "void F();" },
                    new[] { "void" },
                    new[] { "F" },
                    Array.Empty<string>(),
                    Array.Empty<string>(),
                }, result);
            }
        }

        [Fact]
        public void Insert()
        {
            Assert.Equal("", "".UnitInsert(4, " "));
            Assert.Equal("", "".UnitInsert(4, " ", true));
            Assert.Equal("1 2345", "12345".UnitInsert(4, " "));
            Assert.Equal("1234 5", "12345".UnitInsert(4, " ", true));
            Assert.Equal("1234 5678", "12345678".UnitInsert(4, " "));
            Assert.Equal("1234 5678", "12345678".UnitInsert(4, " ", true));
            Assert.Equal("1 2345 6789", "123456789".UnitInsert(4, " "));
            Assert.Equal("1234 5678 9", "123456789".UnitInsert(4, " ", true));
        }

        [Fact]
        public void CapitalizeFirst()
        {
            Assert.Equal("zmjack", "Zmjack".CapitalizeFirst(false));
            Assert.Equal("Zmjack", "zmjack".CapitalizeFirst());
        }

        [Fact]
        public void ConsoleWidth()
        {
            Assert.Equal(7, "English".GetLengthA());
            Assert.Equal(4, "中文".GetLengthA());
        }

        [Fact]
        public void PadLeftUTest()
        {
            Assert.Equal("English", "English".PadLeftA(1));
            Assert.Equal(" English", "English".PadLeftA(8));
            Assert.Equal("中文", "中文".PadLeftA(1));
            Assert.Equal(" 中文", "中文".PadLeftA(5));
            Assert.Equal(" 中文", "中文".PadLeftA(5, '嗯'));
            Assert.Equal("嗯中文", "中文".PadLeftA(6, '嗯'));
            Assert.Equal(".中文", "中文".PadLeftA(5, '.'));
            Assert.Equal("..中文", "中文".PadLeftA(6, '.'));
        }

        [Fact]
        public void PadRightUTest()
        {
            Assert.Equal("English", "English".PadRightA(1));
            Assert.Equal("English ", "English".PadRightA(8));
            Assert.Equal("中文", "中文".PadRightA(1));
            Assert.Equal("中文 ", "中文".PadRightA(5));
            Assert.Equal("中文 ", "中文".PadRightA(5, '嗯'));
            Assert.Equal("中文嗯", "中文".PadRightA(6, '嗯'));
            Assert.Equal("中文.", "中文".PadRightA(5, '.'));
            Assert.Equal("中文..", "中文".PadRightA(6, '.'));
        }

        [Fact]
        public void IndexOfTest()
        {
            Assert.Equal(3, "1110101".IndexOf(c => c == '0'));
            Assert.Equal(5, "1110101".IndexOf(c => c == '0', 4));
            Assert.Equal(-1, "1110101".IndexOf(c => c == '0', 4, 1));
        }

        [Fact]
        public void IsMatchTest()
        {
            Assert.True("你好".IsMatch(new Regex($"^{Unicode.Chinese}+$")));
            Assert.True("こんにちは".IsMatch(new Regex($"^{Unicode.Japanese}+$")));
            Assert.True("안녕".IsMatch(new Regex($"^{Unicode.Korean}+$")));
        }

    }
}
