using Dawnx.Diagnostics;
using System;
using System.Linq;
using System.Threading;
using Xunit;

namespace NStandard.Test
{
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

            var result = Concurrency.Run(id =>
            {
                var value = cache.Value;
                Console.WriteLine($"{id}\t{cache}");
                Thread.Sleep(500);
                return value;
            }, 20, 5).Select(x => x.Value);

            Assert.Equal(updateCount, result.Distinct().Count());
        }

    }
}
