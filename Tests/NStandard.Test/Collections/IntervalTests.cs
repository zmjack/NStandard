using Xunit;

namespace NStandard.Collections.Test;

public class IntervalTests
{
    private static readonly Interval<int> _model1 = new()
    {
        (4, 6),
        (10, 12),
        (16, 18),
    };
    private static readonly Interval<int> _model2 = new()
    {
        (5, 5),
        (11, 11),
        (17, 17),
    };

    [Fact]
    public void AddTest1()
    {
        var range = (_model1 + (5, 17)).Ranges;

        Assert.Equal(
        [
            (4, 18),
        ], (_model1 + (5, 17)).Ranges);

        Assert.Equal(
        [
            (4, 14),
            (16, 18),
        ], (_model1 + (5, 14)).Ranges);

        Assert.Equal(
        [
            (4, 19),
        ], (_model1 + (5, 19)).Ranges);

        Assert.Equal(
        [
            (4, 6),
            (8, 18),
        ], (_model1 + (8, 17)).Ranges);

        Assert.Equal(
        [
            (4, 6),
            (8, 14),
            (16, 18),
        ], (_model1 + (8, 14)).Ranges);

        Assert.Equal(
        [
            (4, 6),
            (8, 19),
        ], (_model1 + (8, 19)).Ranges);

        Assert.Equal(
        [
            (2, 18),
        ], (_model1 + (2, 17)).Ranges);

        Assert.Equal(
        [
            (2, 14),
            (16, 18),
        ], (_model1 + (2, 14)).Ranges);

        Assert.Equal(
        [
            (1, 6),
            (10, 12),
            (16, 18),
        ], (_model1 + (1, 3)).Ranges);

        Assert.Equal(
        [
            (0, 2),
            (4, 6),
            (10, 12),
            (16, 18),
        ], (_model1 + (0, 2)).Ranges);

        Assert.Equal(
        [
            (4, 6),
            (10, 12),
            (16, 21),
        ], (_model1 + (19, 21)).Ranges);

        Assert.Equal(
        [
            (4, 6),
            (10, 12),
            (16, 18),
            (20, 22),
        ], (_model1 + (20, 22)).Ranges);

        Assert.Equal(
        [
            (2, 20),
        ], (_model1 + (2, 20)).Ranges);
    }

    [Fact]
    public void SubtractTest1()
    {
        Assert.Equal(
        [
            (4, 4),
            (18, 18),
        ], (_model1 - (5, 17)).Ranges);

        Assert.Equal(
        [
            (4, 4),
            (16, 18),
        ], (_model1 - (5, 14)).Ranges);

        Assert.Equal(
        [
            (4, 4),
        ], (_model1 - (5, 19)).Ranges);

        Assert.Equal(
        [
            (4, 6),
            (18, 18),
        ], (_model1 - (8, 17)).Ranges);

        Assert.Equal(
        [
            (4, 6),
            (16, 18),
        ], (_model1 - (8, 14)).Ranges);

        Assert.Equal(
        [
            (4, 6),
        ], (_model1 - (8, 19)).Ranges);

        Assert.Equal(
        [
            (18, 18),
        ], (_model1 - (2, 17)).Ranges);

        Assert.Equal(
        [
            (16, 18),
        ], (_model1 - (2, 14)).Ranges);

        Assert.Equal(
        [
            (4, 6),
            (10, 12),
            (16, 18),
        ], (_model1 - (1, 3)).Ranges);

        Assert.Equal(
        [
            (4, 6),
            (10, 12),
            (16, 18),
        ], (_model1 - (0, 2)).Ranges);

        Assert.Equal(
        [
            (4, 6),
            (10, 12),
            (16, 18),
        ], (_model1 - (19, 21)).Ranges);

        Assert.Equal(
        [
            (4, 6),
            (10, 12),
            (16, 18),
        ], (_model1 - (20, 22)).Ranges);

        Assert.Empty((_model1 - (2, 20)).Ranges);
    }

