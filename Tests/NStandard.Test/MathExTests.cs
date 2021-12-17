using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NStandard.Test
{
    public class MathExTests
    {
        [Fact]
        public void PermutationTest()
        {
            Assert.Equal(1, MathEx.Permutation(0, 0));
            Assert.Equal(20, MathEx.Permutation(2, 5));
            Assert.Equal(60, MathEx.Permutation(3, 5));
            Assert.Equal(2000, MathEx.Permutation(1, 2000));
            Assert.Equal(2000 * 1999, MathEx.Permutation(2, 2000));
        }

        [Fact]
        public void CombinationTest()
        {
            Assert.Equal(1, MathEx.Combination(0, 0));
            Assert.Equal(10, MathEx.Combination(2, 5));
            Assert.Equal(10, MathEx.Combination(3, 5));
            Assert.Equal(2000 * 1999 / 2, MathEx.Combination(2, 2000));
        }

    }
}
