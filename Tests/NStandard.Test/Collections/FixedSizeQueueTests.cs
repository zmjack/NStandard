using Xunit;

namespace NStandard.Collections.Test
{
    public class FixedSizeQueueTests
    {
        [Fact]
        public void Test()
        {
            var queue = new FixedSizeQueue<int>(2);
            queue.Enqueue(1);
            queue.Enqueue(2);

            Assert.Equal(1, queue[0]);
            Assert.Equal(2, queue[1]);

            queue.Enqueue(3);

            Assert.Equal(2, queue[0]);
            Assert.Equal(3, queue[1]);
        }

    }
}
