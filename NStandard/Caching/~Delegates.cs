using System;
using System.Collections.Generic;
using System.Text;

namespace NStandard.Caching
{
    public delegate T CacheDelegate<T>();
    public delegate DateTime UpdateCacheExpirationDelegate(DateTime cacheTime);
    public delegate void CacheUpdateEventDelegate<T>(DateTime cacheTime, T value);
}
