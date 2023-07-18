using System;

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
            foreach (var (value, indices) in Any.Zip(@this.AsEnumerable<T>(), stepper))
            {
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
            foreach (var (value, indices) in Any.Zip(@this.AsEnumerable<T>(), stepper))
            {
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
            foreach (var (value, indices) in Any.Zip(@this.AsEnumerable<T>(), stepper))
            {
                task(value, indices);
            }
            return @this;
        }

    }
}
