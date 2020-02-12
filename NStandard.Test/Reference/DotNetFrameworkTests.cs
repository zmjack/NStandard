using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace NStandard.Reference.Test
{
    public class DotNetFrameworkTests
    {
        [Fact]
        public void Test1()
        {
            var fws = DotNetFramework.Parse("net451").CompatibilityFrameworks.Select(x => x.ToString()).ToArray();
            Assert.Equal(new[]
            {
                "net451", "netstandard1.2",
                "net45", "netstandard1.1", "netstandard1.0",
                "net403", "net40", "net35", "net20", "net11"
            }, fws);
        }

        [Fact]
        public void NotDeclaredTFMTest()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                DotNetFramework.Parse("netcoreapp??").CompatibilityFrameworks.Select(x => x.ToString()).ToArray();
            });
        }

    }
}
