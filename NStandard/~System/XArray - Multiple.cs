using System;
using System.Collections.Generic;

namespace NStandard
{
    public static partial class XArray
    {
        #region Multidimensional Array Each
        /// <summary>
        /// Do action for each item of multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static T[,] Each<T>(this T[,] @this, Action<T, int, int> task)
        {
            for (int i0 = 0; i0 < @this.GetLength(0); i0++)
                for (int i1 = 0; i1 < @this.GetLength(1); i1++)
                    task(@this[i0, i1], i0, i1);
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
            for (int i0 = 0; i0 < @this.GetLength(0); i0++)
                for (int i1 = 0; i1 < @this.GetLength(1); i1++)
                    for (int i2 = 0; i2 < @this.GetLength(2); i2++)
                        task(@this[i0, i1, i2], i0, i1, i2);
            return @this;
        }

        /// <summary>
        /// Do action for each item of multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static T[,,,] Each<T>(this T[,,,] @this, Action<T, int, int, int, int> task)
        {
            for (int i0 = 0; i0 < @this.GetLength(0); i0++)
                for (int i1 = 0; i1 < @this.GetLength(1); i1++)
                    for (int i2 = 0; i2 < @this.GetLength(2); i2++)
                        for (int i3 = 0; i3 < @this.GetLength(3); i3++)
                            task(@this[i0, i1, i2, i3], i0, i1, i2, i3);
            return @this;
        }

        /// <summary>
        /// Do action for each item of multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static T[,,,,] Each<T>(this T[,,,,] @this, Action<T, int, int, int, int, int> task)
        {
            for (int i0 = 0; i0 < @this.GetLength(0); i0++)
                for (int i1 = 0; i1 < @this.GetLength(1); i1++)
                    for (int i2 = 0; i2 < @this.GetLength(2); i2++)
                        for (int i3 = 0; i3 < @this.GetLength(3); i3++)
                            for (int i4 = 0; i4 < @this.GetLength(4); i4++)
                                task(@this[i0, i1, i2, i3, i4], i0, i1, i2, i3, i4);
            return @this;
        }

        /// <summary>
        /// Do action for each item of multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static T[,,,,,] Each<T>(this T[,,,,,] @this, Action<T, int, int, int, int, int, int> task)
        {
            for (int i0 = 0; i0 < @this.GetLength(0); i0++)
                for (int i1 = 0; i1 < @this.GetLength(1); i1++)
                    for (int i2 = 0; i2 < @this.GetLength(2); i2++)
                        for (int i3 = 0; i3 < @this.GetLength(3); i3++)
                            for (int i4 = 0; i4 < @this.GetLength(4); i4++)
                                for (int i5 = 0; i5 < @this.GetLength(5); i5++)
                                    task(@this[i0, i1, i2, i3, i4, i5], i0, i1, i2, i3, i4, i5);
            return @this;
        }

        /// <summary>
        /// Do action for each item of multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static T[,,,,,,] Each<T>(this T[,,,,,,] @this, Action<T, int, int, int, int, int, int, int> task)
        {
            for (int i0 = 0; i0 < @this.GetLength(0); i0++)
                for (int i1 = 0; i1 < @this.GetLength(1); i1++)
                    for (int i2 = 0; i2 < @this.GetLength(2); i2++)
                        for (int i3 = 0; i3 < @this.GetLength(3); i3++)
                            for (int i4 = 0; i4 < @this.GetLength(4); i4++)
                                for (int i5 = 0; i5 < @this.GetLength(5); i5++)
                                    for (int i6 = 0; i6 < @this.GetLength(6); i6++)
                                        task(@this[i0, i1, i2, i3, i4, i5, i6], i0, i1, i2, i3, i4, i5, i6);
            return @this;
        }

        /// <summary>
        /// Do action for each item of multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static T[,,,,,,,] Each<T>(this T[,,,,,,,] @this, Action<T, int, int, int, int, int, int, int, int> task)
        {
            for (int i0 = 0; i0 < @this.GetLength(0); i0++)
                for (int i1 = 0; i1 < @this.GetLength(1); i1++)
                    for (int i2 = 0; i2 < @this.GetLength(2); i2++)
                        for (int i3 = 0; i3 < @this.GetLength(3); i3++)
                            for (int i4 = 0; i4 < @this.GetLength(4); i4++)
                                for (int i5 = 0; i5 < @this.GetLength(5); i5++)
                                    for (int i6 = 0; i6 < @this.GetLength(6); i6++)
                                        for (int i7 = 0; i7 < @this.GetLength(7); i7++)
                                            task(@this[i0, i1, i2, i3, i4, i5, i6, i7], i0, i1, i2, i3, i4, i5, i6, i7);
            return @this;
        }
        #endregion

        #region Multidimensional Array ToJaggedArray
        /// <summary>
        /// Converts jagged array to multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T[][] ToJaggedArray<T>(this T[,] @this)
        {
            var ret = new T[@this.GetLength(0)][];

            for (int i0 = 0; i0 < @this.Length; i0++)
            {
                ret[i0] = new T[@this.GetLength(1)];
                for (int i1 = 0; i1 < @this.GetLength(1); i1++)
                {
                    ret[i0][i1] = @this[i0, i1];
                }
            }
            return ret;
        }

