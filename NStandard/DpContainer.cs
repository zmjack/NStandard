using System;
using System.Collections.Generic;

namespace NStandard;

public static class DpContainer
{
    /// <summary>
    /// Provides dynamic programing feature.
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <param name="stateTransferFunc"></param>
    /// <returns></returns>
    public static DefaultDpContainer<TIn, TOut> Create<TIn, TOut>(Func<DefaultDpContainer<TIn, TOut>, TIn, TOut> stateTransferFunc) where TIn : notnull
    {
        return new(stateTransferFunc);
    }
}

public abstract class DpContainer<TIn, TOut> : Dictionary<TIn, TOut> where TIn : notnull
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

public class DefaultDpContainer<TIn, TOut> : DpContainer<TIn, TOut> where TIn : notnull
{
    private readonly Func<DefaultDpContainer<TIn, TOut>, TIn, TOut> StateTransferFunc;

    public DefaultDpContainer(Func<DefaultDpContainer<TIn, TOut>, TIn, TOut> stateTransferFunc)
    {
        StateTransferFunc = stateTransferFunc;
    }

    public override TOut StateTransfer(TIn param) => StateTransferFunc(this, param);
}
