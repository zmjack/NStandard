using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NStandard.Collections
{
    [DebuggerDisplay("Count = {Count}")]
    [Serializable]
    public class HashMap<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private bool _hasNullKey;
        private TValue _nullKeyValue;
        private readonly Dictionary<TKey, TValue> _dictionary = new();

        public TValue this[TKey key]
        {
            get => TryGetValue(key, out var value) ? value : default;
            set
            {
                if (key is null)
                {
                    _hasNullKey = true;
                    _nullKeyValue = value;
                }
                else _dictionary[key] = value;
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                IEnumerable<TKey> EnumKeys()
                {
                    if (_hasNullKey) yield return default;
                    foreach (var key in _dictionary.Keys)
                    {
                        yield return key;
                    }
                }
                return EnumKeys().ToArray();
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                IEnumerable<TValue> EnumValues()
                {
                    if (_hasNullKey) yield return _nullKeyValue;
                    foreach (var value in _dictionary.Values)
                    {
                        yield return value;
                    }
                }
                return EnumValues().ToArray();
            }
        }

        public int Count => _hasNullKey ? _dictionary.Count + 1 : _dictionary.Count;

        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            if (key is null)
            {
                if (_hasNullKey) throw new ArgumentException($"An item with the same key has already been added. Key: null");
                _nullKeyValue = value;
            }
            else
            {
                _dictionary.Add(key, value);
            }
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);

        public void Clear()
        {
            _hasNullKey = false;
            _nullKeyValue = default;
            _dictionary.Clear();
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            if (item.Key is null)
            {
                if (!_hasNullKey) return false;
                else
                {
                    if (_nullKeyValue is null)
                    {
                        return item.Value is null;
                    }
                    else
                    {
                        if (item.Value is null) return false;
                        else return _nullKeyValue.Equals(item.Value);
                    }
                }
            }
            else
            {
                return _dictionary.Contains(item);
            }
        }

        public bool ContainsKey(TKey key) => key is null ? _hasNullKey : _dictionary.ContainsKey(key);
        public bool ContainsValue(TValue value) => Values.Contains(value);

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
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

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            IEnumerable<KeyValuePair<TKey, TValue>> EnumPairs()
            {
                if (_hasNullKey) yield return new KeyValuePair<TKey, TValue>(default, _nullKeyValue);
                foreach (var pair in _dictionary)
                {
                    yield return pair;
                }
            }
            return EnumPairs().GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            if (key is null)
            {
                if (!_hasNullKey) return false;

                _hasNullKey = false;
                _nullKeyValue = default;
                return true;
            }
            else
            {
                return _dictionary.Remove(key);
            }
        }

#if NET5_0_OR_GREATER
        public bool Remove(TKey key, out TValue value)
        {
            if (key is null)
            {
                if (!_hasNullKey)
                {
                    value = default;
                    return false;
                }

                value = _nullKeyValue;
                _hasNullKey = false;
                _nullKeyValue = default;
                return true;
            }
            else
            {
                return _dictionary.Remove(key, out value);
            }
        }
#endif

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (key is null)
            {
                if (!_hasNullKey)
                {
                    value = default;
                    return false;
                }

                value = _nullKeyValue;
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
}
