using System.Runtime.CompilerServices;

namespace NStandard.Threading
{
    public abstract class Scheduler
    {
        public static Thread Thread;
        public static bool SigAbort { get; private set; }
        public static DateTime LastExecutionTime { get; private set; }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Start(TimeSpan interval)
        {
            SigAbort = false;
            if (Thread == null)
            {
                Thread = new Thread(() =>
                {
                    while (true)
                    {
                        if (SigAbort) break;
                        var now = DateTime.Now;

                        LastExecutionTime = now;
                        Thread.Sleep(interval);
                    }
                    Thread = null;
                    SigAbort = false;
                });
                Thread.Start();
            }
            else throw new InvalidOperationException();
        }

        public static void Stop() => SigAbort = true;

        public abstract void Task(DateTime now);

    }

}
