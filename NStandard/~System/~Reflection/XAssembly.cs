using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XAssembly
    {
        public static Type[] GetTypesWhichExtends<TExtendClass>(this Assembly @this, bool recursiveSearch = false)
        {
            return GetTypesWhichExtends(@this, typeof(TExtendClass), recursiveSearch);
        }
        public static Type[] GetTypesWhichExtends(this Assembly @this, Type @class, bool recursiveSearch = false)
        {
            return @this.GetTypes().Where(type => XType.IsExtend(type, @class, recursiveSearch)).ToArray();
        }

        public static Type[] GetTypesWhichImplements<TInterface>(this Assembly @this)
            where TInterface : class
        {
            return GetTypesWhichImplements(@this, typeof(TInterface));
        }
        public static Type[] GetTypesWhichImplements(this Assembly @this, Type @interface)
        {
            return @this.GetTypes().Where(type => XType.IsImplement(type, @interface)).ToArray();
        }

        public static Type[] GetTypesWhichMarkedAs<TAttribute>(this Assembly @this)
            where TAttribute : Attribute
        {
            return GetTypesWhichMarkedAs(@this, typeof(TAttribute));
        }
        public static Type[] GetTypesWhichMarkedAs(this Assembly @this, Type attribute)
        {
            return @this.GetTypes()
                .Where(type => type.Assembly.FullName == @this.FullName)
                .Where(type => type.IsMarkedAs(attribute))
                .ToArray();
        }

    }
}
