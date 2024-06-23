using Xunit;

namespace NStandard.Test;

public class StateTests
{
    [Fact]
    public void BindTest()
    {
        using var a = State.Use(1);
        using var r = State.From(() => a + a);
        Assert.Single(r.Dependencies);
    }

    [Fact]
    public void BindStringTest()
    {
        using var a = State.Use("1");
        using var b = State.Use("2");
        using var r = State.From(() => a.Value + b.Value);
        Assert.Equal(2, r.Dependencies.Length);
        Assert.Equal("12", r.Value);
    }

    [Fact]
    public void ArrayTest()
    {
        var syncs = new State<int>[]
        {
            State.Use(1),
            State.Use(2),
        };
        using var result = State.From(() => syncs.Select(x => x.Value).Sum());

        Assert.False(result.IsValueCreated);
        Assert.Equal(3, result.Value);
        Assert.True(result.IsValueCreated);

        syncs[0].Value = 3;

        Assert.False(result.IsValueCreated);
        Assert.Equal(5, result.Value);
        Assert.True(result.IsValueCreated);
    }

    [Fact]
    public void NewTest()
    {
        using var a = State.Use(1);
        using var result = State.From(() => new { a.Value }.Value);

        Assert.False(result.IsValueCreated);
        Assert.Equal(1, result.Value);
        Assert.True(result.IsValueCreated);
    }

    [Fact]
    public void NewArrayTest()
    {
        using var a = State.Use<int>();
        using var b = State.Use(1);
        using var result = State.From(() => new[] { a.Value, b.Value }.Sum());

        Assert.False(result.IsValueCreated);
        Assert.Equal(1, result.Value);
        Assert.True(result.IsValueCreated);

        a.Value = 1;
        b.Value = 2;

        Assert.False(result.IsValueCreated);
        Assert.Equal(3, result.Value);
    }

    [Fact]
    public void OnChangeTest()
    {
        using var a = State.Use<int>();
        using var result = State.From(() => new { a.Value }.Value);

        a.Changed += value => Assert.Equal(0, value % 2);
        a.Value = (int)(DateTime.Now.Ticks % 100) * 2;
    }

    [Fact]
    public void Test()
    {
        using var a = State.Use(1);
        using var b = State.Use(1.2);
        using var c = State.Use(10);
        var n = 0;

        using var r = State.From(() => a + (int)Math.Round(b.Value) + 1);
        using var r_plus_c_plus_2 = State.From(() => r + c + 2 + n);

        Assert.True(a.CanSetValue);
        Assert.True(b.CanSetValue);
        Assert.True(c.CanSetValue);
        Assert.False(r.CanSetValue);
        Assert.False(r_plus_c_plus_2.CanSetValue);

        Assert.Empty(a.Dependencies);
        Assert.Empty(b.Dependencies);
        Assert.Equal(2, r.Dependencies.Length);
        Assert.Equal(3, r_plus_c_plus_2.Dependencies.Length);

        Assert.False(r.IsValueCreated);
        Assert.Equal(3, r.Value);
        Assert.True(r.IsValueCreated);

        Assert.False(r_plus_c_plus_2.IsValueCreated);
        Assert.Equal(15, r_plus_c_plus_2.Value);
        Assert.True(r_plus_c_plus_2.IsValueCreated);

        a.Value = 2;

        Assert.False(r.IsValueCreated);
        Assert.Equal(4, r.Value);
        Assert.True(r.IsValueCreated);

        Assert.False(r_plus_c_plus_2.IsValueCreated);
        Assert.Equal(16, r_plus_c_plus_2.Value);
        Assert.True(r_plus_c_plus_2.IsValueCreated);

        b.Value = 1.6;

        Assert.False(r.IsValueCreated);
        Assert.Equal(5, r.Value);
        Assert.True(r.IsValueCreated);

        Assert.False(r_plus_c_plus_2.IsValueCreated);
        Assert.Equal(17, r_plus_c_plus_2.Value);
        Assert.True(r_plus_c_plus_2.IsValueCreated);

        // Will only trigger the update operation of r_plus_c_plus_2
        c.Value = 20;

        Assert.True(r.IsValueCreated);
        Assert.Equal(5, r.Value);
        Assert.True(r.IsValueCreated);

        Assert.False(r_plus_c_plus_2.IsValueCreated);
        Assert.Equal(27, r_plus_c_plus_2.Value);
        Assert.True(r_plus_c_plus_2.IsValueCreated);

        // Does not trigger an update operation.
        n = 8;

        Assert.True(r.IsValueCreated);
        Assert.Equal(5, r.Value);
        Assert.True(r.IsValueCreated);

        Assert.True(r_plus_c_plus_2.IsValueCreated);
        Assert.Equal(27, r_plus_c_plus_2.Value);
        Assert.True(r_plus_c_plus_2.IsValueCreated);
    }
}
