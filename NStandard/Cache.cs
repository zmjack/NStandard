using NStandard.Caching;
using System;
using System.Runtime.CompilerServices;

namespace NStandard
{
    public class Cache<T>
    {
        protected CacheDelegate<T> CacheValue;
        protected UpdateCacheExpirationDelegate UpdateCacheExpiration;
        public event CacheUpdateEventDelegate<T> OnCacheUpdate;

        public DateTime CacheTime { get; protected set; }
        public DateTime Expiration { get; protected set; }

        protected T _Value;

        public T Value
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                if (DateTime.Now >= Expiration)
                    Update();
                return _Value;
            }
        }

        public Cache(CacheDelegate<T> cacheDelegate)
        {
            CacheValue = cacheDelegate;
            UpdateCacheExpiration = x => DateTime.MaxValue;
        }

        public Cache(CacheDelegate<T> cacheDelegate, TimeSpan slidingExpiration)
        {
            CacheValue = cacheDelegate;
            UpdateCacheExpiration = x => x.Add(slidingExpiration);
        }

        public Cache(CacheDelegate<T> cache, UpdateCacheExpirationDelegate updateCacheExpiration)
        {
            CacheValue = cache;
            UpdateCacheExpiration = updateCacheExpiration;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update()
        {
            CacheTime = DateTime.Now;
            Expiration = UpdateCacheExpiration(CacheTime);
            _Value = CacheValue();
            OnCacheUpdate?.Invoke(CacheTime, _Value);
        }
    }

}
