using NStandard.Iterators;

namespace NStandard;

public static partial class ArrayExtensions
{
    /// <summary>
    /// Do action for each item of multidimensional array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TRet"></typeparam>
    /// <param name="this"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static IEnumerable<TRet> Select<T, TRet>(this T[,] @this, Func<T, TRet> selector)
    {
        foreach (var value in @this.AsEnumerable<T>())
        {
            yield return selector(value);
        }
    }

    /// <summary>
    /// Do action for each item of multidimensional array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TRet"></typeparam>
    /// <param name="this"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static IEnumerable<TRet> Select<T, TRet>(this T[,,] @this, Func<T, TRet> selector)
    {
        foreach (var value in @this.AsEnumerable<T>())
        {
            yield return selector(value);
        }
    }

    /// <summary>
    /// Do action for each item of multidimensional array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TRet"></typeparam>
    /// <param name="this"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static IEnumerable<TRet> Select<T, TRet>(this Array @this, Func<T, TRet> selector)
    {
        foreach (var value in @this.AsEnumerable<T>())
        {
            yield return selector(value);
        }
    }

    /// <summary>
    /// Do action for each item of multidimensional array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TRet"></typeparam>
    /// <param name="this"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static IEnumerable<TRet> Select<T, TRet>(this T[,] @this, Func<T, int, int, TRet> selector)
    {
        var iterator = new IndecesIterator(@this.GetLengths());
        foreach (var (value, indeces) in Any.Zip(@this.AsEnumerable<T>(), iterator))
        {
            yield return selector(value, indeces[0], indeces[1]);
        }
    }

    /// <summary>
    /// Do action for each item of multidimensional array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TRet"></typeparam>
    /// <param name="this"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static IEnumerable<TRet> Select<T, TRet>(this T[,,] @this, Func<T, int, int, int, TRet> selector)
    {
        var iterator = new IndecesIterator(@this.GetLengths());
        foreach (var (value, indeces) in Any.Zip(@this.AsEnumerable<T>(), iterator))
        {
            yield return selector(value, indeces[0], indeces[1], indeces[2]);
        }
    }

    /// <summary>
    /// Do action for each item of multidimensional array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TRet"></typeparam>
    /// <param name="this"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static IEnumerable<TRet> Select<T, TRet>(this Array @this, Func<T, int[], TRet> selector)
    {
        var iterator = new IndecesIterator(@this.GetLengths());
        foreach (var (value, indeces) in Any.Zip(@this.AsEnumerable<T>(), iterator))
        {
            yield return selector(value, indeces);
        }
    }

}
