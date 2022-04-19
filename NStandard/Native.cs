using System;
using System.Runtime.InteropServices;

namespace NStandard
{
    public static class Native
    {
        [Obsolete("Do not use this function in PRODUCTION environment. GC may change the pointer of MANAGED OBJECT.")]
        public unsafe static IntPtr AddressOf<T>(T obj, bool skipPrefix) where T : class
        {
            var oref = __makeref(obj);
            var pref = (IntPtr**)&oref;
            var pobj = **pref;
            var offset = skipPrefix ? IntPtr.Size : 0;
#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET40_OR_GREATER
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

        [Obsolete("Do not use this function in PRODUCTION environment. GC may change the pointer of MANAGED OBJECT.")]
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
}
