using System;
using System.IO;
using System.Text;
using Xunit;

namespace NStandard.Test
{
    public class ConsoleAgentTests
    {
        [Fact]
        public void UseDefeaultWriterTest()
        {
            using (ConsoleAgent.Begin())
            {
                Console.Write(123);
                Console.Error.Write("e");
                Console.Write(456);
                Assert.Equal("123e456", ConsoleAgent.ReadAllText());
            }
        }

        [Fact]
        public void UseSpecifiedWriterTest()
        {
            var output = new StringBuilder();
            var errorOutput = new StringBuilder();
            using (ConsoleAgent.Begin(new StringWriter(output), new StringWriter(errorOutput)))
            {
                Console.Write(123);
                Console.Error.Write("e");
                Console.Write(456);
                Assert.Equal("123456", output.ToString());
                Assert.Equal("e", errorOutput.ToString());
            }
        }

    }
}
