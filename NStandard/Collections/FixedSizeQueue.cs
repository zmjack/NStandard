namespace NStandard.Collections;

public class FixedSizeQueue<T>
{
    protected readonly T?[] _store;
    protected readonly int _maxIndex;

    protected int _fillCount;

    public int Capacity { get; }
    public bool IsFilled { get; protected set; }

    public FixedSizeQueue(int capacity)
    {
        if (capacity < 1) throw new ArgumentException("The capacity must be greater than or equal to 1.", nameof(capacity));

        Capacity = capacity;
        _store = new T[capacity];
        _maxIndex = capacity - 1;
    }

    public void Enqueue(T? obj)
    {
        var startIndex = IsFilled ? 0 : _maxIndex - _fillCount;
        for (int i = startIndex; i < _maxIndex; i++)
        {
            _store[i] = _store[i + 1];
        }
        _store[_maxIndex] = obj;

        if (!IsFilled)
        {
            _fillCount++;
            if (_fillCount == Capacity) IsFilled = true;
        }
    }

    public T? this[int i]
    {
        get
        {
            if (i <= _maxIndex) return _store[i];
            else throw new IndexOutOfRangeException($"Index out of range (0 to {_maxIndex}).");
        }
    }
}
