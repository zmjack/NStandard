using System.IO;
using System.Linq;
using Xunit;

namespace NStd.IO.Test
{
    public class SequenceInputStreamTests
    {
        [Fact]
        public void Test1()
        {
            using var stream = new SequenceInputStream(EnumerableEx.Concat(new[]
            {
                new MemoryStream("123".Bytes()),
                new MemoryStream("456".Bytes()),
                new MemoryStream("789".Bytes()),
            }).ToArray());
            using var reader = new StreamReader(stream);

            Assert.Equal("123456789", reader.ReadToEnd());
        }
    }
}