using Xunit;

namespace NStandard.Measures.Test;

[Measure("g")] public partial struct g { }
[Measure("kg"), Measure<g>(1000)] public partial struct kg { }
[Measure("t"), Measure<kg>(1000)] public partial struct t { }

public class WeightTests
{
    [Fact]
    public void AddTest()
    {
        kg kg100 = 100;
        kg kg200 = kg100 + kg100;

        Assert.Equal(200_000, (g)kg200);
        Assert.Equal(200, kg200);
        Assert.Equal(0.2m, (t)kg200);
    }

    [Fact]
    public void SubTest()
    {
        kg kg100 = 100;
        kg kg40 = 40;
        kg kg60 = kg100 - kg40;

        Assert.Equal(60_000, (g)kg60);
        Assert.Equal(60, kg60);
        Assert.Equal(0.06m, (t)kg60);
    }

    [Fact]
    public void MulTest()
    {
        kg kg100 = 100;
        kg kg200 = kg100 * 2;

        Assert.Equal(200_000, (g)kg200);
        Assert.Equal(200, kg200);
        Assert.Equal(0.2, (t)kg200);
    }

    [Fact]
    public void DivTest()
    {
        kg kg100 = 100;
        kg kg50 = kg100 / 2;

        Assert.Equal(50_000, (g)kg50);
        Assert.Equal(50, kg50);
        Assert.Equal(0.05, (t)kg50);
        Assert.Equal(2, kg100 / kg50);
    }
}
