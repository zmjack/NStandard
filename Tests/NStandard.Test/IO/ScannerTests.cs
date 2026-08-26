using NStandard.IO;
using Xunit;

namespace NStandard.Test.IO;

public class ScannerTests
{
    public enum TestEnum
    {
        A, B, C
    }

    [Fact]
    public void NextTest()
    {
        var scanner = new Scanner(" 0 1 C 23 45.67\t  89  Hello World  ");
        Assert.Equal(TestEnum.A, scanner.Next<TestEnum>());
        Assert.Equal(TestEnum.B, scanner.Next<TestEnum>());
        Assert.Equal(TestEnum.C, scanner.Next<TestEnum>());
        Assert.Equal(23, scanner.Next<int>());
        Assert.Equal(45.67, scanner.Next<double>());
        Assert.Equal(89m, scanner.Next<decimal>());
        Assert.Equal("Hello", scanner.Next<string>());
        Assert.Equal("World", scanner.Next<string>());
    }

    [Fact]
    public void TryNextTest()
    {
        var scanner = new Scanner(" 0 1 C 23 45.67\t  89  Hello World  ");
        Assert.True(scanner.TryNext<TestEnum>(out var e1));
        Assert.Equal(TestEnum.A, e1);
        Assert.True(scanner.TryNext<TestEnum>(out var e2));
        Assert.Equal(TestEnum.B, e2);
        Assert.True(scanner.TryNext<TestEnum>(out var e3));
        Assert.Equal(TestEnum.C, e3);
        Assert.True(scanner.TryNext<int>(out var i));
        Assert.Equal(23, i);
        Assert.True(scanner.TryNext<double>(out var d));
        Assert.Equal(45.67, d);
        Assert.True(scanner.TryNext<decimal>(out var m));
        Assert.Equal(89m, m);
        Assert.True(scanner.TryNext<string>(out var s1));
        Assert.Equal("Hello", s1);
        Assert.True(scanner.TryNext<string>(out var s2));
        Assert.Equal("World", s2);
        Assert.False(scanner.TryNext<string>(out _));
    }
}
