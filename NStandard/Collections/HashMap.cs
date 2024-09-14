using System.Collections;
using System.Diagnostics;

namespace NStandard.Collections;

[DebuggerDisplay("Count = {Count}")]
[Serializable]
public class HashMap<TKey, TValue> : IDictionary<TKey?, TValue?> where TKey : notnull
{
    private Tuple<TValue?>? _valueOf_NullKey;
    private readonly Dictionary<TKey, TValue?> _dictionary = [];

    public TValue? this[TKey? key]
    {
        get
        {
            if (key is null)
            {
                return _valueOf_NullKey is null
                    ? throw new KeyNotFoundException($"The given key (null) was not present in the dictionary.")
                    : _valueOf_NullKey.Item1;
            }
            else return _dictionary[key];
        }
        set
        {
            if (key is null)
            {
                _valueOf_NullKey = Tuple.Create(value);
            }
            else _dictionary[key] = value;
        }
    }

    private IEnumerable<TKey?> EnumKeys()
    {
        if (_valueOf_NullKey is not null) yield return default;

        foreach (var key in _dictionary.Keys)
        {
            yield return key;
        }
    }

    public ICollection<TKey?> Keys => EnumKeys().ToArray();

    private IEnumerable<TValue?> EnumValues()
    {
        if (_valueOf_NullKey is not null) yield return _valueOf_NullKey.Item1;

        foreach (var value in _dictionary.Values)
        {
            yield return value;
        }
    }

    public ICollection<TValue?> Values => EnumValues().ToArray();

    public int Count => _valueOf_NullKey is not null ? _dictionary.Count + 1 : _dictionary.Count;

    public bool IsReadOnly => false;

    public void Add(TKey? key, TValue? value)
    {
        if (key is null)
        {
            if (_valueOf_NullKey is not null) throw new ArgumentException($"An item with the same key has already been added. Key: null");
            _valueOf_NullKey = Tuple.Create(value);
        }
        else
        {
            _dictionary.Add(key, value);
        }
    }

    void ICollection<KeyValuePair<TKey?, TValue?>>.Add(KeyValuePair<TKey?, TValue?> item) => Add(item.Key, item.Value);

    public void Clear()
    {
        _valueOf_NullKey = null;
        _dictionary.Clear();
    }

    bool ICollection<KeyValuePair<TKey?, TValue?>>.Contains(KeyValuePair<TKey?, TValue?> item)
    {
        if (item.Key is null)
        {
            if (_valueOf_NullKey is null) return false;

            return Equals(_valueOf_NullKey.Item1, item.Value);
        }
        else
        {
            return _dictionary.Contains(item!);
        }
    }

    public bool ContainsKey(TKey? key) => key is null ? _valueOf_NullKey is not null : _dictionary.ContainsKey(key);
    public bool ContainsValue(TValue value)
    {
        return EnumValues().Any(x => Equals(x, value));
    }

    void ICollection<KeyValuePair<TKey?, TValue?>>.CopyTo(KeyValuePair<TKey?, TValue?>[] array, int arrayIndex)
    {
        if (array.Length - arrayIndex < Count) throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");

        var enumerator = GetEnumerator();
        enumerator.MoveNext();
        for (int i = arrayIndex; i < array.Length; i++)
        {
            array[i] = enumerator.Current;
            enumerator.MoveNext();
        }
    }

    public IEnumerator<KeyValuePair<TKey?, TValue?>> GetEnumerator()
    {
        IEnumerable<KeyValuePair<TKey?, TValue?>> EnumPairs()
        {
            if (_valueOf_NullKey is not null)
            {
                yield return new(default, _valueOf_NullKey.Item1);
            }

            foreach (var pair in _dictionary)
            {
                yield return pair!;
            }
        }
        return EnumPairs().GetEnumerator();
    }

    public bool Remove(TKey? key)
    {
        if (key is null)
        {
            if (_valueOf_NullKey is null) return false;

            _valueOf_NullKey = null;
            return true;
        }
        else
        {
            return _dictionary.Remove(key);
        }
    }

#if NET5_0_OR_GREATER
    public bool Remove(TKey? key, out TValue? value)
    {
        if (key is null)
        {
            if (_valueOf_NullKey is null)
            {
                value = default;
                return false;
            }

            value = _valueOf_NullKey.Item1;
            _valueOf_NullKey = null;
            return true;
        }
        else
        {
            return _dictionary.Remove(key, out value);
        }
    }
#endif

    bool ICollection<KeyValuePair<TKey?, TValue?>>.Remove(KeyValuePair<TKey?, TValue?> item)
    {
        return Remove(item.Key);
    }

    public bool TryGetValue(TKey? key, out TValue? value)
    {
        if (key is null)
        {
            if (_valueOf_NullKey is null)
            {
                value = default;
                return false;
            }

            value = _valueOf_NullKey.Item1;
            return true;
        }
        else
        {
            return _dictionary.TryGetValue(key, out value);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
