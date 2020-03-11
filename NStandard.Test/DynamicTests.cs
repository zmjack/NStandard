using Xunit;

namespace NStandard.Test
{
    public class DynamicTests
    {
        [Fact]
        public void Test1()
        {
            static T AddChecked<T>(T left, T right) where T : unmanaged
            {
                return Dynamic.OpAddChecked(left, right);
            }
            Assert.Equal(416, AddChecked(400, 16));
        }

    }
}
