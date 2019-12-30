using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NStandard
{
    public class PropertyReflector : Reflector
    {
        public readonly PropertyInfo FieldInfo;

        public PropertyReflector(PropertyInfo fieldInfo, object obj) : base(obj)
        {
            FieldInfo = fieldInfo;
        }

        public virtual object Value
        {
            get => FieldInfo.GetValue(Object);
            set => FieldInfo.SetValue(Object, value);
        }
    }

    public class PropertyReflector<T> : PropertyReflector
    {
        public PropertyReflector(PropertyInfo fieldInfo, object obj) : base(fieldInfo, obj) { }

        public new T Value
        {
            get => (T)FieldInfo.GetValue(Object);
            set => FieldInfo.SetValue(Object, value);
        }
    }
}
