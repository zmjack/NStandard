using System;

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

#if NETSTANDARD2_0
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

        /// <summary>
        /// Do action for each item of multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static T[][][][][] Each<T>(this T[][][][][] @this, Action<T, int, int, int, int, int> task)
        {
            for (int i0 = 0; i0 < @this.Length; i0++)
                for (int i1 = 0; i1 < @this[i0].Length; i1++)
                    for (int i2 = 0; i2 < @this[i0][i1].Length; i2++)
                        for (int i3 = 0; i3 < @this[i0][i1][i2].Length; i3++)
                            for (int i4 = 0; i4 < @this[i0][i1][i2][i3].Length; i4++)
                                task(@this[i0][i1][i2][i3][i4], i0, i1, i2, i3, i4);
            return @this;
        }

        /// <summary>
        /// Do action for each item of multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static T[][][][][][] Each<T>(this T[][][][][][] @this, Action<T, int, int, int, int, int, int> task)
        {
            for (int i0 = 0; i0 < @this.Length; i0++)
                for (int i1 = 0; i1 < @this[i0].Length; i1++)
                    for (int i2 = 0; i2 < @this[i0][i1].Length; i2++)
                        for (int i3 = 0; i3 < @this[i0][i1][i2].Length; i3++)
                            for (int i4 = 0; i4 < @this[i0][i1][i2][i3].Length; i4++)
                                for (int i5 = 0; i5 < @this[i0][i1][i2][i3][i4].Length; i5++)
                                    task(@this[i0][i1][i2][i3][i4][i5], i0, i1, i2, i3, i4, i5);
            return @this;
        }

        /// <summary>
        /// Do action for each item of multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static T[][][][][][][] Each<T>(this T[][][][][][][] @this, Action<T, int, int, int, int, int, int, int> task)
        {
            for (int i0 = 0; i0 < @this.Length; i0++)
                for (int i1 = 0; i1 < @this[i0].Length; i1++)
                    for (int i2 = 0; i2 < @this[i0][i1].Length; i2++)
                        for (int i3 = 0; i3 < @this[i0][i1][i2].Length; i3++)
                            for (int i4 = 0; i4 < @this[i0][i1][i2][i3].Length; i4++)
                                for (int i5 = 0; i5 < @this[i0][i1][i2][i3][i4].Length; i5++)
                                    for (int i6 = 0; i6 < @this[i0][i1][i2][i3][i4][i5].Length; i6++)
                                        task(@this[i0][i1][i2][i3][i4][i5][i6], i0, i1, i2, i3, i4, i5, i6);
            return @this;
        }

        /// <summary>
        /// Do action for each item of multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static T[][][][][][][][] Each<T>(this T[][][][][][][][] @this, Action<T, int, int, int, int, int, int, int, int> task)
        {
            for (int i0 = 0; i0 < @this.Length; i0++)
                for (int i1 = 0; i1 < @this[i0].Length; i1++)
                    for (int i2 = 0; i2 < @this[i0][i1].Length; i2++)
                        for (int i3 = 0; i3 < @this[i0][i1][i2].Length; i3++)
                            for (int i4 = 0; i4 < @this[i0][i1][i2][i3].Length; i4++)
                                for (int i5 = 0; i5 < @this[i0][i1][i2][i3][i4].Length; i5++)
                                    for (int i6 = 0; i6 < @this[i0][i1][i2][i3][i4][i5].Length; i6++)
                                        for (int i7 = 0; i7 < @this[i0][i1][i2][i3][i4][i5][i6].Length; i7++)
                                            task(@this[i0][i1][i2][i3][i4][i5][i6][i7], i0, i1, i2, i3, i4, i5, i6, i7);
            return @this;
        }
#endif
        #endregion

    }
}
