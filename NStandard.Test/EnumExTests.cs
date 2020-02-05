using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NStandard.Test
{
    public class EnumExTests
    {
        private enum ETest
        {
            A = 1,
            B = 2,
            C = 4,
            BC = B | C,
        }

        [Fact]
        public void Test1()
        {
            var enumType = typeof(ETest);
            var enumOptions = EnumEx.GetOptions(typeof(ETest));
            Assert.Equal(new[]
            {
                new EnumOption(enumType, nameof(ETest.A)),
                new EnumOption(enumType, nameof(ETest.B)),
                new EnumOption(enumType, nameof(ETest.C)),
                new EnumOption(enumType, nameof(ETest.BC)),
            }, enumOptions);
        }

        [Fact]
        public void Test2()
        {
            var enumOptions = EnumEx.GetOptions<ETest, int>();
            Assert.Equal(new[]
            {
                new EnumOption<ETest, int>(nameof(ETest.A)),
                new EnumOption<ETest, int>(nameof(ETest.B)),
                new EnumOption<ETest, int>(nameof(ETest.C)),
                new EnumOption<ETest, int>(nameof(ETest.BC)),
            }, enumOptions);
        }

    }
}
