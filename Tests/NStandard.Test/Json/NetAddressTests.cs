using Newtonsoft.Json;
using NStandard.Json.Converters;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.Json;
using Xunit;
using NewtonsoftJson = Newtonsoft.Json.JsonConvert;
using SystemJson = System.Text.Json.JsonSerializer;

namespace NStandard.Json.Test;

public class NetAddressTests
{
    private readonly JsonSerializerOptions _options = Any.Create(() =>
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new IPAddressConverter());
        options.Converters.Add(new PhysicalAddressConverter());
        return options;
    });
    private readonly JsonSerializerSettings _settings = new()
    {
        Converters =
        [
            new Net.Converters.IPAddressConverter(),
            new Net.Converters.PhysicalAddressConverter(),
        ],
    };

    private void Assert_Serialize<T>(string expected, T actual)
    {
        Assert.Equal(expected, SystemJson.Serialize(actual, _options));
        Assert.Equal(expected, NewtonsoftJson.SerializeObject(actual, _settings));
    }
    private void Assert_Deserialize<T>(T expected, string actual)
    {
        Assert.Equal(expected, SystemJson.Deserialize<T>(actual, _options));
        Assert.Equal(expected, NewtonsoftJson.DeserializeObject<T>(actual, _settings));
    }

    private readonly IPAddress _ip = new([127, 0, 0, 1]);
    private readonly PhysicalAddress _mac = new([0, 1, 2, 3, 4, 5]);

    [Fact]
    public void SerializeTest()
    {
        Assert_Serialize("\"127.0.0.1\"", _ip);
        Assert_Serialize("\"00-01-02-03-04-05\"", _mac);
    }

    [Fact]
    public void DeserializeTest()
    {
        Assert_Deserialize(_ip, "\"127.0.0.1\"");
        Assert_Deserialize(_mac, "\"00-01-02-03-04-05\"");
    }

}
