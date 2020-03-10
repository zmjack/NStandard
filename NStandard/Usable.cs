using System;

namespace NStandard
{
    public static class Usable
    {
        public static Usable<T> Begin<T>(T origin, Action onUsing, Action onUsed) => new Usable<T>(origin, onUsing, onUsed);
        public static Usable<T, TUsingReturn> Begin<T, TUsingReturn>(T origin, Func<TUsingReturn> onUsing, Action<TUsingReturn> onUsed)
        {
            return new Usable<T, TUsingReturn>(origin, onUsing, onUsed);
        }
    }

    public class Usable<T> : IDisposable
    {
        public T Origin { get; private set; }
        private readonly Action OnUsed;

        internal Usable(T origin, Action onUsing, Action onUsed)
        {
            Origin = origin;
            OnUsed = onUsed;

            onUsing();
        }

        public void Dispose() => OnUsed();
    }

    public class Usable<T, TUsingReturn> : IDisposable
    {
        public T Origin { get; private set; }
        private readonly Action<TUsingReturn> OnUsed;
        public TUsingReturn Return { get; private set; }

        internal Usable(T origin, Func<TUsingReturn> onUsing, Action<TUsingReturn> onUsed)
        {
            Origin = origin;
            OnUsed = onUsed;

            Return = onUsing();
        }

        public void Dispose() => OnUsed(Return);
    }
}
