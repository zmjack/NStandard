using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class TypeExtensions
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

        private static readonly Regex GetSimplifiedNameRegex = new(@"^([^`]+)`");
        public static string GetSimplifiedName(this Type @this)
        {
            switch (@this)
            {
                case Type when @this == typeof(object): return "object";
                case Type when @this == typeof(char): return "char";
                case Type when @this == typeof(bool): return "bool";
                case Type when @this == typeof(byte): return "byte";
                case Type when @this == typeof(sbyte): return "sbyte";
                case Type when @this == typeof(short): return "short";
                case Type when @this == typeof(ushort): return "ushort";
                case Type when @this == typeof(int): return "int";
                case Type when @this == typeof(uint): return "uint";
                case Type when @this == typeof(long): return "long";
                case Type when @this == typeof(ulong): return "ulong";
                case Type when @this == typeof(float): return "float";
                case Type when @this == typeof(double): return "double";
                case Type when @this == typeof(decimal): return "decimal";
                case Type when @this == typeof(string): return "string";

                case Type when @this == typeof(char?): return "char?";
                case Type when @this == typeof(bool?): return "bool?";
                case Type when @this == typeof(byte?): return "byte?";
                case Type when @this == typeof(sbyte?): return "sbyte?";
                case Type when @this == typeof(short?): return "short?";
                case Type when @this == typeof(ushort?): return "ushort?";
                case Type when @this == typeof(int?): return "int?";
                case Type when @this == typeof(uint?): return "uint?";
                case Type when @this == typeof(long?): return "long?";
                case Type when @this == typeof(ulong?): return "ulong?";
                case Type when @this == typeof(float?): return "float?";
                case Type when @this == typeof(double?): return "double?";
                case Type when @this == typeof(decimal?): return "decimal?";

                default:
                    if (@this.IsArray) return $"{GetSimplifiedName(@this.GetElementType())}[]";
                    else if (@this.IsNullable()) return $"{GetSimplifiedName(@this.GetGenericArguments()[0])}?";
                    else if (@this.IsGenericTypeDefinition) return $"{@this.Name}<>";
                    else if (@this.IsGenericType) return $"{@this.Name.Extract(GetSimplifiedNameRegex).FirstOrDefault()}<{@this.GetGenericArguments().Select(x => GetSimplifiedName(x)).Join(", ")}>";
                    else return @this.Name;
            };
        }

        public static bool IsAnonymousType(this Type @this)
        {
            return @this.Name.StartsWith("<>f__AnonymousType");
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

        public static bool IsType<TType>(this Type @this) => IsType(@this, typeof(TType));
        public static bool IsType(this Type @this, Type type)
        {
            if (type.IsGenericTypeDefinition)
                return @this.IsGenericType && @this.GetGenericTypeDefinition() == type;
            else return @this == type;
        }

        public static bool IsBasicType(this Type @this, bool includeNullable = false)
        {
            switch (@this)
            {
                case Type when @this == typeof(char):
                case Type when @this == typeof(bool):
                case Type when @this == typeof(byte):
                case Type when @this == typeof(sbyte):
                case Type when @this == typeof(short):
                case Type when @this == typeof(ushort):
                case Type when @this == typeof(int):
                case Type when @this == typeof(uint):
                case Type when @this == typeof(long):
                case Type when @this == typeof(ulong):
                case Type when @this == typeof(float):
                case Type when @this == typeof(double):
                case Type when @this == typeof(decimal):
                case Type when @this == typeof(Guid):
                case Type when @this == typeof(DateTime):
                case Type when @this == typeof(string):
                case Type when @this.IsEnum: return true;
            }

            if (includeNullable)
            {
                switch (@this)
                {
                    case Type when @this == typeof(char?):
                    case Type when @this == typeof(bool?):
                    case Type when @this == typeof(byte?):
                    case Type when @this == typeof(sbyte?):
                    case Type when @this == typeof(short?):
                    case Type when @this == typeof(ushort?):
                    case Type when @this == typeof(int?):
                    case Type when @this == typeof(uint?):
                    case Type when @this == typeof(long?):
                    case Type when @this == typeof(ulong?):
                    case Type when @this == typeof(float?):
                    case Type when @this == typeof(double?):
                    case Type when @this == typeof(decimal?):
                    case Type when @this == typeof(Guid?):
                    case Type when @this == typeof(DateTime?): return true;
                }
            }

            return false;
        }

        public static bool IsNumberType(this Type @this, bool includeNullable = false)
        {
            switch (@this)
            {
                case Type when @this == typeof(byte):
                case Type when @this == typeof(sbyte):
                case Type when @this == typeof(short):
                case Type when @this == typeof(ushort):
                case Type when @this == typeof(int):
                case Type when @this == typeof(uint):
                case Type when @this == typeof(long):
                case Type when @this == typeof(ulong):
                case Type when @this == typeof(float):
                case Type when @this == typeof(double):
                case Type when @this == typeof(decimal): return true;
            }

            if (includeNullable)
            {
                switch (@this)
                {
                    case Type when @this == typeof(byte?):
                    case Type when @this == typeof(sbyte?):
                    case Type when @this == typeof(short?):
                    case Type when @this == typeof(ushort?):
                    case Type when @this == typeof(int?):
                    case Type when @this == typeof(uint?):
                    case Type when @this == typeof(long?):
                    case Type when @this == typeof(ulong?):
                    case Type when @this == typeof(float?):
                    case Type when @this == typeof(double?):
                    case Type when @this == typeof(decimal?): return true;
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

        public static bool IsCompatible(this Type @this, Type compatibleType)
        {
            if (compatibleType.IsInterface)
                return IsType(@this, compatibleType) || IsImplement(@this, compatibleType);
            else return IsType(@this, compatibleType) || IsExtend(@this, compatibleType);
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

        public static object CreateDefault(this Type @this)
        {
            return @this.IsValueType ? Activator.CreateInstance(@this) : null;
        }
        public static object CreateInstance(this Type @this)
        {
#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET46_OR_GREATER
            var args = Array.Empty<object>();
#else
            var args = ArrayEx.Empty<object>();
#endif
            return CreateInstance(@this, args);
        }
        public static object CreateInstance(this Type @this, params object[] args) => Activator.CreateInstance(@this, DeclaredOnlyLookup, null, args, null);

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
                if (!generic && type.FullName == extendType.FullName) return true;
                else if (generic && type.FullName.StartsWith(extendType.FullName)) return true;
                else return RecursiveSearchExtends(type.BaseType, extendType, generic);
            }
            else return false;
        }

        /// <summary>
        /// Get the specified type's extend chain.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static Type[] GetExtendChain(this Type @this)
        {
            var list = new List<Type> { @this };
            for (var baseType = @this.BaseType; baseType is not null; baseType = baseType.BaseType)
                list.Add(baseType);
            return list.ToArray();
        }

        public static MethodInfo GetDeclaredMethod(this Type @this, string name) => @this.GetMethod(name, DeclaredOnlyLookup);
        public static IEnumerable<MethodInfo> GetDeclaredMethods(this Type @this)
        {
            foreach (var item in @this.GetMethods(DeclaredOnlyLookup)) yield return item;
        }
        public static IEnumerable<MethodInfo> GetDeclaredMethods(this Type @this, string name)
        {
            foreach (var item in @this.GetMethods(DeclaredOnlyLookup))
            {
                if (item.Name == name) yield return item;
            }
        }

        public static EventInfo GetDeclaredEvent(this Type @this, string name) => @this.GetEvent(name, DeclaredOnlyLookup);
        public static IEnumerable<EventInfo> GetDeclaredEvents(this Type @this)
        {
            foreach (var item in @this.GetEvents(DeclaredOnlyLookup)) yield return item;
        }
        public static IEnumerable<EventInfo> GetDeclaredEvents(this Type @this, string name)
        {
            foreach (var item in @this.GetEvents(DeclaredOnlyLookup))
            {
                if (item.Name == name) yield return item;
            }
        }

        public static FieldInfo GetDeclaredField(this Type @this, string name) => @this.GetField(name, DeclaredOnlyLookup);
        public static IEnumerable<FieldInfo> GetDeclaredFields(this Type @this)
        {
            foreach (var item in @this.GetFields(DeclaredOnlyLookup)) yield return item;
        }
        public static IEnumerable<FieldInfo> GetDeclaredFields(this Type @this, string name)
        {
            foreach (var item in @this.GetFields(DeclaredOnlyLookup))
            {
                if (item.Name == name) yield return item;
            }
        }

        public static PropertyInfo GetDeclaredProperty(this Type @this, string name) => @this.GetProperty(name, DeclaredOnlyLookup);
        public static IEnumerable<PropertyInfo> GetDeclaredProperties(this Type @this)
        {
            foreach (var item in @this.GetProperties(DeclaredOnlyLookup)) yield return item;
        }
        public static IEnumerable<PropertyInfo> GetDeclaredProperties(this Type @this, string name)
        {
            foreach (var item in @this.GetProperties(DeclaredOnlyLookup))
            {
                if (item.Name == name) yield return item;
            }
        }

        public static Type GetDeclaredNestedType(this Type @this, string name) => @this.GetNestedType(name, DeclaredOnlyLookup);
        public static IEnumerable<Type> GetDeclaredNestedTypes(this Type @this)
        {
            foreach (var item in @this.GetNestedTypes(DeclaredOnlyLookup)) yield return item;
        }
        public static IEnumerable<Type> GetDeclaredNestedTypes(this Type @this, string name)
        {
            foreach (var item in @this.GetNestedTypes(DeclaredOnlyLookup))
            {
                if (item.Name == name) yield return item;
            }
        }

        public static MethodInfo GetToStringMethod(this Type @this) => @this.GetMethodViaQualifiedName("System.String ToString()");
        public static MethodInfo GetGetHashCodeMethod(this Type @this) => @this.GetMethodViaQualifiedName("Int32 GetHashCode()");

        public static Type MakeNullableType(this Type @this)
        {
            if (@this.IsValueType && !@this.IsNullable()) return typeof(Nullable<>).MakeGenericType(@this);
            else throw new ArgumentException($"The type {@this.Name} must be a non-nullable value type");
        }

        public static Type MakeNonNullableType(this Type @this)
        {
            if (@this.IsNullable()) return @this.GetGenericArguments()[0];
            else throw new ArgumentException($"The type {@this.Name} must be a nullable value type");
        }

        public static TypeReflector GetTypeReflector(this Type @this) => new(@this);

    }
}
