using System.Net;

namespace NStandard.Static.Net;

public static class IPAddressEx
{
    public static IPAddress Create(uint value)
    {
        var bytes = BitConverter.GetBytes(value);
        Array.Reverse(bytes);
        return new IPAddress(bytes);
    }

#if NET7_0_OR_GREATER
    public static IPAddress Create(UInt128 value)
    {
        var bytes = BitConverterEx.GetBytes(value);
        Array.Reverse(bytes);
        return new IPAddress(bytes);
    }
#endif
}
