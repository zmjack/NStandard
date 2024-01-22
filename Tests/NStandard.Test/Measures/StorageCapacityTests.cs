using Xunit;
using static NStandard.Measures.StorageCapacity;

namespace NStandard.Measures.Test;

public class StorageCapacityTests
{
    [Fact]
    public void AddTest()
    {
        var KB256 = new KB(256);
        var KB512 = KB256 + KB256;

        Assert.Equal(512 * 1024, (B)KB512);
        Assert.Equal(512, KB512);
        Assert.Equal(0.5, (MB)KB512);

        Assert.Equal(8 * 512 * 1024, (b)KB512);
        Assert.Equal(8 * 512, (kb)KB512);
        Assert.Equal(8 * 0.5, (mb)KB512);
    }

    [Fact]
    public void SubTest()
    {
        var KB256 = new KB(256);
        var KB192 = new KB(192);
        var KB64 = KB256 - KB192;

        Assert.Equal(64 * 1024, (B)KB64);
        Assert.Equal(64, KB64);
        Assert.Equal(0.0625, (MB)KB64);

        Assert.Equal(8 * 64 * 1024, (b)KB64);
        Assert.Equal(8 * 64, (kb)KB64);
        Assert.Equal(8 * 0.0625, (mb)KB64);
    }

    [Fact]
    public void MulTest()
    {
        var KB256 = new KB(256);
        var KB512 = KB256 * 2;

        Assert.Equal(512 * 1024, (B)KB512);
        Assert.Equal(512, KB512);
        Assert.Equal(0.5, (MB)KB512);

        Assert.Equal(8 * 512 * 1024, (b)KB512);
        Assert.Equal(8 * 512, (kb)KB512);
        Assert.Equal(8 * 0.5, (mb)KB512);
    }

    [Fact]
    public void DivTest()
    {
        var KB256 = new KB(256);
        var KB128 = KB256 / 2;

        Assert.Equal(128 * 1024, (B)KB128);
        Assert.Equal(128, KB128);
        Assert.Equal(0.125, (MB)KB128);

        Assert.Equal(8 * 128 * 1024, (b)KB128);
        Assert.Equal(8 * 128, (kb)KB128);
        Assert.Equal(8 * 0.125, (mb)KB128);

        Assert.Equal(2, KB256 / KB128);
    }
}
