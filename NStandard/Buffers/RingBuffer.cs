namespace NStandard.Buffers;

public class RingBuffer<T>
{
    private readonly T[] _buffer;
    private int _head;
    private int _tail;
    private int _count;

    public RingBuffer(int capacity)
    {
        if (capacity <= 0) throw new ArgumentException("Capacity must be greater than zero.", nameof(capacity));

        _buffer = new T[capacity];
        _head = 0;
        _tail = 0;
        _count = 0;
    }

    public int Capacity => _buffer.Length;

    public int Count => _count;

    public bool IsFull => _count == Capacity;

    public bool IsEmpty => _count == 0;

    public void Enqueue(T item)
    {
        _buffer[_head] = item;
        _head = (_head + 1) % Capacity;

        if (IsFull)
        {
            _tail = (_tail + 1) % Capacity;
        }
        else
        {
            _count++;
        }
    }

    public T Dequeue()
    {
        if (IsEmpty) throw new InvalidOperationException("The buffer is empty.");

        var item = _buffer[_tail];
        _tail = (_tail + 1) % Capacity;
        _count--;
        return item;
    }

    public T Peek()
    {
        if (IsEmpty) throw new InvalidOperationException("The buffer is empty.");
        return _buffer[_tail];
    }
}