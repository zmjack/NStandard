using System.Threading;
using Xunit;

namespace NStandard.Test
{
    public class TimePointValueTests
    {
        [Fact]
        public void NormalTest()
        {
            var pointValue = new TimePointValue<int>(1);

            Thread.Sleep(200);
            pointValue.Value = 1;
            Assert.True(pointValue.TimeSpan.TotalMilliseconds > 200);

            Thread.Sleep(200);
            pointValue.Value = 2;
            Assert.True(pointValue.TimeSpan.TotalMilliseconds < 200);

            Thread.Sleep(200);
            pointValue.Value = 1;
            Assert.True(pointValue.TimeSpan.TotalMilliseconds < 200);
        }

        [Fact]
        public void NullTest()
        {
            var pointValue = new TimePointValue<string>();

            Thread.Sleep(200);
            pointValue.Value = null;
            Assert.True(pointValue.TimeSpan.TotalMilliseconds > 200);

            Thread.Sleep(200);
            pointValue.Value = "string";
            Assert.True(pointValue.TimeSpan.TotalMilliseconds < 200);

            Thread.Sleep(200);
            pointValue.Value = null;
            Assert.True(pointValue.TimeSpan.TotalMilliseconds < 200);
        }

    }
}
