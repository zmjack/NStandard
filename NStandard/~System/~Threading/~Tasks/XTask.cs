#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET40_OR_GREATER
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

#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET45_OR_GREATER
        public static async Task CatchAsync(this Task @this, Action<Exception> onException)
        {
            await Task.Run(() =>
            {
                try { @this.Wait(); }
                catch (Exception ex) { onException(ex); }
            });
        }
#else
        public static Task CatchAsync(this Task @this, Action<Exception> onException)
        {
            return Task.Factory.StartNew(() =>
            {
                try { @this.Wait(); }
                catch (Exception ex) { onException(ex); }
            });
        }
#endif

    }
}
#endif
