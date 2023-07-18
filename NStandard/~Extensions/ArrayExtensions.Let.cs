using System;

namespace NStandard
{
    public static partial class ArrayExtensions
    {
        /// <summary>
        /// Use the specified function to initialize each element.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="this"></param>
        /// <param name="init"></param>
        /// <returns></returns>
        public static TElement[] Let<TElement>(this TElement[] @this, Func<int, TElement> init)
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
        /// Use the specified function to initialize each element.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="this"></param>
        /// <param name="init"></param>
        /// <returns></returns>
        public static TElement[,] Let<TElement>(this TElement[,] @this, Func<int, int, TElement> init)
        {
            var lengths = @this.GetLengths();
            var stepper = new IndicesStepper(0, lengths);

            foreach (var indices in stepper)
            {
                @this[indices[0], indices[1]] = init(indices[0], indices[1]);
            }
            return @this;
        }

        /// <summary>
        /// Use the specified function to initialize each element.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="this"></param>
        /// <param name="init"></param>
        /// <returns></returns>
        public static TElement[,,] Let<TElement>(this TElement[,,] @this, Func<int, int, int, TElement> init)
        {
            var lengths = @this.GetLengths();
            var stepper = new IndicesStepper(0, lengths);

            foreach (var indices in stepper)
            {
                @this[indices[0], indices[1], indices[2]] = init(indices[0], indices[1], indices[2]);
            }
            return @this;
        }

        /// <summary>
        /// Use the specified function to initialize each element.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="this"></param>
        /// <param name="init"></param>
        /// <returns></returns>
        public static Array Let<TElement>(this Array @this, Func<int[], TElement> init)
        {
            var lengths = @this.GetLengths();
            var stepper = new IndicesStepper(0, lengths);

            foreach (var indices in stepper)
            {
                @this.SetValue(init(indices), indices);
            }
            return @this;
        }

    }
}
