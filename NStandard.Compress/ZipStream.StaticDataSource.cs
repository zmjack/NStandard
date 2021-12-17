using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace Dawnx.Compress
{
    public partial class ZipStream
    {
        private class StaticDataSource : IStaticDataSource
        {
            private readonly Stream StoredStream;

            public StaticDataSource(Stream stream)
            {
                StoredStream = stream;
            }

            public Stream GetSource() => StoredStream;
        }
    }
}
