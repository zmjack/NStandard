using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NStandard.Test.Collections
{
    public class BoundariesTests
    {
        private static Int32Boundaries NewModel1()
        {
            return new Int32Boundaries()
            {
                (4, 6),
                (10, 12),
                (16, 18),
            };
        }

        private static Int32Boundaries NewModel2()
        {
            return new Int32Boundaries()
            {
                (5, 5),
                (11, 11),
                (17, 17),
            };
        }

        [Fact]
        public void AddTest1()
        {
            Assert.Equal(new[]
            {
                (4, 18),
            }, NewModel1() + (5, 17));

            Assert.Equal(new[]
            {
                (4, 14),
                (16, 18),
            }, NewModel1() + (5, 14));

            Assert.Equal(new[]
            {
                (4, 19),
            }, NewModel1() + (5, 19));

            Assert.Equal(new[]
            {
                (4, 6),
                (8, 18),
            }, NewModel1() + (8, 17));

            Assert.Equal(new[]
            {
                (4, 6),
                (8, 14),
                (16, 18),
            }, NewModel1() + (8, 14));

            Assert.Equal(new[]
            {
                (4, 6),
                (8, 19),
            }, NewModel1() + (8, 19));

            Assert.Equal(new[]
            {
                (2, 18),
            }, NewModel1() + (2, 17));

            Assert.Equal(new[]
            {
                (2, 14),
                (16, 18),
            }, NewModel1() + (2, 14));

            Assert.Equal(new[]
            {
                (1, 6),
                (10, 12),
                (16, 18),
            }, NewModel1() + (1, 3));

            Assert.Equal(new[]
            {
                (0, 2),
                (4, 6),
                (10, 12),
                (16, 18),
            }, NewModel1() + (0, 2));

            Assert.Equal(new[]
            {
                (4, 6),
                (10, 12),
                (16, 21),
            }, NewModel1() + (19, 21));

            Assert.Equal(new[]
            {
                (4, 6),
                (10, 12),
                (16, 18),
                (20, 22),
            }, NewModel1() + (20, 22));

            Assert.Equal(new[]
            {
                (2, 20),
            }, NewModel1() + (2, 20));
        }

        [Fact]
        public void SubtractTest1()
        {
            Assert.Equal(new[]
            {
                (4, 4),
                (18, 18),
            }, NewModel1() - (5, 17));

            Assert.Equal(new[]
            {
                (4, 4),
                (16, 18),
            }, NewModel1() - (5, 14));

            Assert.Equal(new[]
            {
                (4, 4),
            }, NewModel1() - (5, 19));

            Assert.Equal(new[]
            {
                (4, 6),
                (18, 18),
            }, NewModel1() - (8, 17));

            Assert.Equal(new[]
            {
                (4, 6),
                (16, 18),
            }, NewModel1() - (8, 14));

            Assert.Equal(new[]
            {
                (4, 6),
            }, NewModel1() - (8, 19));

            Assert.Equal(new[]
            {
                (18, 18),
            }, NewModel1() - (2, 17));

            Assert.Equal(new[]
            {
                (16, 18),
            }, NewModel1() - (2, 14));

            Assert.Equal(new[]
            {
                (4, 6),
                (10, 12),
                (16, 18),
            }, NewModel1() - (0, 2));

            Assert.Equal(new[]
            {
                (4, 6),
                (10, 12),
                (16, 18),
            }, NewModel1() - (20, 22));

            Assert.Empty(NewModel1() - (2, 20));
        }

        [Fact]
        public void AddTest2()
        {
            Assert.Equal(new[]
            {
                (4, 18),
            }, NewModel2() + (5, 17));

            Assert.Equal(new[]
            {
                (4, 14),
                (16, 18),
            }, NewModel2() + (5, 14));

            Assert.Equal(new[]
            {
                (4, 19),
            }, NewModel2() + (5, 19));

            Assert.Equal(new[]
            {
                (4, 6),
                (8, 18),
            }, NewModel2() + (8, 17));

            Assert.Equal(new[]
            {
                (4, 6),
                (8, 14),
                (16, 18),
            }, NewModel2() + (8, 14));

            Assert.Equal(new[]
            {
                (4, 6),
                (8, 19),
            }, NewModel2() + (8, 19));

            Assert.Equal(new[]
            {
                (2, 18),
            }, NewModel2() + (2, 17));

            Assert.Equal(new[]
            {
                (2, 14),
                (16, 18),
            }, NewModel2() + (2, 14));

            Assert.Equal(new[]
            {
                (1, 6),
                (10, 12),
                (16, 18),
            }, NewModel2() + (1, 3));

            Assert.Equal(new[]
            {
                (0, 2),
                (4, 6),
                (10, 12),
                (16, 18),
            }, NewModel2() + (0, 2));

            Assert.Equal(new[]
            {
                (4, 6),
                (10, 12),
                (16, 21),
            }, NewModel2() + (19, 21));

            Assert.Equal(new[]
            {
                (4, 6),
                (10, 12),
                (16, 18),
                (20, 22),
            }, NewModel2() + (20, 22));

            Assert.Equal(new[]
            {
                (2, 20),
            }, NewModel2() + (2, 20));
        }

        [Fact]
        public void SubtractTest2()
        {
            Assert.Equal(new[]
            {
                (4, 4),
                (18, 18),
            }, NewModel2() - (5, 17));

            Assert.Equal(new[]
            {
                (4, 4),
                (16, 18),
            }, NewModel2() - (5, 14));

            Assert.Equal(new[]
            {
                (4, 4),
            }, NewModel2() - (5, 19));

            Assert.Equal(new[]
            {
                (4, 6),
                (18, 18),
            }, NewModel2() - (8, 17));

            Assert.Equal(new[]
            {
                (4, 6),
                (16, 18),
            }, NewModel2() - (8, 14));

            Assert.Equal(new[]
            {
                (4, 6),
            }, NewModel2() - (8, 19));

            Assert.Equal(new[]
            {
                (18, 18),
            }, NewModel2() - (2, 17));

            Assert.Equal(new[]
            {
                (16, 18),
            }, NewModel2() - (2, 14));

            Assert.Equal(new[]
            {
                (4, 6),
                (10, 12),
                (16, 18),
            }, NewModel2() - (0, 2));

            Assert.Equal(new[]
            {
                (4, 6),
                (10, 12),
                (16, 18),
            }, NewModel2() - (20, 22));

            Assert.Empty(NewModel2() - (2, 20));
        }
    }
}
