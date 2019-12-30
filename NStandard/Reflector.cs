using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NStandard
{
    public class Reflector
    {
        public object Object { get; private set; }
        public Type Type { get; private set; }

        public Reflector(object obj)
        {
            Object = obj;
            Type = obj.GetType();
        }

        public PropertyReflector Property(string name) => Type.GetProperty(name)?.For(x => new PropertyReflector(x, Object));
        public PropertyReflector DeclaredProperty(string name) => Type.GetDeclaredProperty(name)?.For(x => new PropertyReflector(x, Object));
        public FieldReflector Field(string name) => Type.GetField(name)?.For(x => new FieldReflector(x, Object));
        public FieldReflector DeclaredField(string name) => Type.GetDeclaredField(name)?.For(x => new FieldReflector(x, Object));

        public PropertyReflector<T> Property<T>(string name) => Type.GetProperty(name)?.For(x => new PropertyReflector<T>(x, Object));
        public PropertyReflector<T> DeclaredProperty<T>(string name) => Type.GetDeclaredProperty(name)?.For(x => new PropertyReflector<T>(x, Object));
        public FieldReflector<T> Field<T>(string name) => Type.GetField(name)?.For(x => new FieldReflector<T>(x, Object));
        public FieldReflector<T> DeclaredField<T>(string name) => Type.GetDeclaredField(name)?.For(x => new FieldReflector<T>(x, Object));

        public object Invoke(string methodName, params object[] parameters) => Type.GetMethod(methodName).Invoke(Object, parameters);
    }

}
