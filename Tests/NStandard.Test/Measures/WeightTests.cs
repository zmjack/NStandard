using Xunit;
using static NStandard.Measures.Weight;

namespace NStandard.Measures.Test;

public class WeightTests
{
    [Fact]
    public void AddTest()
    {
        var kg100 = new kg(100);
        var kg200 = kg100 + kg100;

        Assert.Equal(200_000, (g)kg200);
        Assert.Equal(200, kg200);
        Assert.Equal(0.2m, (t)kg200);
    }

    [Fact]
    public void SubTest()
    {
        var kg100 = new kg(100);
        var kg40 = new kg(40);
        var kg60 = kg100 - kg40;

        Assert.Equal(60_000, (g)kg60);
        Assert.Equal(60, kg60);
        Assert.Equal(0.06m, (t)kg60);
    }

    [Fact]
    public void MulTest()
    {
        var kg100 = new kg(100);
        var kg200 = kg100 * 2;

        Assert.Equal(200_000, (g)kg200);
        Assert.Equal(200, kg200);
        Assert.Equal(0.2, (t)kg200);
    }

    [Fact]
    public void DivTest()
    {
        var kg100 = new kg(100);
        var kg50 = kg100 / 2;

        Assert.Equal(50_000, (g)kg50);
        Assert.Equal(50, kg50);
        Assert.Equal(0.05, (t)kg50);
        Assert.Equal(2, kg100 / kg50);
    }
}
