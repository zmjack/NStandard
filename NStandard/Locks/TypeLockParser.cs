using NStandard.Flows;
using System;
using System.Threading;

namespace NStandard.Locks;

public class TypeLockParser
{
    public string LockName { get; }

    public TypeLockParser(string lockName)
    {
        LockName = lockName;
    }

    public virtual Lock Parse(Type type)
    {
        return new Lock(string.Intern($"[{StringFlow.UrlEncode(LockName)}<{type.FullName}>]"));
    }
    public virtual Lock Parse<TType>()
    {
        return new Lock(string.Intern($"[{StringFlow.UrlEncode(LockName)}<{typeof(TType).FullName}>]"));
    }

    public virtual Lock ParseThreadLock(Type type)
    {
        return new Lock(string.Intern($"[({Thread.CurrentThread.ManagedThreadId}){StringFlow.UrlEncode(LockName)}<{type.FullName}>]"));
    }
    public virtual Lock ParseThreadLock<TType>()
    {
        return new Lock(string.Intern($"[({Thread.CurrentThread.ManagedThreadId}){StringFlow.UrlEncode(LockName)}<{typeof(TType).FullName}>]"));
    }

}
