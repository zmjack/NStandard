using Xunit;

namespace NStandard.Test
{
    public class ValueWrapperTests
    {
        private static string ReturnString<T>(T str) where T : struct
        {
            if (str is ValueWrapper<string> value)
                return value.Value;
            else return default;
        }

        [Fact]
        public void VStringTest()
        {
            Assert.Equal("abc".GetHashCode(), ((ValueWrapper<string>)"abc").GetHashCode());
            Assert.Equal("abc", ((ValueWrapper<string>)"abc").ToString());

            Assert.True(((ValueWrapper<string>)"abc") == ((ValueWrapper<string>)"abc"));
            Assert.True(((ValueWrapper<string>)"abc") == "abc");
            Assert.True("abc" == ((ValueWrapper<string>)"abc"));

            Assert.False(((ValueWrapper<string>)"abc") != ((ValueWrapper<string>)"abc"));
            Assert.False(((ValueWrapper<string>)"abc") != "abc");
            Assert.False("abc" != ((ValueWrapper<string>)"abc"));
        }

        [Fact]
        public void FunctionWithStructGenericParameterTest()
        {
            Assert.Equal("abc", ReturnString(((ValueWrapper<string>)"abc")));
            Assert.Null(ReturnString(123));
        }

    }
}
