using Xunit;

namespace NStandard.Test;

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
    public void SplitIntoLines()
    {
        Assert.Equal(["ABCDEF", "123456"], StringEx.SplitIntoLines("ABCDEF123456", 6));
    }

    [Fact]
    public void CommonStartsTest()
    {
        Assert.Equal("AB", StringEx.CommonStarts("ABC", "AB123", "ABC23"));
    }

    [Fact]
    public void PascalCaseTest()
    {
        Assert.Equal("", StringEx.PascalCase(""));
        Assert.Equal("ABC", StringEx.PascalCase("ABC"));
        Assert.Equal("CPKey", StringEx.PascalCase("CPKey"));
        Assert.Equal("MySQL", StringEx.PascalCase("MySQL"));
        Assert.Equal("Gate2Name", StringEx.PascalCase("gate2Name"));

        Assert.Equal("NSTDV2", StringEx.PascalCase("NSTDV2"));
        Assert.Equal("NSTD_V2", StringEx.PascalCase("NSTD_V2"));
        Assert.Equal("NStd_V2", StringEx.PascalCase("NStd_V2"));
        Assert.Equal("Nstd_V2", StringEx.PascalCase("Nstd_V2"));
        Assert.Equal("Nstd_V2", StringEx.PascalCase("nstd_V2"));
    }

    [Fact]
    public void CamelCaseTest()
    {
        Assert.Equal("", StringEx.CamelCase(""));
        Assert.Equal("abc", StringEx.CamelCase("ABC"));
        Assert.Equal("cpKey", StringEx.CamelCase("CPKey"));
        Assert.Equal("mySQL", StringEx.CamelCase("MySQL"));
        Assert.Equal("gate2Name", StringEx.CamelCase("gate2Name"));

        Assert.Equal("nstdV2", StringEx.CamelCase("NSTDV2"));
        Assert.Equal("nstD_V2", StringEx.CamelCase("NSTD_V2"));
        Assert.Equal("nStd_V2", StringEx.CamelCase("NStd_V2"));
        Assert.Equal("nstd_V2", StringEx.CamelCase("Nstd_V2"));
    }

    [Fact]
    public void KebabCaseTest()
    {
        Assert.Equal("", StringEx.KebabCase(""));
        Assert.Equal("abc", StringEx.KebabCase("ABC"));
        Assert.Equal("cp-key", StringEx.KebabCase("CPKey"));
        Assert.Equal("my-sql", StringEx.KebabCase("MySQL"));
        Assert.Equal("gate2-name", StringEx.KebabCase("gate2Name"));
        Assert.Equal("nstd-v2", StringEx.KebabCase("NSTDV2"));
        Assert.Equal("nstd-v2", StringEx.KebabCase("NSTD_V2"));
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

    [Fact]
    public void IsValidFileNameTest()
    {
        Assert.True(StringEx.IsValidFileName("abc.txt", PlatformID.Win32NT));
        Assert.False(StringEx.IsValidFileName("abc.txt.", PlatformID.Win32NT));
        Assert.False(StringEx.IsValidFileName("abc<>.txt", PlatformID.Win32NT));
        Assert.False(StringEx.IsValidFileName("CON", PlatformID.Win32NT));
        Assert.False(StringEx.IsValidFileName("abc\0.txt.", PlatformID.Win32NT));

        Assert.True(StringEx.IsValidFileName("abc.txt", PlatformID.Unix));
        Assert.True(StringEx.IsValidFileName("abc.txt.", PlatformID.Unix));
        Assert.True(StringEx.IsValidFileName("abc<>.txt", PlatformID.Unix));
        Assert.True(StringEx.IsValidFileName("CON", PlatformID.Unix));
        Assert.False(StringEx.IsValidFileName("abc\0.txt.", PlatformID.Unix));
    }

}
