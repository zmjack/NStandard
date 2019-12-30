using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NStandard
{
    public class Reflector
    {
        public object Object { get; internal set; }
        public Type Type { get; private set; }

        internal Reflector(Type objType) => Type = objType;
        public Reflector(object obj, Type objType)
        {
            Object = obj;
            Type = objType;
        }

        public PropertyReflector Property(string name) => Type.GetProperty(name)?.For(x => new PropertyReflector(x, Object, x.PropertyType));
        public PropertyReflector Property(string name, Type type) => Type.GetProperty(name)?.For(x => new PropertyReflector(x, Object, type));
        public PropertyReflector<T> Property<T>(string name) => Type.GetProperty(name)?.For(x => new PropertyReflector<T>(x, Object));

        public PropertyReflector DeclaredProperty(string name) => Type.GetDeclaredProperty(name)?.For(x => new PropertyReflector(x, Object, x.PropertyType));
        public PropertyReflector DeclaredProperty(string name, Type type) => Type.GetDeclaredProperty(name)?.For(x => new PropertyReflector(x, Object, type));
        public PropertyReflector<T> DeclaredProperty<T>(string name) => Type.GetDeclaredProperty(name)?.For(x => new PropertyReflector<T>(x, Object));

        public FieldReflector Field(string name) => Type.GetField(name)?.For(x => new FieldReflector(x, Object, x.FieldType));
        public FieldReflector Field(string name, Type type) => Type.GetField(name)?.For(x => new FieldReflector(x, Object, type));
        public FieldReflector<T> Field<T>(string name) => Type.GetField(name)?.For(x => new FieldReflector<T>(x, Object));

        public FieldReflector DeclaredField(string name) => Type.GetDeclaredField(name)?.For(x => new FieldReflector(x, Object, x.FieldType));
        public FieldReflector DeclaredField(string name, Type type) => Type.GetDeclaredField(name)?.For(x => new FieldReflector(x, Object, type));
        public FieldReflector<T> DeclaredField<T>(string name) => Type.GetDeclaredField(name)?.For(x => new FieldReflector<T>(x, Object));

        public object Invoke(string methodName, params object[] parameters) => Type.GetMethod(methodName).Invoke(Object, parameters);
    }

}
