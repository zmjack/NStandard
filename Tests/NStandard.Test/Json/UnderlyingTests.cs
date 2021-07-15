using System;
using System.Text.Json;
using Xunit;
using NewtonsoftJson = Newtonsoft.Json.JsonConvert;
using SystemJson = System.Text.Json.JsonSerializer;

namespace NStandard.Json.Test
{
    public class UnderlyingTests
    {
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions().Then(options =>
        {
            options.Converters.Add(new LazyConverter());
        });
        private readonly Newtonsoft.Json.JsonConverter[] _converters = new Newtonsoft.Json.JsonConverter[]
        {
            new Net.LazyConverter(),
        };

        private string NJson(object obj) => NewtonsoftJson.SerializeObject(obj, _converters);
        private T FromNJson<T>(string json) => NewtonsoftJson.DeserializeObject<T>(json, _converters);

        private string SJson(object obj) => SystemJson.Serialize(obj, _options);
        private T FromSJson<T>(string json) => SystemJson.Deserialize<T>(json, _options);


        [Fact]
        public void SerializeTest()
        {
            var now = DateTime.Now;
            var values = new object[]
            {
                new Lazy<byte>(123),
                new Lazy<short>(123),
                new Lazy<ushort>(123),
                new Lazy<int>(123),
                new Lazy<uint>(123),
                new Lazy<long>(123),
                new Lazy<ulong>(123),
                new Lazy<float>(123.456f),
                new Lazy<double>(123.456),
                new Lazy<decimal>(123.456m),
                new Lazy<DateTime>(now),
            };

            foreach (var value in values)
            {
                var njson = NJson(value);
                var sjson = SJson(value);
                Assert.Equal(njson, sjson);
            }
        }

        [Fact]
        public void DeserializeTest()
        {
            void AssertValue<T>(T expected, string json)
            {
                var nvalue = FromNJson<Lazy<T>>(json);
                var svalue = FromSJson<Lazy<T>>(json);
                Assert.Equal(expected, nvalue.Value);
                Assert.Equal(nvalue.Value, svalue.Value);
            }

            AssertValue((byte)97, "97");
            AssertValue((short)97, "97");
            AssertValue((ushort)97, "97");
            AssertValue(97, "97");
            AssertValue(97u, "97");
            AssertValue(97L, "97");
            AssertValue(97uL, "97");
            AssertValue(97f, "97");
            AssertValue(97d, "97");
            AssertValue(97m, "97");
            AssertValue(new DateTime(2000, 1, 2, 0, 10, 20), "\"2000-01-02T00:10:20\"");
        }

    }
}
