using Newtonsoft.Json;
using System;

namespace NStandard.Json.Net.Converters
{
    public class VariantStringConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType.IsType(typeof(VariantString));

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.TokenType switch
            {
                JsonToken.Null => new VariantString((string)null),
                JsonToken.None => new VariantString((string)null),
                JsonToken.Undefined => new VariantString((string)null),
                JsonToken.Integer => new VariantString((int)reader.Value),
                JsonToken.Float => new VariantString((double)reader.Value),
                JsonToken.String => new VariantString(reader.Value as string),
                JsonToken.Boolean => new VariantString((bool)reader.Value),
                JsonToken.Date => new VariantString((DateTime)reader.Value),
                _ => throw new NotSupportedException($"{reader.TokenType} is not supported."),
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var valueType = value.GetType();
            if (!CanConvert(valueType)) throw new InvalidOperationException($"The converter specified on '{writer.Path}' is not compatible with the type '{typeof(VariantString)}'.");
            serializer.Serialize(writer, (value as VariantString).String ?? string.Empty);
        }
    }
}
