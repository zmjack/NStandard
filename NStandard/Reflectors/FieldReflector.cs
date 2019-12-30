using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NStandard
{
    public class FieldReflector : Reflector
    {
        public readonly FieldInfo FieldInfo;

        public FieldReflector(FieldInfo fieldInfo, object obj) : base(obj)
        {
            FieldInfo = fieldInfo;
        }

        public virtual object Value
        {
            get => FieldInfo.GetValue(Object);
            set => FieldInfo.SetValue(Object, value);
        }
    }

    public class FieldReflector<T> : FieldReflector
    {
        public FieldReflector(FieldInfo fieldInfo, object obj) : base(fieldInfo, obj) { }

        public new T Value
        {
            get => (T)FieldInfo.GetValue(Object);
            set => FieldInfo.SetValue(Object, value);
        }
    }
}
