using System.Collections.Generic;
using System.Linq;

namespace NStandard.Collections
{
    public static class Counter
    {
        public static Counter<TKey> Parse<TKey>(IEnumerable<TKey> source)
        {
            return new Counter<TKey>(source);
        }
    }

    public class Counter<TKey> : Dictionary<TKey, int>
    {
        private Counter() { }
        public Counter(IEnumerable<TKey> source)
        {
            foreach (var key in source)
            {
                if (ContainsKey(key)) this[key]++;
                else this[key] = 1;
            }
        }

        public IEnumerable<TKey> Elements()
        {
            foreach (var key in Keys)
            {
                var count = this[key];
                for (int i = 0; i < count; i++)
                {
                    yield return key;
                }
            }
        }

        public int Total()
        {
            return Values.Sum();
        }

        private static Counter<TKey> Copy(Counter<TKey> source)
        {
            var counter = new Counter<TKey>();
            foreach (var item in source)
            {
                counter[item.Key] = item.Value;
            }
            return counter;
        }

        public static Counter<TKey> operator +(Counter<TKey> left, Counter<TKey> right)
        {
            var counter = Copy(left);
            foreach (var item in right)
            {
                if (counter.ContainsKey(item.Key))
                    counter[item.Key] += item.Value;
                else counter[item.Key] = item.Value;
            }
            return counter;
        }

        public static Counter<TKey> operator -(Counter<TKey> left, Counter<TKey> right)
        {
            var counter = Copy(left);
            foreach (var item in right)
            {
                if (counter.ContainsKey(item.Key))
                    counter[item.Key] -= item.Value;
                else counter[item.Key] = -item.Value;
            }
            return counter;
        }
    }
}
