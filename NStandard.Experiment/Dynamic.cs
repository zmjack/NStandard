using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NStandard
{
    public static class Dynamic
    {
        private delegate BinaryExpression BinaryDelegate(Expression left, Expression right);

        private static Func<TRet> Compile<TRet>(BinaryDelegate @delegate, object left, object right)
        {
            var leftExp = Expression.Constant(left);
            var rightExp = Expression.Constant(right);
            var ret = Expression.Lambda<Func<TRet>>(@delegate(leftExp, rightExp)).Compile();
            return ret;
        }

        public static TRet Add<TRet>(object left, object right) => Compile<TRet>(Expression.Add, left, right)();
        public static TRet AddChecked<TRet>(object left, object right) => Compile<TRet>(Expression.AddChecked, left, right)();
        public static TRet Subtract<TRet>(object left, object right) => Compile<TRet>(Expression.Subtract, left, right)();
        public static TRet SubtractChecked<TRet>(object left, object right) => Compile<TRet>(Expression.SubtractChecked, left, right)();
        public static TRet Multiply<TRet>(object left, object right) => Compile<TRet>(Expression.Multiply, left, right)();
        public static TRet MultiplyChecked<TRet>(object left, object right) => Compile<TRet>(Expression.MultiplyChecked, left, right)();
        public static TRet Divide<TRet>(object left, object right) => Compile<TRet>(Expression.Divide, left, right)();
    }
}
