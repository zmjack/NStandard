using System;
using System.Runtime.CompilerServices;

namespace NStandard.Caching;

public class Cache<T>
{
    public static UpdateCacheExpirationDelegate DefaultUpdateExpirationMethod { get; set; } = x => DateTime.MaxValue;

    public Func<T>? CacheMethod { get; set; }
    public UpdateCacheExpirationDelegate? UpdateExpirationMethod { get; set; }
    public event CacheUpdateEventDelegate<T>? OnCacheUpdated;

    public DateTime CacheTime { get; protected set; }
    public DateTime Expiration { get; protected set; }

    public Cache() { }
    public Cache(Func<T>? cacheMethod)
    {
        CacheMethod = cacheMethod;
    }

    protected T? _Value;

    public T? Value
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get
        {
            if (DateTime.Now >= Expiration) Update();
            return _Value;
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update()
    {
        if (CacheMethod is null) throw new InvalidOperationException("No cache method is set.");

        CacheTime = DateTime.Now;
        Expiration = (UpdateExpirationMethod ?? DefaultUpdateExpirationMethod)(CacheTime);
        _Value = CacheMethod();
        OnCacheUpdated?.Invoke(CacheTime, _Value);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Expire() => Expiration = DateTime.MinValue;

}
