using System.ComponentModel;
using System.Reflection;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class PropertyInfoExtensions
    {
#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NET451_OR_GREATER
#else
        public static object GetValue(this PropertyInfo @this, object obj) => @this.GetValue(obj, null);
        public static void SetValue(this PropertyInfo @this, object obj, object value) => @this.SetValue(obj, value, null);
#endif
    }
}
