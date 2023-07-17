using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace NStandard.Json.Net.Converters
{
    public class PhysicalAddressConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(PhysicalAddress);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.TokenType switch
            {
                JsonToken.Null => null,
                JsonToken.String => PhysicalAddress.Parse(reader.Value as string),
                _ => throw new NotSupportedException($"{reader.TokenType} is not supported."),
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var valueType = value.GetType();
            if (!CanConvert(valueType)) throw new InvalidOperationException($"The converter specified on '{writer.Path}' is not compatible with the type '{typeof(PhysicalAddress)}'.");

            var bytes = ((PhysicalAddress)value).GetAddressBytes();
            var enumerator = bytes.GetEnumerator();
            var sb = new StringBuilder(30);
            if (enumerator.MoveNext())
            {
                sb.Append(((byte)enumerator.Current).ToString("X2"));
                while (enumerator.MoveNext())
                {
                    sb.Append('-');
                    sb.Append(((byte)enumerator.Current).ToString("X2"));
                }
            }

            serializer.Serialize(writer, sb.ToString());
        }
    }
}
