using System;
using Xunit;

namespace NStandard.Test
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

    }
}
