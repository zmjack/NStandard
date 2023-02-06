using System;
using System.Reflection;

namespace NStandard.Reflection
{
    public class TypeFieldReflector : TypeReflector
    {
        public readonly FieldInfo FieldInfo;

        public TypeFieldReflector(Type fieldType, FieldInfo fieldInfo) : base(fieldType)
        {
            FieldInfo = fieldInfo;
        }

        public virtual object GetValue(object obj) => FieldInfo.GetValue(obj);
        public void SetValue(object obj, object value) => FieldInfo.SetValue(obj, value);
    }

    public class TypeFieldReflector<T> : TypeFieldReflector
    {
        public TypeFieldReflector(FieldInfo fieldInfo) : base(typeof(T), fieldInfo) { }

        public new T GetValue(object obj) => (T)base.GetValue(obj);
        public void SetValue(object obj, T value) => base.SetValue(obj, value);
    }

}
