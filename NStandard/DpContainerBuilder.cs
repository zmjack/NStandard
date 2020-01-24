using System;
using System.Collections.Generic;
using System.Text;

namespace NStandard
{
    public class DpContainerBuilder<TData, TIn, TOut>
    {
        public Func<TData, TIn, TOut> StateTransferFunction;

        public DpContainerBuilder(Func<TData, TIn, TOut> stateTransferFunction)
        {
            StateTransferFunction = stateTransferFunction;
        }

        public DpContainer<TData, TIn, TOut> Build(TData data) => new DpContainer<TData, TIn, TOut>(data, StateTransferFunction);
    }
}
