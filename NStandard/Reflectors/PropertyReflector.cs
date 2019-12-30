using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NStandard
{
    public class PropertyReflector : Reflector
    {
        public readonly PropertyInfo PropertyInfo;
        public object DeclaringObject;

        public PropertyReflector(PropertyInfo propertyInfo, object declaringObj, Type propertyType) : base(propertyType)
        {
            PropertyInfo = propertyInfo;
            DeclaringObject = declaringObj;

            Object = Value;
        }

        public virtual object Value
        {
            get => PropertyInfo.GetValue(DeclaringObject);
            set => PropertyInfo.SetValue(DeclaringObject, value);
        }
    }

    public class PropertyReflector<T> : PropertyReflector
    {
        public PropertyReflector(PropertyInfo propertyInfo, object declaringObj) : base(propertyInfo, declaringObj, typeof(T)) { }

        public new T Value
        {
            get => (T)PropertyInfo.GetValue(DeclaringObject);
            set => PropertyInfo.SetValue(DeclaringObject, value);
        }
    }
}
