using Xunit;
using static NStandard.Measures.StorageCapacity;

namespace NStandard.UnitValues.Test;

public class StorageUnitTests
{
    [Fact]
    public void Test1()
    {
        var a = StorageValue.Parse(".5 MB");
        var b = new StorageValue(512, "KB");

        Assert.Equal(1, (a + b).GetValue("MB"));
        Assert.Equal(1024, (a + b).GetValue("KB"));
        Assert.Equal(1024, (a + b).Unit("KB").Value);

        Assert.Equal(1, (a + b).Value);
        Assert.Equal(0, (a - b).Value);
        Assert.Equal(1, (a * 2).Value);
        Assert.Equal(0.25, (a / 2).Value);

        Assert.True(a == b);
        Assert.False(a != b);

        Assert.False(a < b);
        Assert.True(a <= b);
        Assert.False(a > b);
        Assert.True(a >= b);
    }

    [Fact]
    public void Test2()
    {
        var values = new StorageValue[3].Let(i => new StorageValue(i * 5, "kb"));

        var sum = new StorageValue();
        sum.QuickSum(values);
        Assert.Equal(15 * 1024, sum.BitValue);

        var average = new StorageValue();
        average.QuickAverage(values);
        Assert.Equal(5 * 1024, average.BitValue);
    }

    [Fact]
    public void Test3()
    {
        var a = new MB(0.5);
        var b = new KB(512);

        Assert.Equal(1, (a + (MB)b).Value);
        Assert.Equal(1024, ((KB)a + b).Value);

        Assert.Equal(0, ((B)a - (B)b).Value);
        Assert.Equal(1, (a * 2).Value);
        Assert.Equal(0.25, (a / 2).Value);

        Assert.True((KB)a == b);
        Assert.False((KB)a != b);

        Assert.False((KB)a < b);
        Assert.True((KB)a <= b);
        Assert.False((KB)a > b);
        Assert.True((KB)a >= b);
    }

}
