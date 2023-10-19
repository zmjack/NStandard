using System;
using System.Text;
using Xunit;

namespace NStandard.Test
{
    public class ConvertExTests
    {
        [Fact]
        public void ChangeTypeTest()
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
        public void Base58Test()
        {
            Assert.Equal("zpsEBKbce3iT", ConvertEx.ToBase58String("NStandard".Pipe(Encoding.UTF8.GetBytes)));
            Assert.Equal("NStandard", ConvertEx.FromBase58String("zpsEBKbce3iT").Pipe(Encoding.UTF8.GetString));

            Assert.Equal("111zpsEBKbce3iT", ConvertEx.ToBase58String("\0\0\0NStandard".Pipe(Encoding.UTF8.GetBytes)));
            Assert.Equal("\0\0\0NStandard", ConvertEx.FromBase58String("111zpsEBKbce3iT").Pipe(Encoding.UTF8.GetString));
        }

        [Fact]
        public void Base32Test()
        {
        }

    }
}