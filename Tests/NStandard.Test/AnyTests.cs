using NStandard.Infrastructure;
using System.Text;
using Xunit;

namespace NStandard.Test;

public class AnyTests
{
    [Fact]
    public void StrcutCastTest()
    {
        // Hex: 0x3c75c28f
        // Dec: 1014350479
        // Bin: 00111100 01110101 11000010 10001111
        Assert.Equal(0x3c75c28f, Any.Struct.Cast<int>(0.015F));
        Assert.Equal(0.015F, Any.Struct.Cast<float>(1014350479));
    }

    [Fact]
    public void ZipTest()
    {
        var starts = new[] { new DateTime(2018, 7, 15), new DateTime(2018, 8, 15), new DateTime(2019, 1, 1) };
        var ends = new[] { new DateTime(2018, 8, 15), new DateTime(2018, 9, 15) };
        var zip = Any.Zip(starts, ends);

        foreach (var (start, end) in zip)
        {
            Assert.Equal(31, (end - start).TotalDays);
        }

        Assert.Equal(62, zip.Sum(x => (x.Item2 - x.Item1).TotalDays));
        Assert.Equal(62, Any.Zip(starts, ends).Select(tuple => tuple.Item2 - tuple.Item1).Sum(x => x.TotalDays));
    }

    [Fact]
    public void FlatTest()
    {
        var d2 = new string[2, 2]
        {
            { "0", "1" },
            { "2", "3" }
        };
        Assert.Equal(["0", "1", "2", "3"], Any.Flat<string>(d2));
    }

    [Fact]
    public void FlatManyTest()
    {
        var d1_d1 = new string[][]
        {
            ["0", "1"],
            ["2", "3"],
        };
        Assert.Equal(["0", "1", "2", "3"], Any.Flat<string>(d1_d1));
    }

    [Fact]
    public void FlatDeepTest()
    {
        var array = new object[]
        {
            new[] { "0", "1" },
            new object[]
            {
                "2",
                new object[]
                {
                    "3", "4"
                }
            }
        };
        Assert.Equal(["0", "1", "2", "3", "4"], Any.Flat<string>(array));
    }

    [Fact]
    public void FlatThrowTest()
    {
        var array = new object[]
        {
            new[] { "0", "1" },
            new object[] { 2 },
        };
        Assert.ThrowsAny<InvalidCastException>(() => Any.Flat<string>(array).ToArray());
    }


    [Fact]
    public unsafe void FlatUnmanagedTest()
    {
        var d2 = new int[2, 2]
        {
            { 0, 1 },
            { 2, 3 }
        };
        var length = d2.GetSequenceLength();

        fixed (int* pd2 = d2)
        {
            Assert.Equal([0, 1, 2, 3], Any.Flat(pd2, length));
        }
    }

    [Fact]
    public unsafe void FlatUnmanagedManyTest()
    {
        var d1_d1 = new int[][]
        {
            [0, 1],
            [2, 3],
        };
        var lengths = d1_d1.Select(x => x.GetSequenceLength()).ToArray();

        fixed (int* pd0 = d1_d1[0])
        fixed (int* pd1 = d1_d1[1])
        {
            Assert.Equal([0, 1, 2, 3], Any.Flat(new[] { pd0, pd1 }, lengths));
        }
    }

    [Fact]
    public void ReDimReduceTest()
    {
        var d2 = new int[3, 3]
        {
            { 0, 1, 2 },
            { 3, 4, 5 },
            { 6, 7, 8 }
        };
        Any.ReDim(ref d2, 2, 2);

        Assert.Equal(new int[2, 2]
        {
            { 0, 1 },
            { 3, 4 },
        }, d2);
    }

    [Fact]
    public void ReDimExpandTest()
    {
        var d2 = new int[3, 3]
        {
            { 0, 1, 2 },
            { 3, 4, 5 },
            { 6, 7, 8 }
        };
        Any.ReDim(ref d2, 4, 4);

        Assert.Equal(new int[4, 4]
        {
            { 0, 1, 2, 0 },
            { 3, 4, 5, 0 },
            { 6, 7, 8, 0 },
            { 0, 0, 0, 0 },
        }, d2);
    }

