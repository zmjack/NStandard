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
    public void Test2D()
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
            [2, 1],
        ], list);
    }

    [Fact]
    public void Test2DSkip4()
    {
        var iterator = new IndecesIterator([3, 2], 4);
        var list = iterator.Select(CopyIndeces).ToArray();
        Assert.Equal(2, list.Length);
        Assert.Equal(
        [
            [2, 0],
            [2, 1],
        ], list);
    }

    [Fact]
    public void Test2DSkip16()
    {
        var iterator = new IndecesIterator([3, 2], 16);
        var list = iterator.Select(CopyIndeces).ToArray();
        Assert.Empty(list);
    }

    [Fact]
    public void Test3D()
    {
        var iterator = new IndecesIterator([3, 2, 2]);
        var list = iterator.Select(CopyIndeces).ToArray();
        Assert.Equal(12, list.Length);
        Assert.Equal(
        [
            [0, 0, 0],
            [0, 0, 1],
            [0, 1, 0],
            [0, 1, 1],
            [1, 0, 0],
            [1, 0, 1],
            [1, 1, 0],
            [1, 1, 1],
            [2, 0, 0],
            [2, 0, 1],
            [2, 1, 0],
            [2, 1, 1],
        ], list);
    }

    [Fact]
    public void Test3DSkip8()
    {
        var iterator = new IndecesIterator([3, 2, 2], 8);
        var list = iterator.Select(CopyIndeces).ToArray();
        Assert.Equal(4, list.Length);
        Assert.Equal(
        [
            [2, 0, 0],
            [2, 0, 1],
            [2, 1, 0],
            [2, 1, 1],
        ], list);
    }

    [Fact]
    public void Test3DSkip16()
    {
        var iterator = new IndecesIterator([3, 2, 2], 16);
        var list = iterator.Select(CopyIndeces).ToArray();
        Assert.Empty(list);
    }
}
