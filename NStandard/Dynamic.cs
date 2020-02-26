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

        private static readonly Dictionary<string, CacheContainer<Type, Delegate>> OpContainers = new Dictionary<string, CacheContainer<Type, Delegate>>
        {
            [nameof(OpAdd)] = NewOpCacheContainer(Expression.Add),
            [nameof(OpAddChecked)] = NewOpCacheContainer(Expression.AddChecked),
            [nameof(OpSubtract)] = NewOpCacheContainer(Expression.Subtract),
            [nameof(OpSubtractChecked)] = NewOpCacheContainer(Expression.SubtractChecked),
            [nameof(OpMultiply)] = NewOpCacheContainer(Expression.Multiply),
            [nameof(OpMultiplyChecked)] = NewOpCacheContainer(Expression.MultiplyChecked),
            [nameof(OpDivide)] = NewOpCacheContainer(Expression.Divide),

            [nameof(OpLessThan)] = NewOpCacheContainer(Expression.LessThan),
            [nameof(OpLessThanOrEqual)] = NewOpCacheContainer(Expression.LessThanOrEqual),
            [nameof(OpEqual)] = NewOpCacheContainer(Expression.Equal),
            [nameof(OpNotEqual)] = NewOpCacheContainer(Expression.NotEqual),
            [nameof(OpGreaterThan)] = NewOpCacheContainer(Expression.GreaterThan),
            [nameof(OpGreaterThanOrEqual)] = NewOpCacheContainer(Expression.GreaterThanOrEqual),
        };

        private static CacheContainer<Type, Delegate> NewOpCacheContainer(BinaryDelegate @delegate)
        {
            var container = new CacheContainer<Type, Delegate>(
                key => new CacheDelegate<Delegate>(() =>
                {
                    var const_delegate = Expression.Constant(@delegate);
                    var method = MakeGenericMethod_Op(key);
                    var exp = Expression.Call(method, const_delegate);

                    return key switch
                    {
                        Type type when type == typeof(sbyte) => Expression.Lambda<Func<Func<sbyte, sbyte, sbyte>>>(exp).Compile()(),
                        Type type when type == typeof(byte) => Expression.Lambda<Func<Func<byte, byte, byte>>>(exp).Compile()(),
                        Type type when type == typeof(short) => Expression.Lambda<Func<Func<short, short, short>>>(exp).Compile()(),
                        Type type when type == typeof(ushort) => Expression.Lambda<Func<Func<ushort, ushort, ushort>>>(exp).Compile()(),
                        Type type when type == typeof(int) => Expression.Lambda<Func<Func<int, int, int>>>(exp).Compile()(),
                        Type type when type == typeof(uint) => Expression.Lambda<Func<Func<uint, uint, uint>>>(exp).Compile()(),
                        Type type when type == typeof(long) => Expression.Lambda<Func<Func<long, long, long>>>(exp).Compile()(),
                        Type type when type == typeof(ulong) => Expression.Lambda<Func<Func<ulong, ulong, ulong>>>(exp).Compile()(),
                        Type type when type == typeof(float) => Expression.Lambda<Func<Func<float, float, float>>>(exp).Compile()(),
                        Type type when type == typeof(double) => Expression.Lambda<Func<Func<double, double, double>>>(exp).Compile()(),
                        Type type when type == typeof(decimal) => Expression.Lambda<Func<Func<decimal, decimal, decimal>>>(exp).Compile()(),
                        _ => throw new NotSupportedException("Only these types are supported: sbyte, byte, short, ushort, int, uint, long, ulong, float, double, decimal."),
                    };
                }));
            return container;
        }

        private static MethodInfo MakeGenericMethod_Op(Type type) => typeof(Dynamic).GetDeclaredMethod(nameof(Op)).MakeGenericMethod(type);
        private static Func<TRet, TRet, TRet> Op<TRet>(BinaryDelegate @delegate) where TRet : unmanaged
        {
            var leftExp = Expression.Parameter(typeof(TRet), "left");
            var rightExp = Expression.Parameter(typeof(TRet), "right");
            var lambda = Expression.Lambda<Func<TRet, TRet, TRet>>(@delegate(leftExp, rightExp), leftExp, rightExp);
            return lambda.Compile();
        }

        public static TRet OpAdd<TRet>(TRet left, TRet right) where TRet : unmanaged => (OpContainers[nameof(OpAdd)][typeof(TRet)].Value as Func<TRet, TRet, TRet>)(left, right);
        public static TRet OpAddChecked<TRet>(TRet left, TRet right) where TRet : unmanaged => (OpContainers[nameof(OpAddChecked)][typeof(TRet)].Value as Func<TRet, TRet, TRet>)(left, right);
        public static TRet OpSubtract<TRet>(TRet left, TRet right) where TRet : unmanaged => (OpContainers[nameof(OpSubtract)][typeof(TRet)].Value as Func<TRet, TRet, TRet>)(left, right);
        public static TRet OpSubtractChecked<TRet>(TRet left, TRet right) where TRet : unmanaged => (OpContainers[nameof(OpSubtractChecked)][typeof(TRet)].Value as Func<TRet, TRet, TRet>)(left, right);
        public static TRet OpMultiply<TRet>(TRet left, TRet right) where TRet : unmanaged => (OpContainers[nameof(OpMultiply)][typeof(TRet)].Value as Func<TRet, TRet, TRet>)(left, right);
        public static TRet OpMultiplyChecked<TRet>(TRet left, TRet right) where TRet : unmanaged => (OpContainers[nameof(OpMultiplyChecked)][typeof(TRet)].Value as Func<TRet, TRet, TRet>)(left, right);
        public static TRet OpDivide<TRet>(TRet left, TRet right) where TRet : unmanaged => (OpContainers[nameof(OpDivide)][typeof(TRet)].Value as Func<TRet, TRet, TRet>)(left, right);

        public static TRet OpLessThan<TRet>(TRet left, TRet right) where TRet : unmanaged => (OpContainers[nameof(OpLessThan)][typeof(TRet)].Value as Func<TRet, TRet, TRet>)(left, right);
        public static TRet OpLessThanOrEqual<TRet>(TRet left, TRet right) where TRet : unmanaged => (OpContainers[nameof(OpLessThanOrEqual)][typeof(TRet)].Value as Func<TRet, TRet, TRet>)(left, right);
        public static TRet OpEqual<TRet>(TRet left, TRet right) where TRet : unmanaged => (OpContainers[nameof(OpEqual)][typeof(TRet)].Value as Func<TRet, TRet, TRet>)(left, right);
        public static TRet OpNotEqual<TRet>(TRet left, TRet right) where TRet : unmanaged => (OpContainers[nameof(OpNotEqual)][typeof(TRet)].Value as Func<TRet, TRet, TRet>)(left, right);
        public static TRet OpGreaterThan<TRet>(TRet left, TRet right) where TRet : unmanaged => (OpContainers[nameof(OpGreaterThan)][typeof(TRet)].Value as Func<TRet, TRet, TRet>)(left, right);
        public static TRet OpGreaterThanOrEqual<TRet>(TRet left, TRet right) where TRet : unmanaged => (OpContainers[nameof(OpGreaterThanOrEqual)][typeof(TRet)].Value as Func<TRet, TRet, TRet>)(left, right);
    }
}
