using System;
using System.Reflection;

namespace NStandard
{
    public class FieldReflector : Reflector
    {
        public readonly FieldInfo FieldInfo;
        public readonly object DeclaringObject;

        public FieldReflector(FieldInfo fieldInfo, object declaringObj, Type fieldType) : base(fieldType)
        {
            FieldInfo = fieldInfo;
            DeclaringObject = declaringObj;

            Object = Value;
        }

        public virtual object Value
        {
            get => DeclaringObject?.For(x => FieldInfo.GetValue(x));
            set
            {
                if (!(DeclaringObject is null))
                    FieldInfo.SetValue(DeclaringObject, value);
                else throw new AccessViolationException();
            }
        }
        public virtual object GetValue(object obj) => FieldInfo.GetValue(obj);
        public void SetValue(object obj, object value) => FieldInfo.SetValue(obj, value);
    }

    public class FieldReflector<T> : FieldReflector
    {
        public FieldReflector(FieldInfo fieldInfo, object declaringObj) : base(fieldInfo, declaringObj, typeof(T)) { }

        public new T Value
        {
            get => base.Value.For(x => x is null ? default : (T)x);
            set => base.Value = value;
        }
        public new T GetValue(object obj) => (T)base.GetValue(obj);
        public void SetValue(object obj, T value) => base.SetValue(obj, value);
    }
}
