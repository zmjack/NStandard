using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class DelegateExtensions
    {
        /// <summary>
        /// Convert a function to its higher-order form.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <param name="this"></param>
        /// <param name="degree"></param>
        /// <returns></returns>
        public static Func<TSelf, TSelf> Higher<TSelf>(this Func<TSelf, TSelf> @this, int degree)
        {
            var parameter = Expression.Parameter(typeof(TSelf), "Param_0");
            var func = Expression.Constant(@this);
            Expression higher = parameter;
            for (int i = 0; i < degree; i++)
                higher = Expression.Invoke(func, higher);
            var lambda = Expression.Lambda<Func<TSelf, TSelf>>(higher, parameter);
            return lambda.Compile();
        }

        /// <summary>
        /// Convert a function to its higher-order form.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <param name="this"></param>
        /// <param name="degree"></param>
        /// <returns></returns>
        public static UnaryOpFunc<TSelf> Higher<TSelf>(this UnaryOpFunc<TSelf> @this, int degree)
        {
            var parameter = Expression.Parameter(typeof(TSelf), "Param_0");
            var func = Expression.Constant(@this);
            Expression higher = parameter;
            for (int i = 0; i < degree; i++)
                higher = Expression.Invoke(func, higher);
            var lambda = Expression.Lambda<UnaryOpFunc<TSelf>>(higher, parameter);
            return lambda.Compile();
        }

    }
}
