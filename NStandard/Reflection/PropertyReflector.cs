using System.Reflection;

namespace NStandard.Reflection;

public class PropertyReflector : Reflector
{
    public readonly PropertyInfo PropertyInfo;
    public readonly object? DeclaringObject;

    public PropertyReflector(PropertyInfo propertyInfo, object? declaringObj) : this(propertyInfo, propertyInfo.PropertyType, declaringObj) { }
    public PropertyReflector(PropertyInfo propertyInfo, Type type, object? declaringObj) : base(type, declaringObj)
    {
        PropertyInfo = propertyInfo;
        DeclaringObject = declaringObj;
        Object = Value;
    }

    public virtual object? Value
    {
        get => DeclaringObject?.Pipe(PropertyInfo.GetValue);
        set
        {
            if (DeclaringObject is not null)
            {
                PropertyInfo.SetValue(DeclaringObject, value!);
            }
            else throw new AccessViolationException();
        }
    }
    public virtual object? GetValue() => PropertyInfo.GetValue(DeclaringObject!);
    public void SetValue(object? value) => PropertyInfo.SetValue(DeclaringObject!, value!);
}

public class PropertyReflector<T>(PropertyInfo propertyInfo, object? declaringObj) : PropertyReflector(propertyInfo, typeof(T), declaringObj)
{
    public new T? Value
    {
        get => base.Value is null ? default : (T)base.Value;
        set => base.Value = value;
    }

    public new T? GetValue() => (T?)base.GetValue();
    public void SetValue(T? value) => base.SetValue(value);
}
