using System.Diagnostics.CodeAnalysis;
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

            [SuppressMessage("Code Quality", "IDE0052:Remove unread private members", Justification = "<Pending>")]
            [SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "<Pending>")]
            private Inner Inner = new();

            public override string ToString() => $"{PrivateField} {PrivateProperty} {PublicField} {PublicProperty}";
        }

        private class InnerSuper
        {
            public int Public { get; set; }

            [SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
            private int Private { get; set; }
        }

        private class Inner : InnerSuper
        {
        }

        [Fact]
        public void FieldAndPropertyTest()
        {
            var cls = new Outter();
            var reflector = cls.GetReflector();

            Assert.Null(reflector.Field("PrivateField"));
            Assert.Null(reflector.Property("PrivateProperty"));
            reflector.Field("PublicField").Value = 3;
            reflector.Property("PublicProperty").Value = 4;
            Assert.Null(reflector.Field("Inner"));
            Assert.Equal("0 0 3 4", cls.ToString());

            reflector.DeclaredField("PrivateField").Value = 5;
            reflector.DeclaredProperty("PrivateProperty").Value = 6;
            reflector.DeclaredField("PublicField").Value = 7;
            reflector.DeclaredProperty("PublicProperty").Value = 8;
            Assert.Equal("5 6 7 8", cls.ToString());
        }

        [Fact]
        public void GenericFieldAndPropertyTest()
        {
            var cls = new Outter();
            var reflector = cls.GetReflector();

            Assert.Null(reflector.Field<int>("PrivateField"));
            Assert.Null(reflector.Property<int>("PrivateProperty"));
            reflector.Field<long>("PublicField").Value = 3;
            reflector.Property<long>("PublicProperty").Value = 4;
            Assert.Null(reflector.Field<Inner>("Inner"));
            Assert.Equal("0 0 3 4", cls.ToString());

            reflector.DeclaredField<int>("PrivateField").Value = 5;
            reflector.DeclaredProperty<int>("PrivateProperty").Value = 6;
            reflector.DeclaredField<long>("PublicField").Value = 7;
            reflector.DeclaredProperty<long>("PublicProperty").Value = 8;
            Assert.Equal("5 6 7 8", cls.ToString());
        }

        [Fact]
        public void MethodTest()
        {
            var cls = new Outter { PublicProperty = 4 };
            var reflector = cls.GetReflector();
            Assert.Equal("0 0 0 4", reflector.Method("ToString").Call() as string);
            Assert.Equal("0 0 0 4", reflector.Method("ToString").Call() as string);
        }

        [Fact]
        public void InvokeAndChainTest()
        {
            var cls = new Outter { PublicProperty = 4 };
            var reflector = cls.GetReflector();

            Assert.NotNull(reflector.DeclaredField("Inner").Property("Public"));
            Assert.Null(reflector.DeclaredField("Inner").Property("Private"));
            Assert.Null(reflector.DeclaredField("Inner").DeclaredProperty("Public"));
            Assert.Null(reflector.DeclaredField("Inner").DeclaredProperty("Private"));

            Assert.NotNull(reflector.DeclaredField<InnerSuper>("Inner").Property("Public"));
            Assert.Null(reflector.DeclaredField<InnerSuper>("Inner").Property("Private"));
            Assert.NotNull(reflector.DeclaredField<InnerSuper>("Inner").DeclaredProperty("Public"));
            Assert.NotNull(reflector.DeclaredField<InnerSuper>("Inner").DeclaredProperty("Private"));
        }

        [Fact]
        public void ObjectBasicMethodTest()
        {
            var number = 416;
            var reflector = number.GetReflector();

            Assert.Equal("416", reflector.ToStringMethod().Call());
            Assert.Equal(416, reflector.GetHashCodeMethod().Call());
        }

        [Fact]
        public void OnlyTypeReflectorTest()
        {
            var cls = new Outter { PublicProperty = 416 };
            var field = typeof(Outter).GetTypeReflector().DeclaredProperty<long>(nameof(Outter.PublicProperty));

            Assert.Equal(416, field.GetValue(cls));
            field.SetValue(cls, 417);
            Assert.Equal(417, cls.PublicProperty);
        }

    }
}
