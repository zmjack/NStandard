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
