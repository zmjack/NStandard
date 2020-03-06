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

#if NET40
        public static Task CatchAsync(this Task @this, Action<Exception> onException)
        {
            return Task.Factory.StartNew(() =>
            {
                try { @this.Wait(); }
                catch (Exception ex) { onException(ex); }
            });
        }
#else
        public static async Task CatchAsync(this Task @this, Action<Exception> onException)
        {
            await Task.Run(() =>
            {
                try { @this.Wait(); }
                catch (Exception ex) { onException(ex); }
            });
        }
#endif

    }
}
#endif
