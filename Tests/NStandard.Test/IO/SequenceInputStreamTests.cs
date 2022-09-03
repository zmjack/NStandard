using System.IO;
using System.Text;
using Xunit;

namespace NStandard.IO.Test
{
    public class SequenceInputStreamTests
    {
        [Fact]
        public void Test1()
        {
            using var stream = new SequenceInputStream(
                new MemoryStream("123".Bytes(Encoding.UTF8)),
                new MemoryStream("456".Bytes(Encoding.UTF8)),
                new MemoryStream("789".Bytes(Encoding.UTF8))
            );
            using var reader = new StreamReader(stream);

            Assert.Equal("123456789", reader.ReadToEnd());
        }
    }
}