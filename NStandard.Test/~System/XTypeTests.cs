using System;
using System.Collections.Generic;
using Xunit;

namespace NStandard.Test
{
    public class XTypeTests
    {
        private interface IInterfaceA<T> { }
        private interface IInterfaceB { }
        private class ClassA<T> : IInterfaceA<T> { }
        private class ClassB : ClassA<int>, IInterfaceB { }
        private class ClassC : ClassB { }
        private struct StructA { }

        [Fact]
        public void SimplifiedNameTest()
        {
            Assert.Equal("StructA?", typeof(StructA?).GetSimplifiedName());
            Assert.Equal("List<List<int>>", typeof(List<List<int>>).GetSimplifiedName());
            Assert.Equal("KeyValuePair<string, KeyValuePair<string, List<int>>>", typeof(KeyValuePair<string, KeyValuePair<string, List<int>>>).GetSimplifiedName());
        }

        [Fact]
        public void IsTypeTest()
        {
            Assert.True(typeof(ClassA<long>).IsType(typeof(ClassA<>)));
            Assert.True(typeof(ClassA<long>).IsType(typeof(ClassA<long>)));
            Assert.False(typeof(ClassA<long>).IsType(typeof(ClassA<int>)));
        }

        [Fact]
        public void IsImplementTest()
        {
            Assert.True(typeof(ClassC).IsImplement<IInterfaceA<int>>());
            Assert.True(typeof(ClassC).IsImplement(typeof(IInterfaceA<int>)));
            Assert.True(typeof(ClassC).IsImplement(typeof(IInterfaceA<>)));

            Assert.True(typeof(ClassC).IsImplement<IInterfaceB>());
        }

        [Fact]
        public void AsInterfaceTest()
        {
            Assert.Equal(typeof(IInterfaceA<int>), typeof(ClassB).AsInterface<IInterfaceA<int>>());
            Assert.Equal(typeof(IInterfaceA<int>), typeof(ClassB).AsInterface(typeof(IInterfaceA<>)));
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
        public void AsClassTest()
        {
            Assert.Equal(typeof(ClassB), typeof(ClassC).AsClass<ClassB>());
            Assert.Equal(typeof(ClassA<int>), typeof(ClassC).AsClass(typeof(ClassA<int>)));
        }

        [Fact]
        public void IsNullableTest()
        {
            Assert.False(typeof(DateTime).IsNullable());
            Assert.True(typeof(DateTime?).IsNullable());
        }

        [Fact]
        public void DefaultTest()
        {
            var str = typeof(string).CreateDefault();
            Assert.Null(str);

            var ndt = typeof(DateTime?).CreateDefault();
            Assert.Null(ndt);

            var dt = typeof(DateTime).CreateDefault();
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
