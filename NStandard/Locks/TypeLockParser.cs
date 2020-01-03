using NStandard.Flows;
using System;
using System.Threading;

namespace NStandard.Locks
{
    public class TypeLockParser
    {
        public string LockName { get; }

        public TypeLockParser(string lockName)
        {
            LockName = lockName;
        }

        public virtual Lock Parse(Type type)
        {
            return new Lock(string.Intern($"[{LockName.Flow(StringFlow.UrlEncode)}<{type.FullName}>]"));
        }
        public virtual Lock Parse<TType>()
        {
            return new Lock(string.Intern($"[{LockName.Flow(StringFlow.UrlEncode)}<{typeof(TType).FullName}>]"));
        }

        public virtual Lock ParseThreadLock(Type type)
        {
            return new Lock(string.Intern($"[({Thread.CurrentThread.ManagedThreadId}){LockName.Flow(StringFlow.UrlEncode)}<{type.FullName}>]"));
        }
        public virtual Lock ParseThreadLock<TType>()
        {
            return new Lock(string.Intern($"[({Thread.CurrentThread.ManagedThreadId}){LockName.Flow(StringFlow.UrlEncode)}<{typeof(TType).FullName}>]"));
        }

    }
}
