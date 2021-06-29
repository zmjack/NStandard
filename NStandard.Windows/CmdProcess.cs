using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace NStandard.Windows
{
    [Obsolete("Designing.", false)]
    public class CmdProcess : IDisposable
    {
        public Process Process { get; }
        public TextWriter OutputRedirect { get; set; }

        private bool disposedValue;
        private DateTime lastWriteTime = DateTime.MaxValue;

        public CmdProcess()
        {
            Process = Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
            });
            Process.Start();

            Process.BeginOutputReadLine();
            Process.BeginErrorReadLine();
            Process.OutputDataReceived += Process_OutputDataReceived;
            Process.ErrorDataReceived += Process_ErrorDataReceived;
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            lastWriteTime = DateTime.Now;
            OutputRedirect?.WriteLine(e.Data);
        }

        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            lastWriteTime = DateTime.Now;
            OutputRedirect?.WriteLine(e.Data);
        }

        public void WriteLine(string cmd)
        {
            Process.StandardInput.WriteLine(cmd);
            Process.StandardInput.Flush();
        }

        public void WaitUnchanged(TimeSpan span) => WaitUnchanged(span, TimeSpan.FromSeconds(1));

        public void WaitUnchanged(TimeSpan span, TimeSpan checkInterval)
        {
            while (true)
            {
                if (DateTime.Now - lastWriteTime > span) break;
                else Thread.Sleep(checkInterval);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    Process.Kill();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~CmdProcess()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
