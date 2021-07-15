using Xunit;

namespace NStandard.UnitValues.Test
{
    public class StorageUnitTests
    {
        [Fact]
        public void Test1()
        {
            var a = StorageValue.Parse(".5 MB");
            var b = new StorageValue(512, "KB");

            Assert.Equal(1024, (a + b).Format("KB").Value);
            Assert.Equal(1024 * 1024, (a + b).Format("B").Value);

            Assert.Equal(1, (a + b).Value);
            Assert.Equal(0, (a - b).Value);
            Assert.Equal(1, (a * 2).Value);
            Assert.Equal(0.25, (a / 2).Value);

            Assert.True(a == b);
            Assert.False(a != b);

            Assert.False(a < b);
            Assert.True(a <= b);
            Assert.False(a > b);
            Assert.True(a >= b);
        }

    }
}
