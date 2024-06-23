using System;
using System.Reflection;

namespace NStandard.Reflection;

public class TypePropertyReflector(PropertyInfo propertyInfo, Type type) : TypeReflector(type)
{
    public readonly PropertyInfo PropertyInfo = propertyInfo;

    public TypePropertyReflector(PropertyInfo propertyInfo) : this(propertyInfo, propertyInfo.PropertyType)
    {
    }

    public virtual object? GetValue(object? obj) => PropertyInfo.GetValue(obj!);
    public void SetValue(object? obj, object? value) => PropertyInfo.SetValue(obj!, value!);
}

public class TypePropertyReflector<T>(PropertyInfo propertyInfo) : TypePropertyReflector(propertyInfo, typeof(T))
{
    public new T? GetValue(object? obj) => (T?)base.GetValue(obj);
    public void SetValue(object? obj, T value) => base.SetValue(obj!, value);
}
