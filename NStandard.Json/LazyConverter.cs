using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NStandard.Json
{
    public class LazyConverter<TValue> : JsonConverter<Lazy<TValue>>
    {
        private Lazy<TValue> CreateLazy(TValue value)
        {
            var lazy = new Lazy<TValue>(() => value);
            var ret = lazy.Value;
            return lazy;
        }

        public override Lazy<TValue> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null) return null;
            var value = JsonSerializer.Deserialize<TValue>(ref reader, options);
            return CreateLazy(value);
        }

        public override void Write(Utf8JsonWriter writer, Lazy<TValue> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.Value, typeof(TValue), options);
        }
    }
}
