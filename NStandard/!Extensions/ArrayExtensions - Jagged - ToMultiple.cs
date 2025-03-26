namespace NStandard;

public static partial class ArrayExtensions
{
    #region Jagged Array ToMultiArray
    /// <summary>
    /// Converts jagged array to multidimensional array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    [Obsolete("May be removed in the future, replaced by a better implementation.")]
    public static T[,] ToMultiArray<T>(this T[][] @this)
    {
        var ret = new T[@this.Length,
            @this.Max(x1 => x1.Length)];

        for (int i0 = 0; i0 < @this.Length; i0++)
            for (int i1 = 0; i1 < @this[i0].Length; i1++)
                ret[i0, i1] = @this[i0][i1];
        return ret;
    }

    /// <summary>
    /// Converts jagged array to multidimensional array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    [Obsolete("May be removed in the future, replaced by a better implementation.")]
    public static T[,,] ToMultiArray<T>(this T[][][] @this)
    {
        var ret = new T[@this.Length,
            @this.Max(x1 => x1.Length),
            @this.Max(x1 => x1.Max(x2 => x2.Length))];

        for (int i0 = 0; i0 < @this.Length; i0++)
            for (int i1 = 0; i1 < @this[i0].Length; i1++)
                for (int i2 = 0; i2 < @this[i0][i1].Length; i2++)
                    ret[i0, i1, i2] = @this[i0][i1][i2];
        return ret;
    }

#if EXPERIMENT
    /// <summary>
    /// Converts jagged array to multidimensional array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    [Obsolete("May be removed in the future, replaced by a better implementation.")]
    public static T[,,,] ToMultiArray<T>(this T[][][][] @this)
    {
        var ret = new T[@this.Length,
            @this.Max(x1 => x1.Length),
            @this.Max(x1 => x1.Max(x2 => x2.Length)),
            @this.Max(x1 => x1.Max(x2 => x2.Max(x3 => x3.Length)))];

        for (int i0 = 0; i0 < @this.Length; i0++)
            for (int i1 = 0; i1 < @this[i0].Length; i1++)
                for (int i2 = 0; i2 < @this[i0][i1].Length; i2++)
                    for (int i3 = 0; i3 < @this[i0][i1][i2].Length; i3++)
                        ret[i0, i1, i2, i3] = @this[i0][i1][i2][i3];
        return ret;
    }
#endif
    #endregion

}
