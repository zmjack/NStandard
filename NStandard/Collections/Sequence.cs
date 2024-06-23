using System.Collections;
using System.Collections.Generic;

namespace NStandard.Collections;

public class Sequence<T> : IEnumerable<T>, IList<T>
{
    private readonly List<T> _list = [];

    public Sequence()
    {
        Clear();
    }

    public T this[int index] { get => ((IList<T>)_list)[index]; set => ((IList<T>)_list)[index] = value; }

    public int Count => ((ICollection<T>)_list).Count;

    public bool IsReadOnly => ((ICollection<T>)_list).IsReadOnly;

    public void Add(T value)
    {
        _list.Add(value);
    }

    public void Add(IEnumerable<T> values)
    {
        _list.AddRange(values);
    }

    public void Clear()
    {
        _list.Clear();
    }

    public bool Contains(T item)
    {
        return ((ICollection<T>)_list).Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        ((ICollection<T>)_list).CopyTo(array, arrayIndex);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _list.GetEnumerator();
    }

    public int IndexOf(T item)
    {
        return ((IList<T>)_list).IndexOf(item);
    }

    public void Insert(int index, T item)
    {
        ((IList<T>)_list).Insert(index, item);
    }

    public bool Remove(T item)
    {
        return ((ICollection<T>)_list).Remove(item);
    }

    public void RemoveAt(int index)
    {
        ((IList<T>)_list).RemoveAt(index);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _list.GetEnumerator();
    }
}
