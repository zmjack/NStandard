﻿using System;
using System.Reflection;

namespace NStandard.Reflection
{
    public class Reflector
    {
        public object Object { get; internal set; }
        public Type Type { get; private set; }

        public Reflector(Type objType, object obj)
        {
            Object = obj;
            Type = objType;
        }

        public FieldReflector Field(string name) => Type.GetField(name)?.Pipe(x => new FieldReflector(x.FieldType, x, Object));
        public FieldReflector Field(string name, Type type) => Type.GetField(name)?.Pipe(x => new FieldReflector(type, x, Object));
        public FieldReflector<T> Field<T>(string name) => Type.GetField(name)?.Pipe(x => new FieldReflector<T>(x, Object));
        public FieldReflector Field(string name, BindingFlags bindingAttr) => Type.GetField(name, bindingAttr)?.Pipe(x => new FieldReflector(x.FieldType, x, Object));
        public FieldReflector Field(string name, Type type, BindingFlags bindingAttr) => Type.GetField(name, bindingAttr)?.Pipe(x => new FieldReflector(type, x, Object));
        public FieldReflector<T> Field<T>(string name, BindingFlags bindingAttr) => Type.GetField(name, bindingAttr)?.Pipe(x => new FieldReflector<T>(x, Object));
        public FieldReflector DeclaredField(string name) => Type.GetDeclaredField(name)?.Pipe(x => new FieldReflector(x.FieldType, x, Object));
        public FieldReflector DeclaredField(string name, Type type) => Type.GetDeclaredField(name)?.Pipe(x => new FieldReflector(type, x, Object));
        public FieldReflector<T> DeclaredField<T>(string name) => Type.GetDeclaredField(name)?.Pipe(x => new FieldReflector<T>(x, Object));


        public PropertyReflector Property(string name) => Type.GetProperty(name)?.Pipe(x => new PropertyReflector(x.PropertyType, x, Object));
        public PropertyReflector Property(string name, Type type) => Type.GetProperty(name)?.Pipe(x => new PropertyReflector(type, x, Object));
        public PropertyReflector<T> Property<T>(string name) => Type.GetProperty(name)?.Pipe(x => new PropertyReflector<T>(x, Object));
        public PropertyReflector Property(string name, BindingFlags bindingAttr) => Type.GetProperty(name, bindingAttr)?.Pipe(x => new PropertyReflector(x.PropertyType, x, Object));
        public PropertyReflector Property(string name, Type type, BindingFlags bindingAttr) => Type.GetProperty(name, bindingAttr)?.Pipe(x => new PropertyReflector(type, x, Object));
        public PropertyReflector<T> Property<T>(string name, BindingFlags bindingAttr) => Type.GetProperty(name, bindingAttr)?.Pipe(x => new PropertyReflector<T>(x, Object));
        public PropertyReflector DeclaredProperty(string name) => Type.GetDeclaredProperty(name)?.Pipe(x => new PropertyReflector(x.PropertyType, x, Object));
        public PropertyReflector DeclaredProperty(string name, Type type) => Type.GetDeclaredProperty(name)?.Pipe(x => new PropertyReflector(type, x, Object));
        public PropertyReflector<T> DeclaredProperty<T>(string name) => Type.GetDeclaredProperty(name)?.Pipe(x => new PropertyReflector<T>(x, Object));

        public MethodReflector Method(string name) => new(Type.GetMethod(name), Object);
        public MethodReflector Method(string name, BindingFlags bindingAttr) => new(Type.GetMethod(name, bindingAttr), Object);
        public MethodReflector DeclaredMethod(string name) => new(Type.GetDeclaredMethod(name), Object);

        public MethodReflector MethodViaQualifiedName(string name) => new(Type.GetMethodViaQualifiedName(name), Object);
        public MethodReflector MethodViaQualifiedName(string name, BindingFlags bindingAttr) => new(Type.GetMethodViaQualifiedName(name, bindingAttr), Object);
        public MethodReflector DeclaredMethodViaQualifiedName(string name) => new(Type.GetDeclaredMethodViaQualifiedName(name), Object);

        public MethodReflector ToStringMethod() => new(Type.GetToStringMethod(), Object);
        public MethodReflector GetHashCodeMethod() => new(Type.GetGetHashCodeMethod(), Object);

    }

}
