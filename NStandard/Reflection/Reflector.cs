using NStandard.Static;
using System.Reflection;

namespace NStandard.Reflection;

public class Reflector(Type objType, object? obj)
{
    private static ArgumentException FieldNotFound(string name) => throw new ArgumentException("The field can not be found.", name);
    private static ArgumentException PropertyNotFound(string name) => throw new ArgumentException("The property can not be found.", name);
    private static ArgumentException MethodNotFound(string name) => throw new ArgumentException("The method can not be found.", name);

    public object? Object { get; internal set; } = obj;
    public Type Type { get; private set; } = objType;

    private FieldInfo GetField(string name)
    {
        return Type.GetField(name) ?? throw FieldNotFound(nameof(name));
    }
    private FieldInfo GetField(string name, BindingFlags bindingAttr)
    {
        return Type.GetField(name, bindingAttr) ?? throw FieldNotFound(nameof(name));
    }

    public FieldReflector Field(string name) => new(GetField(name), Object);
    public FieldReflector<T> Field<T>(string name) => new(GetField(name), Object);
    public FieldReflector Field(string name, BindingFlags bindingAttr) => new(GetField(name, bindingAttr), Object);
    public FieldReflector<T> Field<T>(string name, BindingFlags bindingAttr) => new(GetField(name, bindingAttr), Object);

    public FieldReflector DeclaredField(string name) => new(GetField(name, TypeEx.DeclaredOnlyLookup), Object);
    public FieldReflector<T> DeclaredField<T>(string name) => new(GetField(name, TypeEx.DeclaredOnlyLookup), Object);

    private PropertyInfo GetProperty(string name)
    {
        return Type.GetProperty(name) ?? throw PropertyNotFound(nameof(name));
    }
    private PropertyInfo GetProperty(string name, BindingFlags bindingAttr)
    {
        return Type.GetProperty(name, bindingAttr) ?? throw PropertyNotFound(nameof(name));
    }
    public PropertyReflector Property(string name) => new(GetProperty(name), Object);
    public PropertyReflector<T> Property<T>(string name) => new(GetProperty(name), Object);
    public PropertyReflector Property(string name, BindingFlags bindingAttr) => new(GetProperty(name, bindingAttr), Object);
    public PropertyReflector<T> Property<T>(string name, BindingFlags bindingAttr) => new(GetProperty(name, bindingAttr), Object);

    public PropertyReflector DeclaredProperty(string name) => new(GetProperty(name, TypeEx.DeclaredOnlyLookup), Object);
    public PropertyReflector<T> DeclaredProperty<T>(string name) => new(GetProperty(name, TypeEx.DeclaredOnlyLookup), Object);

    private MethodInfo GetMethod(string name)
    {
        return Type.GetMethod(name) ?? throw MethodNotFound(nameof(name));
    }
    private MethodInfo GetMethod(string name, BindingFlags bindingAttr)
    {
        return Type.GetMethod(name, bindingAttr) ?? throw MethodNotFound(nameof(name));
    }
    public MethodReflector Method(string name) => new(GetMethod(name), Object);
    public MethodReflector Method(string name, BindingFlags bindingAttr) => new(GetMethod(name, bindingAttr), Object);
    public MethodReflector DeclaredMethod(string name) => new(GetMethod(name, TypeEx.DeclaredOnlyLookup), Object);

    public MethodReflector MethodViaQualifiedName(string name) => new(Type.GetMethodViaQualifiedName(name), Object);
    public MethodReflector MethodViaQualifiedName(string name, BindingFlags bindingAttr) => new(Type.GetMethodViaQualifiedName(name, bindingAttr), Object);
    public MethodReflector DeclaredMethodViaQualifiedName(string name) => new(Type.GetMethodViaQualifiedName(name, TypeEx.DeclaredOnlyLookup), Object);

    public MethodReflector ToStringMethod() => new(Type.GetToStringMethod(), Object);
    public MethodReflector GetHashCodeMethod() => new(Type.GetGetHashCodeMethod(), Object);

}
