using NStandard.Caching;
using NStandard.Diagnostics;
using Xunit;

namespace NStandard.Test;

public class CacheTests
{
    [Fact]
    public void Test1()
    {
        var cache = new Cache<DateTime>
        {
            CacheMethod = () => DateTime.Now,
            UpdateExpirationMethod = cacheTime => cacheTime.Add(TimeSpan.FromSeconds(1)),
        };

        var updateCount = 0;
        cache.OnCacheUpdated += (cacheTime, value) => updateCount += 1;

        var report = Concurrency.Run(id =>
        {
            var value = cache.Value;
            Console.WriteLine($"{id}\t{cache}");
            Thread.Sleep(500);
            return value;
        }, 20);

        var reportUpdate = report.Results.Select(x => x.Return).Distinct().Count();
        Assert.Equal(updateCount, reportUpdate);
    }

}
