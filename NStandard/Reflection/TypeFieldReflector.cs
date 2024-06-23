using System;
using System.Reflection;

namespace NStandard.Reflection;

public class TypeFieldReflector(FieldInfo fieldInfo, Type type) : TypeReflector(type)
{
    public readonly FieldInfo FieldInfo = fieldInfo;

    public TypeFieldReflector(FieldInfo fieldInfo) : this(fieldInfo, fieldInfo.FieldType)
    {
    }

    public virtual object? GetValue(object? obj) => FieldInfo.GetValue(obj);
    public void SetValue(object obj, object? value) => FieldInfo.SetValue(obj, value);
}

public class TypeFieldReflector<T>(FieldInfo fieldInfo) : TypeFieldReflector(fieldInfo, typeof(T))
{
    public new T? GetValue(object obj) => (T?)base.GetValue(obj);
    public void SetValue(object obj, T? value) => base.SetValue(obj, value);
}
