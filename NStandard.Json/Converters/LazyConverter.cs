using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NStandard.Json.Converters
{
    public class LazyConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (!typeToConvert.IsGenericType) return false;
            return typeToConvert.GetGenericTypeDefinition() == typeof(Lazy<>);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var wrappedType = typeToConvert.GetGenericArguments()[0];
            var converter = (JsonConverter)Activator.CreateInstance(typeof(LazyConverter<>).MakeGenericType(wrappedType));
            return converter;
        }
    }

    public class LazyConverter<TValue> : JsonConverter<Lazy<TValue>>
    {
        private Lazy<TValue> CreateLazy(TValue value)
        {
            var lazy = new Lazy<TValue>(() => value);
            var ret = lazy.Value;
            return lazy;
        }

        public override bool CanConvert(Type objectType) => objectType == typeof(Lazy<TValue>);

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