        /// <summary>
        /// Converts jagged array to multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T[][][] ToJaggedArray<T>(this T[,,] @this)
        {
            var ret = new T[@this.GetLength(0)][][];

            for (int i0 = 0; i0 < @this.Length; i0++)
            {
                ret[i0] = new T[@this.GetLength(1)][];
                for (int i1 = 0; i1 < @this.GetLength(1); i1++)
                {
                    ret[i0][i1] = new T[@this.GetLength(2)];
                    for (int i2 = 0; i2 < @this.GetLength(2); i2++)
                    {
                        ret[i0][i1][i2] = @this[i0, i1, i2];
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Converts jagged array to multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T[][][][] ToJaggedArray<T>(this T[,,,] @this)
        {
            var ret = new T[@this.GetLength(0)][][][];

            for (int i0 = 0; i0 < @this.Length; i0++)
            {
                ret[i0] = new T[@this.GetLength(1)][][];
                for (int i1 = 0; i1 < @this.GetLength(1); i1++)
                {
                    ret[i0][i1] = new T[@this.GetLength(2)][];
                    for (int i2 = 0; i2 < @this.GetLength(2); i2++)
                    {
                        ret[i0][i1][i2] = new T[@this.GetLength(3)];
                        for (int i3 = 0; i3 < @this.GetLength(3); i3++)
                        {
                            ret[i0][i1][i2][i3] = @this[i0, i1, i2, i3];
                        }
                    }
                }
            }
            return ret;
        }
        #endregion

        #region Multidimensional Array Select
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
            for (int i0 = 0; i0 < @this.GetLength(0); i0++)
                for (int i1 = 0; i1 < @this.GetLength(1); i1++)
                    yield return selector(@this[i0, i1]);
        }

        /// <summary>
        /// Do action for each item of multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static IEnumerable<TRet> Select<T, TRet>(this T[,,] @this, Func<T, TRet> selector)
        {
            for (int i0 = 0; i0 < @this.GetLength(0); i0++)
                for (int i1 = 0; i1 < @this.GetLength(1); i1++)
                    for (int i2 = 0; i2 < @this.GetLength(2); i2++)
                        yield return selector(@this[i0, i1, i2]);
        }

        /// <summary>
        /// Do action for each item of multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static IEnumerable<TRet> Select<T, TRet>(this T[,,,] @this, Func<T, TRet> selector)
        {
            for (int i0 = 0; i0 < @this.GetLength(0); i0++)
                for (int i1 = 0; i1 < @this.GetLength(1); i1++)
                    for (int i2 = 0; i2 < @this.GetLength(2); i2++)
                        for (int i3 = 0; i3 < @this.GetLength(3); i3++)
                            yield return selector(@this[i0, i1, i2, i3]);
        }

        /// <summary>
        /// Do action for each item of multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static IEnumerable<TRet> Select<T, TRet>(this T[,,,,] @this, Func<T, TRet> selector)
        {
            for (int i0 = 0; i0 < @this.GetLength(0); i0++)
                for (int i1 = 0; i1 < @this.GetLength(1); i1++)
                    for (int i2 = 0; i2 < @this.GetLength(2); i2++)
                        for (int i3 = 0; i3 < @this.GetLength(3); i3++)
                            for (int i4 = 0; i4 < @this.GetLength(4); i4++)
                                yield return selector(@this[i0, i1, i2, i3, i4]);
        }

        /// <summary>
        /// Do action for each item of multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static IEnumerable<TRet> Select<T, TRet>(this T[,,,,,] @this, Func<T, TRet> selector)
        {
            for (int i0 = 0; i0 < @this.GetLength(0); i0++)
                for (int i1 = 0; i1 < @this.GetLength(1); i1++)
                    for (int i2 = 0; i2 < @this.GetLength(2); i2++)
                        for (int i3 = 0; i3 < @this.GetLength(3); i3++)
                            for (int i4 = 0; i4 < @this.GetLength(4); i4++)
                                for (int i5 = 0; i5 < @this.GetLength(5); i5++)
                                    yield return selector(@this[i0, i1, i2, i3, i4, i5]);
        }

        /// <summary>
        /// Do action for each item of multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static IEnumerable<TRet> Select<T, TRet>(this T[,,,,,,] @this, Func<T, TRet> selector)
        {
            for (int i0 = 0; i0 < @this.GetLength(0); i0++)
                for (int i1 = 0; i1 < @this.GetLength(1); i1++)
                    for (int i2 = 0; i2 < @this.GetLength(2); i2++)
                        for (int i3 = 0; i3 < @this.GetLength(3); i3++)
                            for (int i4 = 0; i4 < @this.GetLength(4); i4++)
                                for (int i5 = 0; i5 < @this.GetLength(5); i5++)
                                    for (int i6 = 0; i6 < @this.GetLength(6); i6++)
                                        yield return selector(@this[i0, i1, i2, i3, i4, i5, i6]);
        }

        /// <summary>
        /// Do action for each item of multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static IEnumerable<TRet> Select<T, TRet>(this T[,,,,,,,] @this, Func<T, TRet> selector)
        {
            for (int i0 = 0; i0 < @this.GetLength(0); i0++)
                for (int i1 = 0; i1 < @this.GetLength(1); i1++)
                    for (int i2 = 0; i2 < @this.GetLength(2); i2++)
                        for (int i3 = 0; i3 < @this.GetLength(3); i3++)
                            for (int i4 = 0; i4 < @this.GetLength(4); i4++)
                                for (int i5 = 0; i5 < @this.GetLength(5); i5++)
                                    for (int i6 = 0; i6 < @this.GetLength(6); i6++)
                                        for (int i7 = 0; i7 < @this.GetLength(7); i7++)
                                            yield return selector(@this[i0, i1, i2, i3, i4, i5, i6, i7]);
        }
        #endregion
    }
}
