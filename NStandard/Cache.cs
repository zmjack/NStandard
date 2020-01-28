using NStandard.Caching;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace NStandard
{
    public class Cache<T>
    {
        protected CacheDelegate<T> CacheDelegate;
        protected UpdateCacheExpirationDelegate UpdateCacheExpirationDelegate;
        public event CacheUpdateEventDelegate<T> OnCacheUpdate;

        public DateTime CacheTime { get; protected set; }
        public DateTime Expiration { get; protected set; }

        protected T _CacheValue;

        public T Value
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                if (DateTime.Now >= Expiration)
                    Update();
                return _CacheValue;
            }
        }

        public Cache(CacheDelegate<T> cacheDelegate, TimeSpan slidingExpiration)
        {
            CacheDelegate = cacheDelegate;
            UpdateCacheExpirationDelegate = x => x.Add(slidingExpiration);
        }

        public Cache(CacheDelegate<T> cacheDelegate, UpdateCacheExpirationDelegate updateCacheExpirationDelegate)
        {
            CacheDelegate = cacheDelegate;
            UpdateCacheExpirationDelegate = updateCacheExpirationDelegate;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update()
        {
            CacheTime = DateTime.Now;
            Expiration = UpdateCacheExpirationDelegate(CacheTime);
            _CacheValue = CacheDelegate();
            OnCacheUpdate?.Invoke(CacheTime, _CacheValue);
        }
    }

}