    [Fact]
    public void AddTest2()
    {
        Assert.Equal(
        [
            (5, 17),
        ], (_model2 + (5, 17)).Ranges);

        Assert.Equal(
        [
            (5, 14),
            (17, 17),
        ], (_model2 + (5, 14)).Ranges);

        Assert.Equal(
        [
            (5, 19),
        ], (_model2 + (5, 19)).Ranges);

        Assert.Equal(
        [
            (5, 5),
            (8, 17),
        ], (_model2 + (8, 17)).Ranges);

        Assert.Equal(
        [
            (5, 5),
            (8, 14),
            (17, 17),
        ], (_model2 + (8, 14)).Ranges);

        Assert.Equal(
        [
            (5, 5),
            (8, 19),
        ], (_model2 + (8, 19)).Ranges);

        Assert.Equal(
        [
            (2, 17),
        ], (_model2 + (2, 17)).Ranges);

        Assert.Equal(
        [
            (2, 14),
            (17, 17),
        ], (_model2 + (2, 14)).Ranges);

        Assert.Equal(
        [
            (1, 3),
            (5, 5),
            (11, 11),
            (17, 17),
        ], (_model2 + (1, 3)).Ranges);

        Assert.Equal(
        [
            (0, 2),
            (5, 5),
            (11, 11),
            (17, 17),
        ], (_model2 + (0, 2)).Ranges);

        Assert.Equal(
        [
            (5, 5),
            (11, 11),
            (17, 17),
            (19, 21),
        ], (_model2 + (19, 21)).Ranges);

        Assert.Equal(
        [
            (5, 5),
            (11, 11),
            (17, 17),
            (20, 22),
        ], (_model2 + (20, 22)).Ranges);

        Assert.Equal(
        [
            (2, 20),
        ], (_model2 + (2, 20)).Ranges);
    }

    [Fact]
    public void SubtractTest2()
    {
        Assert.Empty((_model2 - (5, 17)).Ranges);

        Assert.Equal(
        [
            (17, 17),
        ], (_model2 - (5, 14)).Ranges);

        Assert.Empty((_model2 - (5, 19)).Ranges);

        Assert.Equal(
        [
            (5, 5),
        ], (_model2 - (8, 17)).Ranges);

        Assert.Equal(
        [
            (5, 5),
            (17, 17),
        ], (_model2 - (8, 14)).Ranges);

        Assert.Equal(
        [
            (5, 5),
        ], (_model2 - (8, 19)).Ranges);

        Assert.Empty((_model2 - (2, 17)).Ranges);

        Assert.Equal(
        [
            (17, 17),
        ], (_model2 - (2, 14)).Ranges);

        Assert.Equal(
        [
            (5, 5),
            (11, 11),
            (17, 17),
        ], (_model2 - (1, 3)).Ranges);

        Assert.Equal(
        [
            (5, 5),
            (11, 11),
            (17, 17),
        ], (_model2 - (0, 2)).Ranges);

        Assert.Equal(
        [
            (5, 5),
            (11, 11),
            (17, 17),
        ], (_model2 - (19, 21)).Ranges);

        Assert.Equal(
        [
            (5, 5),
            (11, 11),
            (17, 17),
        ], (_model2 - (20, 22)).Ranges);

        Assert.Empty((_model2 - (2, 20)).Ranges);
    }

    [Fact]
    public void ContainsTest()
    {
        var interval = new Interval<int> { 1, 2, 4, 5, 6 };

        Assert.False(interval.Contains(new() { (1, 6) }));
        Assert.False(interval.Contains(new() { (2, 3) }));
        Assert.False(interval.Contains(new() { (2, 4) }));
        Assert.True(interval.Contains(new() { (1, 2) }));
        Assert.True(interval.Contains(new() { (4, 6) }));
        Assert.True(interval.Contains(new() { (5, 6) }));
    }

    [Fact]
    public void EachTest()
    {
        var interval = new Interval<int>()
        {
            5,
            (2, 3),
            (7, 8),
        };
        Assert.Equal([2, 3, 5, 7, 8], interval);
    }
}
