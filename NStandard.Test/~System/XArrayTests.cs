using System;
using Xunit;

namespace NStandard.Test
{
    public class XArrayTests
    {
        private class Class { }

        [Fact]
        public void LetTest1()
        {
            var arr = new int[5].Let(i => i * 2 + 1);
            Assert.Equal(new[] { 1, 3, 5, 7, 9 }, arr);
        }

        [Fact]
        public void LetTest2()
        {
            var classes = new Class[2].Let(() => new Class());
            Assert.NotNull(classes[0]);
            Assert.NotNull(classes[1]);
        }

        [Fact]
        public void LUBoundTest()
        {
            var array = Array.CreateInstance(typeof(int), new[] { 2 }, new[] { 5 });
            Assert.Equal(5, array.LBound());
            Assert.Equal(6, array.UBound());
        }

        [Fact]
        public void ShuffleTest()
        {
            var random = new Random();
            var arr = new int[100].Let(i => i);
            arr.Shuffle();
        }


        [Fact]
        public void JaggedArrayTest()
        {
            var arr = new string[2, 3]
            {
                { "0,0", "0,1", "0,2" },
                { "1,0", "1,1", "1,2" },
            };
            arr.Each((v, i1, i2) =>
            {
                Assert.Equal($"{i1},{i2}", v);
                Assert.Equal($"{i1},{i2}", arr[i1, i2]);
            });

        }

    }
}
