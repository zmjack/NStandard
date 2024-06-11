using Xunit;

namespace NStandard.Collections.Test;

public class SequnceTests
{
    [Fact]
    public void Test1()
    {
        var sequence = new Sequence<int>
        {
            1, 2, 3,
            new[] { 4, 5 },
        };
        Assert.Equal(new[]
        {
            1, 2, 3, 4, 5
        }, sequence);
    }
}
