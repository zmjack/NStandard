using System;
using System.Reflection;

namespace NStandard
{
    public class FieldReflector : Reflector
    {
        public readonly FieldInfo FieldInfo;
        public object DeclaringObject;

        public FieldReflector(FieldInfo fieldInfo, object declaringObj, Type fieldType) : base(fieldType)
        {
            FieldInfo = fieldInfo;
            DeclaringObject = declaringObj;

            Object = Value;
        }

        public virtual object Value
        {
            get => FieldInfo.GetValue(DeclaringObject);
            set => FieldInfo.SetValue(DeclaringObject, value);
        }
    }

    public class FieldReflector<T> : FieldReflector
    {
        public FieldReflector(FieldInfo fieldInfo, object declaringObj) : base(fieldInfo, declaringObj, typeof(T)) { }

        public new T Value
        {
            get => (T)FieldInfo.GetValue(DeclaringObject);
            set => FieldInfo.SetValue(DeclaringObject, value);
        }
    }
}
