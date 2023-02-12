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
        public delegate void UpdatingHandler(object value);
        public delegate void ChangedHandler(object value);

        public static State<TValue> Use<TValue>() => new();
        public static State<TValue> Use<TValue>(TValue value) => new(value);
        public static State<TValue> From<TValue>(Expression<Func<TValue>> value) => new(value);
    }

    public interface IState
    {
        event State.UpdatingHandler Updating;
        event State.ChangedHandler Changed;
        event Action Noticing;

        IState[] Dependencies { get; }
        object Value { get; set; }
        Type ValueType { get; }
    }

    public sealed class State<TValue> : IState, IDisposable
    {
        public delegate void UpdatingHandler(TValue value);
        public delegate void ChangedHandler(TValue value);

        public event Action Noticing;

        public event UpdatingHandler Updating;
        private event State.UpdatingHandler IStateUpdating;
        event State.UpdatingHandler IState.Updating
        {
            add => IStateUpdating += value;
            remove => IStateUpdating -= value;
        }

        public event ChangedHandler Changed;
        private event State.ChangedHandler IStateChanged;
        event State.ChangedHandler IState.Changed
        {
            add => IStateChanged += value;
            remove => IStateChanged -= value;
        }

        private bool disposedValue;

        private readonly HashSet<IState> _dependencyList = new();
        public IState[] Dependencies => _dependencyList.ToArray();

        private readonly Func<TValue> _getValue;
        private TValue _value;

#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NET40_OR_GREATER
        private readonly Lazy<Type> _valueType = new(() => typeof(TValue));
        public Type ValueType => _valueType.Value;
#else
        public Type ValueType => typeof(TValue);
#endif

        public bool IsValueCreated { get; private set; }
        public void Update()
        {
            IsValueCreated = false;
            if (Updating is not null || IStateUpdating is not null)
            {
                var value = Value;
                Updating?.Invoke(value);
                IStateUpdating?.Invoke(value);
            }
        }

        private TValue GetStoredValue() => _value;

        internal State() : this(default(TValue)) { }
        internal State(TValue value)
        {
            _value = value;
            IsValueCreated = true;
            _getValue = GetStoredValue;
        }

        internal State(Expression<Func<TValue>> getValue)
        {
            _getValue = getValue.Compile();
            CollectDependencies(getValue);
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

        public void CollectDependencies(Expression<Func<TValue>> getValue)
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
                if (Dependencies.Length != 0) throw new InvalidOperationException("Cannot set a value for a state object that has dependencies.");

                if (!_value.Equals(value))
                {
                    _value = value;

                    Changed?.Invoke(value);
                    IStateChanged?.Invoke(value);

                    Noticing?.Invoke();
                }
            }
        }

        object IState.Value
        {
            get => Value;
            set => Value = (TValue)value;
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

        public static implicit operator TValue(State<TValue> @this)
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
                    IStateUpdating = null;

                    Changed = null;
                    IStateChanged = null;

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
