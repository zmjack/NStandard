using System;

namespace NStandard
{
    public class Usable : IDisposable
    {
        private readonly Action OnDisposing;

        internal Usable(Action onUsing, Action onDisposing)
        {
            OnDisposing = onDisposing;
            onUsing();
        }

        public void Dispose() => OnDisposing();

        public static Usable Begin(Action onUsing, Action onDisposing) => new Usable(onUsing, onDisposing);
        public static Usable<TUsingReturn> Begin<TUsingReturn>(Func<TUsingReturn> onUsing, Action<TUsingReturn> onDisposing)
        {
            return new Usable<TUsingReturn>(onUsing, onDisposing);
        }
    }

    public class Usable<TUsingReturn> : IDisposable
    {
        private readonly Action<TUsingReturn> OnDisposing;
        public TUsingReturn Value { get; private set; }

        internal Usable(Func<TUsingReturn> onUsing, Action<TUsingReturn> onDisposing)
        {
            OnDisposing = onDisposing;
            Value = onUsing();
        }

        public void Dispose() => OnDisposing(Value);
    }
}
