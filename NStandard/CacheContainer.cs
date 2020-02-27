using NStandard.Caching;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NStandard
{
    public class CacheContainer<TKey, TValue> : Dictionary<TKey, Cache<TValue>>
    {
        private readonly Func<TKey, CacheDelegate<TValue>> Cache;
        protected UpdateCacheExpirationDelegate UpdateCacheExpiration;

        public CacheContainer(Func<TKey, CacheDelegate<TValue>> cache)
        {
            Cache = cache;
            UpdateCacheExpiration = x => DateTime.MaxValue;
        }

        public CacheContainer(Func<TKey, CacheDelegate<TValue>> cache, TimeSpan slidingExpiration)
        {
            Cache = cache;
            UpdateCacheExpiration = x => x.Add(slidingExpiration);
        }

        public CacheContainer(Func<TKey, CacheDelegate<TValue>> cache, UpdateCacheExpirationDelegate updateCacheExpiration)
        {
            Cache = cache;
            UpdateCacheExpiration = updateCacheExpiration;
        }

        public new Cache<TValue> this[TKey key]
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                if (!ContainsKey(key))
                    base[key] = new Cache<TValue>(Cache(key), UpdateCacheExpiration);
                return base[key];
            }
        }

    }
}
