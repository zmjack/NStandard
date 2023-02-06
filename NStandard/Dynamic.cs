using NStandard.Caching;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace NStandard
{
    public static class Dynamic
    {
        private delegate BinaryExpression BinaryDelegate(Expression left, Expression right);

        private static readonly Dictionary<string, CacheSet<Type, Delegate>> OpContainers = new()
        {
            [nameof(OpAdd)] = NewOpFunc(Expression.Add),
            [nameof(OpAddChecked)] = NewOpFunc(Expression.AddChecked),
            [nameof(OpSubtract)] = NewOpFunc(Expression.Subtract),
            [nameof(OpSubtractChecked)] = NewOpFunc(Expression.SubtractChecked),
            [nameof(OpMultiply)] = NewOpFunc(Expression.Multiply),
            [nameof(OpMultiplyChecked)] = NewOpFunc(Expression.MultiplyChecked),
            [nameof(OpDivide)] = NewOpFunc(Expression.Divide),

            [nameof(OpLessThan)] = NewOpFunc(Expression.LessThan, typeof(bool)),
            [nameof(OpLessThanOrEqual)] = NewOpFunc(Expression.LessThanOrEqual, typeof(bool)),
            [nameof(OpEqual)] = NewOpFunc(Expression.Equal, typeof(bool)),
            [nameof(OpNotEqual)] = NewOpFunc(Expression.NotEqual, typeof(bool)),
            [nameof(OpGreaterThan)] = NewOpFunc(Expression.GreaterThan, typeof(bool)),
            [nameof(OpGreaterThanOrEqual)] = NewOpFunc(Expression.GreaterThanOrEqual, typeof(bool)),
        };

        private static CacheSet<Type, Delegate> NewOpFunc(BinaryDelegate @delegate)
        {
            Func<Delegate> cacheMethodBuilder(Type operandType)
            {
                return () =>
                {
                    var const_delegate = Expression.Constant(@delegate);
                    var method = GetOpMethod(operandType, operandType, operandType);
                    var exp = Expression.Call(method, const_delegate);

                    // Expression.Lambda<Func<Func<operandType, operandType, operandType>>>(exp).Compile()()
                    var lambdaGenericType = typeof(Func<>).MakeGenericType(typeof(Func<,,>).MakeGenericType(operandType, operandType, operandType));
                    var lambdaMethod = typeof(Expression)
                        .GetMethodViaQualifiedName("System.Linq.Expressions.Expression`1[TDelegate] Lambda[TDelegate](System.Linq.Expressions.Expression, System.Linq.Expressions.ParameterExpression[])")
                        .MakeGenericMethod(lambdaGenericType);

#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_3_OR_GREATER || NET46_OR_GREATER
                    var param = Array.Empty<ParameterExpression>();
#else
                    var param = ArrayEx.Empty<ParameterExpression>();
#endif
                    return (lambdaMethod.Invoke(null, new object[] { exp, param }) as LambdaExpression).Compile().DynamicInvoke() as Delegate;
                };
            }
            var container = new CacheSet<Type, Delegate> { CacheMethodBuilder = cacheMethodBuilder };
            return container;
        }

        private static CacheSet<Type, Delegate> NewOpFunc(BinaryDelegate @delegate, Type retType)
        {
            Func<Delegate> cacheMethodBuilder(Type operandType)
            {
                return () =>
                {
                    var const_delegate = Expression.Constant(@delegate);
                    var method = GetOpMethod(operandType, operandType, retType);
                    var exp = Expression.Call(method, const_delegate);

                    // Expression.Lambda<Func<Func<operandType, operandType, retType>>>(exp).Compile()()
                    var lambdaGenericType = typeof(Func<>).MakeGenericType(typeof(Func<,,>).MakeGenericType(operandType, operandType, retType));
                    var lambdaMethod = typeof(Expression)
                        .GetMethodViaQualifiedName("System.Linq.Expressions.Expression`1[TDelegate] Lambda[TDelegate](System.Linq.Expressions.Expression, System.Linq.Expressions.ParameterExpression[])")
                        .MakeGenericMethod(lambdaGenericType);

#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_3_OR_GREATER || NET46_OR_GREATER
                    var param = Array.Empty<ParameterExpression>();
#else
                    var param = ArrayEx.Empty<ParameterExpression>();
#endif
                    return (lambdaMethod.Invoke(null, new object[] { exp, param }) as LambdaExpression).Compile().DynamicInvoke() as Delegate;
                };
            }
            var container = new CacheSet<Type, Delegate> { CacheMethodBuilder = cacheMethodBuilder };
            return container;
        }

        private static MethodInfo GetOpMethod(Type op1, Type op2, Type ret)
        {
            return typeof(Dynamic).GetDeclaredMethod(nameof(Op)).MakeGenericMethod(op1, op2, ret);
        }

        private static Func<TOp1, TOp2, TRet> Op<TOp1, TOp2, TRet>(BinaryDelegate @delegate)
        {
            var leftExp = Expression.Parameter(typeof(TOp1), "left");
            var rightExp = Expression.Parameter(typeof(TOp2), "right");
            var lambda = Expression.Lambda<Func<TOp1, TOp2, TRet>>(@delegate(leftExp, rightExp), leftExp, rightExp);
            return lambda.Compile();
        }

        public static TOperand OpAdd<TOperand>(TOperand left, TOperand right) => (OpContainers[nameof(OpAdd)][typeof(TOperand)].Value as Func<TOperand, TOperand, TOperand>)(left, right);
        public static TOperand OpAddChecked<TOperand>(TOperand left, TOperand right) => (OpContainers[nameof(OpAddChecked)][typeof(TOperand)].Value as Func<TOperand, TOperand, TOperand>)(left, right);
        public static TOperand OpSubtract<TOperand>(TOperand left, TOperand right) => (OpContainers[nameof(OpSubtract)][typeof(TOperand)].Value as Func<TOperand, TOperand, TOperand>)(left, right);
        public static TRet OpSubtractChecked<TRet>(TRet left, TRet right) => (OpContainers[nameof(OpSubtractChecked)][typeof(TRet)].Value as Func<TRet, TRet, TRet>)(left, right);
        public static TRet OpMultiply<TRet>(TRet left, TRet right) => (OpContainers[nameof(OpMultiply)][typeof(TRet)].Value as Func<TRet, TRet, TRet>)(left, right);
        public static TRet OpMultiplyChecked<TRet>(TRet left, TRet right) => (OpContainers[nameof(OpMultiplyChecked)][typeof(TRet)].Value as Func<TRet, TRet, TRet>)(left, right);
        public static TRet OpDivide<TRet>(TRet left, TRet right) => (OpContainers[nameof(OpDivide)][typeof(TRet)].Value as Func<TRet, TRet, TRet>)(left, right);

        public static bool OpLessThan<TRet>(TRet left, TRet right) => (OpContainers[nameof(OpLessThan)][typeof(TRet)].Value as Func<TRet, TRet, bool>)(left, right);
        public static bool OpLessThanOrEqual<TRet>(TRet left, TRet right) => (OpContainers[nameof(OpLessThanOrEqual)][typeof(TRet)].Value as Func<TRet, TRet, bool>)(left, right);
        public static bool OpEqual<TRet>(TRet left, TRet right) => (OpContainers[nameof(OpEqual)][typeof(TRet)].Value as Func<TRet, TRet, bool>)(left, right);
        public static bool OpNotEqual<TRet>(TRet left, TRet right) => (OpContainers[nameof(OpNotEqual)][typeof(TRet)].Value as Func<TRet, TRet, bool>)(left, right);
        public static bool OpGreaterThan<TRet>(TRet left, TRet right) => (OpContainers[nameof(OpGreaterThan)][typeof(TRet)].Value as Func<TRet, TRet, bool>)(left, right);
        public static bool OpGreaterThanOrEqual<TRet>(TRet left, TRet right) => (OpContainers[nameof(OpGreaterThanOrEqual)][typeof(TRet)].Value as Func<TRet, TRet, bool>)(left, right);
    }
}
