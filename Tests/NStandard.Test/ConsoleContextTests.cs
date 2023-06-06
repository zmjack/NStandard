using System;
using System.IO;
using System.Text;
using Xunit;

namespace NStandard.Test
{
    public class ConsoleContextTests
    {
        [Fact]
        public void UseDefeaultWriterTest()
        {
            var outBuilder = new StringBuilder();
            var errorBuilder = new StringBuilder();

            using (ConsoleContext.Begin())
            {
                Console.SetOut(new StringWriter(outBuilder));
                Console.SetError(new StringWriter(errorBuilder));

                Console.Write(123);
                Console.Error.Write("e");
                Console.Write(456);
                Assert.Equal("123456", outBuilder.ToString());
                Assert.Equal("e", errorBuilder.ToString());
            }

            outBuilder.Clear();
            errorBuilder.Clear();

            using (ConsoleContext.Begin())
            {
                Console.SetOut(new StringWriter(outBuilder));

                Console.Write(123);
                Console.Error.Write("e");
                Console.Write(456);
                Assert.Equal("123456", outBuilder.ToString());
                Assert.Equal("", errorBuilder.ToString());
            }
        }

    }
}
