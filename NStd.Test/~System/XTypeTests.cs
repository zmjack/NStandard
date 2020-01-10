using System;
using Xunit;

namespace NStd.Test
{
    public class XTypeTests
    {
        private interface InterfaceA<T> { }
        private interface InterfaceB { }
        private class ClassA<T> : InterfaceA<T> { }
        private class ClassB : ClassA<int>, InterfaceB { }
        private class ClassC : ClassB { }

        [Fact]
        public void IsImplementTest()
        {
            Assert.True(typeof(ClassC).IsImplement<InterfaceA<int>>());
            Assert.True(typeof(ClassC).IsImplement(typeof(InterfaceA<int>)));
            Assert.True(typeof(ClassC).IsImplement(typeof(InterfaceA<>)));

            Assert.True(typeof(ClassC).IsImplement<InterfaceB>());
        }

        [Fact]
        public void IsExtendTest()
        {
            Assert.False(typeof(ClassC).IsExtend<ClassA<int>>());
            Assert.False(typeof(ClassC).IsExtend(typeof(ClassA<int>)));
            Assert.False(typeof(ClassC).IsExtend(typeof(ClassA<>)));

            Assert.True(typeof(ClassC).IsExtend<ClassA<int>>(true));
            Assert.True(typeof(ClassC).IsExtend(typeof(ClassA<int>), true));
            Assert.True(typeof(ClassC).IsExtend(typeof(ClassA<>), true));

            Assert.True(typeof(ClassC).IsExtend<ClassB>());
            Assert.True(typeof(ClassC).IsExtend(typeof(ClassB)));
        }

        [Fact]
        public void IsNullableTest()
        {
            Assert.False(typeof(DateTime).IsNullable());
            Assert.True(typeof(DateTime?).IsNullable());
        }

        [Fact]
        public void CreateDefaultInstanceTest()
        {
            var str = typeof(string).CreateDefaultInstance();
            Assert.Null(str);

            var ndt = typeof(DateTime?).CreateDefaultInstance();
            Assert.Null(ndt);

            var dt = typeof(DateTime).CreateDefaultInstance();
            Assert.Equal(new DateTime(), dt);
        }

        [Fact]
        public void IsBasicTypeTest()
        {
            Assert.True(typeof(int).IsBasicType());
            Assert.True(typeof(int).IsBasicType(true));
            Assert.False(typeof(int?).IsBasicType());
            Assert.True(typeof(int?).IsBasicType(true));
        }

        [Fact]
        public void ObjectBasicMethodTest()
        {
            var number = 416;
            Assert.Equal("416", number.GetType().GetToStringMethod().Invoke(number, null));
            Assert.Equal(416, number.GetType().GetGetHashCodeMethod().Invoke(number, null));
        }

        [Fact]
        public void MakeNullableTypeTest()
        {
            Assert.Equal(typeof(int?), typeof(int).MakeNullableType());
        }

        [Fact]
        public void MakeNonNullableTypeTest()
        {
            Assert.Equal(typeof(int), typeof(int?).MakeNonNullableType());
        }

    }
}
