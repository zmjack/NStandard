using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NStandard
{
    public class PropertyReflector : Reflector
    {
        public readonly PropertyInfo FieldInfo;
        public object DeclaredObject;

        public PropertyReflector(PropertyInfo fieldInfo, object declaredObject) : base(fieldInfo.GetValue(declaredObject))
        {
            FieldInfo = fieldInfo;
            DeclaredObject = declaredObject;
        }

        public virtual object Value
        {
            get => FieldInfo.GetValue(DeclaredObject);
            set => FieldInfo.SetValue(DeclaredObject, value);
        }
    }

    public class PropertyReflector<T> : PropertyReflector
    {
        public PropertyReflector(PropertyInfo fieldInfo, object declaredObject) : base(fieldInfo, declaredObject) { }

        public new T Value
        {
            get => (T)FieldInfo.GetValue(DeclaredObject);
            set => FieldInfo.SetValue(DeclaredObject, value);
        }
    }
}
