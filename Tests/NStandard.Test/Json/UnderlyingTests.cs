using Newtonsoft.Json;
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
        private readonly Newtonsoft.Json.JsonSerializerSettings _settings = new Newtonsoft.Json.JsonSerializerSettings
        {
            Converters = new Newtonsoft.Json.JsonConverter[] { new Net.LazyConverter() },
        };

        [Fact]
        public void SerializeTest()
        {
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
                new Lazy<DateTime>(new DateTime(2000, 1, 2, 0, 10, 20)),
            };

            foreach (var value in values)
            {
                Assert.Equal(SystemJson.Serialize(value, _options), NewtonsoftJson.SerializeObject(value, _settings));
            }
        }

        [Fact]
        public void DeserializeTest()
        {
            void AssertValue<T>(T expected, string json)
            {
                Assert.Equal(expected, SystemJson.Deserialize<Lazy<T>>(json, _options).Value);
                Assert.Equal(expected, NewtonsoftJson.DeserializeObject<Lazy<T>>(json, _settings).Value);
            }

            AssertValue((byte)123, "123");
            AssertValue((short)123, "123");
            AssertValue((ushort)123, "123");
            AssertValue(123, "123");
            AssertValue((uint)123, "123");
            AssertValue((long)123, "123");
            AssertValue((ulong)123, "123");
            AssertValue((float)123.456, "123.456");
            AssertValue((double)123.456, "123.456");
            AssertValue((decimal)123.456, "123.456");
            AssertValue("123", "\"123\"");
            AssertValue(new DateTime(2000, 1, 2, 0, 10, 20), "\"2000-01-02T00:10:20\"");
        }

    }
}
