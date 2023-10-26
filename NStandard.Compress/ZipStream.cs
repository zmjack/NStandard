using ICSharpCode.SharpZipLib.Zip;
using NStandard;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dawnx.Compress;

public partial class ZipStream : Stream, IEnumerable, IDisposable
{
    public Stream MappedStream { get; set; }
    public ZipFile ZipFile { get; private set; }

    /// <summary>
    /// Creates a new <see cref="ZipStream"/> whose data will be stored on a stream.
    /// </summary>
    public ZipStream()
    {
        MappedStream = new MemoryStream();
        ZipFile = ZipFile.Create(MappedStream);
    }

    /// <summary>
    /// Opens a Zip file with the given name for reading.
    /// </summary>
    /// <param name="path"></param>
    public ZipStream(string path)
    {
        MappedStream = File.Open(path, FileMode.Open, FileAccess.ReadWrite);
        ZipFile = new ZipFile(MappedStream);
    }

    /// <summary>
    /// Opens a Zip file reading the given System.IO.Stream.
    /// </summary>
    /// <param name="stream"></param>
    public ZipStream(Stream stream)
    {
        MappedStream = stream;
        ZipFile = new ZipFile(stream);
    }

    /// <summary>
    /// Add a <see cref="ZipEntry"/> that contains no data.
    /// </summary>
    /// <param name="entry"></param>
    /// <returns></returns>
    public ZipStream AddEntry(ZipEntry entry)
    {
        ZipFile.Add(entry);
        return this;
    }

    /// <summary>
    /// Add a file entry with data.
    /// </summary>
    /// <param name="inStream"></param>
    /// <param name="entryName"></param>
    /// <param name="method"></param>
    /// <returns></returns>
    public ZipStream AddEntry(string entryName, Stream inStream, CompressionMethod method = CompressionMethod.Deflated)
    {
        using (new UpdateScope(this)) ZipFile.Add(new StaticDataSource(inStream), entryName, method, true);
        return this;
    }
    /// <summary>
    /// Add a file entry with data.
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="entryName"></param>
    /// <param name="method"></param>
    /// <returns></returns>
    public ZipStream AddEntry(string entryName, byte[] bytes, CompressionMethod method = CompressionMethod.Deflated)
    {
        return AddEntry(entryName, new MemoryStream(bytes), method);
    }

    /// <summary>
    /// Add a file entry with data.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="entryName"></param>
    /// <param name="method"></param>
    /// <returns></returns>
    public ZipStream AddFileEntry(string entryName, string path, CompressionMethod method = CompressionMethod.Deflated)
    {
        using (var file = new FileStream(path, FileMode.Open, FileAccess.Read)) AddEntry(entryName, file, method);
        return this;
    }

    /// <summary>
    /// Add a directory entry to the archive.
    /// </summary>
    /// <param name="dictionaryName"></param>
    /// <returns></returns>
    public ZipStream AddDictionary(string dictionaryName)
    {
        using (new UpdateScope(this)) ZipFile.AddDirectory(dictionaryName);
        return this;
    }

    /// <summary>
    /// Set the password to be used for encrypting/decrypting files.
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public ZipStream SetPassword(string password)
    {
        ZipFile.Password = password;
        return this;
    }

    /// <summary>
    /// Set the file comment to be recorded.
    /// </summary>
    /// <param name="comment"></param>
    /// <returns></returns>
    public ZipStream SetComment(string comment)
    {
        ZipFile.SetComment(comment);
        return this;
    }

    /// <summary>
    /// Save the Zip file as a new file.
    /// </summary>
    /// <param name="path"></param>
    public void SaveAs(string path)
    {
        var position = MappedStream.Position;

        MappedStream.Seek(0, SeekOrigin.Begin);
        using (var file = new FileStream(path, FileMode.Create, FileAccess.Write))
        {
            // The default ZipFile's buffer size is 4096
            MappedStream.CopyTo(file, 4096);
        }
        MappedStream.Seek(position, SeekOrigin.Begin);
    }

    /// <summary>
    /// Extract part of files into a directory.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="predicate"></param>
    public void Extract(string path, Func<ZipEntry, bool> predicate)
    {
        var entries = predicate is null
            ? ZipFile.OfType<ZipEntry>().ToArray()
            : ZipFile.OfType<ZipEntry>().Where(predicate).ToArray();

        foreach (var entry in entries)
        {
            var filePath = Path.Combine(path, entry.Name);

            var dir = new DirectoryInfo(Path.GetDirectoryName(filePath));
            if (!dir.Exists) dir.Create();

            if (!Path.GetFileName(entry.Name).IsNullOrWhiteSpace())
            {
                using (var file = new FileStream(Path.Combine(path, entry.Name), FileMode.Create))
                using (var stream = ZipFile.GetInputStream(entry))
                {
                    ZipFile.GetInputStream(entry).CopyTo(file, 1024 * 1024);
                }
            }
        }
    }

    /// <summary>
    /// Extract all file into a directory.
    /// </summary>
    /// <param name="path"></param>
    public void ExtractAll(string path) => Extract(path, null);

    protected override void Dispose(bool disposing)
    {
        MappedStream.Dispose();
        base.Dispose(disposing);
    }

    public IEnumerator<ZipEntry> GetEnumerator()
    {
        IEnumerable<ZipEntry> Iterator()
        {
            foreach (var entry in ZipFile)
                yield return entry as ZipEntry;
        }
        return Iterator().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => ZipFile.GetEnumerator();

    public override bool CanRead => false;
    public override bool CanSeek => false;
    public override bool CanWrite => false;
    public override long Length => MappedStream.Length;

    public override long Position
    {
        get => MappedStream.Position;
        set => throw new NotSupportedException();
    }

    public override void Flush() => MappedStream.Flush();
    public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();
    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
    public override void SetLength(long value) => throw new NotSupportedException();
    public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
}
