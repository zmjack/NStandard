using NStandard.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace NStandard
{
    public static class Dynamic
    {
        private delegate BinaryExpression BinaryDelegate(Expression left, Expression right);

        private static readonly Dictionary<string, CacheContainer<Type, Delegate>> OpContainers = new Dictionary<string, CacheContainer<Type, Delegate>>
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

        private static CacheContainer<Type, Delegate> NewOpFunc(BinaryDelegate @delegate, Type retType = null)
        {
            var container = new CacheContainer<Type, Delegate>(operandType => new CacheDelegate<Delegate>(() =>
            {
                retType ??= operandType;

                var const_delegate = Expression.Constant(@delegate);
                var method = GetOpMethod(operandType, operandType, retType);
                var exp = Expression.Call(method, const_delegate);

                switch (operandType)
                {
                    case Type type when type == typeof(sbyte): return Expression.Lambda<Func<Func<sbyte, sbyte, sbyte>>>(exp).Compile()();
                    case Type type when type == typeof(byte): return Expression.Lambda<Func<Func<byte, byte, byte>>>(exp).Compile()();
                    case Type type when type == typeof(short): return Expression.Lambda<Func<Func<short, short, short>>>(exp).Compile()();
                    case Type type when type == typeof(ushort): return Expression.Lambda<Func<Func<ushort, ushort, ushort>>>(exp).Compile()();
                    case Type type when type == typeof(int): return Expression.Lambda<Func<Func<int, int, int>>>(exp).Compile()();
                    case Type type when type == typeof(uint): return Expression.Lambda<Func<Func<uint, uint, uint>>>(exp).Compile()();
                    case Type type when type == typeof(long): return Expression.Lambda<Func<Func<long, long, long>>>(exp).Compile()();
                    case Type type when type == typeof(ulong): return Expression.Lambda<Func<Func<ulong, ulong, ulong>>>(exp).Compile()();
                    case Type type when type == typeof(char): return Expression.Lambda<Func<Func<char, char, char>>>(exp).Compile()();
                    case Type type when type == typeof(float): return Expression.Lambda<Func<Func<float, float, float>>>(exp).Compile()();
                    case Type type when type == typeof(double): return Expression.Lambda<Func<Func<double, double, double>>>(exp).Compile()();
                    case Type type when type == typeof(decimal): return Expression.Lambda<Func<Func<decimal, decimal, decimal>>>(exp).Compile()();
                    default:
                        var lambdaGenericType = typeof(Func<>).MakeGenericType(typeof(Func<,,>).MakeGenericType(operandType, operandType, retType));
                        var lambdaMethod = typeof(Expression)
                            .GetMethodViaQualifiedName("System.Linq.Expressions.Expression`1[TDelegate] Lambda[TDelegate](System.Linq.Expressions.Expression, System.Linq.Expressions.ParameterExpression[])")
                            .MakeGenericMethod(lambdaGenericType);
                        return (lambdaMethod.Invoke(null, new object[] { exp, new ParameterExpression[0] }) as LambdaExpression).Compile().DynamicInvoke() as Delegate;
                };
            }));
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

        public static TRet OpAdd<TRet>(TRet left, TRet right) => (OpContainers[nameof(OpAdd)][typeof(TRet)].Value as Func<TRet, TRet, TRet>)(left, right);
        public static TRet OpAddChecked<TRet>(TRet left, TRet right) => (OpContainers[nameof(OpAddChecked)][typeof(TRet)].Value as Func<TRet, TRet, TRet>)(left, right);
        public static TRet OpSubtract<TRet>(TRet left, TRet right) => (OpContainers[nameof(OpSubtract)][typeof(TRet)].Value as Func<TRet, TRet, TRet>)(left, right);
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
