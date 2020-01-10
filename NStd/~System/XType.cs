using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace NStd
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

        public static string GetSimplifiedName(this Type @this)
        {
            var isArray = @this.IsArray;

            if (isArray)
                return $"{GetSimplifiedName(@this.GetElementType())}[]";
            else
            {
                switch (@this)
                {
                    case Type _ when @this == typeof(object): return "object";
                    case Type _ when @this == typeof(char): return "char";
                    case Type _ when @this == typeof(bool): return "bool";
                    case Type _ when @this == typeof(byte): return "byte";
                    case Type _ when @this == typeof(sbyte): return "sbyte";
                    case Type _ when @this == typeof(short): return "short";
                    case Type _ when @this == typeof(ushort): return "ushort";
                    case Type _ when @this == typeof(int): return "int";
                    case Type _ when @this == typeof(uint): return "uint";
                    case Type _ when @this == typeof(long): return "long";
                    case Type _ when @this == typeof(ulong): return "ulong";
                    case Type _ when @this == typeof(float): return "float";
                    case Type _ when @this == typeof(double): return "double";
                    case Type _ when @this == typeof(decimal): return "decimal";
                    case Type _ when @this == typeof(string): return "string";

                    case Type _ when @this == typeof(char?): return "char?";
                    case Type _ when @this == typeof(bool?): return "bool?";
                    case Type _ when @this == typeof(byte?): return "byte?";
                    case Type _ when @this == typeof(sbyte?): return "sbyte?";
                    case Type _ when @this == typeof(short?): return "short?";
                    case Type _ when @this == typeof(ushort?): return "ushort?";
                    case Type _ when @this == typeof(int?): return "int?";
                    case Type _ when @this == typeof(uint?): return "uint?";
                    case Type _ when @this == typeof(long?): return "long?";
                    case Type _ when @this == typeof(ulong?): return "ulong?";
                    case Type _ when @this == typeof(float?): return "float?";
                    case Type _ when @this == typeof(double?): return "double?";
                    case Type _ when @this == typeof(decimal?): return "decimal?";
                    default: return @this.Name;
                }
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
            if (@interface.IsGenericType)
                return @this.GetInterfaces().Any(x => x.FullName.StartsWith(@interface.FullName));
            else return @this.GetInterfaces().Any(x => x.FullName == @interface.FullName);
        }

        public static bool IsExtend<TExtendType>(this Type @this, bool recursiveSearch = false)
        {
            return IsExtend(@this, typeof(TExtendType), recursiveSearch);
        }
        public static bool IsExtend(this Type @this, Type extendType, bool recursiveSearch = false)
        {
            if (extendType.IsGenericType)
            {
                if (recursiveSearch)
                    return RecursiveSearchExtends(@this.BaseType, extendType, true);
                else return @this.BaseType?.FullName.StartsWith(extendType.FullName) ?? false;
            }
            else
            {
                if (recursiveSearch)
                    return RecursiveSearchExtends(@this.BaseType, extendType, false);
                else return @this.BaseType?.FullName == extendType.FullName;
            }
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

        public static object CreateDefaultInstance(this Type @this)
        {
            return @this.IsValueType ? Activator.CreateInstance(@this) : null;
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

        public static Reflector GetTypeReflector(this Type @this) => new Reflector(@this, null);

    }
}
