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
            new int[2, 3].Let((i0, i1) => i0 * 3 + i1).Then(arr =>
            {
                Assert.Equal(new[,] { { 0, 1, 2 }, { 3, 4, 5 } }, arr);
            });

            new int[2, 3].Let(i => i).Then(arr =>
            {
                Assert.Equal(new[,] { { 0, 1, 2 }, { 3, 4, 5 } }, arr);
            });

            new int[2, 3].Let(new[] { 0, 1, 2, 3, 4, 5 }).Then(arr =>
            {
                Assert.Equal(new[,] { { 0, 1, 2 }, { 3, 4, 5 } }, arr);
            });
        }

        [Fact]
        public void LetTest3()
        {
            new int[2, 3, 2].Let((i0, i1, i2) => i0 * 6 + i1 * 2 + i2).Then(arr =>
            {
                Assert.Equal(new[, ,]
                {
                    { { 0, 1 }, { 2, 3 }, { 4, 5 } },
                    { { 6, 7 }, { 8, 9 }, { 10, 11 } },
                }, arr);
            });

            new int[2, 3, 2].Let(i => i).Then(arr =>
            {
                Assert.Equal(new[, ,]
                {
                    { { 0, 1 }, { 2, 3 }, { 4, 5 } },
                    { { 6, 7 }, { 8, 9 }, { 10, 11 } },
                }, arr);
            });

            new int[2, 3, 2].Let(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }).Then(arr =>
            {
                Assert.Equal(new[, ,]
                {
                    { { 0, 1 }, { 2, 3 }, { 4, 5 } },
                    { { 6, 7 }, { 8, 9 }, { 10, 11 } },
                }, arr);
            });
        }

        [Fact]
        public void LetClassTest()
        {
            var classes = new Class[2].Let(() => new Class());
            Assert.NotNull(classes[0]);
            Assert.NotNull(classes[1]);
        }

        [Fact]
        public void ToLinearTest()
        {
            new int[2, 2].Let(i => i).Then(arr =>
            {
                Assert.Equal(new[] { 0, 1, 2, 3 }, arr.ToLinearArray());
            });

            new int[2, 2, 2].Let(i => i).Then(arr =>
            {
                Assert.Equal(new[] { 0, 1, 2, 3, 4, 5, 6, 7 }, arr.ToLinearArray());
            });
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
