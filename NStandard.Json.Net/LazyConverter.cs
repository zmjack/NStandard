using Newtonsoft.Json;
using System;

namespace NStandard.Json.Net
{
	public class LazyConverter<TValue> : JsonConverter
	{
		private Lazy<TValue> CreateLazy(TValue value)
		{
			var lazy = new Lazy<TValue>(() => value);
			var ret = lazy.Value;
			return lazy;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(Lazy<TValue>);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;
			var value = serializer.Deserialize<TValue>(reader);
			return CreateLazy(value);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var valueType = value.GetType();
			if (!CanConvert(valueType)) throw new InvalidOperationException($"The converter specified on '{writer.Path}' is not compatible with the type '{typeof(Lazy<TValue>)}'.");

			var lazy = value as Lazy<TValue>;
			serializer.Serialize(writer, lazy.Value);
		}
	}
}
