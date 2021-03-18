using System;
using System.Collections.Generic;

namespace NStandard
{
    public static class DpContainer
    {
        public static DefaultDpContainer<TIn, TOut> Create<TIn, TOut>(Func<DefaultDpContainer<TIn, TOut>, TIn, TOut> stateTransferFunc) => new(stateTransferFunc);
    }

    public abstract class DpContainer<TIn, TOut> : Dictionary<TIn, TOut>
    {
        public abstract TOut StateTransfer(TIn param);

        public new TOut this[TIn key]
        {
            get
            {
                var @this = this as Dictionary<TIn, TOut>;
                if (!ContainsKey(key)) @this[key] = StateTransfer(key);
                return @this[key];
            }
            set => (this as Dictionary<TIn, TOut>)[key] = value;
        }
    }

    public class DefaultDpContainer<TIn, TOut> : DpContainer<TIn, TOut>
    {
        private readonly Func<DefaultDpContainer<TIn, TOut>, TIn, TOut> StateTransferFunc;

        public DefaultDpContainer(Func<DefaultDpContainer<TIn, TOut>, TIn, TOut> stateTransferFunc)
        {
            StateTransferFunc = stateTransferFunc;
        }

        public override TOut StateTransfer(TIn param) => StateTransferFunc(this, param);
    }

}
