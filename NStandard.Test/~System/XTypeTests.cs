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
        public void Test1()
        {
            Assert.True(typeof(ClassC).IsImplement<InterfaceA<int>>());
            Assert.True(typeof(ClassC).IsImplement(typeof(InterfaceA<int>)));
            Assert.True(typeof(ClassC).IsImplement(typeof(InterfaceA<>)));

            Assert.True(typeof(ClassC).IsImplement<InterfaceB>());

            Assert.False(typeof(ClassC).IsExtend<ClassA<int>>());
            Assert.False(typeof(ClassC).IsExtend(typeof(ClassA<int>)));
            Assert.False(typeof(ClassC).IsExtend(typeof(ClassA<>)));

            Assert.True(typeof(ClassC).IsExtend<ClassA<int>>(true));
            Assert.True(typeof(ClassC).IsExtend(typeof(ClassA<int>), true));
            Assert.True(typeof(ClassC).IsExtend(typeof(ClassA<>), true));

            Assert.True(typeof(ClassC).IsExtend<ClassB>());
            Assert.True(typeof(ClassC).IsExtend(typeof(ClassB)));
        }

    }
}
