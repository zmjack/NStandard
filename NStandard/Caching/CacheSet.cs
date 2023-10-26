using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NStandard.Caching;

public class CacheSet<TKey, TValue> : Dictionary<TKey, Cache<TValue>>
{
    public Func<TKey, Func<TValue>> CacheMethodBuilder;
    public UpdateCacheExpirationDelegate UpdateExpirationMethod;

    public CacheSet() { }
    public CacheSet(Func<TKey, Func<TValue>> cacheMethodBuilder)
    {
        CacheMethodBuilder = cacheMethodBuilder;
    }

    public new Cache<TValue> this[TKey key]
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get
        {
            if (!ContainsKey(key))
            {
                base[key] = new Cache<TValue>
                {
                    CacheMethod = CacheMethodBuilder?.Invoke(key),
                    UpdateExpirationMethod = UpdateExpirationMethod,
                };
            }
            return base[key];
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void UpdateAll()
    {
        foreach (var key in Keys) base[key].Update();
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void ExpireAll()
    {
        foreach (var key in Keys) base[key].Expire();
    }

}
