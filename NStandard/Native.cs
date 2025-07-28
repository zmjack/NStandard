using System.Runtime.InteropServices;

namespace NStandard;

#pragma warning disable CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
public static class Native
{
    [Obsolete(Obsoletions.ProductionIncompatible)]
    public unsafe static IntPtr AddressOf<T>(T obj, bool skipPrefix) where T : class
    {
        var oref = __makeref(obj);
        var pref = (IntPtr**)&oref;
        var pobj = **pref;
        var offset = skipPrefix ? IntPtr.Size : 0;
#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NET451_OR_GREATER
        return pobj + offset;
#else
        return new IntPtr(pobj.ToInt64() + offset);
#endif
    }

    public unsafe static IntPtr AddressOf<T>(ref T obj) where T : struct
    {
        var oref = __makeref(obj);
        var pref = (IntPtr*)&oref;
        var pobj = *pref;
        return pobj;
    }

    [Obsolete(Obsoletions.ProductionIncompatible)]
    public static bool AreSame<T>(T obj1, T obj2) where T : class => AddressOf(obj1, false) == AddressOf(obj2, false);
    public static bool AreSame<T>(ref T obj1, ref T obj2) where T : struct => AddressOf(ref obj1) == AddressOf(ref obj2);

    public static byte[] ReadMemory(IntPtr ptr, int length)
    {
        var ret = new byte[length];
        Marshal.Copy(ptr, ret, 0, length);
        return ret;
    }

    public static void WriteMemory(IntPtr ptr, byte[] bytes)
    {
        Marshal.Copy(bytes, 0, ptr, bytes.Length);
    }

}
#pragma warning restore CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
