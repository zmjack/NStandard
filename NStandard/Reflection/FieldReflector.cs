﻿using System;
using System.Reflection;

namespace NStandard.Reflection
{
    public class FieldReflector : Reflector
    {
        public readonly FieldInfo FieldInfo;
        public readonly object DeclaringObject;

        public FieldReflector(Type fieldType, FieldInfo fieldInfo, object declaringObj) : base(fieldType, declaringObj)
        {
            FieldInfo = fieldInfo;
            DeclaringObject = declaringObj;

            Object = Value;
        }

        public virtual object Value
        {
            get => DeclaringObject?.Pipe(FieldInfo.GetValue);
            set
            {
                if (!(DeclaringObject is null))
                    FieldInfo.SetValue(DeclaringObject, value);
                else throw new AccessViolationException();
            }
        }
        public virtual object GetValue() => FieldInfo.GetValue(DeclaringObject);
        public void SetValue(object value) => FieldInfo.SetValue(DeclaringObject, value);
    }

    public class FieldReflector<T> : FieldReflector
    {
        public FieldReflector(FieldInfo fieldInfo, object declaringObj) : base(typeof(T), fieldInfo, declaringObj) { }

        public new T Value
        {
            get => base.Value is null ? default : (T)base.Value;
            set => base.Value = value;
        }
        public new T GetValue() => (T)base.GetValue();
        public void SetValue(T value) => base.SetValue(value);
    }
}
