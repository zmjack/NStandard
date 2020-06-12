using System;

namespace NStandard
{
    public interface IFlow<T, TRet>
    {
        TRet Execute(T origin);
    }

    public class Flow<T, TRet> : IFlow<T, TRet>
    {
        public Func<T, TRet> Ret { get; }

        public Flow(Func<T, TRet> ret)
        {
            Ret = ret;
        }

        public TRet Execute(T origin) => Ret(origin);
    }

}
