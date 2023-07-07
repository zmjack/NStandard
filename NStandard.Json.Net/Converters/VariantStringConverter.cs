using Newtonsoft.Json;
using System;

namespace NStandard.Json.Net.Converters
{
    public class VariantStringConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType.IsType(typeof(Variant));

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.TokenType switch
            {
                JsonToken.Null => new Variant((string)null),
                JsonToken.None => new Variant((string)null),
                JsonToken.Undefined => new Variant((string)null),
                JsonToken.Integer => new Variant((int)reader.Value),
                JsonToken.Float => new Variant((double)reader.Value),
                JsonToken.String => new Variant(reader.Value as string),
                JsonToken.Boolean => new Variant((bool)reader.Value),
                JsonToken.Date => new Variant((DateTime)reader.Value),
                _ => throw new NotSupportedException($"{reader.TokenType} is not supported."),
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var valueType = value.GetType();
            if (!CanConvert(valueType)) throw new InvalidOperationException($"The converter specified on '{writer.Path}' is not compatible with the type '{typeof(Variant)}'.");
            serializer.Serialize(writer, (value as Variant).ToString());
        }
    }
}
