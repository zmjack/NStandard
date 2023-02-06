using NStandard.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace NStandard
{
    public static class Sync
    {
        public static Sync<TValue> From<TValue>(Expression<Func<TValue>> value) => new(value);
    }

    public interface ISync
    {
        event Action Noticing;
        ISync[] Dependencies { get; }
    }

    public sealed class Sync<TValue> : ISync, IDisposable
    {
        public delegate void UpdatingDelegate(TValue value);
        public delegate void ChangedDelegate(TValue value);

        public event UpdatingDelegate Updating;
        public event ChangedDelegate Changed;
        public event Action Noticing;

        private bool disposedValue;

        private readonly HashSet<ISync> _dependencyList = new();
        public ISync[] Dependencies => _dependencyList.ToArray();

        private readonly Func<TValue> _getValue;
        private TValue _value;

        public bool IsValueCreated { get; private set; }
        public void Update()
        {
            IsValueCreated = false;
            if (Updating is not null)
            {
                var value = Value;
                Updating?.Invoke(value);
            }
        }

        private TValue GetStoredValue() => _value;

        public Sync() : this(default(TValue)) { }
        public Sync(TValue value)
        {
            _value = value;
            IsValueCreated = true;
            _getValue = GetStoredValue;
        }

        internal Sync(Expression<Func<TValue>> getValue)
        {
            _getValue = getValue.Compile();
            CollectDependencies(getValue);
        }

        /// <summary>
        /// Sets the specified **ISync** as a dependency.
        /// </summary>
        /// <param name="dependency"></param>
        public void Watch(ISync dependency)
        {
            _dependencyList.Add(dependency);
            dependency.Noticing += Update;
        }

        /// <summary>
        /// Removes the specified **ISync** from the list of dependencies.
        /// </summary>
        /// <param name="dependency"></param>
        public void Unwatch(ISync dependency)
        {
            _dependencyList.Remove(dependency);
            dependency.Noticing -= Update;
        }

        public void CollectDependencies(Expression<Func<TValue>> getValue)
        {
            _dependencyList.Clear();

            void InnerCollectDependencies(ISync[] dependencies)
            {
                foreach (var dependency in dependencies)
                {
                    if (dependency.Dependencies.Length == 0)
                    {
                        Watch(dependency);
                    }
                    else InnerCollectDependencies(dependency.Dependencies);
                }
            }

            var collector = new DependencyCollector();
            collector.Collect(getValue, typeof(Sync<>));

            var dependencies = collector.GetDependencies().OfType<ISync>().ToArray();
            InnerCollectDependencies(dependencies);
        }

        public TValue Value
        {
            get
            {
                if (!IsValueCreated)
                {
                    _value = _getValue();
                    IsValueCreated = true;
                }
                return _value;
            }
            set
            {
                if (Dependencies.Length != 0) throw new InvalidOperationException("Cannot set a value for a sync object that has dependencies.");

                if (!_value.Equals(value))
                {
                    _value = value;
                    Changed?.Invoke(value);
                    Noticing?.Invoke();
                }
            }
        }

        /// <summary>
        /// Set value to Expired.
        /// </summary>
        public void Expire() => IsValueCreated = false;

        /// <summary>
        /// Cancel all subscriptions that depend on the object.
        /// </summary>
        public void Release() => Noticing = null;

        /// <summary>
        /// Subscribe to update notifications for all dependencies.
        /// </summary>
        public void Rebind()
        {
            foreach (var dependency in Dependencies)
            {
                Watch(dependency);
            }
        }

        public static implicit operator TValue(Sync<TValue> @this)
        {
            return @this.Value;
        }

        protected void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    foreach (var dependency in Dependencies)
                    {
                        dependency.Noticing -= Update;
                    }
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
}
