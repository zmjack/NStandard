using System;
using Xunit;

namespace NStandard.Test
{
    public class ConvertExTests
    {
        [Fact]
        public void Test1()
        {
            DateTime? ndt, expected;

            ndt = ConvertEx.ChangeType("2019/12/11 8:58:57", typeof(DateTime?)) as DateTime?;
            expected = (DateTime?)DateTime.Parse("2019/12/11 8:58:57");
            Assert.Equal(expected, ndt);

            ndt = ConvertEx.ChangeType(null, typeof(DateTime?)) as DateTime?;
            Assert.Null(ndt);

            Assert.Throws<InvalidCastException>(() => Convert.ChangeType("2019/12/11 8:58:57", typeof(DateTime?)));
        }

        [Fact]
        public void Test2()
        {
            var ndt = ConvertEx.ChangeType("2019/12/11 8:58:57", typeof(string));
        }
    }
}