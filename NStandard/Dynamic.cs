using NStandard.Caching;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace NStandard;

public static class Dynamic
{
    private delegate UnaryExpression UnaryDelegate(Expression self);
    private delegate BinaryExpression BinaryDelegate(Expression left, Expression right);
    private static readonly MethodInfo _lambdaMethod = typeof(Expression).GetMethodViaQualifiedName("System.Linq.Expressions.Expression`1[TDelegate] Lambda[TDelegate](System.Linq.Expressions.Expression, System.Linq.Expressions.ParameterExpression[])");

    private static readonly Dictionary<string, CacheSet<Type, Delegate>> OpContainers = new()
    {
        [nameof(OpAdd)] = NewOpBinaryFuncSet(Expression.Add),
        [nameof(OpAddChecked)] = NewOpBinaryFuncSet(Expression.AddChecked),
        [nameof(OpSubtract)] = NewOpBinaryFuncSet(Expression.Subtract),
        [nameof(OpSubtractChecked)] = NewOpBinaryFuncSet(Expression.SubtractChecked),
        [nameof(OpMultiply)] = NewOpBinaryFuncSet(Expression.Multiply),
        [nameof(OpMultiplyChecked)] = NewOpBinaryFuncSet(Expression.MultiplyChecked),
        [nameof(OpDivide)] = NewOpBinaryFuncSet(Expression.Divide),

        [nameof(OpLessThan)] = NewOpBinaryFuncSet(Expression.LessThan, typeof(bool)),
        [nameof(OpLessThanOrEqual)] = NewOpBinaryFuncSet(Expression.LessThanOrEqual, typeof(bool)),
        [nameof(OpEqual)] = NewOpBinaryFuncSet(Expression.Equal, typeof(bool)),
        [nameof(OpNotEqual)] = NewOpBinaryFuncSet(Expression.NotEqual, typeof(bool)),
        [nameof(OpGreaterThan)] = NewOpBinaryFuncSet(Expression.GreaterThan, typeof(bool)),
        [nameof(OpGreaterThanOrEqual)] = NewOpBinaryFuncSet(Expression.GreaterThanOrEqual, typeof(bool)),

#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET451_OR_GREATER
        [nameof(OpIncrement)] = NewOpUnaryFuncSet(Expression.Increment),
        [nameof(OpDecrement)] = NewOpUnaryFuncSet(Expression.Decrement),
#endif
    };

    private static CacheSet<Type, Delegate> NewOpUnaryFuncSet(UnaryDelegate @delegate)
    {
        Func<Delegate> cacheMethodBuilder(Type operandType)
        {
            return () =>
            {
                var const_delegate = Expression.Constant(@delegate);
                var method = GetOpMethod(operandType, operandType);
                var exp = Expression.Call(method, const_delegate);

                // Expression.Lambda<Func<Func<operandType, operandType>>>(exp).Compile()()
                var lambdaGenericType = typeof(Func<>).MakeGenericType(typeof(Func<,>).MakeGenericType(operandType, operandType));
                var lambdaMethod = _lambdaMethod.MakeGenericMethod(lambdaGenericType);

#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET46_OR_GREATER
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

    private static CacheSet<Type, Delegate> NewOpBinaryFuncSet(BinaryDelegate @delegate)
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
                var lambdaMethod = _lambdaMethod.MakeGenericMethod(lambdaGenericType);

#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET46_OR_GREATER
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

    private static CacheSet<Type, Delegate> NewOpBinaryFuncSet(BinaryDelegate @delegate, Type retType)
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
                var lambdaMethod = _lambdaMethod.MakeGenericMethod(lambdaGenericType);

#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET46_OR_GREATER
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

    private static MethodInfo GetOpMethod(Type op1, Type ret)
    {
        return typeof(Dynamic).GetDeclaredMethod(nameof(Op2)).MakeGenericMethod(op1, ret);
    }

    private static MethodInfo GetOpMethod(Type op1, Type op2, Type ret)
    {
        return typeof(Dynamic).GetDeclaredMethod(nameof(Op3)).MakeGenericMethod(op1, op2, ret);
    }

    private static Func<TOp1, TRet> Op2<TOp1, TRet>(UnaryDelegate @delegate)
    {
        var leftExp = Expression.Parameter(typeof(TOp1), "self");
        var lambda = Expression.Lambda<Func<TOp1, TRet>>(@delegate(leftExp), leftExp);
        return lambda.Compile();
    }

    private static Func<TOp1, TOp2, TRet> Op3<TOp1, TOp2, TRet>(BinaryDelegate @delegate)
    {
        var leftExp = Expression.Parameter(typeof(TOp1), "left");
        var rightExp = Expression.Parameter(typeof(TOp2), "right");
        var lambda = Expression.Lambda<Func<TOp1, TOp2, TRet>>(@delegate(leftExp, rightExp), leftExp, rightExp);
        return lambda.Compile();
    }

    public static TOperand OpIncrement<TOperand>(TOperand self) => (OpContainers[nameof(OpIncrement)][typeof(TOperand)].Value as Func<TOperand, TOperand>)(self);
    public static TOperand OpDecrement<TOperand>(TOperand self) => (OpContainers[nameof(OpDecrement)][typeof(TOperand)].Value as Func<TOperand, TOperand>)(self);

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
