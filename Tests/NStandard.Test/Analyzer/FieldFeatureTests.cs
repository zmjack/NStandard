using NStandard.Design;
using Xunit;

namespace NStandard.Analyzer.Test;

[FieldFeature]
public partial class Model
{
    [FixedArray(2)]
    public partial int[] FixedSize2 { get; set; }
}

public class FieldFeatureTests
{
    private static void RunLocationTest<T>() where T : IFieldFeatureLocationTestModel, new()
    {
        var item = new T { Number = 666 };
        Assert.Equal(666, item.Number);
    }

    [Fact]
    public void NormalTest()
    {
        var item = new Model { };
        Assert.Equal([0, 0], item.FixedSize2);
        Assert.ThrowsAny<ArgumentException>(() => item.FixedSize2 = [1, 2, 3, 4]);
        item.FixedSize2 = [7, 77];
        Assert.Equal([7, 77], item.FixedSize2);
    }

    [Fact]
    public void LocationTest()
    {
        RunLocationTest<FieldFeatureClassWrapper.Model>();
        RunLocationTest<FieldFeatureClassWrapper.ValueModel>();
        RunLocationTest<FieldFeatureStructWrapper.Model>();
        RunLocationTest<FieldFeatureStructWrapper.ValueModel>();
    }
}
