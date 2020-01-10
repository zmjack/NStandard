using System;
using System.Reflection;

namespace NStd
{
    public class Reflector
    {
        public object Object { get; internal set; }
        public Type Type { get; private set; }

        public Reflector(Type objType) => Type = objType;
        public Reflector(Type objType, object obj)
        {
            Object = obj;
            Type = objType;
        }

        public FieldReflector Field(string name) => Type.GetField(name)?.For(x => new FieldReflector(x, Object, x.FieldType));
        public FieldReflector Field(string name, Type type) => Type.GetField(name)?.For(x => new FieldReflector(x, Object, type));
        public FieldReflector<T> Field<T>(string name) => Type.GetField(name)?.For(x => new FieldReflector<T>(x, Object));
        public PropertyReflector Property(string name) => Type.GetProperty(name)?.For(x => new PropertyReflector(x, Object, x.PropertyType));
        public PropertyReflector Property(string name, Type type) => Type.GetProperty(name)?.For(x => new PropertyReflector(x, Object, type));
        public PropertyReflector<T> Property<T>(string name) => Type.GetProperty(name)?.For(x => new PropertyReflector<T>(x, Object));

        public FieldReflector Field(string name, BindingFlags bindingAttr) => Type.GetField(name, bindingAttr)?.For(x => new FieldReflector(x, Object, x.FieldType));
        public FieldReflector Field(string name, Type type, BindingFlags bindingAttr) => Type.GetField(name, bindingAttr)?.For(x => new FieldReflector(x, Object, type));
        public FieldReflector<T> Field<T>(string name, BindingFlags bindingAttr) => Type.GetField(name, bindingAttr)?.For(x => new FieldReflector<T>(x, Object));
        public PropertyReflector Property(string name, BindingFlags bindingAttr) => Type.GetProperty(name, bindingAttr)?.For(x => new PropertyReflector(x, Object, x.PropertyType));
        public PropertyReflector Property(string name, Type type, BindingFlags bindingAttr) => Type.GetProperty(name, bindingAttr)?.For(x => new PropertyReflector(x, Object, type));
        public PropertyReflector<T> Property<T>(string name, BindingFlags bindingAttr) => Type.GetProperty(name, bindingAttr)?.For(x => new PropertyReflector<T>(x, Object));

        public FieldReflector DeclaredField(string name) => Type.GetDeclaredField(name)?.For(x => new FieldReflector(x, Object, x.FieldType));
        public FieldReflector DeclaredField(string name, Type type) => Type.GetDeclaredField(name)?.For(x => new FieldReflector(x, Object, type));
        public FieldReflector<T> DeclaredField<T>(string name) => Type.GetDeclaredField(name)?.For(x => new FieldReflector<T>(x, Object));
        public PropertyReflector DeclaredProperty(string name) => Type.GetDeclaredProperty(name)?.For(x => new PropertyReflector(x, Object, x.PropertyType));
        public PropertyReflector DeclaredProperty(string name, Type type) => Type.GetDeclaredProperty(name)?.For(x => new PropertyReflector(x, Object, type));
        public PropertyReflector<T> DeclaredProperty<T>(string name) => Type.GetDeclaredProperty(name)?.For(x => new PropertyReflector<T>(x, Object));

        public MethodReflector Method(string name) => new MethodReflector(Type.GetMethod(name), Object);
        public MethodReflector MethodViaQualifiedName(string name) => new MethodReflector(Type.GetMethodViaQualifiedName(name), Object);
        public MethodReflector Method(string name, BindingFlags bindingAttr) => new MethodReflector(Type.GetMethod(name, bindingAttr), Object);
        public MethodReflector MethodViaQualifiedName(string name, BindingFlags bindingAttr) => new MethodReflector(Type.GetMethodViaQualifiedName(name, bindingAttr), Object);

        public MethodReflector ToStringMethod() => new MethodReflector(Type.GetToStringMethod(), Object);
        public MethodReflector GetHashCodeMethod() => new MethodReflector(Type.GetGetHashCodeMethod(), Object);

    }

}
