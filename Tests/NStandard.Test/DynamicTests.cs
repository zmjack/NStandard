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
            public static Operand operator +(Operand left, Operand right) => new(left.Value + right.Value);
            public static Operand operator ++(Operand left) => new(left.Value + 1);
            public static Operand operator --(Operand left) => new(left.Value - 1);
        }

        [Fact]
        public void IncrementTest()
        {
            var o1 = new Operand(1);
            var result = Dynamic.OpIncrement(o1);
            Assert.Equal(2, result.Value);
        }

        [Fact]
        public void DecrementTest()
        {
            var o1 = new Operand(1);
            var result = Dynamic.OpDecrement(o1);
            Assert.Equal(0, result.Value);
        }

        [Fact]
        public void Test1()
        {
            Assert.Equal(416, Dynamic.OpAddChecked(400, 16));
        }

        [Fact]
        public void Test2()
        {
            var o1 = new Operand(400);
            var o2 = new Operand(16);
            var result = Dynamic.OpAddChecked(o1, o2);

            Assert.Equal(416, (o1 + o2).Value);
            Assert.Equal(416, result.Value);
        }

        [Fact]
        public void Test3()
        {
            var o1 = new NoAddOperand(400);
            var o2 = new NoAddOperand(16);
            Assert.Throws<TargetInvocationException>(() => Dynamic.OpAddChecked(o1, o2));
        }

    }
}
