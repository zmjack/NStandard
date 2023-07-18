#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NET451_OR_GREATER
#else
using System.ComponentModel;
using System.Text;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class StringBuilderExtensions
    {
        public static void Clear(this StringBuilder @this)
        {
            @this.Length = 0;
        }
    }
}
#endif
