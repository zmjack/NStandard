using NStandard.Iterators;

namespace NStandard;

public static partial class ArrayExtensions
{
    /// <summary>
    /// Use the specified function to initialize each element.
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    /// <param name="this"></param>
    /// <param name="init"></param>
    /// <returns></returns>
    public static TElement[] Let<TElement>(this TElement[] @this, Func<int, TElement> init)
    {
        int i = 0;
        foreach (var item in @this)
        {
            @this[i] = init(i);
            i++;
        }
        return @this;
    }

    /// <summary>
    /// Use the specified function to initialize each element.
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    /// <param name="this"></param>
    /// <param name="init"></param>
    /// <returns></returns>
    public static TElement[,] Let<TElement>(this TElement[,] @this, Func<int, int, TElement> init)
    {
        var lengths = @this.GetLengths();
        var iterator = new IndecesIterator(lengths);
        foreach (var indeces in iterator)
        {
            @this[indeces[0], indeces[1]] = init(indeces[0], indeces[1]);
        }
        return @this;
    }

    /// <summary>
    /// Use the specified function to initialize each element.
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    /// <param name="this"></param>
    /// <param name="init"></param>
    /// <returns></returns>
    public static TElement[,,] Let<TElement>(this TElement[,,] @this, Func<int, int, int, TElement> init)
    {
        var lengths = @this.GetLengths();
        var iterator = new IndecesIterator(lengths);
        foreach (var indeces in iterator)
        {
            @this[indeces[0], indeces[1], indeces[2]] = init(indeces[0], indeces[1], indeces[2]);
        }
        return @this;
    }

    /// <summary>
    /// Use the specified function to initialize each element.
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    /// <param name="this"></param>
    /// <param name="init"></param>
    /// <returns></returns>
    public static Array Let<TElement>(this Array @this, Func<int[], TElement> init)
    {
        var lengths = @this.GetLengths();
        var iterator = new IndecesIterator(lengths);
        foreach (var indeces in iterator)
        {
            @this.SetValue(init(indeces), indeces);
        }
        return @this;
    }

    /// <summary>
    /// Use the specified function to initialize each element.
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    /// <param name="this"></param>
    /// <param name="initValue"></param>
    /// <returns></returns>
    public static TElement[] Let<TElement>(this TElement[] @this, TElement initValue)
    {
        int i = 0;
        foreach (var item in @this)
        {
            @this[i] = initValue;
            i++;
        }
        return @this;
    }

    /// <summary>
    /// Use the specified function to initialize each element.
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    /// <param name="this"></param>
    /// <param name="initValue"></param>
    /// <returns></returns>
    public static TElement[,] Let<TElement>(this TElement[,] @this, TElement initValue)
    {
        var lengths = @this.GetLengths();
        var iterator = new IndecesIterator(lengths);
        foreach (var indeces in iterator)
        {
            @this[indeces[0], indeces[1]] = initValue;
        }
        return @this;
    }

    /// <summary>
    /// Use the specified function to initialize each element.
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    /// <param name="this"></param>
    /// <param name="initValue"></param>
    /// <returns></returns>
    public static TElement[,,] Let<TElement>(this TElement[,,] @this, TElement initValue)
    {
        var lengths = @this.GetLengths();
        var iterator = new IndecesIterator(lengths);
        foreach (var indeces in iterator)
        {
            @this[indeces[0], indeces[1], indeces[2]] = initValue;
        }
        return @this;
    }

    /// <summary>
    /// Use the specified function to initialize each element.
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    /// <param name="this"></param>
    /// <param name="initValue"></param>
    /// <returns></returns>
    public static Array Let<TElement>(this Array @this, TElement initValue)
    {
        var lengths = @this.GetLengths();
        var iterator = new IndecesIterator(lengths);
        foreach (var indeces in iterator)
        {
            @this.SetValue(initValue, indeces);
        }
        return @this;
    }
}
