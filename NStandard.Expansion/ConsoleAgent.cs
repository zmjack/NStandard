using System;
using System.IO;
using System.Threading;

namespace NStandard
{
    public class ConsoleAgent : IDisposable
    {
        private static readonly object _locker = new object();
        private readonly TextWriter _originalOut;
        private MemoryStream _stream;
        private StreamWriter _writer;

        public ConsoleAgent()
        {
            Monitor.Enter(_locker);
            _originalOut = Console.Out;
            Initialize();
        }

        private void Initialize()
        {
            _stream = new MemoryStream();
            _writer = new StreamWriter(_stream);
            Console.SetOut(_writer);
        }

        public string ReadAllText()
        {
            _writer.Flush();
            _stream.Seek(0, SeekOrigin.Begin);
            var ret = _stream.ToArray().String();
            _writer.Dispose();
            _stream.Dispose();
            Initialize();
            return ret;
        }

        public void Dispose()
        {
            _writer.Dispose();
            _stream.Dispose();
            Monitor.Exit(_locker);
            Console.SetOut(_originalOut);
        }
    }
}
