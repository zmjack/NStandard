using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XICustomAttributeProvider
    {
        public static bool HasAttribute<TAttribute>(this ICustomAttributeProvider @this, bool inherit = true) where TAttribute : Attribute
        {
            return @this.GetCustomAttributes(typeof(TAttribute), inherit).Any();
        }

        public static bool HasAttribute(this ICustomAttributeProvider @this, Type attribute, bool inherit = true)
        {
            return @this.GetCustomAttributes(attribute, inherit).Any();
        }

    }
}
