using System;
using System.Linq;
using Xunit;

namespace NStandard.Test;

public class ArrayExtensionsTests
{
    private class Class { }

    [Fact]
    public void LetTest1()
    {
        var arr = new int[5].Let(i => i * 2 + 1);
        Assert.Equal(new[] { 1, 3, 5, 7, 9 }, arr);
    }

    [Fact]
    public void LetTest2()
    {
        var arr = new int[2, 3].Let((i0, i1) => i0 * 3 + i1);
        Assert.Equal(new[,] { { 0, 1, 2 }, { 3, 4, 5 } }, arr);
    }

    [Fact]
    public void LetTest3()
    {
        var arr = new int[2, 3, 2].Let((i0, i1, i2) => i0 * 6 + i1 * 2 + i2);
        Assert.Equal(new[, ,]
        {
            { { 0, 1 }, { 2, 3 }, { 4, 5 } },
            { { 6, 7 }, { 8, 9 }, { 10, 11 } },
        }, arr);
    }

    [Fact]
    public void LetTest4()
    {
        var arr = new int[2, 2, 3, 2].Let(indices => indices[0] * 12 + indices[1] * 6 + indices[2] * 2 + indices[3]) as int[,,,];
        Assert.Equal(new[, , ,]
        {
            {
                {
                    { 0, 1 }, { 2, 3 }, { 4, 5 }
                },
                {
                    { 6, 7 }, { 8, 9 }, { 10, 11 }
                },
            },
            {
                {
                    { 12, 13 }, { 14, 15 }, { 16, 17 }
                },
                {
                    { 18, 19 }, { 20, 21 }, { 22, 23 }
                },
            }
        }, arr);
    }

    [Fact]
    public void LetClassTest()
    {
        var classes = new Class[2].Let(i => new Class());
        Assert.NotNull(classes[0]);
        Assert.NotNull(classes[1]);
    }

    [Fact]
    public void ShuffleTest()
    {
        var random = new Random();
        var arr = new int[100].Let(i => i);
        arr.Shuffle();
    }

    [Fact]
    public void JaggedArrayTest()
    {
        var arr = new string[2, 3]
        {
            { "0,0", "0,1", "0,2" },
            { "1,0", "1,1", "1,2" },
        };
        arr.Each((v, i1, i2) =>
        {
            Assert.Equal($"{i1},{i2}", v);
            Assert.Equal($"{i1},{i2}", arr[i1, i2]);
        });
    }

    [Fact]
    public void DeconstructTest()
    {
        var arr = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        var (i1, i2, i3, i4, i5, i6, i7, (i8, _)) = arr;
        Assert.Equal(1, i1);
        Assert.Equal(2, i2);
        Assert.Equal(3, i3);
        Assert.Equal(4, i4);
        Assert.Equal(5, i5);
        Assert.Equal(6, i6);
        Assert.Equal(7, i7);
        Assert.Equal(8, i8);
    }

    [Fact]
    public void LocateTest()
    {
        Assert.Equal(15, "BBC ABCDAB ABCDABCDABDE".ToCharArray().Locate("ABCDABD".ToCharArray()));
        Assert.Equal(0, "AAAA".ToCharArray().Locate("AA".ToCharArray()));
        Assert.Equal(0, "A".ToCharArray().Locate("A".ToCharArray()));
        Assert.ThrowsAny<ArgumentException>(() => "A".ToCharArray().Locate("".ToCharArray()));
    }

    [Fact]
    public void LocatesTest()
    {
        Assert.Single("BBC ABCDAB ABCDABCDABDE".ToCharArray().Locates("ABCDABD".ToCharArray()));
        Assert.Equal(3, "AAAA".ToCharArray().Locates("AA".ToCharArray()).Count());
        Assert.Single("A".ToCharArray().Locates("A".ToCharArray()));
        Assert.ThrowsAny<ArgumentException>(() => "A".ToCharArray().Locates("".ToCharArray()));
    }
}
