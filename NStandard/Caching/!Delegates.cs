namespace NStandard.Caching;

/// <summary>
/// 
/// </summary>
/// <param name="cacheTime"></param>
/// <returns>Expiration Time</returns>
public delegate DateTime UpdateCacheExpirationDelegate(DateTime cacheTime);
public delegate void CacheUpdateEventDelegate<T>(DateTime cacheTime, T value);
