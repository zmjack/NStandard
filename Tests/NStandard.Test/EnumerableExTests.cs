using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NStandard.Test;

public class EnumerableExTests
{
    [Fact]
    public void Test1()
    {
        static IEnumerable<int> Range(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                yield return i;
            }
        }

        var ranges = new[]
        {
            Range(1, 3),
            Range(4, 6),
            Range(7, 9),
        }.AsEnumerable();

        var s = EnumerableEx.Concat(ranges).Join("");
        Assert.Equal("123456789", s);
    }

    [Fact]
    public void Test2()
    {
        var s = new string(EnumerableEx.Concat("123", "456", "789").ToArray());
        Assert.Equal("123456789", s);
    }

}
