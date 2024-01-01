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
        var result = numbers.Slide(2, true).Select(x => x[0]);
        Assert.Equal(new[] { 100, 200 }, result);
    }

}
