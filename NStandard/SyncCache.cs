using System;

namespace NStandard
{
    public class SyncLazy<TCheck, TValue> where TCheck : IEquatable<TCheck>
    {
        private readonly Func<TCheck> Check;
        private readonly Func<TValue> Set;
        private TCheck _checkObject;
        private TValue _value;

        public bool IsValueCreated { get; private set; }

        public SyncLazy(Func<TCheck> check, Func<TValue> set)
        {
            Check = check;
            Set = set;
        }

        public TValue Value
        {
            get
            {
                var newValue = Check();
                if (IsValueCreated && newValue is not null && _checkObject is not null && newValue.Equals(_checkObject)) return _value;
                else
                {
                    _checkObject = newValue;
                    _value = Set();
                    IsValueCreated = true;
                    return _value;
                }
            }
        }

    }
}
