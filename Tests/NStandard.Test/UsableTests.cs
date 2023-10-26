using Xunit;

namespace NStandard.Test;

public class UsableTests
{
    [Fact]
    public void Test1()
    {
        int number = 0;
        var usable = Usable.Begin(() => number = 414, () => number = 416);
        using (usable)
        {
            Assert.Equal(414, number);
        }
        Assert.Equal(416, number);
    }

}
