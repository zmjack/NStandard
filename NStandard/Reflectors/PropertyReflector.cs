using System;
using System.Reflection;

namespace NStandard
{
    public class PropertyReflector : Reflector
    {
        public readonly PropertyInfo PropertyInfo;
        public readonly object DeclaringObject;

        public PropertyReflector(Type propertyType, PropertyInfo propertyInfo, object declaringObj) : base(propertyType, declaringObj)
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
        public virtual object GetValue() => PropertyInfo.GetValue(DeclaringObject);
        public void SetValue(object value) => PropertyInfo.SetValue(DeclaringObject, value);
    }

    public class PropertyReflector<T> : PropertyReflector
    {
        public PropertyReflector(PropertyInfo propertyInfo, object declaringObj) : base(typeof(T), propertyInfo, declaringObj) { }

        public new T Value
        {
            get => base.Value.For(x => x is null ? default : (T)x);
            set => base.Value = value;
        }

        public new T GetValue() => (T)base.GetValue();
        public void SetValue(T value) => base.SetValue(value);
    }
}
