using System;
using System.Reflection;

namespace NStandard
{
    public class PropertyReflector : Reflector
    {
        public readonly PropertyInfo PropertyInfo;
        public readonly object DeclaringObject;

        public PropertyReflector(PropertyInfo propertyInfo, object declaringObj, Type propertyType) : base(propertyType)
        {
            PropertyInfo = propertyInfo;
            DeclaringObject = declaringObj;

            Object = Value;
        }

        public virtual object Value
        {
            get => DeclaringObject?.For(x => PropertyInfo.GetValue(x));
            set
            {
                if (!(DeclaringObject is null))
                    PropertyInfo.SetValue(DeclaringObject, value);
                else throw new AccessViolationException();
            }
        }
        public virtual object GetValue(object obj) => PropertyInfo.GetValue(obj);
        public void SetValue(object obj, object value) => PropertyInfo.SetValue(obj, value);
    }

    public class PropertyReflector<T> : PropertyReflector
    {
        public PropertyReflector(PropertyInfo propertyInfo, object declaringObj) : base(propertyInfo, declaringObj, typeof(T)) { }

        public new T Value
        {
            get => base.Value.For(x => x is null ? default : (T)x);
            set => base.Value = value;
        }

        public new T GetValue(object obj) => (T)base.GetValue(obj);
        public new void SetValue(object obj, T value) => base.SetValue(obj, value);
    }
}
