#if !NET35
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XTask
    {
        public static void Catch(this Task @this, Action<Exception> onException)
        {
            try { @this.Wait(); }
            catch (Exception ex) { onException(ex); }
        }

    }
}
#endif
