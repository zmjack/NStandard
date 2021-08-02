using System.Text.Json;
using Xunit;
using NewtonsoftJson = Newtonsoft.Json.JsonConvert;
using SystemJson = System.Text.Json.JsonSerializer;

namespace NStandard.Test.Json
{
    public class JsonTests
    {
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions().Then(options =>
        {
            options.Converters.Add(new NStandard.Json.NetSingleConverter());
            options.Converters.Add(new NStandard.Json.NetDoubleConverter());
        });

        [Fact]
        public void SerializeTest()
        {
            var values = new object[]
            {
                null,
                float.NaN, float.PositiveInfinity, float.NegativeInfinity,
                (float?)float.NaN, (float?)float.PositiveInfinity, (float?)float.NegativeInfinity,
                double.NaN, double.PositiveInfinity, double.NegativeInfinity,
                (double?)double.NaN, (double?)double.PositiveInfinity, (double?)double.NegativeInfinity,
            };
            foreach (var value in values)
            {
                Assert.Equal(NewtonsoftJson.SerializeObject(value), SystemJson.Serialize(value, _options));
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
}
