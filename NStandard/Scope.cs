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
            var scopeType = typeof(Scope<TSelf>).GetGenericArguments()[0];
            if (GetType() != scopeType) throw new TypeLoadException($"Generic type `TSelf` must be defined as '{GetType().FullName}'.");

            var lockParser = new TypeLockParser(nameof(NStandard));
            var _lock = lockParser.ParseThreadLock(GetType());

            _lock.UseDoubleCheckLocking(() => Scopes is null, () =>
            {
                Scopes = new Stack<TSelf>();
            });
            Scopes.Push(this as TSelf);
        }

        /// <summary>
        /// Do not override this method. Use Disposing instead.
        /// </summary>
        public void Dispose() { Disposing(); Scopes.Pop(); }

        public virtual void Disposing() { }

        // Use TSelf to make sure the ThreadStatic attribute working correctly.
        /// <summary>
        /// Hint: The `Scopes` is null until it is initialized.
        /// </summary>
        [ThreadStatic]
        public static Stack<TSelf> Scopes;

        public static TSelf Current => (Scopes?.Count > 0 ? Scopes.Peek() : null) ?? null;
    }

}
