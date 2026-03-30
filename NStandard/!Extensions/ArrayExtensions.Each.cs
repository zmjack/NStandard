using NStandard.Iterators;

namespace NStandard;

public static partial class ArrayExtensions
{
    /// <summary>
    /// Do action for each item of multidimensional array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="task"></param>
    /// <returns></returns>
    public static T[,] Each<T>(this T[,] @this, Action<T, int, int> task)
    {
        var iterator = new IndecesIterator(@this.GetLengths());
        foreach (var (value, indeces) in Any.Zip(@this.AsEnumerable<T>(), iterator))
        {
            task(value, indeces[0], indeces[1]);
        }
        return @this;
    }

    /// <summary>
    /// Do action for each item of multidimensional array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="task"></param>
    /// <returns></returns>
    public static T[,,] Each<T>(this T[,,] @this, Action<T, int, int, int> task)
    {
        var iterator = new IndecesIterator(@this.GetLengths());
        foreach (var (value, indeces) in Any.Zip(@this.AsEnumerable<T>(), iterator))
        {
            task(value, indeces[0], indeces[1], indeces[2]);
        }
        return @this;
    }

    /// <summary>
    /// Do action for each item of multidimensional array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="task"></param>
    /// <returns></returns>
    public static Array Each<T>(this Array @this, Action<T, int[]> task)
    {
        var iterator = new IndecesIterator(@this.GetLengths());
        foreach (var (value, indeces) in Any.Zip(@this.AsEnumerable<T>(), iterator))
        {
            task(value, indeces);
        }
        return @this;
    }

}
