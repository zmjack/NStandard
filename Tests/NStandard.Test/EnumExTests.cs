﻿using Xunit;

namespace NStandard.Test;

public class EnumExTests
{
    private enum TestEnums
    {
        Undefined = 0,
        A = 1,
        B = 2,
        D = 12,
        AB = A | B,
        AD = A | D,
    }

    [Fact]
    public void GetOptionsTest1()
    {
        var enumType = typeof(TestEnums);
        var enumOptions = EnumEx.GetOptions(typeof(TestEnums));
        Assert.Equal(
        [
            new EnumOption(enumType, nameof(TestEnums.Undefined)),
            new EnumOption(enumType, nameof(TestEnums.A)),
            new EnumOption(enumType, nameof(TestEnums.B)),
            new EnumOption(enumType, nameof(TestEnums.AB)),
            new EnumOption(enumType, nameof(TestEnums.D)),
            new EnumOption(enumType, nameof(TestEnums.AD)),
        ], enumOptions);
    }

    [Fact]
    public void GetOptionsTest2()
    {
        var enumOptions = EnumEx.GetOptions<TestEnums, int>();
        Assert.Equal(
        [
            new EnumOption<TestEnums, int>(nameof(TestEnums.Undefined)),
            new EnumOption<TestEnums, int>(nameof(TestEnums.A)),
            new EnumOption<TestEnums, int>(nameof(TestEnums.B)),
            new EnumOption<TestEnums, int>(nameof(TestEnums.AB)),
            new EnumOption<TestEnums, int>(nameof(TestEnums.D)),
            new EnumOption<TestEnums, int>(nameof(TestEnums.AD)),
        ], enumOptions);
    }

    [Fact]
    public void GetFlagsTest1()
    {
        Assert.Equal([TestEnums.A, TestEnums.B], EnumEx.GetFlags(typeof(TestEnums)));
        Assert.Equal([TestEnums.A, TestEnums.B], EnumEx.GetFlags<TestEnums>());
    }

    [Fact]
    public void GetFlagsTest2()
    {
        Assert.Equal([TestEnums.A], TestEnums.A.GetFlags());
        Assert.Equal([TestEnums.B], TestEnums.B.GetFlags());
        Assert.Equal(Array.Empty<TestEnums>(), TestEnums.D.GetFlags());
        Assert.Equal([TestEnums.A, TestEnums.B], TestEnums.AB.GetFlags());
        Assert.Equal([TestEnums.A], TestEnums.AD.GetFlags());
    }

}
