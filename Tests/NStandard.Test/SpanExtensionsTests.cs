using System;
using Xunit;

namespace NStandard.Test;

public class SpanExtensionsTests
{
    [Fact]
    public void DeconstructTest()
    {
        var arr = new[] { 1, 2, 3, 4, 5, 6, 7, 8 }.AsSpan();
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

}
