using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace NStandard.Windows;

[Obsolete("Designing.", false)]
public class ShellProcess : IDisposable
{
    public string FileName { get; }
    public Process Process { get; }

    public TextWriter Out { get; set; }
    public TextWriter Error { get; set; }

    private bool disposedValue;
    private DateTime _lastWriteTime = DateTime.Now;

    public ShellProcess(string fileName, string? arguments = null)
    {
        Process = Process.Start(new ProcessStartInfo
        {
            FileName = fileName,
            Arguments = arguments,
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            CreateNoWindow = true,
        }) ?? throw new InvalidOperationException("Failed to create process.");

        Process.OutputDataReceived += Process_OutputDataReceived;
        Process.ErrorDataReceived += Process_ErrorDataReceived;

        Process.Start();
        Process.BeginOutputReadLine();
        Process.BeginErrorReadLine();
    }

    private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
    {
        _lastWriteTime = DateTime.Now;
        try
        {
            Out?.WriteLine(e.Data);
        }
        catch { }
    }

    private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
    {
        _lastWriteTime = DateTime.Now;
        try
        {
            Error?.WriteLine(e.Data);
        }
        catch { }
    }

    public void WriteLine(string cmd)
    {
        Process.StandardInput.WriteLine(cmd);
        Process.StandardInput.Flush();
    }

    public void WaitAnyOutput(TimeSpan span)
    {
        TimeSpan interval;
        while ((interval = DateTime.Now - _lastWriteTime) < span)
        {
            Thread.Sleep(interval / 2);
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                Process.Kill(true);
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
