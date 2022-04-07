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

            Assert.Equal(1, (a + b).GetValue("MB"));
            Assert.Equal(1024, (a + b).GetValue("KB"));
            Assert.Equal(1024, (a + b).Unit("KB").Value);

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

        [Fact]
        public void Test2()
        {
            var values = new StorageValue[3].Let(i => new StorageValue(i * 5));

            var sum = new StorageValue();
            sum.QuickSum(values);
            Assert.Equal(15, sum.BitValue);

            var average = new StorageValue();
            average.QuickAverage(values);
            Assert.Equal(5, average.BitValue);
        }

    }
}
