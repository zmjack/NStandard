using System.ComponentModel;
using System.Net;

namespace NStandard;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class IPAddressExtensions
{
    public static uint ToUInt32(this IPAddress @this)
    {
        var bytes = @this.GetAddressBytes();
        if (bytes.Length != 4) throw new InvalidCastException("The length of IPAddress must be 32 bits.");

        Array.Reverse(bytes);
        return BitConverter.ToUInt32(bytes, 0);
    }

#if NET7_0_OR_GREATER
    public static UInt128 ToUInt128(this IPAddress @this)
    {
        var bytes = @this.GetAddressBytes();
        if (bytes.Length != 16) throw new InvalidCastException("The length of IPAddress must be 128 bits.");

        Array.Reverse(bytes);
        return BitConverterEx.ToUInt128(bytes, 0);
    }
#endif

}
