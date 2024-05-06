using NStandard.Collections;
using System.Linq;
using Xunit;

namespace NStandard.Test;

public class SlidingTests
{
    [Fact]
    public void Test1()
    {
        var numbers = new[] { 100, 200, 300 };
        var result = numbers.Slide(2, false).ToArray();
        Assert.Equal(
        [
            [100, 200],
            [200, 300],
        ], result);
    }

    [Fact]
    public void Test2()
    {
        var numbers = new[] { 100, 200, 300 };
        var result = numbers.Slide(2, true).ToArray();
        Assert.Equal(
        [
            [200, 300],
            [200, 300],
        ], result);
    }

    [Fact]
    public void Test3()
    {
        var numbers = new[] { 100, 200, 300 };
        var result = numbers.Slide(2, true).Select(w => w[0]).ToArray();
        Assert.Equal([100, 200], result);
    }

    [Fact]
    public void PadLeftTest()
    {
        var numbers = new[] { 100, 200, 300 };
        var result = numbers.Slide(2, true, SlidingMode.PadLeft, 100).Select(w => w.Sum()).ToArray();
        Assert.Equal([200, 300, 500], result);
    }

    [Fact]
    public void PadRightTest()
    {
        var numbers = new[] { 100, 200, 300 };
        var result = numbers.Slide(2, true, SlidingMode.PadRight, 300).Select(w => w.Sum()).ToArray();
        Assert.Equal([300, 500, 600], result);
    }

    [Fact]
    public void PadBothTest()
    {
        var numbers = new[] { 100, 200, 300 };
        var result = numbers.Slide(2, true, SlidingMode.PadBoth, 50).Select(w => w.Sum()).ToArray();
        Assert.Equal([150, 300, 500, 350], result);
    }

}
