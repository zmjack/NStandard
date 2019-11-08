using System;
using System.Linq;

namespace NStandard
{
    public static partial class XArray
    {
        #region Jagged Array Each
        /// <summary>
        /// Do action for each item of multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static T[][] Each<T>(this T[][] @this, Action<T, int, int> task)
        {
            for (int i0 = 0; i0 < @this.Length; i0++)
                for (int i1 = 0; i1 < @this[i0].Length; i1++)
                    task(@this[i0][i1], i0, i1);
            return @this;
        }

        /// <summary>
        /// Do action for each item of multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static T[][][] Each<T>(this T[][][] @this, Action<T, int, int, int> task)
        {
            for (int i0 = 0; i0 < @this.Length; i0++)
                for (int i1 = 0; i1 < @this[i0].Length; i1++)
                    for (int i2 = 0; i2 < @this[i0][i1].Length; i2++)
                        task(@this[i0][i1][i2], i0, i1, i2);
            return @this;
        }

        /// <summary>
        /// Do action for each item of multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static T[][][][] Each<T>(this T[][][][] @this, Action<T, int, int, int, int> task)
        {
            for (int i0 = 0; i0 < @this.Length; i0++)
                for (int i1 = 0; i1 < @this[i0].Length; i1++)
                    for (int i2 = 0; i2 < @this[i0][i1].Length; i2++)
                        for (int i3 = 0; i3 < @this[i0][i1][i2].Length; i3++)
                            task(@this[i0][i1][i2][i3], i0, i1, i2, i3);
            return @this;
        }
        #endregion

        #region Jagged Array ToMultiArray
        /// <summary>
        /// Converts jagged array to multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Converts jagged array to multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
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
        #endregion

    }
}
