using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XType
    {
        public const BindingFlags DeclaredOnlyLookup = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;

        public static MethodInfo GetMethodViaQualifiedName(this Type @this, string qualifiedName)
        {
            return @this.GetMethods().First(x => x.ToString() == qualifiedName);
        }
        public static MethodInfo GetMethodViaQualifiedName(this Type @this, string qualifiedName, BindingFlags bindingAttr)
        {
            return @this.GetMethods(bindingAttr).First(x => x.ToString() == qualifiedName);
        }
        public static MethodInfo GetDeclaredMethodViaQualifiedName(this Type @this, string qualifiedName)
        {
            return GetMethodViaQualifiedName(@this, qualifiedName, DeclaredOnlyLookup);
        }

        public static string GetSimplifiedName(this Type @this)
        {
            var isArray = @this.IsArray;

            if (isArray)
                return $"{GetSimplifiedName(@this.GetElementType())}[]";
            else
            {
                return @this switch
                {
                    Type _ when @this == typeof(object) => "object",
                    Type _ when @this == typeof(char) => "char",
                    Type _ when @this == typeof(bool) => "bool",
                    Type _ when @this == typeof(byte) => "byte",
                    Type _ when @this == typeof(sbyte) => "sbyte",
                    Type _ when @this == typeof(short) => "short",
                    Type _ when @this == typeof(ushort) => "ushort",
                    Type _ when @this == typeof(int) => "int",
                    Type _ when @this == typeof(uint) => "uint",
                    Type _ when @this == typeof(long) => "long",
                    Type _ when @this == typeof(ulong) => "ulong",
                    Type _ when @this == typeof(float) => "float",
                    Type _ when @this == typeof(double) => "double",
                    Type _ when @this == typeof(decimal) => "decimal",
                    Type _ when @this == typeof(string) => "string",

                    Type _ when @this == typeof(char?) => "char?",
                    Type _ when @this == typeof(bool?) => "bool?",
                    Type _ when @this == typeof(byte?) => "byte?",
                    Type _ when @this == typeof(sbyte?) => "sbyte?",
                    Type _ when @this == typeof(short?) => "short?",
                    Type _ when @this == typeof(ushort?) => "ushort?",
                    Type _ when @this == typeof(int?) => "int?",
                    Type _ when @this == typeof(uint?) => "uint?",
                    Type _ when @this == typeof(long?) => "long?",
                    Type _ when @this == typeof(ulong?) => "ulong?",
                    Type _ when @this == typeof(float?) => "float?",
                    Type _ when @this == typeof(double?) => "double?",
                    Type _ when @this == typeof(decimal?) => "decimal?",
                    _ => @this.Name,
                };
            }
        }

        public static bool IsAnonymousType(this Type @this)
        {
            return @this.Name.StartsWith("<>f__AnonymousType");
        }

        public static bool IsType<TType>(this Type @this) => IsType(@this, typeof(TType));
        public static bool IsType(this Type @this, Type type)
        {
            if (type.IsGenericType)
                return @this.FullName.StartsWith(type.FullName);
            else return @this.FullName == type.FullName;
        }

        public static bool IsBasicType(this Type @this, bool includeNullable = false)
        {
            switch (@this)
            {
                case Type _ when @this == typeof(char):
                case Type _ when @this == typeof(bool):
                case Type _ when @this == typeof(byte):
                case Type _ when @this == typeof(sbyte):
                case Type _ when @this == typeof(short):
                case Type _ when @this == typeof(ushort):
                case Type _ when @this == typeof(int):
                case Type _ when @this == typeof(uint):
                case Type _ when @this == typeof(long):
                case Type _ when @this == typeof(ulong):
                case Type _ when @this == typeof(float):
                case Type _ when @this == typeof(double):
                case Type _ when @this == typeof(decimal):
                case Type _ when @this == typeof(Guid):
                case Type _ when @this == typeof(DateTime):
                case Type _ when @this == typeof(string):
                case Type _ when @this.IsEnum: return true;
            }

            if (includeNullable)
            {
                switch (@this)
                {
                    case Type _ when @this == typeof(char?):
                    case Type _ when @this == typeof(bool?):
                    case Type _ when @this == typeof(byte?):
                    case Type _ when @this == typeof(sbyte?):
                    case Type _ when @this == typeof(short?):
                    case Type _ when @this == typeof(ushort?):
                    case Type _ when @this == typeof(int?):
                    case Type _ when @this == typeof(uint?):
                    case Type _ when @this == typeof(long?):
                    case Type _ when @this == typeof(ulong?):
                    case Type _ when @this == typeof(float?):
                    case Type _ when @this == typeof(double?):
                    case Type _ when @this == typeof(decimal?):
                    case Type _ when @this == typeof(Guid?):
                    case Type _ when @this == typeof(DateTime?): return true;
                }
            }

            return false;
        }

        public static bool IsNumberType(this Type @this, bool includeNullable = false)
        {
            switch (@this)
            {
                case Type _ when @this == typeof(byte):
                case Type _ when @this == typeof(sbyte):
                case Type _ when @this == typeof(short):
                case Type _ when @this == typeof(ushort):
                case Type _ when @this == typeof(int):
                case Type _ when @this == typeof(uint):
                case Type _ when @this == typeof(long):
                case Type _ when @this == typeof(ulong):
                case Type _ when @this == typeof(float):
                case Type _ when @this == typeof(double):
                case Type _ when @this == typeof(decimal): return true;
            }

            if (includeNullable)
            {
                switch (@this)
                {
                    case Type _ when @this == typeof(byte?):
                    case Type _ when @this == typeof(sbyte?):
                    case Type _ when @this == typeof(short?):
                    case Type _ when @this == typeof(ushort?):
                    case Type _ when @this == typeof(int?):
                    case Type _ when @this == typeof(uint?):
                    case Type _ when @this == typeof(long?):
                    case Type _ when @this == typeof(ulong?):
                    case Type _ when @this == typeof(float?):
                    case Type _ when @this == typeof(double?):
                    case Type _ when @this == typeof(decimal?): return true;
                }
            }

            return false;
        }

        public static bool IsImplement<TInterface>(this Type @this)
            where TInterface : class
        {
            return IsImplement(@this, typeof(TInterface));
        }
        public static bool IsImplement(this Type @this, Type @interface)
        {
            // Similar to AsInterface Method.
            if (@interface.IsGenericTypeDefinition)
                return @this.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == @interface);
            else return @this.GetInterfaces().Any(x => x == @interface);
        }

        public static Type AsInterface<TInterface>(this Type @this)
            where TInterface : class
        {
            return AsInterface(@this, typeof(TInterface));
        }
        public static Type AsInterface(this Type @this, Type @interface)
        {
            // Similar to IsImplement Method.
            if (@interface.IsGenericTypeDefinition)
                return @this.GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == @interface);
            else return @this.GetInterfaces().FirstOrDefault(x => x == @interface);
        }

        public static bool IsExtend<TExtendType>(this Type @this, bool indirect = false)
        {
            return IsExtend(@this, typeof(TExtendType), indirect);
        }
        public static bool IsExtend(this Type @this, Type extendType, bool indirect = false)
        {
            // Similar to AsClass Method.
            var baseType = @this.BaseType;
            if (baseType != null)
            {
                if (extendType.IsGenericTypeDefinition)
                {
                    if (baseType.IsGenericType)
                    {
                        if (baseType.GetGenericTypeDefinition() == extendType) return true;
                    }
                    else if (baseType == extendType) return true;
                }
                else if (baseType == extendType) return true;

                return indirect ? IsExtend(baseType, extendType, indirect) : false;
            }
            else return false;
        }

        public static Type AsClass<TInterface>(this Type @this)
            where TInterface : class
        {
            return AsClass(@this, typeof(TInterface));
        }
        public static Type AsClass(this Type @this, Type extendType)
        {
            // Similar to IsExtend Method.
            var baseType = @this.BaseType;
            if (baseType != null)
            {
                if (extendType.IsGenericTypeDefinition)
                {
                    if (baseType.IsGenericType)
                    {
                        if (baseType.GetGenericTypeDefinition() == extendType) return baseType;
                    }
                    else if (baseType == extendType) return baseType;
                }
                else if (baseType == extendType) return baseType;

                return AsClass(baseType, extendType);
            }
            else return null;
        }

        public static bool IsNullable(this Type @this)
        {
            if (@this.IsGenericType)
            {
                var genericType = @this.GetGenericTypeDefinition();
                return genericType == typeof(Nullable<>);
            }
            else return false;
        }

        public static object CreateDefault(this Type @this)
        {
            return @this.IsValueType ? Activator.CreateInstance(@this) : null;
        }
        public static object CreateInstance(this Type @this) => Activator.CreateInstance(@this);
        public static object CreateInstance(this Type @this, params object[] args) => Activator.CreateInstance(@this, args);

        public static Type GetCoClassType(this Type @this)
        {
            var attr = @this.GetCustomAttribute<CoClassAttribute>();
            if (attr == null) throw new InvalidOperationException($"Can not find CoClass from '{@this.FullName}'.");
            return attr.CoClass;
        }

        private static bool RecursiveSearchExtends(Type type, Type extendType, bool generic)
        {
            if (type != null)
            {
                if (!generic && type.FullName == extendType.FullName)
                    return true;
                else if (generic && type.FullName.StartsWith(extendType.FullName))
                    return true;
                else return RecursiveSearchExtends(type.BaseType, extendType, generic);
            }
            else return false;
        }

        public static EventInfo GetDeclaredEvent(this Type @this, string name) => @this.GetEvent(name, DeclaredOnlyLookup);
        public static FieldInfo GetDeclaredField(this Type @this, string name) => @this.GetField(name, DeclaredOnlyLookup);
        public static MethodInfo GetDeclaredMethod(this Type @this, string name) => @this.GetMethod(name, DeclaredOnlyLookup);
        public static IEnumerable<MethodInfo> GetDeclaredMethods(this Type @this, string name)
        {
            foreach (MethodInfo method in @this.GetMethods(DeclaredOnlyLookup))
            {
                if (method.Name == name)
                    yield return method;
            }
        }
        public static Type GetDeclaredNestedType(this Type @this, string name) => @this.GetNestedType(name, DeclaredOnlyLookup);
        public static PropertyInfo GetDeclaredProperty(this Type @this, string name) => @this.GetProperty(name, DeclaredOnlyLookup);

        public static MethodInfo GetToStringMethod(this Type @this) => @this.GetMethodViaQualifiedName("System.String ToString()");
        public static MethodInfo GetGetHashCodeMethod(this Type @this) => @this.GetMethodViaQualifiedName("Int32 GetHashCode()");

        public static Type MakeNullableType(this Type @this)
        {
            if (@this.IsValueType && !@this.IsNullable())
                return typeof(Nullable<>).MakeGenericType(@this);
            else throw new ArgumentException($"The type {@this.Name} must be a non-nullable value type");
        }

        public static Type MakeNonNullableType(this Type @this)
        {
            if (@this.IsNullable())
                return @this.GetGenericArguments()[0];
            else throw new ArgumentException($"The type {@this.Name} must be a nullable value type");
        }

        public static TypeReflector GetTypeReflector(this Type @this) => new TypeReflector(@this);

    }
}
