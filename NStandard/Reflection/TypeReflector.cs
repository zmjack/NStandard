using NStandard.Static;
using System.Reflection;

namespace NStandard.Reflection;

public class TypeReflector(Type objType)
{
    private static ArgumentException FieldNotFound(string name) => throw new ArgumentException("The field can not be found.", name);
    private static ArgumentException PropertyNotFound(string name) => throw new ArgumentException("The property can not be found.", name);
    private static ArgumentException MethodNotFound(string name) => throw new ArgumentException("The method can not be found.", name);

    public Type Type { get; private set; } = objType;

    private FieldInfo GetField(string name)
    {
        return Type.GetField(name) ?? throw FieldNotFound(nameof(name));
    }
    private FieldInfo GetField(string name, BindingFlags bindingAttr)
    {
        return Type.GetField(name, bindingAttr) ?? throw FieldNotFound(nameof(name));
    }
    public TypeFieldReflector Field(string name) => new(GetField(name));
    public TypeFieldReflector<T> Field<T>(string name) => new(GetField(name));
    public TypeFieldReflector Field(string name, BindingFlags bindingAttr) => new(GetField(name, bindingAttr));
    public TypeFieldReflector<T> Field<T>(string name, BindingFlags bindingAttr) => new(GetField(name, bindingAttr));
    public TypeFieldReflector DeclaredField(string name) => new(GetField(name, TypeEx.DeclaredOnlyLookup));
    public TypeFieldReflector<T> DeclaredField<T>(string name) => new(GetField(name, TypeEx.DeclaredOnlyLookup));

    private PropertyInfo GetProperty(string name)
    {
        return Type.GetProperty(name) ?? throw PropertyNotFound(nameof(name));
    }
    private PropertyInfo GetProperty(string name, BindingFlags bindingAttr)
    {
        return Type.GetProperty(name, bindingAttr) ?? throw PropertyNotFound(nameof(name));
    }
    public TypePropertyReflector Property(string name) => new(GetProperty(name));
    public TypePropertyReflector<T> Property<T>(string name) => new(GetProperty(name));
    public TypePropertyReflector Property(string name, BindingFlags bindingAttr) => new(GetProperty(name, bindingAttr));
    public TypePropertyReflector<T> Property<T>(string name, BindingFlags bindingAttr) => new(GetProperty(name, bindingAttr));
    public TypePropertyReflector DeclaredProperty(string name) => new(GetProperty(name, TypeEx.DeclaredOnlyLookup));
    public TypePropertyReflector<T> DeclaredProperty<T>(string name) => new(GetProperty(name, TypeEx.DeclaredOnlyLookup));

    private MethodInfo GetMethod(string name)
    {
        return Type.GetMethod(name) ?? throw MethodNotFound(nameof(name));
    }
    private MethodInfo GetMethod(string name, BindingFlags bindingAttr)
    {
        return Type.GetMethod(name, bindingAttr) ?? throw MethodNotFound(nameof(name));
    }
    public TypeMethodReflector Method(string name) => new(GetMethod(name));
    public TypeMethodReflector Method(string name, BindingFlags bindingAttr) => new(GetMethod(name, bindingAttr));
    public TypeMethodReflector DeclaredMethod(string name) => new(GetMethod(name, TypeEx.DeclaredOnlyLookup));

    public TypeMethodReflector MethodViaQualifiedName(string name) => new(Type.GetMethodViaQualifiedName(name));
    public TypeMethodReflector MethodViaQualifiedName(string name, BindingFlags bindingAttr) => new(Type.GetMethodViaQualifiedName(name, bindingAttr));
    public TypeMethodReflector DeclaredMethodViaQualifiedName(string name) => new(Type.GetMethodViaQualifiedName(name, TypeEx.DeclaredOnlyLookup));

    public TypeMethodReflector ToStringMethod() => new(Type.GetToStringMethod());
    public TypeMethodReflector GetHashCodeMethod() => new(Type.GetGetHashCodeMethod());

}
