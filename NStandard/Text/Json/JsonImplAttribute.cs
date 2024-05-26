using System;

namespace NStandard.Text.Json;

#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;

[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
public class JsonImplAttribute<TSelf> : JsonConverterAttribute
{
    public JsonImplAttribute() : base(typeof(JsonImplConverter<TSelf>))
    {
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public class JsonImplAttribute<TSelf, TTarget> : JsonConverterAttribute
{
    public JsonImplAttribute() : base(typeof(JsonImplConverter<TSelf, TTarget>))
    {
    }
}
#else
[Obsolete("Must be manually configured based on the actual serializer used.")]
[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
public class JsonImplAttribute<TSelf> : Attribute
{
    public JsonImplAttribute()
    {
    }
}

[Obsolete("Must be manually configured based on the actual serializer used.")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public class JsonImplAttribute<TSelf, TTarget> : Attribute
{
    public JsonImplAttribute()
    {
    }
}
#endif
