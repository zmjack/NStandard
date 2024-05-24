using System;

namespace NStandard.Text.Json;

#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public class JsonImplAttribute<TSource, TTarget> : JsonConverterAttribute
{
    public JsonImplAttribute() : base(typeof(JsonImplConverter<TSource, TTarget>))
    {
    }
}
#else
[Obsolete("Must be manually configured based on the actual serializer used.")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public class JsonImplAttribute<TSource, TTarget> : Attribute
{
    public JsonImplAttribute()
    {
    }
}
#endif
