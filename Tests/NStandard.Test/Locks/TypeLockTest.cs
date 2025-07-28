using NStandard.Diagnostics;
using Xunit;

namespace NStandard.Locks.Test;

public class TypeLockTest
{
    [Fact]
    public void Test()
    {
        var lockParser = new TypeLockParser(nameof(NStandard));

        var report = Concurrency.Run(2, 2, id =>
        {
            using var _lock = lockParser.Parse<TypeLockTest>().TryBegin(500);

            if (_lock.Value)
            {
                Thread.Sleep(1000);
                return "Entered";
            }
            else return "Timeout";
        });

        Assert.Equal(1, report.Results.Count(x => x.Return == "Entered"));
        Assert.Equal(1, report.Results.Count(x => x.Return == "Timeout"));
        Assert.True(report.AverageElapsed?.TotalMilliseconds < 2000);
    }

}
