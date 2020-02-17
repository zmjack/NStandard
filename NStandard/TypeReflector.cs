using System;
using System.Reflection;

namespace NStandard
{
    public class TypeReflector
    {
        public Type Type { get; private set; }

        public TypeReflector(Type objType) => Type = objType;

        public TypeFieldReflector Field(string name) => Type.GetField(name)?.For(x => new TypeFieldReflector(x.FieldType, x));
        public TypeFieldReflector Field(string name, Type type) => Type.GetField(name)?.For(x => new TypeFieldReflector(type, x));
        public TypeFieldReflector<T> Field<T>(string name) => Type.GetField(name)?.For(x => new TypeFieldReflector<T>(x));
        public TypeFieldReflector Field(string name, BindingFlags bindingAttr) => Type.GetField(name, bindingAttr)?.For(x => new TypeFieldReflector(x.FieldType, x));
        public TypeFieldReflector Field(string name, Type type, BindingFlags bindingAttr) => Type.GetField(name, bindingAttr)?.For(x => new TypeFieldReflector(type, x));
        public TypeFieldReflector<T> Field<T>(string name, BindingFlags bindingAttr) => Type.GetField(name, bindingAttr)?.For(x => new TypeFieldReflector<T>(x));
        public TypeFieldReflector DeclaredField(string name) => Type.GetDeclaredField(name)?.For(x => new TypeFieldReflector(x.FieldType, x));
        public TypeFieldReflector DeclaredField(string name, Type type) => Type.GetDeclaredField(name)?.For(x => new TypeFieldReflector(type, x));
        public TypeFieldReflector<T> DeclaredField<T>(string name) => Type.GetDeclaredField(name)?.For(x => new TypeFieldReflector<T>(x));


        public TypePropertyReflector Property(string name) => Type.GetProperty(name)?.For(x => new TypePropertyReflector(x.PropertyType, x));
        public TypePropertyReflector Property(string name, Type type) => Type.GetProperty(name)?.For(x => new TypePropertyReflector(type, x));
        public TypePropertyReflector<T> Property<T>(string name) => Type.GetProperty(name)?.For(x => new TypePropertyReflector<T>(x));
        public TypePropertyReflector Property(string name, BindingFlags bindingAttr) => Type.GetProperty(name, bindingAttr)?.For(x => new TypePropertyReflector(x.PropertyType, x));
        public TypePropertyReflector Property(string name, Type type, BindingFlags bindingAttr) => Type.GetProperty(name, bindingAttr)?.For(x => new TypePropertyReflector(type, x));
        public TypePropertyReflector<T> Property<T>(string name, BindingFlags bindingAttr) => Type.GetProperty(name, bindingAttr)?.For(x => new TypePropertyReflector<T>(x));
        public TypePropertyReflector DeclaredProperty(string name) => Type.GetDeclaredProperty(name)?.For(x => new TypePropertyReflector(x.PropertyType, x));
        public TypePropertyReflector DeclaredProperty(string name, Type type) => Type.GetDeclaredProperty(name)?.For(x => new TypePropertyReflector(type, x));
        public TypePropertyReflector<T> DeclaredProperty<T>(string name) => Type.GetDeclaredProperty(name)?.For(x => new TypePropertyReflector<T>(x));

        public TypeMethodReflector Method(string name) => new TypeMethodReflector(Type.GetMethod(name));
        public TypeMethodReflector Method(string name, BindingFlags bindingAttr) => new TypeMethodReflector(Type.GetMethod(name, bindingAttr));
        public TypeMethodReflector DeclaredMethod(string name) => new TypeMethodReflector(Type.GetDeclaredMethod(name));

        public TypeMethodReflector MethodViaQualifiedName(string name) => new TypeMethodReflector(Type.GetMethodViaQualifiedName(name));
        public TypeMethodReflector MethodViaQualifiedName(string name, BindingFlags bindingAttr) => new TypeMethodReflector(Type.GetMethodViaQualifiedName(name, bindingAttr));
        public TypeMethodReflector DeclaredMethodViaQualifiedName(string name) => new TypeMethodReflector(Type.GetDeclaredMethodViaQualifiedName(name));

        public TypeMethodReflector ToStringMethod() => new TypeMethodReflector(Type.GetToStringMethod());
        public TypeMethodReflector GetHashCodeMethod() => new TypeMethodReflector(Type.GetGetHashCodeMethod());

    }

}
