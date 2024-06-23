using NStandard.Caching;
using Xunit;

namespace NStandard.Test;

public class CacheContainerTests
{
    [Fact]
    public void Test1()
    {
        var cacheContainer = new CacheSet<string, Guid>
        {
            CacheMethodBuilder = key => () => Guid.NewGuid(),
            UpdateExpirationMethod = cacheTime => cacheTime.Add(TimeSpan.FromMilliseconds(100)),
        };

        var a0 = cacheContainer["a"].Value;
        var a1 = cacheContainer["a"].Value;

        var b0 = cacheContainer["b"].Value;
        Thread.Sleep(100);
        var b1 = cacheContainer["b"].Value;

        Assert.Equal(a0, a1);
        Assert.NotEqual(b0, b1);
    }

}
