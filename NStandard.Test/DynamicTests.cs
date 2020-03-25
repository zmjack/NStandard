using System;
using System.Reflection;
using Xunit;

namespace NStandard.Test
{
    public class DynamicTests
    {
        private class NoAddOperand
        {
            public readonly int Value;
            public NoAddOperand(int value) => Value = value;
        }

        private class Operand
        {
            public readonly int Value;
            public Operand(int value) => Value = value;
            public static Operand operator +(Operand left, Operand right) => new Operand(left.Value + right.Value);
        }

        private static T AddChecked<T>(T left, T right) => Dynamic.OpAddChecked(left, right);

        [Fact]
        public void Test1()
        {
            Assert.Equal(416, AddChecked(400, 16));
        }

        [Fact]
        public void Test2()
        {
            var o1 = new Operand(400);
            var o2 = new Operand(16);
            var result = AddChecked(o1, o2);

            Assert.Equal(416, result.Value);
        }

        [Fact]
        public void Test3()
        {
            var o1 = new NoAddOperand(400);
            var o2 = new NoAddOperand(16);
            Assert.Throws<TargetInvocationException>(() => AddChecked(o1, o2));
        }

    }
}
