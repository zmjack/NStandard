using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Xunit;

namespace NStandard.Test
{
    public class ReflectorTests
    {
        private class Outter
        {
            private readonly int PrivateField = 0;
            private int PrivateProperty { get; set; }

            public readonly long PublicField = 0;
            public long PublicProperty { get; set; }

            private Inner Inner = new Inner();

            public override string ToString() => $"{PrivateField} {PrivateProperty} {PublicField} {PublicProperty} {Inner.Value}";
        }

        private class Inner
        {
            public int Value { get; set; }
        }

        [Fact]
        public void Test1()
        {
            var cls = new Outter();
            var reflector = cls.GetReflector();

            Assert.Null(reflector.Field("PrivateField"));
            Assert.Null(reflector.Property("PrivateProperty"));
            reflector.Field("PublicField").Value = 3;
            reflector.Property("PublicProperty").Value = 4;
            Assert.Null(reflector.Field("Inner"));
            Assert.Equal("0 0 3 4 0", cls.ToString());

            reflector.DeclaredField("PrivateField").Value = 5;
            reflector.DeclaredProperty("PrivateProperty").Value = 6;
            reflector.DeclaredField("PublicField").Value = 7;
            reflector.DeclaredProperty("PublicProperty").Value = 8;
            reflector.DeclaredField("Inner").Property("Value").Value = 9;
            Assert.Equal("5 6 7 8 9", cls.ToString());
        }

        [Fact]
        public void Test2()
        {
            var cls = new Outter();
            var reflector = cls.GetReflector();

            Assert.Null(reflector.Field<int>("PrivateField"));
            Assert.Null(reflector.Property<int>("PrivateProperty"));
            reflector.Field<long>("PublicField").Value = 3;
            reflector.Property<long>("PublicProperty").Value = 4;
            Assert.Null(reflector.Field<Inner>("Inner"));
            Assert.Equal("0 0 3 4 0", cls.ToString());

            reflector.DeclaredField<int>("PrivateField").Value = 5;
            reflector.DeclaredProperty<int>("PrivateProperty").Value = 6;
            reflector.DeclaredField<long>("PublicField").Value = 7;
            reflector.DeclaredProperty<long>("PublicProperty").Value = 8;
            reflector.DeclaredField<Inner>("Inner").Property<int>("Value").Value = 9;
            Assert.Equal("5 6 7 8 9", cls.ToString());
        }

        [Fact]
        public void InvokeTest()
        {
            var cls = new Outter { PublicProperty = 4 };
            var reflector = cls.GetReflector();
            Assert.Equal("0 0 0 4 0", reflector.Invoke("ToString") as string);
        }

    }
}
