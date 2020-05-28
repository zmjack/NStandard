using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace NStandard.Windows
{
    [Obsolete("Designing.", true)]
    public class CmdProcess
    {
        public Process Process;
        public Thread OutputThread;
        private readonly StringBuilder Output = new StringBuilder();
        private readonly object OutputLock = new object();

        public CmdProcess()
        {
            Process = Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
            });
            Process.Start();

            OutputThread = new Thread(() =>
            {
                var reader = Process.StandardOutput;
                while (!reader.EndOfStream)
                {
                    lock (OutputLock)
                    {
                        var line = reader.ReadLine();
                        Output.AppendLine(line);
                    }
                }
            });
            OutputThread.Start();
        }

        public void WriteLine(string cmd)
        {
            Process.StandardInput.WriteLine(cmd);
        }

        public string ReadAllText()
        {
            lock (OutputLock)
            {
                var ret = Output.ToString();
                Output.Clear();
                return ret;
            }
        }

        public void Kill() => Process.Kill();

    }
}
