using System.Linq;
using Xunit;

namespace NStandard.Schema.Test;

public class LoopTests
{
    [Fact]
    public void Test1()
    {
        var values = Loop.Create(2, 3).ToArray();
        Assert.Equal(new[]
        {
            new int?[] { 0, 0 }, new int?[] { 0, 1 }, new int?[] { 0, 2 },
            new int?[] { 1, 0 }, new int?[] { 1, 1 }, new int?[] { 1, 2 },
        }, values);
    }

    [Fact]
    public void Test2()
    {
        var values = Loop.Create(new[] { 1, 2 }, new[] { 3, 4 }).ToArray();
        Assert.Equal(new[]
        {
            new int?[] { 1, 3 }, new int?[] { 1, 4 },
            new int?[] { 2, 3 }, new int?[] { 2, 4 },
        }, values);
    }

    [Fact]
    public void Test3()
    {
        var values = Loop.Create(
            LoopFor.Create(() => 1, i => i < 4, i => i += 2),
            LoopFor.Create(() => 2, i => i < 5, i => i += 2)).ToArray();

        Assert.Equal(new[]
        {
            new int?[] { 1, 2 }, new int?[] { 1, 4 },
            new int?[] { 3, 2 }, new int?[] { 3, 4 },
        }, values);
    }

    [Fact]
    public void Test4()
    {
        var values = Loop.Create(2, 0, 3).ToArray();
        Assert.Equal(new[]
        {
            new int?[] { 0, null, null },
            new int?[] { 1, null, null },
        }, values);
    }

}
