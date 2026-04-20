using NStandard.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NStandard.Test.IO;

public class ScannerTests
{
    [Fact]
    public void Test()
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

    public enum TestEnum
    {
        A, B, C
    }
}
