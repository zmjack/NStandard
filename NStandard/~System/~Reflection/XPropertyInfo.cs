using System.ComponentModel;
using System.Reflection;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XPropertyInfo
    {
#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET45_OR_GREATER
#else
        public static object GetValue(this PropertyInfo @this, object obj) => @this.GetValue(obj, null);
        public static void SetValue(this PropertyInfo @this, object obj, object value) => @this.SetValue(obj, value, null);
#endif
    }
}
