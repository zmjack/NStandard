using Def;
using NStandard.Flows;
using System;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace NStandard.Test
{
    public class XStringTests
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
        public void GetBytesTest()
        {
            var str = "黎明";
            Assert.Equal(Encoding.UTF8.GetBytes(str), str.Bytes(Encoding.UTF8));
            Assert.Equal(Encoding.UTF8.GetBytes(str), str.Bytes("utf-8"));
            Assert.NotEqual(Encoding.ASCII.GetBytes(str), str.Bytes(Encoding.UTF8));
            Assert.NotEqual(Encoding.ASCII.GetBytes(str), str.Bytes("utf-8"));

            var hexString = "0c66182ec710840065ebaa47c5e6ce90";
            var hexString_Base64 = "MGM2NjE4MmVjNzEwODQwMDY1ZWJhYTQ3YzVlNmNlOTA=";
            var hexString_Bytes = new byte[]
            {
                0x0C, 0x66, 0x18, 0x2E, 0xC7, 0x10, 0x84, 0x00, 0x65, 0xEB, 0xAA, 0x47, 0xC5, 0xE6, 0xCE, 0x90
            };
            Assert.Equal(hexString_Bytes, hexString.Flow(BytesFlow.FromHexString));
            Assert.Equal(hexString, hexString_Bytes.Flow(BytesFlow.HexString));

            Assert.Equal(hexString,
                hexString_Base64.Flow(BytesFlow.FromBase64).String(Encoding.Default));
        }

        [Fact]
        public void NormalizeNewLineTest()
        {
            Assert.Equal("123456", "123\n456".NormalizeNewLine().Replace(Environment.NewLine, ""));
            Assert.Equal("123456", "123\r\n456".NormalizeNewLine().Replace(Environment.NewLine, ""));
        }

        [Fact]
        public void GetLinesTest()
        {
            string nullString = null;
            Assert.Equal(new string[0], nullString.GetLines());
            Assert.Equal(new[] { "1", "2" }, $"1{Environment.NewLine}2".GetLines());
            Assert.Equal(new[] { "1", "2" }, $"1{Environment.NewLine}2{Environment.NewLine}".GetLines());
            Assert.Equal(new[] { "1", " " }, $"1{Environment.NewLine} ".GetLines());
            Assert.Equal(new[] { "", "1", " " }, $"{Environment.NewLine}1{Environment.NewLine} ".GetLines());

            Assert.Equal(new string[0], nullString.GetLines());
            Assert.Equal(new[] { "1", "2" }, $"1{ControlChars.Lf}2".GetLines(true));
            Assert.Equal(new[] { "1", "2" }, $"1{ControlChars.CrLf}2".GetLines(true));
        }

        [Fact]
        public void GetPureLinesTest()
        {
            string nullString = null;
            Assert.Equal(new string[0], nullString.GetPureLines());
            Assert.Equal(new[] { "1", "2" }, $"1{Environment.NewLine}2".GetPureLines());
            Assert.Equal(new[] { "1", "2" }, $"1{Environment.NewLine}2{Environment.NewLine}".GetPureLines());
            Assert.Equal(new[] { "1" }, $"1{Environment.NewLine} ".GetPureLines());
            Assert.Equal(new[] { "1" }, $"{Environment.NewLine}1{Environment.NewLine} ".GetPureLines());
        }

        [Fact]
        public void UniqueTest()
        {
            Assert.Equal("123 456 7890", "  123  456    7890 ".Unique());
            Assert.Equal("123 456 7890", "  123  456 7890".Unique());
            Assert.Equal("123 456 7890", "  123\r\n456 7890".Unique());
            Assert.Equal("123 456 7890", "  123\r\n 456 7890".Unique());
        }

        [Fact]
        public void ProjectTest()
        {
            var regex = new Regex(@"(.+?)(?:(?=\()|(?=（)|$)");
            Assert.Equal("zmjack", "zmjack(1)".Project(regex).Trim());
            Assert.Equal("zmjack", "zmjack (1)".Project(regex).Trim());
            Assert.Equal("zmjack", "zmjack (".Project(regex).Trim());
            Assert.Equal("zmjack", "zmjack".Project(regex).Trim());
            Assert.Equal("ja", "zmjack".Project(new Regex(@"(ja)"), "$1"));
        }

        [Fact]
        public void ProjectToArrayTest()
        {
            Assert.Equal(new string[][]
            {
                new [] { "A|1|11|B|2|22" },
                new [] { "A|1|11", "B|2|22" },
            }, "A|1|11|B|2|22".ProjectToArray(new Regex(@"(?:(?:^|\|)(.+?\|.+?\|.+?)(?=\||$))*")));
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
            Assert.Equal("Zmjack", "Zmjack".CapitalizeFirst());
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
