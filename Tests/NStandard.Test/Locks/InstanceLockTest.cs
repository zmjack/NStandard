using Dawnx.Diagnostics;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Xunit;

namespace NStandard.Locks.Test
{
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

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = Concurrency.Run(resultId =>
            {
                using (var _lock = lockParser.Parse(model).TryBegin(500))
                {
                    if (_lock.Value)
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
            Assert.True(stopwatch.ElapsedMilliseconds < 2000);
        }

        [Fact]
        public void WrongInitializeTest()
        {
            Assert.ThrowsAny<ArgumentException>(() => new InstanceLockParser<Model>(nameof(NStandard), x => x.Year, x => x.Obj));
        }

    }
}
