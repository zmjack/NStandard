using System.Drawing;
using System.Text.Json;
using Xunit;

namespace NStandard.Drawing.Test;

public class RgbaColorTests
{
    private static string Json(object obj)
    {
        return JsonSerializer.Serialize(obj);
    }
    private static T FromJson<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json);
    }

    [Fact]
    public void SerializeTest0()
    {
        RgbaColor? color = null;
        Assert.Equal("null", Json(color));
    }

    [Fact]
    public void SerializeTest1()
    {
        var color = new RgbaColor(11, 22, 33);
        Assert.Equal("\"rgb(11,22,33)\"", Json(color));
    }

    [Fact]
    public void SerializeTest2()
    {
        var color = new RgbaColor(11, 22, 33, 1);
        Assert.Equal("\"rgba(11,22,33,0.0039)\"", Json(color));
    }

    [Fact]
    public void DeserializeTest0()
    {
        Assert.Null(FromJson<RgbaColor?>("null"));
        Assert.Equal(new RgbaColor(), FromJson<RgbaColor>("null"));
    }

    [Fact]
    public void DeserializeTest1()
    {
        {
            var color = FromJson<RgbaColor>("\"rgb(11,22,33)\"");
            Assert.Equal(11, color.R);
            Assert.Equal(22, color.G);
            Assert.Equal(33, color.B);
            Assert.Equal(byte.MaxValue, color.A);
        }
    }

    [Fact]
    public void DeserializeTest2()
    {
        {
            var color = FromJson<RgbaColor>("\"rgba(11,22,33,0.0039)\"");
            Assert.Equal(11, color.R);
            Assert.Equal(22, color.G);
            Assert.Equal(33, color.B);
            Assert.Equal(1, color.A);
        }

        {
            var color = FromJson<RgbaColor>("\"rgba(11,22,33,0.39%)\"");
            Assert.Equal(11, color.R);
            Assert.Equal(22, color.G);
            Assert.Equal(33, color.B);
            Assert.Equal(1, color.A);
        }
    }

    [Fact]
    public void ConvertTest1()
    {
        var rgba = new RgbaColor(11, 22, 33);
        Assert.Equal(11, rgba.R);
        Assert.Equal(22, rgba.G);
        Assert.Equal(33, rgba.B);
        Assert.Equal(255, rgba.A);

        var color = (Color)rgba;
        Assert.Equal(11, color.R);
        Assert.Equal(22, color.G);
        Assert.Equal(33, color.B);
        Assert.Equal(255, rgba.A);

        var back = (RgbaColor)color;
        Assert.Equal(11, back.R);
        Assert.Equal(22, back.G);
        Assert.Equal(33, back.B);
        Assert.Equal(255, rgba.A);
    }

    [Fact]
    public void ConvertTest2()
    {
        var rgba = new RgbaColor(11, 22, 33, 1);
        Assert.Equal(11, rgba.R);
        Assert.Equal(22, rgba.G);
        Assert.Equal(33, rgba.B);
        Assert.Equal(1, rgba.A);

        var color = (Color)rgba;
        Assert.Equal(11, color.R);
        Assert.Equal(22, color.G);
        Assert.Equal(33, color.B);
        Assert.Equal(1, color.A);

        var back = (RgbaColor)color;
        Assert.Equal(11, back.R);
        Assert.Equal(22, back.G);
        Assert.Equal(33, back.B);
        Assert.Equal(1, back.A);
    }
}
