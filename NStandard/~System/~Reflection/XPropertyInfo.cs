using System.ComponentModel;
using System.Reflection;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XPropertyInfo
    {
#if NET35 || NET40
        public static object GetValue(this PropertyInfo @this, object obj) => @this.GetValue(obj, null);
        public static void SetValue(this PropertyInfo @this, object obj, object value) => @this.SetValue(obj, value, null);
#endif
    }
}
