using System;
using System.Threading;

namespace NStandard
{
    public class Lock
    {
        public object ObjectLock { get; }

        public Lock(object objectLock)
        {
            ObjectLock = objectLock;
        }

        public void Enter() => Monitor.Enter(ObjectLock);
        public void Exit() => Monitor.Exit(ObjectLock);
        public void Pulse() => Monitor.Pulse(ObjectLock);
        public void PulseAll() => Monitor.PulseAll(ObjectLock);
        public bool TryEnter(TimeSpan timeout) => Monitor.TryEnter(ObjectLock, timeout);
        public bool TryEnter(int millisecondsTimeout) => Monitor.TryEnter(ObjectLock, millisecondsTimeout);
        public bool TryEnter() => Monitor.TryEnter(ObjectLock);
        public bool Wait() => Monitor.Wait(ObjectLock);
        public bool Wait(int millisecondsTimeout) => Monitor.Wait(ObjectLock, millisecondsTimeout);
        public bool Wait(int millisecondsTimeout, bool exitContext) => Monitor.Wait(ObjectLock, millisecondsTimeout, exitContext);
        public bool Wait(TimeSpan timeout) => Monitor.Wait(ObjectLock, timeout);
        public bool Wait(TimeSpan timeout, bool exitContext) => Monitor.Wait(ObjectLock, timeout, exitContext);

#if NETSTANDARD2_0
        public bool IsEntered() => Monitor.IsEntered(ObjectLock);
#endif
#if NETSTANDARD2_0 || NET40
        public void Enter(ref bool lockTaken) => Monitor.Enter(ObjectLock, ref lockTaken);
        public void TryEnter(TimeSpan timeout, ref bool lockTaken) => Monitor.TryEnter(ObjectLock, timeout, ref lockTaken);
        public void TryEnter(int millisecondsTimeout, ref bool lockTaken) => Monitor.TryEnter(ObjectLock, millisecondsTimeout, ref lockTaken);
        public void TryEnter(ref bool lockTaken) => Monitor.TryEnter(ObjectLock, ref lockTaken);
#endif

        public Usable<Lock> Begin() => Usable.Begin(this, () => Enter(), () => Exit());
        public Usable<Lock, bool> TryBegin() => Usable.Begin(this, () => TryEnter(), ret => { if (ret) Exit(); });
        public Usable<Lock, bool> TryBegin(int millisecondsTimeout) => Usable.Begin(this, () => TryEnter(millisecondsTimeout), ret => { if (ret) Exit(); });
        public Usable<Lock, bool> TryBegin(TimeSpan timeout) => Usable.Begin(this, () => TryEnter(timeout), ret => { if (ret) Exit(); });

        public void UseDoubleCheckLocking(Func<bool> enterCondition, Action task)
        {
            if (enterCondition())
                lock (ObjectLock)
                    if (enterCondition())
                        task();
        }
    }
}
