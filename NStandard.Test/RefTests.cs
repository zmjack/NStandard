using Xunit;

namespace NStandard.Test
{
    public class RefTests
    {
        [Fact]
        public void AssertTest()
        {
            int eight = 8;
            var rs = new Ref<int>[] { eight, eight };
            Assert.Equal(rs[0], rs[1]);
            Assert.False(rs[0] == rs[1]);
            Assert.False(rs[0].RefValue == rs[1].RefValue);
        }

        [Fact]
        public void AssertTest2()
        {
            object eight = 8;
            var rs = new[] { new Ref<int>(eight), new Ref<int>(eight) };
            Assert.Equal(rs[0], rs[1]);
            Assert.True(rs[0] == rs[1]);
            Assert.True(rs[0].RefValue == rs[1].RefValue);
        }

        [Fact]
        public void RRTest()
        {
            var rs = new Ref<int>[] { 8, 8 };

            Assert.True(rs[0].Value == rs[1].Value);
            Assert.False(rs[0].RefValue == rs[1].RefValue);
            Assert.False(rs[0] == rs[1]);

            Assert.False(rs[0].Value != rs[1].Value);
            Assert.True(rs[0].RefValue != rs[1].RefValue);
            Assert.True(rs[0] != rs[1]);

            Assert.True(rs[0].Equals(rs[1]));
        }

        [Fact]
        public void RITest()
        {
            Ref<int> ri = Ref.Bind(0);
            int i = 0;

            Assert.True(ri.Value == i);
            Assert.True(i == ri.Value);
            Assert.False(ri.RefValue == (object)i);
            Assert.False((object)i == ri.RefValue);
            Assert.False(ri == i);
            Assert.False(i == ri);

            Assert.False(ri.Value != i);
            Assert.False(i != ri.Value);
            Assert.True(ri.RefValue != (object)i);
            Assert.True((object)i != ri.RefValue);
            Assert.True(ri != i);
            Assert.True(i != ri);

            Assert.True(ri.Equals(i));
            Assert.True(i.Equals(ri));
        }

        [Fact]
        public void RDTest()
        {
            Ref<double> rd = Ref.Bind(0d);
            int i = 0;

            Assert.True(rd.Value == i);
            Assert.True(i == rd.Value);
            Assert.False(rd.RefValue == (object)i);
            Assert.False((object)i == rd.RefValue);
            Assert.False(rd == i);
            Assert.False(i == rd);

            Assert.False(rd.Value != i);
            Assert.False(i != rd.Value);
            Assert.True(rd.RefValue != (object)i);
            Assert.True((object)i != rd.RefValue);
            Assert.True(rd != i);
            Assert.True(i != rd);

            Assert.False(rd.Equals(i));
            Assert.False(i.Equals(rd));
        }

    }
}
