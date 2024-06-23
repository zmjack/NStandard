using NStandard.Collections;
using Xunit;

namespace NStandard.Test;

public class ArrayVisitorTests
{
    [Fact]
    public void TypeNotSameTest()
    {
        var src = new string[4, 4].Let((i0, i1) => $"{i0}, {i1}");
        var visitor = new ArrayVisitor<int>(src);
        Assert.Throws<InvalidCastException>(() => visitor.GetValue(0));
    }

    [Fact]
    public void Test()
    {
        var src = new string[4, 4].Let((i0, i1) => $"{i0}, {i1}");
        var visitor = new ArrayVisitor<string>(src);
        visitor.SetValue("-", 15);
        Assert.Equal("-", src[3, 3]);
        Assert.Equal("-", visitor.GetValue(15));
    }
}
