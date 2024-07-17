using NStandard.Measures;
using Xunit;

namespace NStandard.Test;

public class _FeatureTests
{
    [Fact]
    public void Test1()
    {
        var kmArray = new _km[100].Let(i => new(1));
        Assert.Equal("100 km", kmArray.QuickSum().ToString());
        Assert.Equal("1 km", kmArray.QuickAverage().ToString());
        Assert.Equal("1 km", kmArray.QuickAverageOrDefault().ToString());
        Assert.Equal("1 km", kmArray.QuickAverageOrDefault(10).ToString());
    }

    [Fact]
    public void Test2()
    {
        var kmArray = new _km[0];
        Assert.Equal("0 km", kmArray.QuickSum().ToString());
        Assert.ThrowsAny<InvalidOperationException>(() => kmArray.QuickAverage().ToString());
        Assert.Equal("0 km", kmArray.QuickAverageOrDefault().ToString());
        Assert.Equal("10 km", kmArray.QuickAverageOrDefault(10).ToString());
    }

    [Fact]
    public void Test3()
    {
        var kmArray = new _km?[100].Let(i => default);
        Assert.Equal("0 km", kmArray.QuickSum().ToString());
        Assert.Null(kmArray.QuickAverage());
        Assert.Null(kmArray.QuickAverageOrDefault());
        Assert.Equal("10 km", kmArray.QuickAverageOrDefault(10).ToString());
    }

    [Fact]
    public void Test4()
    {
        var kmArray = new _km?[0];
        Assert.Equal("0 km", kmArray.QuickSum().ToString());
        Assert.Null(kmArray.QuickAverage());
        Assert.Null(kmArray.QuickAverageOrDefault());
        Assert.Equal("10 km", kmArray.QuickAverageOrDefault(10).ToString());
    }
}

[Measure("km"), Measure<_m>(1000)]
public partial struct _km
{
}

[Measure("m"), Measure<_cm>(100)]
public partial struct _m
{
}

public partial struct _cm
{
}
