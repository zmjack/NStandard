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
                new MemoryStream("123".Pipe(Encoding.UTF8.GetBytes)),
                new MemoryStream("456".Pipe(Encoding.UTF8.GetBytes)),
                new MemoryStream("789".Pipe(Encoding.UTF8.GetBytes))
            );
            using var reader = new StreamReader(stream);

            Assert.Equal("123456789", reader.ReadToEnd());
        }
    }
}