using Newtonsoft.Json;
using NStandard.Json.Converters;
using System;
using System.Text.Json;
using Xunit;
using NewtonsoftJson = Newtonsoft.Json.JsonConvert;
using SystemJson = System.Text.Json.JsonSerializer;

namespace NStandard.Json.Test
{
    public class VariantTests
    {
        private readonly JsonSerializerOptions _options = Any.Create(() =>
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new VariantConverter());
            return options;
        });
        private readonly JsonSerializerSettings _settings = new()
        {
            Converters = new JsonConverter[]
            {
                new Net.Converters.VariantConverter(),
            },
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

        [Fact]
        public void SerializeTest()
        {
            Assert_Serialize("\"\"", new Variant(null as string));
            Assert_Serialize("\"\"", new Variant(""));
            Assert_Serialize("\"123\"", new Variant("123"));
            Assert_Serialize("\"123.456\"", new Variant("123.456"));
            Assert_Serialize($"\"{new DateTime(2000, 1, 2, 0, 10, 20)}\"", new Variant(new DateTime(2000, 1, 2, 0, 10, 20)));
        }

        [Fact]
        public void DeserializeTest()
        {
            Assert_Deserialize(new Variant(""), "\"\"");
            Assert_Deserialize(new Variant("123"), "\"123\"");
            Assert_Deserialize(new Variant("123.456"), "\"123.456\"");
            Assert_Deserialize(new Variant(new DateTime(2000, 1, 2, 0, 10, 20)), $"\"{new DateTime(2000, 1, 2, 0, 10, 20)}\"");
        }

    }
}
