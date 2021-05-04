using System;
using System.IO;
using System.Text;
using Xunit;

namespace NStandard.Test
{
    public class ConsoleAgentTests
    {
        private readonly object _lock = new();

        [Fact]
        public void UseDefeaultWriterTest()
        {
            lock (_lock)
            {
                using (ConsoleAgent.Begin())
                {
                    Console.Write(123);
                    Console.Error.Write("e");
                    Console.Write(456);
                    Assert.Equal("123e456", ConsoleAgent.ReadAllText());
                }
            }
        }

        [Fact]
        public void UseSpecifiedWriterTest()
        {
            lock (_lock)
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
}
