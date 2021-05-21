using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NStandard.Json
{
    public class LazyConverter<T> : JsonConverter<Lazy<T>>
    {
        private Lazy<T> CreateLazy(T value)
        {
            var lazy = new Lazy<T>(() => value);
            var ret = lazy.Value;
            return lazy;
        }

        public override Lazy<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            object value = reader.TokenType switch
            {
                JsonTokenType.True => true,
                JsonTokenType.False => false,
                JsonTokenType.Number when reader.TryGetInt64(out long l) => l,
                JsonTokenType.Number => reader.GetDouble(),
                JsonTokenType.String when reader.TryGetDateTime(out DateTime datetime) => datetime,
                JsonTokenType.String => reader.GetString(),
                _ => JsonDocument.ParseValue(ref reader).RootElement.Clone(),
            };

            if (value is JsonElement element)
                return CreateLazy(JsonSerializer.Deserialize<T>(element.GetRawText()));
            else return CreateLazy((T)value);
        }

        public override void Write(Utf8JsonWriter writer, Lazy<T> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.Value, typeof(T), options);
        }
    }
}
