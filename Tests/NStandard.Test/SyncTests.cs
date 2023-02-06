using System;
using System.Linq;
using Xunit;

namespace NStandard.Test
{
    public class SyncTests
    {
        [Fact]
        public void BindTest()
        {
            using var a = new Sync<int>(1);
            using var r = Sync.From(() => a + a);
            Assert.Single(r.Dependencies);
        }

        [Fact]
        public void ArrayTest()
        {
            var syncs = new Sync<int>[]
            {
                new Sync<int>(1),
                new Sync<int>(2),
            };
            using var result = Sync.From(() => syncs.Select(x => x.Value).Sum());

            Assert.False(result.IsValueCreated);
            Assert.Equal(3, result.Value);
            Assert.True(result.IsValueCreated);

            syncs[0].Value = 3;

            Assert.False(result.IsValueCreated);
            Assert.Equal(5, result.Value);
            Assert.True(result.IsValueCreated);
        }

        [Fact]
        public void NewTest()
        {
            using var a = new Sync<int>(1);
            using var result = Sync.From(() => new { a.Value }.Value);

            Assert.False(result.IsValueCreated);
            Assert.Equal(1, result.Value);
            Assert.True(result.IsValueCreated);
        }

        [Fact]
        public void NewArrayTest()
        {
            using var a = new Sync<int>();
            using var b = new Sync<double>(1);
            using var result = Sync.From(() => new[] { a.Value, b.Value }.Sum());

            Assert.False(result.IsValueCreated);
            Assert.Equal(1, result.Value);
            Assert.True(result.IsValueCreated);

            a.Value = 1;
            b.Value = 2;

            Assert.False(result.IsValueCreated);
            Assert.Equal(3, result.Value);
        }

        [Fact]
        public void OnChangeTest()
        {
            using var a = new Sync<int>();
            using var result = Sync.From(() => new { a.Value }.Value);

            a.Changed += value => Assert.Equal(0, value % 2);
            a.Value = (int)(DateTime.Now.Ticks % 100) * 2;
        }

        [Fact]
        public void Test()
        {
            using var a = new Sync<int>(1);
            using var b = new Sync<double>(1.2);
            using var c = new Sync<int>(10);
            var n = 0;

            using var r = Sync.From(() => a + (int)Math.Round(b.Value) + 1);
            using var r_plus_c_plus_2 = Sync.From(() => r + c + 2 + n);

            Assert.Empty(a.Dependencies);
            Assert.Empty(b.Dependencies);
            Assert.Equal(2, r.Dependencies.Length);
            Assert.Equal(3, r_plus_c_plus_2.Dependencies.Length);

            Assert.False(r.IsValueCreated);
            Assert.Equal(3, r.Value);
            Assert.True(r.IsValueCreated);

            Assert.False(r_plus_c_plus_2.IsValueCreated);
            Assert.Equal(15, r_plus_c_plus_2.Value);
            Assert.True(r_plus_c_plus_2.IsValueCreated);

            a.Value = 2;

            Assert.False(r.IsValueCreated);
            Assert.Equal(4, r.Value);
            Assert.True(r.IsValueCreated);

            Assert.False(r_plus_c_plus_2.IsValueCreated);
            Assert.Equal(16, r_plus_c_plus_2.Value);
            Assert.True(r_plus_c_plus_2.IsValueCreated);

            b.Value = 1.6;

            Assert.False(r.IsValueCreated);
            Assert.Equal(5, r.Value);
            Assert.True(r.IsValueCreated);

            Assert.False(r_plus_c_plus_2.IsValueCreated);
            Assert.Equal(17, r_plus_c_plus_2.Value);
            Assert.True(r_plus_c_plus_2.IsValueCreated);

            // Will only trigger the update operation of r_plus_c_plus_2
            c.Value = 20;

            Assert.True(r.IsValueCreated);
            Assert.Equal(5, r.Value);
            Assert.True(r.IsValueCreated);

            Assert.False(r_plus_c_plus_2.IsValueCreated);
            Assert.Equal(27, r_plus_c_plus_2.Value);
            Assert.True(r_plus_c_plus_2.IsValueCreated);

            // Does not trigger an update operation.
            n = 8;

            Assert.True(r.IsValueCreated);
            Assert.Equal(5, r.Value);
            Assert.True(r.IsValueCreated);

            Assert.True(r_plus_c_plus_2.IsValueCreated);
            Assert.Equal(27, r_plus_c_plus_2.Value);
            Assert.True(r_plus_c_plus_2.IsValueCreated);
        }
    }
}
