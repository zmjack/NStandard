using Newtonsoft.Json;
using NStandard.Json.Converters;
using System.Text.Json;
using Xunit;
using NewtonsoftJson = Newtonsoft.Json.JsonConvert;
using SystemJson = System.Text.Json.JsonSerializer;

namespace NStandard.Json.Test;

public class LazyTests
{
    private readonly JsonSerializerOptions _options = Any.Create(() =>
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new LazyConverter());
        return options;
    });
    private readonly JsonSerializerSettings _settings = new()
    {
        Converters = new JsonConverter[]
        {
            new Net.Converters.LazyConverter(),
        },
    };

    private void Assert_Serialize<T>(string expected, T actual)
    {
        Assert.Equal(expected, SystemJson.Serialize(actual, _options));
        Assert.Equal(expected, NewtonsoftJson.SerializeObject(actual, _settings));
    }
    private void Assert_Deserialize<T>(T expected, string actual)
    {
        Assert.Equal(expected, SystemJson.Deserialize<Lazy<T>>(actual, _options).Value);
        Assert.Equal(expected, NewtonsoftJson.DeserializeObject<Lazy<T>>(actual, _settings).Value);
    }

    [Fact]
    public void SerializeTest()
    {
        Assert_Serialize("123", new Lazy<byte>(123));
        Assert_Serialize("123", new Lazy<short>(123));
        Assert_Serialize("123", new Lazy<ushort>(123));
        Assert_Serialize("123", new Lazy<int>(123));
        Assert_Serialize("123", new Lazy<uint>(123));
        Assert_Serialize("123", new Lazy<long>(123));
        Assert_Serialize("123", new Lazy<ulong>(123));
        Assert_Serialize("123.456", new Lazy<float>(123.456f));
        Assert_Serialize("123.456", new Lazy<double>(123.456));
        Assert_Serialize("123.456", new Lazy<decimal>(123.456m));
        Assert_Serialize("\"2000-01-02T00:10:20Z\"", new Lazy<DateTime>(new DateTime(2000, 1, 2, 0, 10, 20, DateTimeKind.Utc)));
    }

    [Fact]
    public void DeserializeTest()
    {
        Assert_Deserialize((byte)(123), "123");
        Assert_Deserialize((short)123, "123");
        Assert_Deserialize((ushort)123, "123");
        Assert_Deserialize(123, "123");
        Assert_Deserialize((uint)123, "123");
        Assert_Deserialize((long)123, "123");
        Assert_Deserialize((ulong)123, "123");
        Assert_Deserialize((float)123.456, "123.456");
        Assert_Deserialize((double)123.456, "123.456");
        Assert_Deserialize((decimal)123.456, "123.456");
        Assert_Deserialize("123", "\"123\"");
        Assert_Deserialize(new DateTime(2000, 1, 2, 0, 10, 20, DateTimeKind.Utc), "\"2000-01-02T00:10:20Z\"");
    }

}
