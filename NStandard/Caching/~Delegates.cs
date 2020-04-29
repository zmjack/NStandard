using System;

namespace NStandard.Caching
{
    public delegate DateTime UpdateCacheExpirationDelegate(DateTime cacheTime);
    public delegate void CacheUpdateEventDelegate<T>(DateTime cacheTime, T value);
}
