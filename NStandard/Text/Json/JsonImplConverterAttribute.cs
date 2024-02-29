#if NET5_0_OR_GREATER
using System;
using System.Text.Json.Serialization;

namespace NStandard.Text.Json;

[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
public class JsonImplConverterAttribute<T> : JsonConverterAttribute
{
    public JsonImplConverterAttribute() : base(typeof(JsonImplConverter<T>))
    {
    }
}
#endif
