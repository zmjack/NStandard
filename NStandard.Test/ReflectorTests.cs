using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NStandard.Test
{
    public class ReflectorTests
    {
        private class SimpleClass
        {
            private readonly int PrivateField = 0;
            private int PrivateProperty { get; set; }

            public readonly long PublicField = 0;
            public long PublicProperty { get; set; }

            public override string ToString() => $"{PrivateField} {PrivateProperty} {PublicField} {PublicProperty}";
        }

        [Fact]
        public void Test1()
        {
            var cls = new SimpleClass();
            var reflector = cls.GetReflector();

            Assert.Null(reflector.Field("PrivateField"));
            Assert.Null(reflector.Property("PrivateProperty"));
            reflector.Field("PublicField").Value = 3;
            reflector.Property("PublicProperty").Value = 4;
            Assert.Equal("0 0 3 4", cls.ToString());

            reflector.DeclaredField("PrivateField").Value = 5;
            reflector.DeclaredProperty("PrivateProperty").Value = 6;
            reflector.DeclaredField("PublicField").Value = 7;
            reflector.DeclaredProperty("PublicProperty").Value = 8;
            Assert.Equal("5 6 7 8", cls.ToString());
        }

        [Fact]
        public void Test2()
        {
            var cls = new SimpleClass();
            var reflector = cls.GetReflector();

            Assert.Null(reflector.Field<int>("PrivateField"));
            Assert.Null(reflector.Property<int>("PrivateProperty"));
            reflector.Field<long>("PublicField").Value = 3;
            reflector.Property<long>("PublicProperty").Value = 4;
            Assert.Equal("0 0 3 4", cls.ToString());

            reflector.DeclaredField<int>("PrivateField").Value = 5;
            reflector.DeclaredProperty<int>("PrivateProperty").Value = 6;
            reflector.DeclaredField<long>("PublicField").Value = 7;
            reflector.DeclaredProperty<long>("PublicProperty").Value = 8;
            Assert.Equal("5 6 7 8", cls.ToString());
        }

        [Fact]
        public void InvokeTest()
        {
            var cls = new SimpleClass { PublicProperty = 4 };
            var reflector = cls.GetReflector();
            Assert.Equal("0 0 0 4", reflector.Invoke("ToString") as string);
        }

    }
}
