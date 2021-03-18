using System;
using System.IO;
using System.Text;
using System.Threading;

namespace NStandard
{
    public class ConsoleAgent : Scope<ConsoleAgent>
    {
        protected static readonly object Locker = new();
        protected readonly TextWriter OriginalOut;
        protected StringBuilder Output;
        protected TextWriter Writer;

        protected ConsoleAgent()
        {
            Monitor.Enter(Locker);
            OriginalOut = Console.Out;
            Output = new StringBuilder();
            Writer = new StringWriter(Output);
            Console.SetOut(Writer);
        }

        protected ConsoleAgent(TextWriter writer)
        {
            Monitor.Enter(Locker);
            OriginalOut = Console.Out;
            Writer = writer;
            Console.SetOut(Writer);
        }

        public static ConsoleAgent Begin() => new();
        public static ConsoleAgent Begin(TextWriter writer) => new(writer);

        public override void Disposing()
        {
            Monitor.Exit(Locker);
            Console.SetOut(Current.OriginalOut);
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
