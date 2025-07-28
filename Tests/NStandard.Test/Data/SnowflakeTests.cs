using NStandard.Algorithm;
using Xunit;

namespace NStandard.Data.Test;

public class SnowflakeTests
{
    private class FixedSnowflakeWorker : SnowflakeWorker
    {
        public FixedSnowflakeWorker(long workerId, long dataCenterId) : base(workerId, dataCenterId)
        {
        }

        protected override long NewTimestamp() => 0x12c148d03c1;
    }

    private class FixedSnowflakeWorker128 : SnowflakeWorker128
    {
        public FixedSnowflakeWorker128(long workerId, long dataCenterId) : base(workerId, dataCenterId)
        {
        }

        protected override Int128 NewTimestamp() => 0x12c148d03c1;
    }

    [Fact]
    public void LongTest()
    {
        var snowflake = new FixedSnowflakeWorker(31, 31);
        var id = snowflake.NewId();
        Assert.Equal(4190208, id);
    }

    [Fact]
    public void Int128Test()
    {
        var snowflake = new FixedSnowflakeWorker128(0xff, 0xcc);
        var id = snowflake.NewId();
        Assert.Equal(new Int128(0x0000012c148d03c1, 0x00cc00ff00000000), id);
    }

    [Fact]
    public void GuidTest()
    {
        var snowflake = new FixedSnowflakeWorker128(0xff, 0xcc);
        var guid = snowflake.NewGuid();
        Assert.Equal(Guid.Parse("00000000-00ff-00cc-c103-8d142c010000"), guid);
    }
}
