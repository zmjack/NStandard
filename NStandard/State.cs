using NStandard.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace NStandard
{
    public class State
    {
        public delegate void ValueReceivedHandler<T>(T value);

        public static State<TValue> Use<TValue>() => new();
        public static State<TValue> Use<TValue>(TValue value) => new(value);
        public static State<TValue> From<TValue>(Expression<Func<TValue>> value) => new(value);
    }

    public interface IState
    {
        event State.ValueReceivedHandler<object> Updating;
        event State.ValueReceivedHandler<object> Changed;
        event Action Noticing;

        IState[] Dependencies { get; }
        object Value { get; set; }
        Type ValueType { get; }

        bool CanSetValue { get; }
    }

    public sealed class State<T> : IState, IDisposable
    {
        public event Action Noticing;

        public event State.ValueReceivedHandler<T> Updating;
        private event State.ValueReceivedHandler<object> ValueUpdating;
        event State.ValueReceivedHandler<object> IState.Updating
        {
            add => ValueUpdating += value;
            remove => ValueUpdating -= value;
        }

        public event State.ValueReceivedHandler<T> Changed;
        private event State.ValueReceivedHandler<object> ValueChanged;
        event State.ValueReceivedHandler<object> IState.Changed
        {
            add => ValueChanged += value;
            remove => ValueChanged -= value;
        }

        private bool disposedValue;

        private readonly HashSet<IState> _dependencyList = new();
        public IState[] Dependencies => _dependencyList.ToArray();

        private readonly Func<T> _getValue;
        private T _value;

#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NET40_OR_GREATER
        private readonly Lazy<Type> _valueType = new(() => typeof(T));
        public Type ValueType => _valueType.Value;
#else
        public Type ValueType => typeof(T);
#endif

        public bool IsValueCreated { get; private set; }
        public void Update()
        {
            IsValueCreated = false;
            if (Updating is not null || ValueUpdating is not null)
            {
                var value = Value;
                Updating?.Invoke(value);
                ValueUpdating?.Invoke(value);
            }
        }

        public bool CanSetValue { get; }
        private T GetStoredValue() => _value;

        internal State() : this(default(T)) { }
        internal State(T value)
        {
            IsValueCreated = true;
            CanSetValue = true;

            _value = value;
            _getValue = GetStoredValue;
        }

        internal State(Expression<Func<T>> getValue)
        {
            CanSetValue = false;

            CollectDependencies(getValue);
            _getValue = getValue.Compile();
        }

        /// <summary>
        /// Sets the specified <see cref="IState"/> as a dependency.
        /// </summary>
        /// <param name="dependency"></param>
        public void Watch(IState dependency)
        {
            _dependencyList.Add(dependency);
            dependency.Noticing += Update;
        }

        /// <summary>
        /// Removes the specified <see cref="IState"/> from the list of dependencies.
        /// </summary>
        /// <param name="dependency"></param>
        public void Unwatch(IState dependency)
        {
            _dependencyList.Remove(dependency);
            dependency.Noticing -= Update;
        }

        public void CollectDependencies(Expression<Func<T>> getValue)
        {
            _dependencyList.Clear();

            void InnerCollectDependencies(IState[] dependencies)
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
            collector.Collect(getValue, typeof(State<>));

            var dependencies = collector.GetDependencies().OfType<IState>().ToArray();
            InnerCollectDependencies(dependencies);
        }

        public T Value
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
                if (!CanSetValue) throw new InvalidOperationException("Cannot set value for state which is calculated from other object.");

                if (!_value.Equals(value))
                {
                    _value = value;

                    Changed?.Invoke(value);
                    ValueChanged?.Invoke(value);
                    Noticing?.Invoke();
                }
            }
        }

        object IState.Value
        {
            get => Value;
            set => Value = (T)value;
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

        public static implicit operator T(State<T> @this)
        {
            return @this.Value;
        }

        protected void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Updating = null;
                    Changed = null;

                    ValueUpdating = null;
                    ValueChanged = null;

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
