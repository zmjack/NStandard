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
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static T[,] Each<T>(this T[,] @this, Action<T, int, int> task)
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
                task(value, indices[0], indices[1]);
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
                task(value, indices[0], indices[1], indices[2]);
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
                task(value, indices);
            }
            return @this;
        }

    }
}
