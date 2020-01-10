using System;
using Xunit;

namespace NStd.Test
{
    public class ConsoleAgentTests
    {
        [Fact]
        public void Test1()
        {
            using (var agent = new ConsoleAgent())
            {
                Console.WriteLine(123);
                Console.WriteLine(456);

                var output = agent.ReadAllText();
                Assert.Equal($@"123{Environment.NewLine}456{Environment.NewLine}", output);
            }

        }

    }
}
