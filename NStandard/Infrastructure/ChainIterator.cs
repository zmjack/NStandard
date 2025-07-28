namespace NStandard.Infrastructure;

public struct ChainIterator<T>
{
    public ChainOrigin Origin { get; set; }
    public T[] Iterators { get; set; }
    public int Cursor { get; set; }
    public readonly T Current => Iterators[Cursor];
}
