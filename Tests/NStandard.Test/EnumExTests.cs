using Xunit;

namespace NStandard.Test
{
    public class EnumExTests
    {
        private enum TestEnums
        {
            A = 1,
            B = 2,
            D = 12,
            AB = A | B,
            AD = A | D,
        }

        [Fact]
        public void Test1()
        {
            var enumType = typeof(TestEnums);
            var enumOptions = EnumEx.GetOptions(typeof(TestEnums));
            Assert.Equal(new[]
            {
                new EnumOption(enumType, nameof(TestEnums.A)),
                new EnumOption(enumType, nameof(TestEnums.B)),
                new EnumOption(enumType, nameof(TestEnums.AB)),
                new EnumOption(enumType, nameof(TestEnums.D)),
                new EnumOption(enumType, nameof(TestEnums.AD)),
            }, enumOptions);
        }

        [Fact]
        public void Test2()
        {
            var enumOptions = EnumEx.GetOptions<TestEnums, int>();
            Assert.Equal(new[]
            {
                new EnumOption<TestEnums, int>(nameof(TestEnums.A)),
                new EnumOption<TestEnums, int>(nameof(TestEnums.B)),
                new EnumOption<TestEnums, int>(nameof(TestEnums.AB)),
                new EnumOption<TestEnums, int>(nameof(TestEnums.D)),
                new EnumOption<TestEnums, int>(nameof(TestEnums.AD)),
            }, enumOptions);
        }

        [Fact]
        public void Test3()
        {
            Assert.Equal(new object[] { TestEnums.A, TestEnums.B }, EnumEx.GetFlags(typeof(TestEnums)));
            Assert.Equal(new[] { TestEnums.A, TestEnums.B }, EnumEx.GetFlags<TestEnums>());
        }

        [Fact]
        public void Test4()
        {
            Assert.Equal(new[] { TestEnums.A }, TestEnums.A.GetFlags());
            Assert.Equal(new[] { TestEnums.B }, TestEnums.B.GetFlags());
            Assert.Equal(new TestEnums[0], TestEnums.D.GetFlags());
            Assert.Equal(new[] { TestEnums.A, TestEnums.B }, TestEnums.AB.GetFlags());
            Assert.Equal(new[] { TestEnums.A }, TestEnums.AD.GetFlags());
        }

    }
}
