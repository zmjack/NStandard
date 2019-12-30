using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NStandard
{
    public class FieldReflector : Reflector
    {
        public readonly FieldInfo FieldInfo;
        public object DeclaredObject;

        public FieldReflector(FieldInfo fieldInfo, object declaredObject) : base(fieldInfo.GetValue(declaredObject))
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

    public class FieldReflector<T> : FieldReflector
    {
        public FieldReflector(FieldInfo fieldInfo, object declaredObject) : base(fieldInfo, declaredObject) { }

        public new T Value
        {
            get => (T)FieldInfo.GetValue(DeclaredObject);
            set => FieldInfo.SetValue(DeclaredObject, value);
        }
    }
}
