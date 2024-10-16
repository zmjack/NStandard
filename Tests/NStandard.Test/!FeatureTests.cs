using NStandard.Measures.Test;
using Xunit;

namespace NStandard.Test;

public class _FeatureTests
{
    [Fact]
    public void ValuesTest()
    {
        var kms = new km[10].Let(i => new(i));
        Assert.Equal("45 km", kms.QSum().ToString());
        Assert.Equal("4.5 km", kms.QAverage().ToString());
        Assert.Equal("4.5 km", kms.QAverageOrDefault().ToString());
        Assert.Equal("4.5 km", kms.QAverageOrDefault(10).ToString());
    }

    [Fact]
    public void EmptyValuesTest()
    {
        km[] kms = [];
        Assert.Equal("0 km", kms.QSum().ToString());
        Assert.ThrowsAny<InvalidOperationException>(() => kms.QAverage().ToString());
        Assert.Equal("0 km", kms.QAverageOrDefault().ToString());
        Assert.Equal("10 km", kms.QAverageOrDefault(10).ToString());
    }

    [Fact]
    public void NullsTest()
    {
        var kms = new km?[10].Let(i => null);
        Assert.Equal("0 km", kms.QSum().ToString());
        Assert.Null(kms.QAverage());
        Assert.Null(kms.QAverageOrDefault());
        Assert.Equal("10 km", kms.QAverageOrDefault(10).ToString());
    }

    [Fact]
    public void EmptyNullableValuesTest()
    {
        km?[] kms = [];
        Assert.Equal("0 km", kms.QSum().ToString());
        Assert.Null(kms.QAverage());
        Assert.Null(kms.QAverageOrDefault());
        Assert.Equal("10 km", kms.QAverageOrDefault(10).ToString());
    }

    [Fact]
    public void NullableValuesTest()
    {
        var kms = new km?[10].Let(i => i % 2 == 0 ? new(i) : null);
        Assert.Equal("20 km", kms.QSum().ToString());
        Assert.Equal("4 km", kms.QAverage().ToString());
        Assert.Equal("4 km", kms.QAverageOrDefault().ToString());
        Assert.Equal("4 km", kms.QAverageOrDefault(10).ToString());
    }
}
