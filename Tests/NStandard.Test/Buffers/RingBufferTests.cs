using NStandard.Buffers;
using Xunit;

namespace NStandard.Test.Buffers;

public class RingBufferTests
{
    [Fact]
    public void Test()
    {
        var ringBuffer = new RingBuffer<int>(3);
        Assert.True(ringBuffer.IsEmpty);
        Assert.False(ringBuffer.IsFull);
        Assert.Equal(0, ringBuffer.Count);
        ringBuffer.Enqueue(1);
        ringBuffer.Enqueue(2);
        ringBuffer.Enqueue(3);
        Assert.False(ringBuffer.IsEmpty);
        Assert.True(ringBuffer.IsFull);
        Assert.Equal(3, ringBuffer.Count);
        Assert.Equal(1, ringBuffer.Dequeue());
        Assert.Equal(2, ringBuffer.Dequeue());
        Assert.False(ringBuffer.IsEmpty);
        Assert.False(ringBuffer.IsFull);
        Assert.Equal(1, ringBuffer.Count);
        ringBuffer.Enqueue(4);
        ringBuffer.Enqueue(5);
        Assert.False(ringBuffer.IsEmpty);
        Assert.True(ringBuffer.IsFull);
        Assert.Equal(3, ringBuffer.Count);
        Assert.Equal(3, ringBuffer.Dequeue());
        Assert.Equal(4, ringBuffer.Dequeue());
        Assert.Equal(5, ringBuffer.Dequeue());
        Assert.True(ringBuffer.IsEmpty);
        Assert.False(ringBuffer.IsFull);
        Assert.Equal(0, ringBuffer.Count);
    }
}
