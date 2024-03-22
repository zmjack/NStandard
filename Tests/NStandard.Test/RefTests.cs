using Xunit;

namespace NStandard.Test;

public class RefTests
{
    [Fact]
    public void StructTest()
    {
        var refs = new Ref<int>[] { 8, 8 };

        Assert.NotEqual(refs[0], refs[1]);
        Assert.NotSame(refs[0], refs[1]);

        Assert.Equal(refs[0].Target, refs[1].Target);
        Assert.NotSame(refs[0].Target, refs[1].Target);
    }

    [Fact]
    public void ClassTest()
    {
        var str = "str";
        var refs = new Ref<string>[] { str, str };

        Assert.NotEqual(refs[0], refs[1]);
        Assert.NotSame(refs[0], refs[1]);

        Assert.Equal(refs[0].Target, refs[1].Target);
        Assert.Same(refs[0].Target, refs[1].Target);
    }

}
