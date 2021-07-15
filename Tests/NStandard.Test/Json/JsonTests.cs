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

        private string NJson(object obj) => NewtonsoftJson.SerializeObject(obj);
        private T FromNJson<T>(string json) => NewtonsoftJson.DeserializeObject<T>(json);

        private string SJson(object obj) => SystemJson.Serialize(obj, _options);
        private T FromSJson<T>(string json) => SystemJson.Deserialize<T>(json, _options);

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
                Assert.Equal(NJson(value), SJson(value));
            }
        }

        [Fact]
        public void DeserializeTest()
        {
            var values = new string[] { "\"NaN\"", "\"Infinity\"", "\"-Infinity\"" };
            foreach (var value in values)
            {
                Assert.Equal(FromNJson<float>(value), FromSJson<float>(value));
                Assert.Equal(FromNJson<double>(value), FromSJson<double>(value));
            }
            var nullableValues = new string[] { "null", "\"NaN\"", "\"Infinity\"", "\"-Infinity\"" };
            foreach (var value in nullableValues)
            {
                Assert.Equal(FromNJson<float?>(value), FromSJson<float?>(value));
                Assert.Equal(FromNJson<double?>(value), FromSJson<double?>(value));
            }
        }

    }
}
