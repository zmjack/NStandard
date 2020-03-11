using System.Collections.Generic;

namespace NStandard
{
    public abstract class DpContainer<TIn, TOut> : Dictionary<TIn, TOut>
    {
        public abstract TOut StateTransfer(TIn param);

        public new TOut this[TIn key]
        {
            get
            {
                var @this = this as Dictionary<TIn, TOut>;
                if (!ContainsKey(key))
                    @this[key] = StateTransfer(key);
                return @this[key];
            }
            set => (this as Dictionary<TIn, TOut>)[key] = value;
        }
    }

}
