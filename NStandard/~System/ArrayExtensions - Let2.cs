using System;

namespace NStandard
{
    public static partial class ArrayExtensions
    {
        /// <summary>
        /// Use a method to initialize each element of an array.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <param name="this"></param>
        /// <param name="init"></param>
        /// <returns></returns>
        public static TSelf[,] Let<TSelf>(this TSelf[,] @this, Func<int, TSelf> init)
        {
            var calc = new DimensionIndexCalculator(@this);
            int dv(int i, int dimension) => calc.GetDimensionIndex(i, dimension);
            int i = 0;

            foreach (var item in @this)
            {
                var di = new[] { dv(i, 0), dv(i, 1) };
                @this[di[0], di[1]] = init(i);
                i++;
            }
            return @this;
        }

        /// <summary>
        /// Use a method to initialize each element of an array.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <param name="this"></param>
        /// <param name="init"></param>
        /// <returns></returns>
        public static TSelf[,] Let<TSelf>(this TSelf[,] @this, Func<int, int, TSelf> init)
        {
            var calc = new DimensionIndexCalculator(@this);
            int dv(int i, int dimension) => calc.GetDimensionIndex(i, dimension);
            int i = 0;

            foreach (var item in @this)
            {
                var di = new[] { dv(i, 0), dv(i, 1) };
                @this[di[0], di[1]] = init(di[0], di[1]);
                i++;
            }
            return @this;
        }

        /// <summary>
        /// Use a method to initialize each element of an array.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <param name="this"></param>
        /// <param name="init"></param>
        /// <returns></returns>
        public static TSelf[,] Let<TSelf>(this TSelf[,] @this, Func<TSelf> init)
        {
            var calc = new DimensionIndexCalculator(@this);
            int dv(int i, int dimension) => calc.GetDimensionIndex(i, dimension);
            int i = 0;

            foreach (var item in @this)
            {
                var di = new[] { dv(i, 0), dv(i, 1) };
                @this[di[0], di[1]] = init();
                i++;
            }
            return @this;
        }

        /// <summary>
        /// Use a method to initialize each element of an array.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <param name="this"></param>
        /// <param name="offset"></param>
        /// <param name="inits"></param>
        /// <returns></returns>
#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET461_OR_GREATER
        public static TSelf[,] Let<TSelf>(this TSelf[,] @this, (int Item1, int Item2) offset, TSelf[] inits)
#else
        public static TSelf[,] Let<TSelf>(this TSelf[,] @this, Tuple<int, int> offset, TSelf[] inits)
#endif
        {
            var lengths = new[] { @this.GetLength(0), @this.GetLength(1) };
            int i = offset.Item1 * lengths[1] + offset.Item2;
            return Let(@this, i, inits);
        }

        /// <summary>
        /// Use a method to initialize each element of an array.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <param name="this"></param>
        /// <param name="offset"></param>
        /// <param name="inits"></param>
        /// <returns></returns>
        public static TSelf[,] Let<TSelf>(this TSelf[,] @this, int offset, TSelf[] inits)
        {
            var calc = new DimensionIndexCalculator(@this);
            int dv(int i, int dimension) => calc.GetDimensionIndex(i, dimension);
            int i = offset;

            foreach (var init in inits)
            {
                var di = new[] { dv(i, 0), dv(i, 1) };
                @this[di[0], di[1]] = init;
                i++;
            }
            return @this;
        }

    }
}
