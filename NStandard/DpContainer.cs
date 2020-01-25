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

    public class DpContainer<TData, TIn, TOut> : DpContainer<TIn, TOut>
    {
        public TData Data;
        public Func<TData, TIn, TOut> StateTransferFunction;

        public DpContainer(TData data, Func<TData, TIn, TOut> stateTransferFunction)
        {
            Data = data;
            StateTransferFunction = stateTransferFunction;
        }

        public override TOut StateTransfer(TIn x) => StateTransferFunction(Data, x);
    }

}
