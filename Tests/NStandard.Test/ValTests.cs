using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace NStandard.Test;

public class ValTests
{
    [Fact]
    [SuppressMessage("Assertions", "xUnit2005:Do not use identity check on value type", Justification = "<Pending>")]
    public void StructTest()
    {
        var vals = new Val<int>[] { 8, 8 };

        Assert.Equal(vals[0], vals[1]);
        Assert.NotSame(vals[0], vals[1]);

        Assert.Equal(vals[0].Target, vals[1].Target);
        Assert.NotSame(vals[0].Target, vals[1].Target);
    }

    [Fact]
    [SuppressMessage("Assertions", "xUnit2005:Do not use identity check on value type", Justification = "<Pending>")]
    public void ClassTest()
    {
        var str = "str";
        var vals = new Val<string>[] { str, str };

        Assert.Equal(vals[0], vals[1]);
        Assert.NotSame(vals[0], vals[1]);

        Assert.Equal(vals[0].Target, vals[1].Target);
        Assert.Same(vals[0].Target, vals[1].Target);
    }

}
