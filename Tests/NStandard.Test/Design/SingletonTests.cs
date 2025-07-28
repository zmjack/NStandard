using Xunit;

namespace NStandard.Design.Test;

public class SingletonTests
{
    public class SingletonSample : ISingleton<SingletonSample>
    {
        /// <summary>
        /// <inheritdoc cref="ISingleton{Form}.Instance"/>
        /// </summary>
        public static SingletonSample Instance => Singleton<SingletonSample>.GetOrCreate();

        public static int Number { get; set; }

        public void OnInit()
        {
            Number += 100;
        }
    }

    [Fact]
    public void SingletonTest()
    {
        Assert.Equal(0, SingletonSample.Number);

        _ = SingletonSample.Instance;
        Assert.Equal(100, SingletonSample.Number);

        _ = SingletonSample.Instance;
        Assert.Equal(100, SingletonSample.Number);
    }

    public class StaticSingletonSample : ISingleton<StaticSingletonSample>
    {
        /// <summary>
        /// <inheritdoc cref="ISingleton{Form}.Instance"/>
        /// </summary>
        public static StaticSingletonSample Instance => StaticSingleton<StaticSingletonSample>.Instance;

        public static int Number { get; set; }

        public void OnInit()
        {
            Number += 100;
        }
    }

    [Fact]
    public void StaticSingletonTest()
    {
        Assert.Equal(0, StaticSingletonSample.Number);

        _ = StaticSingletonSample.Instance;
        Assert.Equal(100, StaticSingletonSample.Number);

        _ = StaticSingletonSample.Instance;
        Assert.Equal(100, StaticSingletonSample.Number);
    }
}
