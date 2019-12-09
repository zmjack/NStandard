using System.ComponentModel;
using System.Reflection;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XPropertyInfo
    {
#if NET35
        public static void SetValue(this PropertyInfo @this, object obj, object value)
        {
            @this.SetValue(obj, value, null);
        }
#endif
    }
}
