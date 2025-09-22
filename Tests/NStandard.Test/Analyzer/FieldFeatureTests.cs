using Xunit;

namespace NStandard.Analyzer.Test;

#region LegacyModel
public partial class LegacyModel
{
    private int _number;
    public int Number
    {
        get => _number;
        set => _number = value;
    }

    private int[] _numbers;
    public int[] Numbers
    {
        get => _numbers;
        set => _numbers = value;
    }
}
#endregion

public class FieldFeatureTests
{
    private static void Test<T>() where T : new()
    {
        dynamic first = new T();
        first.Number = 123;
        first.Numbers = new int[] { 1, 2, 3 };

        dynamic second = new T();
        second.Number = 123;
        second.Numbers = new int[] { 1, 2, 3 };

        Assert.Equal(123, second.Number);
        Assert.Equal([1, 2, 3], second.Numbers as int[]);
        Assert.Equal(123, second.Number);
        Assert.Equal([1, 2, 3], second.Numbers as int[]);
    }

    [Fact]
    public void AllTests()
    {
        Test<LegacyModel>();

        Test<Model>();
        Test<ValueModel>();
        Test<ClassWrapper.Model>();
        Test<ClassWrapper.ValueModel>();
        Test<StructWrapper.Model>();
        Test<StructWrapper.ValueModel>();

        Test<global::Model>();
        Test<global::ValueModel>();
        Test<global::ClassWrapper.Model>();
        Test<global::ClassWrapper.ValueModel>();
        Test<global::StructWrapper.Model>();
        Test<global::StructWrapper.ValueModel>();
    }
}
