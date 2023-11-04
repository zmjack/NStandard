using Xunit;

namespace NStandard.Test;

public class MathExTests
{
    [Fact]
    public void PermutationTest()
    {
        Assert.Equal(1, MathEx.Permut(0, 0));
        Assert.Equal(20, MathEx.Permut(5, 2));
        Assert.Equal(60, MathEx.Permut(5, 3));
        Assert.Equal(2000, MathEx.Permut(2000, 1));
        Assert.Equal(2000 * 1999, MathEx.Permut(2000, 2));
    }

    [Fact]
    public void CombinationTest()
    {
        Assert.Equal(1, MathEx.Combin(0, 0));
        Assert.Equal(10, MathEx.Combin(5, 2));
        Assert.Equal(10, MathEx.Combin(5, 3));
        Assert.Equal(2000 * 1999 / 2, MathEx.Combin(2000, 2));
    }

    [Fact]
    public void CeilingIntegerTest()
    {
        Assert.Equal(0, MathEx.Ceiling(0, 0));
        Assert.Equal(0, MathEx.Ceiling(0, 7));
        Assert.Equal(0, MathEx.Ceiling(7, 0));
        Assert.Equal(7, MathEx.Ceiling(7, 7));
        Assert.Equal(9, MathEx.Ceiling(7, 3));
        Assert.Equal(9, MathEx.Ceiling(7, 3, true));
        Assert.Equal(-6, MathEx.Ceiling(-7, 3));
        Assert.Equal(-9, MathEx.Ceiling(-7, 3, true));
    }

    [Fact]
    public void CeilingDecimalTest()
    {
        Assert.Equal(0m, MathEx.Ceiling(0m, 0m));
        Assert.Equal(0m, MathEx.Ceiling(0m, 7m));
        Assert.Equal(0m, MathEx.Ceiling(7m, 0m));
        Assert.Equal(7m, MathEx.Ceiling(7m, 7m));
        Assert.Equal(7.2m, MathEx.Ceiling(7m, 0.3m));
        Assert.Equal(7.2m, MathEx.Ceiling(7m, 0.3m, true));
        Assert.Equal(-6.9m, MathEx.Ceiling(-7m, 0.3m));
        Assert.Equal(-7.2m, MathEx.Ceiling(-7m, 0.3m, true));
    }

    [Fact]
    public void CeilingDoubleTest()
    {
        Assert.Equal(0, MathEx.Ceiling(0, 0));
        Assert.Equal(0, MathEx.Ceiling(0, 7));
        Assert.Equal(0, MathEx.Ceiling(7, 0));
        Assert.Equal(7, MathEx.Ceiling(7, 7));
        Assert.Equal(7.2, MathEx.Ceiling(7, 0.3));
        Assert.Equal(7.2, MathEx.Ceiling(7, 0.3, true));
        Assert.Equal(-6.9, MathEx.Ceiling(-7, 0.3));
        Assert.Equal(-7.2, MathEx.Ceiling(-7, 0.3, true));
    }

    [Fact]
    public void FloorIntegerTest()
    {
        Assert.Equal(0, MathEx.Floor(0, 0));
        Assert.Equal(0, MathEx.Floor(0, 7));
        Assert.Equal(0, MathEx.Floor(7, 0));
        Assert.Equal(7, MathEx.Floor(7, 7));
        Assert.Equal(6, MathEx.Floor(7, 3));
        Assert.Equal(6, MathEx.Floor(7, 3, true));
        Assert.Equal(-9, MathEx.Floor(-7, 3));
        Assert.Equal(-6, MathEx.Floor(-7, 3, true));
    }

    [Fact]
    public void FloorDecimalTest()
    {
        Assert.Equal(0m, MathEx.Floor(0m, 0m));
        Assert.Equal(0m, MathEx.Floor(0m, 7m));
        Assert.Equal(0m, MathEx.Floor(7m, 0m));
        Assert.Equal(7m, MathEx.Floor(7m, 7m));
        Assert.Equal(6.9m, MathEx.Floor(7m, 0.3m));
        Assert.Equal(6.9m, MathEx.Floor(7m, 0.3m, true));
        Assert.Equal(-7.2m, MathEx.Floor(-7m, 0.3m));
        Assert.Equal(-6.9m, MathEx.Floor(-7m, 0.3m, true));
    }

    [Fact]
    public void FloorDoubleTest()
    {
        Assert.Equal(0, MathEx.Floor(0, 0));
        Assert.Equal(0, MathEx.Floor(0, 7));
        Assert.Equal(0, MathEx.Floor(7, 0));
        Assert.Equal(7, MathEx.Floor(7, 7));
        Assert.Equal(6.9, MathEx.Floor(7, 0.3));
        Assert.Equal(6.9, MathEx.Floor(7, 0.3, true));
        Assert.Equal(-7.2, MathEx.Floor(-7, 0.3));
        Assert.Equal(-6.9, MathEx.Floor(-7, 0.3, true));
    }

}
