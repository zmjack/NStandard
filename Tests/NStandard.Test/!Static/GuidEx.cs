using NStandard.Static;
using Xunit;

namespace NStandard.Test;

public class GuidExTest
{
    [Fact]
    public void Test1()
    {
        Assert.Equal(Guid.Empty.ToString(), GuidEx.EmptyString);
    }

}
