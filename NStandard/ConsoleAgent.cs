using System;
using System.IO;
using System.Text;
using System.Threading;

namespace NStandard
{
    public class ConsoleAgent : Scope<ConsoleAgent>
    {
        protected static readonly object Locker = new();
        protected readonly TextWriter OriginalOut = Console.Out;
        protected readonly TextWriter OriginalError = Console.Error;
        protected StringBuilder Output = new();

        protected ConsoleAgent()
        {
            Monitor.Enter(Locker);
            var writer = new StringWriter(Output);
            Console.SetOut(writer);
            Console.SetError(writer);
        }

        protected ConsoleAgent(TextWriter outWriter, TextWriter errorWriter)
        {
            Monitor.Enter(Locker);
            Console.SetOut(outWriter);
            Console.SetError(errorWriter);
        }

        public static ConsoleAgent Begin() => new();
        public static ConsoleAgent Begin(TextWriter outWriter, TextWriter errorWriter) => new(outWriter, errorWriter);

        public override void Disposing()
        {
            Monitor.Exit(Locker);
            Console.SetOut(Current.OriginalOut);
            Console.SetOut(Current.OriginalError);
        }

        public static string ReadAllText()
        {
            var ret = Current.Output.ToString();
#if NET35
            Current.Output.Remove(0, Current.Output.Length);
#else
            Current.Output.Clear();
#endif
            return ret;
        }

    }
}
