#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET40_OR_GREATER
#else
using System.Text;

namespace NStandard
{
    public static class XStringBuilder
    {
        public static void Clear(this StringBuilder @this)
        {
            @this.Length = 0;
        }
    }
}
#endif
