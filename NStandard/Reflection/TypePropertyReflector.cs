using System;
using System.Reflection;

namespace NStandard.Reflection
{
    public class TypePropertyReflector : TypeReflector
    {
        public readonly PropertyInfo PropertyInfo;

        public TypePropertyReflector(Type propertyType, PropertyInfo propertyInfo) : base(propertyType)
        {
            PropertyInfo = propertyInfo;
        }

        public virtual object GetValue(object obj) => PropertyInfo.GetValue(obj);
        public void SetValue(object obj, object value) => PropertyInfo.SetValue(obj, value);
    }

    public class TypePropertyReflector<T> : TypePropertyReflector
    {
        public TypePropertyReflector(PropertyInfo propertyInfo) : base(typeof(T), propertyInfo) { }

        public new T GetValue(object obj) => (T)base.GetValue(obj);
        public void SetValue(object obj, T value) => base.SetValue(obj, value);
    }
}
