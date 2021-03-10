using System;

namespace NStandard
{
    public static partial class XArray
    {
        /// <summary>
        /// Use a method to initialize each element of an array.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <param name="this"></param>
        /// <param name="init"></param>
        /// <returns></returns>
        public static TSelf[,,] Let<TSelf>(this TSelf[,,] @this, Func<int, TSelf> init)
        {
            var calc = new DimensionIndexCalculator(@this);
            int dv(int i, int dimension) => calc.GetDimensionIndex(i, dimension);
            int i = 0;

            foreach (var item in @this)
            {
                var di = new[] { dv(i, 0), dv(i, 1), dv(i, 2) };
                @this[di[0], di[1], di[2]] = init(i);
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
        public static TSelf[,,] Let<TSelf>(this TSelf[,,] @this, Func<int, int, int, TSelf> init)
        {
            var calc = new DimensionIndexCalculator(@this);
            int dv(int i, int dimension) => calc.GetDimensionIndex(i, dimension);
            int i = 0;

            foreach (var item in @this)
            {
                var di = new[] { dv(i, 0), dv(i, 1), dv(i, 2) };
                @this[di[0], di[1], di[2]] = init(di[0], di[1], di[2]);
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
        public static TSelf[,,] Let<TSelf>(this TSelf[,,] @this, Func<TSelf> init)
        {
            var calc = new DimensionIndexCalculator(@this);
            int dv(int i, int dimension) => calc.GetDimensionIndex(i, dimension);
            int i = 0;

            foreach (var item in @this)
            {
                var di = new[] { dv(i, 0), dv(i, 1), dv(i, 2) };
                @this[di[0], di[1], di[2]] = init();
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
#if NET35 || NET40 || NET45 || NET451 || NET46
        public static TSelf[,,] Let<TSelf>(this TSelf[,,] @this, Tuple<int, int, int> offset, TSelf[] inits)
#else
        public static TSelf[,,] Let<TSelf>(this TSelf[,,] @this, (int Item1, int Item2, int Item3) offset, TSelf[] inits)
#endif
        {
            var lengths = new[] { @this.GetLength(0), @this.GetLength(1), @this.GetLength(2) };
            int i = offset.Item1 * lengths[1] * lengths[2] + offset.Item2 * lengths[2] + offset.Item3;
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
        public static TSelf[,,] Let<TSelf>(this TSelf[,,] @this, int offset, TSelf[] inits)
        {
            var calc = new DimensionIndexCalculator(@this);
            int dv(int i, int dimension) => calc.GetDimensionIndex(i, dimension);
            int i = offset;

            foreach (var init in inits)
            {
                var di = new[] { dv(i, 0), dv(i, 1), dv(i, 2) };
                @this[di[0], di[1], di[2]] = init;
                i++;
            }
            return @this;
        }

    }
}
