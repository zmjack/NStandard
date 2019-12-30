using NLinq;
using System;
using System.Linq;
using Xunit;

namespace NStandard.Test
{
    public partial class DpContainerTests
    {
        /// <summary>
        /// Longest increasing subsequence
        /// </summary>
        public class DpLIS : DpContainer<int, int[]>
        {
            public int[] Sequence { get; private set; }

            public DpLIS(int[] sequence)
            {
                Sequence = sequence;
            }

            public override int[] StateTransfer(int i)
            {
                if (i == 0)
                {
                    return new[] { Sequence[i] };
                }

                var mi = new int[i].Let(i => i)
                    .Where(j => this[j].Last() < Sequence[i])
                    .WhereMax(j => this[j].Length)
                    .MaxOrDefault(-1);

                switch (mi)
                {
                    case int _mi when _mi == -1:
                        return this[i - 1];

                    case int _mi when _mi == i - 1:
                        return this[mi].Concat(new[] { Sequence[i] }).ToArray();

                    case int _mi when _mi < i - 1:
                        var scheme = new[]
                        {
                            (Scheme: 0, AfterSubSequence: this[i - 1]),
                            (Scheme: 1, AfterSubSequence: this[mi].Concat(new[] { Sequence[i] }).ToArray()),
                        }
                        .WhereMax(x => x.AfterSubSequence.Length)
                        .WhereMin(x => x.AfterSubSequence.Last())
                        .First();

                        return scheme.AfterSubSequence;

                    default: throw new NotSupportedException();
                }
            }

            public int[] FindResult() => this[Sequence.Length - 1];

        }

        [Fact]
        public void DpLISTest()
        {
            var lis1 = new DpLIS(new[] { 2, 7, 1, 5, 6, 4, 3, 8, 9 });
            var s = lis1.FindResult();

            Assert.Equal(new[] { 2 }, lis1[0]);
            Assert.Equal(new[] { 2, 7 }, lis1[1]);
            Assert.Equal(new[] { 2, 7 }, lis1[2]);
            Assert.Equal(new[] { 2, 5 }, lis1[3]);
            Assert.Equal(new[] { 2, 5, 6 }, lis1[4]);
            Assert.Equal(new[] { 2, 5, 6 }, lis1[5]);
            Assert.Equal(new[] { 2, 5, 6 }, lis1[6]);
            Assert.Equal(new[] { 2, 5, 6, 8 }, lis1[7]);
            Assert.Equal(new[] { 2, 5, 6, 8, 9 }, lis1[8]);
        }

    }
}
