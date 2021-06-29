using System.Collections.Generic;

namespace NStandard
{
    public static class XIDictionary
    {
        public static TValue GetValueOrDefault<TKey, TValue>(IDictionary<TKey, TValue> @this, TKey key) where TKey : notnull
        {
            if (@this.TryGetValue(key, out var value)) return value;
            else return default;
        }
    }
}
