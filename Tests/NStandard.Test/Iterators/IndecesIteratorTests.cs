using NStandard.Iterators;
using Xunit;

namespace NStandard.Test.Iterators;

public class IndecesIteratorTests
{
    private static int[] CopyIndeces(int[] indeces)
    {
        var final = new int[indeces.Length];
        Array.Copy(indeces, final, indeces.Length);
        return final;
    }

    [Fact]
    public void Test1()
    {
        var iterator = new IndecesIterator([3, 2]);
        var list = iterator.Select(CopyIndeces).ToArray();
        Assert.Equal(6, list.Length);
        Assert.Equal(
        [
            [0, 0],
            [0, 1],
            [1, 0],
            [1, 1],
            [2, 0],
            [2, 1]
        ], list);
    }

    [Fact]
    public void Test2()
    {
        var iterator = new IndecesIterator([3, 2], 3);
        var list = iterator.Select(CopyIndeces).ToArray();
        Assert.Equal(3, list.Length);
        Assert.Equal(
        [
            [1, 1],
            [2, 0],
            [2, 1]
        ], list);
    }
}
