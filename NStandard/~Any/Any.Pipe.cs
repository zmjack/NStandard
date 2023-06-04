using System;
using System.Linq.Expressions;

namespace NStandard
{
    public static partial class Any
    {
        private static ArgumentException ExpressionNullException(int index, Exception innerException)
        {
            throw new ArgumentException($"The expression can not be null. (Index: {index})", innerException);
        }

        private static ArgumentException ExpressionTypeNotMatchException(int index, Exception innerException)
        {
            throw new ArgumentException($"The expression type can not be match to. (Index: {index})", innerException);
        }

        /// <summary>
        /// Create a pipeline expression from multiple expressions.
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="expressions"></param>
        /// <returns></returns>
        [Obsolete("It is not recommended to use, and there may be major changes in the future.")]
        public static Expression<Func<TIn, TOut>> Pipe<TIn, TOut>(params LambdaExpression[] expressions)
        {
            var enumerator = expressions.GetEnumerator();
            if (enumerator.MoveNext())
            {
                var current = enumerator.Current as Expression ?? throw ExpressionNullException(0, null);

                var param = Expression.Parameter(typeof(TIn), "param");
                Expression exp = Expression.Invoke(current, new[] { param });

                for (int index = 1; enumerator.MoveNext(); index++)
                {
                    current = enumerator.Current as LambdaExpression;
                    if (current is null) throw ExpressionNullException(index, null); ;

                    try
                    {
                        exp = Expression.Invoke(current, new[] { exp });
                    }
                    catch (Exception ex)
                    {
                        throw ExpressionTypeNotMatchException(index, ex);
                    }
                }

                return Expression.Lambda<Func<TIn, TOut>>(exp, new[] { param });
            }

            return null;
        }
    }
}
