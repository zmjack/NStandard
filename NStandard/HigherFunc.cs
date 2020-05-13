using System.ComponentModel;
using System.Linq.Expressions;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XDelegate
    {
        /// <summary>
        /// Convert a function to its higher-order form.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <param name="delegate"></param>
        /// <param name="this"></param>
        /// <returns></returns>
        public static SingleOpFunc<TSelf> Higher<TSelf>(this SingleOpFunc<TSelf> @this, int degree)
        {
            var parameter = Expression.Parameter(typeof(TSelf), "Param_0");
            var func = Expression.Constant(@this);
            Expression higher = parameter;
            for (int i = 0; i < degree; i++)
                higher = Expression.Invoke(func, higher);
            var lambda = Expression.Lambda<SingleOpFunc<TSelf>>(higher, parameter);
            return lambda.Compile();
        }

    }
}
