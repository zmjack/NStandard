using NStandard.Diagnostics;
using Xunit;

namespace NStandard.Test;

public class ScopeTests
{
    private class StringScope : Scope<StringScope>
    {
        public string Model;
        public StringScope(string model) { Model = model; }
    }

    private class IntScope : Scope<IntScope>
    {
        public int Model;
        public IntScope(int model) { Model = model; }
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
        var stringScopes = StringScope.Scopes;
        var intScopes = IntScope.Scopes;

        Assert.Null(StringScope.Current);

        using (new StringScope("outter"))
        {
            Assert.Equal("outter", StringScope.Current.Model);
            Assert.Single(stringScopes);
            Assert.Empty(intScopes);

            using (new StringScope("inner"))
            {
                Assert.Equal("inner", StringScope.Current.Model);
                Assert.Equal(2, StringScope.Scopes.Count);

                Assert.Equal(2, stringScopes.Count);
                Assert.Empty(intScopes);
            }

            Assert.Equal("outter", StringScope.Current.Model);
            Assert.Single(stringScopes);
            Assert.Empty(intScopes);
        }
    }

    [Fact]
    public void ConcurrencyTest()
    {
        var report = Concurrency.Run(cid =>
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
        }, level: 20, threadCount: 10);
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
