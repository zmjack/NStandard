using NStandard.Diagnostics;
using Xunit;

namespace NStandard.Locks.Test;

public class InstanceLockTest
{
    public enum Sex { Male, Female }

    public class Model
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public object Obj { get; set; }
        public Sex Sex { get; set; }
        public string Name { get; set; }
    }

    [Fact]
    public void Test()
    {
        var lockParser = new InstanceLockParser<Model>(nameof(NStandard), x => x.Year, x => x.Month, x => x.Sex, x => "const");
        var model = new Model { Year = 2012, Month = 4, };

        var report = Concurrency.Run(2, 2, id =>
        {
            using var _lock = lockParser.Parse(model).TryBegin(500);

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

    [Fact]
    public void WrongInitializeTest()
    {
        Assert.ThrowsAny<ArgumentException>(() => new InstanceLockParser<Model>(nameof(NStandard), x => x.Year, x => x.Obj));
    }

}
