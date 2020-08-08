using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;

namespace NStandard
{
    public static class StaticPoint
    {
        public class InnerChangedValue
        {
            public object OldValue;
            public object CurrentValue;

            internal InnerChangedValue() { }
        }

        public class ChangedValue<T>
        {
            private InnerChangedValue _InnerChangedValue;

            public ChangedValue(InnerChangedValue innerChangedValue)
            {
                _InnerChangedValue = innerChangedValue;
            }

            public T OldValue
            {
                get => (T)_InnerChangedValue.OldValue;
                set => _InnerChangedValue.OldValue = value;
            }
            public T CurrentValue
            {
                get => (T)_InnerChangedValue.CurrentValue;
                set => _InnerChangedValue.CurrentValue = value;
            }
        }

        public static Dictionary<string, InnerChangedValue> Points { get; } = new Dictionary<string, InnerChangedValue>();

        public static ChangedValue<T> Save<T>(string id, T obj)
        {
            lock (string.Intern($"{nameof(NStandard)}.{nameof(StaticPoint)}:{id}"))
            {
                InnerChangedValue innerChangedValue;
                if (!Points.ContainsKey(id))
                {
                    innerChangedValue = new InnerChangedValue
                    {
                        OldValue = typeof(T).CreateDefault(),
                        CurrentValue = obj
                    };
                    Points.Add(id, innerChangedValue);
                }
                else
                {
                    innerChangedValue = Points[id];
                    if (innerChangedValue.CurrentValue.GetType() == typeof(T))
                    {
                        innerChangedValue.OldValue = innerChangedValue.CurrentValue;
                        innerChangedValue.CurrentValue = obj;
                    }
                    else throw new ArgumentException($"The stored type is `{innerChangedValue.CurrentValue.GetType()}`, and can not match the object(`{typeof(T).FullName}`) to be saved.", nameof(obj));
                }

                return new ChangedValue<T>(innerChangedValue);
            }
        }

    }
}
