using Dawnx.Diagnostics;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Xunit;

namespace NStandard.Locks.Test
{
    public class TypeLockTest
    {
        [Fact]
        public void Test()
        {
            var lockParser = new TypeLockParser(nameof(NStandard));

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = Concurrency.Run(resultId =>
            {
                using (var _lock = lockParser.Parse<TypeLockTest>().TryBegin(500))
                {
                    if (_lock.Return)
                    {
                        Thread.Sleep(1000);
                        return "Entered";
                    }
                    else return "Timeout";
                }
            }, level: 2, threadCount: 2);
            stopwatch.Stop();

            Assert.Equal(1, result.Values.Count(x => x == "Entered"));
            Assert.Equal(1, result.Values.Count(x => x == "Timeout"));
            Assert.True(stopwatch.ElapsedMilliseconds < 1900);
        }

    }
}
