using Xunit;

namespace NStandard.Measures.Test;

[Measure("b")] public partial struct b { }
[Measure("kb"), Measure<b>(1024)] public partial struct kb { }
[Measure("mb"), Measure<kb>(1024)] public partial struct mb { }

[Measure("B"), Measure<b>(8)] public partial struct B { }
[Measure("KB"), Measure<B>(1024)] public partial struct KB { }
[Measure("MB"), Measure<KB>(1024)] public partial struct MB { }

public class StorageCapacityTests
{
    [Fact]
    public void AddTest()
    {
        KB KB256 = 256;
        KB KB512 = KB256 + KB256;

        Assert.Equal(512 * 1024, (B)KB512);
        Assert.Equal(512, KB512);
        Assert.Equal(0.5m, (MB)KB512);

        Assert.Equal(8 * 512 * 1024, (b)KB512);
        Assert.Equal(8 * 512, (kb)KB512);
        Assert.Equal(8 * 0.5, (mb)KB512);
    }

    [Fact]
    public void SubTest()
    {
        KB KB256 = 256;
        KB KB192 = 192;
        KB KB64 = KB256 - KB192;

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
        KB KB256 = 256;
        KB KB512 = KB256 * 2;

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
        KB KB256 = 256;
        KB KB128 = KB256 / 2;

        Assert.Equal(128 * 1024, (B)KB128);
        Assert.Equal(128, KB128);
        Assert.Equal(0.125, (MB)KB128);

        Assert.Equal(8 * 128 * 1024, (b)KB128);
        Assert.Equal(8 * 128, (kb)KB128);
        Assert.Equal(8 * 0.125, (mb)KB128);

        Assert.Equal(2, KB256 / KB128);
    }
}
