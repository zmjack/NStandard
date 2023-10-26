using System;

namespace NStandard;

public class Usable : IDisposable
{
    private readonly Action OnDisposing;

    internal Usable(Action onUsing, Action onDisposing)
    {
        OnDisposing = onDisposing;
        onUsing();
    }

    public static Usable Begin(Action onUsing, Action onDisposing) => new(onUsing, onDisposing);
    public static Usable<TUsingReturn> Begin<TUsingReturn>(Func<TUsingReturn> onUsing, Action<TUsingReturn> onDisposing)
    {
        return new Usable<TUsingReturn>(onUsing, onDisposing);
    }

    private bool disposedValue;
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                OnDisposing();
            }
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
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

    private bool disposedValue;
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                OnDisposing(Value);
            }
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