    [Fact]
    public void HashTest()
    {
        var str = "nstandard.net";
        var chars = str.ToCharArray();

        Assert.Equal(739162880, Any.Text.ComputeHashCode(str));
        Assert.Equal(739162880, Any.Text.ComputeHashCode(chars));
        Assert.Equal(739162880, Any.Text.ComputeHashCode($"({str})".ToCharArray(), 1, 13));
        Assert.ThrowsAny<OverflowException>(() => Any.Text.ComputeHashCode($"({str})".ToCharArray(), 1, 15));
    }

    [Fact]
    public void ForwardTest()
    {
        var exception = new Exception("3", new Exception("2", new Exception("1")));
        var forwards = Any.Forward(exception, x => x.InnerException);

        /*
         * Exception 3      = First
         * - Exception 2
         * - - Exception 1  = Last
         */

        Assert.Equal("1", (from ex in forwards select ex).Last().Message);
        Assert.Equal("1", (from ex in forwards where ex.InnerException is null select ex).First().Message);
        Assert.Equal("2", (from iv in forwards.Pairs() where iv.Index == 1 select iv.Value).First().Message);
        Assert.Equal("3", (from iv in forwards.Pairs() select iv.Value).First().Message);
    }

    [Fact]
    public void MapTest_d1_d1()
    {
        var d1_d1 = new string[2][]
        {
            ["0"],
            ["1", "2"],
        };
        var result = d1_d1.Map((string s) => int.Parse(s)) as int[][];

        /* 
         * [
         *     [ 0 ],
         *     [ 1, 2 ]
         * ]
         */

        Assert.Equal(new[] { 0, 1, 2 }, Any.Flat<int>(result));
    }

    [Fact]
    public void MapTest_d1_d2()
    {
        var d1_d2 = new string[2][,]
        {
            new string[2, 1]
            {
                { "0" },
                { "1" },
            },
            new string[1, 2]
            {
                { "2", "3" },
            },
        };
        var result = d1_d2.Map((string s) => int.Parse(s)) as int[][,];

        /* 
         * [
         *     [
         *         ( 0 ),
         *         ( 1 )
         *     ],
         *     [
         *         ( 2, 3 )
         *     ]
         * ]
         */

        Assert.Equal(new[] { 0, 1, 2, 3 }, Any.Flat<int>(result));
    }

    [Fact]
    public void MapTest_d1_d2_d1()
    {
        var d1_d2_d1 = new string[2][,][]
        {
            new string[1, 2][]
            {
                {
                    new string [1] { "0", },
                    new string [2] { "1", "2" },
                },
            },
            new string[2, 1][]
            {
                {
                    new string [2] { "3", "4",},
                },
                {
                    new string [3] { "5", "6", "7"},
                },
            },
        };
        var result = d1_d2_d1.Map((string s) => int.Parse(s)) as int[][,][];

        /* 
         * [
         *     [
         *         (
         *             [ 0 ],
         *             [ 1, 2 ]
         *         )
         *     ],
         *     [
         *         (
         *             [ 3, 4 ]
         *         ),
         *         (
         *             [ 5, 6, 7 ]
         *         )
         *     ]
         * ]
         */

        Assert.Equal(new[] { 0, 1, 2, 3, 4, 5, 6, 7 }, Any.Flat<int>(result));
    }

    [Fact]
    public void ChainTest1()
    {
        var chainIterators = Any.Chain(1,
        [
            x => new int[2].Let(i => x + i),
            x => new int[3].Let(i => 10 * x + i),
        ])
            .Where(x => x.Origin == ChainOrigin.Current)
            .Select(x => x.Iterators.ToArray())
            .ToArray();

        Assert.Equal(
        [
            [1, 10],
            [1, 11],
            [1, 12],
            [2, 20],
            [2, 21],
            [2, 22],
        ], chainIterators);
    }

    [Fact]
    public void ChainTest2()
    {
        var chainIterators = Any.Chain(
        [
            [1, 2],
            [3, 4, 5],
        ])
            .Where(x => x.Origin == ChainOrigin.Current).Select(x => x.Iterators.ToArray())
            .ToArray();

        Assert.Equal(
        [
            [1, 3],
            [1, 4],
            [1, 5],
            [2, 3],
            [2, 4],
            [2, 5],
        ], chainIterators);
    }

    [Fact]
    public void ComposeTest()
    {
        var str2hex = Any<string>
            .Compose(Encoding.UTF8.GetBytes)
            .Compose(Convert.ToHexString);
        Assert.Equal("303030", str2hex("000"));
    }

}
