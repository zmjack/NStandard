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
                Assert.Equal("123", ConsoleAgent.ReadAllText());

                Console.Write(456);
                Assert.Equal("456", ConsoleAgent.ReadAllText());
            }
        }

        [Fact]
        public void UseSpecifiedWriterTest()
        {
            var output = new StringBuilder();
            var writer = new StringWriter(output);
            using (ConsoleAgent.Begin(writer))
            {
                Console.Write(123);
                Assert.Equal("123", output.ToString());

                Console.Write(456);
                Assert.Equal("123456", output.ToString());
            }
        }

    }
}
