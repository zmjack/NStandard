namespace NStandard.Text.Json;

public interface IJsonValue
{
#if NET5_0_OR_GREATER
    /// <summary>
    /// Property setter always input <see cref="System.Buffers.ReadOnlySequence{Byte}"/> .
    /// </summary>
#endif
    object? Value { get; set; }
}
