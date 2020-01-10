using System.ComponentModel;
using System.IO;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XStream
    {
        public delegate void ReadingHandler(byte[] buffer, int readLength);
        public delegate void WritingHandler(Stream toStream, byte[] buffer, int totalWrittenLength);

        public static void Reading(this Stream @this, int bufferSize, ReadingHandler handler)
        {
            var buffer = new byte[bufferSize];
            int readLength;
            while ((readLength = @this.Read(buffer, 0, bufferSize)) > 0)
                handler(buffer, readLength);
        }

        public static void Writing(this Stream @this, Stream writeTarget, int bufferSize, WritingHandler handler)
        {
            int totalWritten = 0;
            Reading(@this, bufferSize, (buffer, readLength) =>
            {
                writeTarget.Write(buffer, 0, readLength);
                totalWritten += readLength;
                handler(writeTarget, buffer, totalWritten);
            });
        }
    }
}
