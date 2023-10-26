namespace NStandard.Infrastructure;

public struct ChainIterator<T>
{
    public ChainOrigin Origin { get; set; }
    public T[] Iterators { get; set; }
    public int Cursor { get; set; }
    public T Current => Iterators[Cursor];
}
