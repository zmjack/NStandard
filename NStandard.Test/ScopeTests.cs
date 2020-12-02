using Dawnx.Diagnostics;
using System;
using Xunit;

namespace NStandard.Test
{
    public class ScopeTests
    {
        private class StringScope : Scope<StringScope>
        {
            public string Model;
            public StringScope(string model) { Model = model; }
        }

        private class WrongScope : Scope<StringScope>
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
            ref var s = ref StringScope.Scopes;

            Assert.Null(StringScope.Current);

            using (new StringScope("outter"))
            {
                Assert.Equal("outter", StringScope.Current.Model);

                using (new StringScope("inner"))
                {
                    Assert.Equal("inner", StringScope.Current.Model);
                    Assert.Equal(2, StringScope.Scopes.Count);
                }

                Assert.Equal("outter", StringScope.Current.Model);
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

        private class FakeTransaction : Scope<FakeTransaction>
        {
            public string Name { get; private set; }
            public FakeTransaction(string name) => Name = name;
        }

        private string MockSaveChanges()
        {
            return FakeTransaction.Current?.Name ?? "[New transaction]";
        }

        [Fact]
        public void NotScopedTest()
        {
            var ret = MockSaveChanges();
            Assert.Equal("[New transaction]", ret);
        }

        [Fact]
        public void ScopedTest()
        {
            using (new FakeTransaction("Transaction 1"))
            {
                var ret = MockSaveChanges();
                Assert.Equal("Transaction 1", ret);
            }
        }

    }
}
