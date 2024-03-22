using System.Text.Json;
using Xunit;
using NewtonsoftJson = Newtonsoft.Json.JsonConvert;
using SystemJson = System.Text.Json.JsonSerializer;

namespace NStandard.Test.Json;

public class JsonTests
{
    private readonly JsonSerializerOptions _options = Any.Create(() =>
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new NStandard.Json.Converters.NetSingleConverter());
        options.Converters.Add(new NStandard.Json.Converters.NetDoubleConverter());
        return options;
    });

    [Fact]
    public void SerializeTest()
    {
        var values = new object[]
        {
            null,
            1f, 2d,
            float.NaN, float.PositiveInfinity, float.NegativeInfinity,
            float.NaN, float.PositiveInfinity, float.NegativeInfinity,
            double.NaN, double.PositiveInfinity, double.NegativeInfinity,
            double.NaN, double.PositiveInfinity, double.NegativeInfinity,
        };
        foreach (var value in values)
        {
            var nresult = NewtonsoftJson.SerializeObject(value);
            var sresult = SystemJson.Serialize(value, _options);

            if (double.TryParse(nresult, out var n) && double.TryParse(sresult, out var s))
            {
                // n is 1.0, but s is 1
                Assert.Equal(n, s);
            }
            else
            {
                Assert.Equal(nresult, sresult);
            }
        }
    }

    [Fact]
    public void DeserializeTest()
    {
        var values = new string[] { "\"NaN\"", "\"Infinity\"", "\"-Infinity\"" };
        foreach (var value in values)
        {
            Assert.Equal(NewtonsoftJson.DeserializeObject<float>(value), SystemJson.Deserialize<float>(value, _options));
            Assert.Equal(NewtonsoftJson.DeserializeObject<double>(value), SystemJson.Deserialize<double>(value, _options));
        }
        var nullableValues = new string[] { "null", "\"NaN\"", "\"Infinity\"", "\"-Infinity\"" };
        foreach (var value in nullableValues)
        {
            Assert.Equal(NewtonsoftJson.DeserializeObject<float?>(value), SystemJson.Deserialize<float?>(value, _options));
            Assert.Equal(NewtonsoftJson.DeserializeObject<double?>(value), SystemJson.Deserialize<double?>(value, _options));
        }
    }

}
