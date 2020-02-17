using Dawnx.Diagnostics;
using System;
using Xunit;

namespace NStandard.Test
{
    public class ScopeTests
    {
        public class StringScope : Scope<StringScope>
        {
            public string Model;
            public StringScope(string model) { Model = model; }
        }

        public class WrongScope : Scope<StringScope>
        {
            public WrongScope() { }
        }

        [Fact]
        public void ExceptionTest()
        {
            Assert.Throws<TypeLoadException>(() => new WrongScope());
        }

        [Fact]
        public void NestTest()
        {
            Assert.Null(StringScope.Current);
            using (new StringScope("outter"))
            {
                Assert.Equal("outter", StringScope.Current.Model);

                using (new StringScope("inner"))
                {
                    Assert.Equal("inner", StringScope.Current.Model);
                    Assert.Equal(2, StringScope.Scopes.Count);
                }
            }
        }

        [Fact]
        public void ConcurrencyTest()
        {
            Concurrency.Run(cid =>
            {
                Assert.Null(StringScope.Current);
                using (new StringScope("outter"))
                {
                    Assert.Equal("outter", StringScope.Current.Model);

                    using (new StringScope("inner"))
                    {
                        Assert.Equal("inner", StringScope.Current.Model);
                        Assert.Equal(2, StringScope.Scopes.Count);
                    }
                }
                return 0;
            }, level: 20, threadCount: 4);
        }

    }
}
