#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_1_OR_GREATER
using System;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ZipFileExtensions
    {
        public static ZipArchiveEntry CreateEntryFromSource(this ZipArchive @this, Stream source, string entryName)
        {
            if (@this == null) throw new ArgumentNullException(nameof(@this));
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (entryName == null) throw new ArgumentNullException(nameof(entryName));

            var entry = @this.CreateEntry(entryName);
            using (var stream = entry.Open())
            {
                source.CopyTo(stream);
            }
            return entry;
        }

        public static ZipArchiveEntry CreateEntryFromSource(this ZipArchive @this, byte[] source, string entryName)
        {
            if (@this == null) throw new ArgumentNullException(nameof(@this));
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (entryName == null) throw new ArgumentNullException(nameof(entryName));

            var entry = @this.CreateEntry(entryName);
            using (var stream = entry.Open())
            {
                stream.Write(source, 0, source.Length);
            }
            return entry;
        }

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        public static ZipArchiveEntry CreateEntryFromSource(this ZipArchive @this, ReadOnlySpan<byte> source, string entryName)
        {
            if (@this == null) throw new ArgumentNullException(nameof(@this));
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (entryName == null) throw new ArgumentNullException(nameof(entryName));

            var entry = @this.CreateEntry(entryName);
            using (var stream = entry.Open())
            {
                stream.Write(source);
            }
            return entry;
        }
#endif

    }
}
#endif
