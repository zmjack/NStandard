using System;

namespace NStandard;

public static partial class Any
{
    /// <summary>
    /// Creates a instance by the specified function.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="func"></param>
    /// <returns></returns>
    public static T Create<T>(Func<T> func) => func();

    /// <summary>
    /// Swaps the values of two instances.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    public static void Swap<T>(ref T a, ref T b)
    {
        var tmp = a;
        a = b;
        b = tmp;
    }

}
