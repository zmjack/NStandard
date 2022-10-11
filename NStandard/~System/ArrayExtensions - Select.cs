using System;
using System.Collections.Generic;

namespace NStandard
{
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
            var stepper = new IndicesStepper(0, @this.GetLengths());
#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER
            foreach (var (value, indices) in Any.Zip(@this.AsEnumerable<T>(), stepper))
            {
#else
            foreach (var pair in Any.Zip(@this.AsEnumerable<T>(), stepper, (Value, Indices) => new { Value, Indices }))
            {
                var value = pair.Value;
                var indices = pair.Indices;
#endif
                yield return selector(value, indices[0], indices[1]);
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
            var stepper = new IndicesStepper(0, @this.GetLengths());
#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER
            foreach (var (value, indices) in Any.Zip(@this.AsEnumerable<T>(), stepper))
            {
#else
            foreach (var pair in Any.Zip(@this.AsEnumerable<T>(), stepper, (Value, Indices) => new { Value, Indices }))
            {
                var value = pair.Value;
                var indices = pair.Indices;
#endif
                yield return selector(value, indices[0], indices[1], indices[2]);
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
            var stepper = new IndicesStepper(0, @this.GetLengths());
#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER
            foreach (var (value, indices) in Any.Zip(@this.AsEnumerable<T>(), stepper))
            {
#else
            foreach (var pair in Any.Zip(@this.AsEnumerable<T>(), stepper, (Value, Indices) => new { Value, Indices }))
            {
                var value = pair.Value;
                var indices = pair.Indices;
#endif
                yield return selector(value, indices);
            }
        }

    }
}
