using Xunit;

namespace NStd.Test
{
    public class VStringTests
    {
        private string ReturnString<T>(T str) where T : struct
        {
            return VString.GetString(str);
        }

        [Fact]
        public void VStringTest()
        {
            Assert.Equal("abc".GetHashCode(), "abc".VString().GetHashCode());
            Assert.Equal("abc", "abc".VString().ToString());

            Assert.True("abc".VString() == "abc".VString());
            Assert.True("abc".VString() == "abc");
            Assert.True("abc" == "abc".VString());

            Assert.False("abc".VString() != "abc".VString());
            Assert.False("abc".VString() != "abc");
            Assert.False("abc" != "abc".VString());
        }

        [Fact]
        public void FunctionWithStructGenericParameterTest()
        {
            Assert.Equal("abc", ReturnString("abc".VString()));
            Assert.Null(ReturnString(123));
        }

    }
}
