using NStandard.Locks;
using System;
using System.Collections.Generic;

namespace NStandard
{
    /// <summary>
    /// Cooperate with 'using' keyword to use thread safe <see cref="Scope{TSelf}"/>.
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    public abstract class Scope<TSelf> : IDisposable
        where TSelf : Scope<TSelf>
    {
        protected Scope()
        {
            var lockParser = new TypeLockParser(nameof(NStandard));
            var _lock = lockParser.ParseThreadLock(GetType());

            _lock.UseDoubleCheckLocking(() => Scopes is null, () =>
            {
                Scopes = new Stack<Scope<TSelf>>();
            });
            Scopes.Push(this);
        }
        public void Dispose() { Disposing(); Scopes.Pop(); }

        public virtual void Disposing() { }

        // Use TSelf to make sure the ThreadStatic attribute working correctly.
        [ThreadStatic]
        public static Stack<Scope<TSelf>> Scopes;

        public static Scope<TSelf> Current => Scopes?.Peek();

    }

    /// <summary>
    /// Cooperate with 'using' keyword to use thread safe <see cref="Scope{T, TSelf}"/>.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TSelf"></typeparam>
    public abstract class Scope<TModel, TSelf> : IDisposable
        where TSelf : Scope<TModel, TSelf>
    {
        public TModel Model { get; protected set; }

        protected Scope(TModel model)
        {
            var lockParser = new TypeLockParser(nameof(NStandard));
            var _lock = lockParser.ParseThreadLock(GetType());

            _lock.UseDoubleCheckLocking(() => Scopes is null, () =>
            {
                Scopes = new Stack<Scope<TModel, TSelf>>();
            });
            Model = model;
            Scopes.Push(this);
        }
        public void Dispose() { Disposing(); Scopes.Pop(); }

        public virtual void Disposing() { }

        // Use TSelf to make sure the ThreadStatic attribute working correctly.
        [ThreadStatic]
        public static Stack<Scope<TModel, TSelf>> Scopes;

        public static Scope<TModel, TSelf> Current => (Scopes?.Count > 0 ? Scopes.Peek() : null) ?? null;

    }
}
