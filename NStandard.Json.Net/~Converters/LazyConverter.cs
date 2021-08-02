using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace NStandard.Json.Net
{
    public class LazyConverter : JsonConverter
    {
        private object GetValue(object obj)
        {
            var underlyingType = obj.GetType().GetGenericArguments()[0];
            var prop = typeof(Lazy<>).MakeGenericType(underlyingType).GetProperty(nameof(Lazy<object>.Value));
            return prop.GetValue(obj);
        }

        private object CreateLazy(object value, Type underlying)
        {
            var constructor = typeof(Lazy<>).MakeGenericType(underlying).GetConstructor(new[] { typeof(Func<>).MakeGenericType(underlying) });
            var func = Expression.Lambda(Expression.Constant(value)).Compile();
            var lazy = constructor.Invoke(new[] { func });
            return lazy;
        }

        public override bool CanConvert(Type objectType) => objectType.IsType(typeof(Lazy<>));

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var underlyingType = objectType.GetGenericArguments()[0];
            var value = serializer.Deserialize(reader, underlyingType);
            return CreateLazy(value, underlyingType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var valueType = value.GetType();
            if (!CanConvert(valueType)) throw new InvalidOperationException($"The converter specified on '{writer.Path}' is not compatible with the type '{typeof(Lazy<>)}'.");
            var underlyingValue = GetValue(value);
            serializer.Serialize(writer, underlyingValue);
        }
    }
}
