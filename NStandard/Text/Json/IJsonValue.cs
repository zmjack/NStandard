#if NET5_0_OR_GREATER
using System.Text.Json;
#endif

namespace NStandard.Text.Json;

public interface IJsonValue
{
    object? Value { get; }

#if NET5_0_OR_GREATER
    JsonElement RawValue { set; }
#endif
}
