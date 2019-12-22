using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XType
    {
        public static MethodInfo GetMethodViaQualifiedName(this Type @this, string formatName)
        {
            return @this.GetMethods().First(x => x.ToString() == formatName);
        }

        //TODO: Use more smart method to rebulid this
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
                case Type _ when @this == typeof(Enum):
                case Type _ when @this == typeof(string): return true;
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

        public static bool IsType<TType>(this Type @this) => IsType(@this, typeof(TType));
        public static bool IsType(this Type @this, Type type)
        {
            if (type.IsGenericType)
                return @this.FullName.StartsWith(type.FullName);
            else return @this.FullName == type.FullName;
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
    }
}
