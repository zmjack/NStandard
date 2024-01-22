using Xunit;
using static NStandard.Measures.Length;

namespace NStandard.Measures.Test;

public class LengthTests
{
    [Fact]
    public void AddTest()
    {
        var cm100 = new cm(100);
        var cm200 = cm100 + cm100;

        Assert.Equal(2000, (mm)cm200);
        Assert.Equal(200, cm200);
        Assert.Equal(20, (dm)cm200);
        Assert.Equal(2, (m)cm200);
        Assert.Equal(0.002, (km)cm200);
    }

    [Fact]
    public void SubTest()
    {
        var cm100 = new cm(100);
        var cm40 = new cm(40);
        var cm60 = cm100 - cm40;

        Assert.Equal(600, (mm)cm60);
        Assert.Equal(60, cm60);
        Assert.Equal(6, (dm)cm60);
        Assert.Equal(0.6, (m)cm60);
        Assert.Equal(0.0006, (km)cm60);
    }

    [Fact]
    public void MulTest()
    {
        var cm100 = new cm(100);
        var cm200 = cm100 * 2;

        Assert.Equal(2000, (mm)cm200);
        Assert.Equal(200, cm200);
        Assert.Equal(20, (dm)cm200);
        Assert.Equal(2, (m)cm200);
        Assert.Equal(0.002, (km)cm200);
    }

    [Fact]
    public void DivTest()
    {
        var cm100 = new cm(100);
        var cm50 = cm100 / 2;

        Assert.Equal(500, (mm)cm50);
        Assert.Equal(50, cm50);
        Assert.Equal(5, (dm)cm50);
        Assert.Equal(0.5, (m)cm50);
        Assert.Equal(0.0005, (km)cm50);
        Assert.Equal(2, cm100 / cm50);
    }
}
