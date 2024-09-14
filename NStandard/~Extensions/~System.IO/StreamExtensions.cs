using System.ComponentModel;

namespace NStandard;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class StreamExtensions
{
    public delegate void ReadingHandler(byte[] buffer, int readLength);
    public delegate void WritingHandler(Stream toStream, byte[] buffer, int totalWrittenLength);

    public static void Scan(this Stream @this, int bufferSize, ReadingHandler handler)
    {
        var buffer = new byte[bufferSize];
        int readLength;
        while ((readLength = @this.Read(buffer, 0, bufferSize)) > 0)
        {
            handler(buffer, readLength);
        }
    }

    public static void ScanAndWriteTo(this Stream @this, Stream writeTarget, int bufferSize)
    {
        int totalWritten = 0;
        Scan(@this, bufferSize, (buffer, readLength) =>
        {
            writeTarget.Write(buffer, 0, readLength);
            totalWritten += readLength;
        });
    }

    public static void ScanAndWriteTo(this Stream @this, Stream writeTarget, int bufferSize, WritingHandler handler)
    {
        int totalWritten = 0;
        Scan(@this, bufferSize, (buffer, readLength) =>
        {
            writeTarget.Write(buffer, 0, readLength);
            totalWritten += readLength;
            handler(writeTarget, buffer, totalWritten);
        });
    }

}
