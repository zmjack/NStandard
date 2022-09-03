using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NStandard.Json.Converters
{
    public class VariantStringConverter : JsonConverter<VariantString>
    {
        public override bool CanConvert(Type objectType) => objectType.IsType(typeof(VariantString));

        public override VariantString Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.TokenType switch
            {
                JsonTokenType.None => new VariantString(null as string),
                JsonTokenType.Null => new VariantString(null as string),
                JsonTokenType.String => reader.GetString(),
                JsonTokenType.Number => reader.GetDouble(),
                JsonTokenType.False => false,
                JsonTokenType.True => true,
                _ => throw new NotSupportedException($"{reader.TokenType} is not supported."),
            };
        }

        public override void Write(Utf8JsonWriter writer, VariantString value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.String ?? string.Empty);
        }

    }
}
