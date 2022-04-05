using System.IO;
using System.Linq;
using Xunit;

namespace NStandard.IO.Test
{
    public class SequenceInputStreamTests
    {
        [Fact]
        public void Test1()
        {
            using var stream = new SequenceInputStream(
                new MemoryStream("123".Bytes()),
                new MemoryStream("456".Bytes()),
                new MemoryStream("789".Bytes())
            );
            using var reader = new StreamReader(stream);

            Assert.Equal("123456789", reader.ReadToEnd());
        }
    }
}