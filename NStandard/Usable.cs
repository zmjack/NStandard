using System;

namespace NStandard
{
    public class Usable
    {
        private readonly Action OnDisposing;

        internal Usable(Action onUsing, Action onDisposing)
        {
            OnDisposing = onDisposing;
            onUsing();
        }

        public void Dispose() => OnDisposing();


        public static Usable Begin(Action onUsing, Action onDispose) => new Usable(onUsing, onDispose);
        public static Usable<TUsingReturn> Begin<TUsingReturn>(Func<TUsingReturn> onUsing, Action<TUsingReturn> onDisposing)
        {
            return new Usable<TUsingReturn>(onUsing, onDisposing);
        }
    }

    public class Usable<TUsingReturn> : IDisposable
    {
        private readonly Action<TUsingReturn> OnDisposing;
        public TUsingReturn Return { get; private set; }

        internal Usable(Func<TUsingReturn> onUsing, Action<TUsingReturn> onDisposing)
        {
            OnDisposing = onDisposing;
            Return = onUsing();
        }

        public void Dispose() => OnDisposing(Return);
    }
}
