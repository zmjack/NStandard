using NStandard.Json;
using System;
using System.Text.Json;
using Xunit;
using NewtonsoftJson = Newtonsoft.Json.JsonConvert;
using SystemJson = System.Text.Json.JsonSerializer;

namespace NStandard.Json.Test
{
    public class VariantStringTests
    {
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions().Then(options =>
        {
            options.Converters.Add(new VariantStringConverter());
        });
        private readonly Newtonsoft.Json.JsonConverter[] _converters = new Newtonsoft.Json.JsonConverter[]
        {
            new Net.VariantStringConverter(),
        };

        [Fact]
        public void SerializeTest()
        {
            void AssertValue(string expected, VariantString obj)
            {
                Assert.Equal(expected, SystemJson.Serialize(obj, _options));
                Assert.Equal(expected, NewtonsoftJson.SerializeObject(obj, _converters));
            }

            AssertValue("\"\"", new VariantString(null as string));
            AssertValue("\"\"", new VariantString(""));
            AssertValue("\"123\"", new VariantString("123"));
            AssertValue("\"123.456\"", new VariantString("123.456"));
            AssertValue($"\"{new DateTime(2000, 1, 2, 0, 10, 20)}\"", new VariantString(new DateTime(2000, 1, 2, 0, 10, 20)));
        }

        [Fact]
        public void DeserializeTest()
        {
            void AssertValue(VariantString expected, string json)
            {
                Assert.Equal(expected, SystemJson.Deserialize<VariantString>(json, _options));
                Assert.Equal(expected, NewtonsoftJson.DeserializeObject<VariantString>(json, _converters));
            }

            AssertValue(new VariantString(""), "\"\"");
            AssertValue(new VariantString("123"), "\"123\"");
            AssertValue(new VariantString("123.456"), "\"123.456\"");
            AssertValue(new VariantString(new DateTime(2000, 1, 2, 0, 10, 20)), $"\"{new DateTime(2000, 1, 2, 0, 10, 20)}\"");
        }

    }
}
