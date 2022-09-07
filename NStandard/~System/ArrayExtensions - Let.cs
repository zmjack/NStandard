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
        /// <param name="offset"></param>
        /// <param name="inits"></param>
        /// <returns></returns>
        public static TSelf[] Let<TSelf>(this TSelf[] @this, int offset, TSelf[] inits)
        {
            int i = offset;
            foreach (var init in inits)
            {
                @this[i] = init;
                i++;
            }
            return @this;
        }

    }
}
