using System.Text;

#if NET35
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
