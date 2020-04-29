using NStandard.Caching;
using System;
using System.Runtime.CompilerServices;

namespace NStandard
{
    public class Cache<T>
    {
        public static UpdateCacheExpirationDelegate DefaultUpdateExpirationMethod = x => DateTime.MaxValue;

        public Func<T> CacheMethod;
        public UpdateCacheExpirationDelegate UpdateExpirationMethod;
        public event CacheUpdateEventDelegate<T> OnCacheUpdated;

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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update()
        {
            if (CacheMethod == null) throw new InvalidOperationException("No cache method is set.");

            CacheTime = DateTime.Now;
            Expiration = (UpdateExpirationMethod ?? DefaultUpdateExpirationMethod)(CacheTime);
            _Value = CacheMethod();
            OnCacheUpdated?.Invoke(CacheTime, _Value);
        }

    }

}
