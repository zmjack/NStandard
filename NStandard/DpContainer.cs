using System;
using System.Collections.Generic;

namespace NStandard
{
    public abstract class DpContainer<TIn, TOut> : Dictionary<TIn, TOut>
    {
        public abstract TOut StateTransfer(TIn x);

        public new TOut this[TIn key]
        {
            get
            {
                if (!ContainsKey(key))
                    this[key] = StateTransfer(key);
                return this[key];
            }
            set => this[key] = value;
        }
    }

}
