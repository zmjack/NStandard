﻿using System;
using System.Collections.Generic;

namespace NStandard;

/// <summary>
/// Cooperate with 'using' keyword to use thread safe <see cref="Scope{TSelf}"/>.
/// </summary>
/// <typeparam name="TSelf"></typeparam>
public abstract class Scope<TSelf> : IDisposable where TSelf : Scope<TSelf>
{
    protected Scope()
    {
        CheckAndThrow();
        Scopes.Push(this as TSelf);
    }

    private void CheckAndThrow()
    {
        var scopeType = typeof(Scope<TSelf>).GetGenericArguments()[0];
        if (GetType() != scopeType) throw new TypeLoadException($"Generic type `TSelf` must be defined as '{GetType().FullName}'.");
    }

    public virtual void Disposing() { }

    // Use TSelf to make sure the ThreadStatic attribute working correctly.
    [ThreadStatic]
    private static Stack<TSelf> _scopes;

    public static Stack<TSelf> Scopes
    {
        get
        {
            if (_scopes is null) _scopes = new Stack<TSelf>();
            return _scopes;
        }
    }

    public static TSelf Current => (Scopes.Count > 0 ? Scopes.Peek() : null) ?? null;

    private bool disposedValue;
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                Disposing();
                if (Scopes.Peek() == this) Scopes.Pop();
                else throw new InvalidOperationException("Scope must pop up in order.");
            }
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
