using System.Collections;
using System.Collections.Generic;

namespace NStandard
{
    public abstract class DpContainer<TIn, TOut> : IDictionary<TIn, TOut>
    {
        public abstract TOut StateTransfer(TIn x);

        #region IDictionary
        private Dictionary<TIn, TOut> Dp = new Dictionary<TIn, TOut>();

        public TOut this[TIn key]
        {
            get
            {
                if (!Dp.ContainsKey(key))
                    Dp[key] = StateTransfer(key);
                return Dp[key];
            }
            set => ((IDictionary<TIn, TOut>)Dp)[key] = value;
        }

        public ICollection<TIn> Keys => ((IDictionary<TIn, TOut>)Dp).Keys;

        public ICollection<TOut> Values => ((IDictionary<TIn, TOut>)Dp).Values;

        public int Count => ((IDictionary<TIn, TOut>)Dp).Count;

        public bool IsReadOnly => ((IDictionary<TIn, TOut>)Dp).IsReadOnly;

        public void Add(TIn key, TOut value) => ((IDictionary<TIn, TOut>)Dp).Add(key, value);

        public void Add(KeyValuePair<TIn, TOut> item) => ((IDictionary<TIn, TOut>)Dp).Add(item);

        public void Clear() => ((IDictionary<TIn, TOut>)Dp).Clear();

        public bool Contains(KeyValuePair<TIn, TOut> item) => ((IDictionary<TIn, TOut>)Dp).Contains(item);

        public bool ContainsKey(TIn key) => ((IDictionary<TIn, TOut>)Dp).ContainsKey(key);

        public void CopyTo(KeyValuePair<TIn, TOut>[] array, int arrayIndex) => ((IDictionary<TIn, TOut>)Dp).CopyTo(array, arrayIndex);

        public IEnumerator<KeyValuePair<TIn, TOut>> GetEnumerator() => ((IDictionary<TIn, TOut>)Dp).GetEnumerator();

        public bool Remove(TIn key) => ((IDictionary<TIn, TOut>)Dp).Remove(key);

        public bool Remove(KeyValuePair<TIn, TOut> item) => ((IDictionary<TIn, TOut>)Dp).Remove(item);

        public bool TryGetValue(TIn key, out TOut value) => ((IDictionary<TIn, TOut>)Dp).TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => ((IDictionary<TIn, TOut>)Dp).GetEnumerator();
        #endregion
    }
}
