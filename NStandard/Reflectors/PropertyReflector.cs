using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NStandard
{
    public class PropertyReflector : Reflector
    {
        public readonly PropertyInfo FieldInfo;
        public object DeclaringObject;

        public PropertyReflector(PropertyInfo propertyInfo, object declaringObj, Type propertyType) : base(propertyType)
        {
            FieldInfo = propertyInfo;
            DeclaringObject = declaringObj;

            Object = Value;
        }

        public virtual object Value
        {
            get => FieldInfo.GetValue(DeclaringObject);
            set => FieldInfo.SetValue(DeclaringObject, value);
        }
    }

    public class PropertyReflector<T> : PropertyReflector
    {
        public PropertyReflector(PropertyInfo propertyInfo, object declaringObj) : base(propertyInfo, declaringObj, typeof(T)) { }

        public new T Value
        {
            get => (T)FieldInfo.GetValue(DeclaringObject);
            set => FieldInfo.SetValue(DeclaringObject, value);
        }
    }
}
