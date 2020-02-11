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
        public static TSelf[] Let<TSelf>(this TSelf[] @this, Func<int, TSelf> init)
        {
            int i = 0;
            foreach (var item in @this)
            {
                @this[i] = init(i);
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
        public static TSelf[] Let<TSelf>(this TSelf[] @this, Func<TSelf> init)
        {
            int i = 0;
            foreach (var item in @this)
            {
                @this[i] = init();
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
        /// <param name="inits"></param>
        /// <returns></returns>
        public static TSelf[,] Let<TSelf>(this TSelf[,] @this, TSelf[] inits)
        {
            var calc = new DimensionIndexCalculator(@this);
            int dv(int i, int dimension) => calc.GetDimensionIndex(i, dimension);
            int i = 0;

            foreach (var item in @this)
            {
                var di = new[] { dv(i, 0), dv(i, 1) };
                @this[di[0], di[1]] = i < inits.Length ? inits[i] : default;
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
        /// <param name="inits"></param>
        /// <returns></returns>
        public static TSelf[,,] Let<TSelf>(this TSelf[,,] @this, TSelf[] inits)
        {
            var calc = new DimensionIndexCalculator(@this);
            int dv(int i, int dimension) => calc.GetDimensionIndex(i, dimension);
            int i = 0;

            foreach (var item in @this)
            {
                var di = new[] { dv(i, 0), dv(i, 1), dv(i, 2) };
                @this[di[0], di[1], di[2]] = i < inits.Length ? inits[i] : default;
                i++;
            }
            return @this;
        }

    }
}
