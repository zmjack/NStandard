using System;
using System.Runtime.InteropServices;

namespace NStandard
{
    public static class Native
    {
        [Obsolete("Do not use this function in PRODUCTION environment. GC may change the pointer and can not be listened.")]
        public unsafe static IntPtr AddressOf<T>(T obj, bool verbose = false) where T : class
        {
            var oref = __makeref(obj);
            var pref = (IntPtr**)&oref;
            var pobj = **pref;
            var offset = verbose ? 0 : IntPtr.Size;
#if NET35
            return new IntPtr(pobj.ToInt64() + offset);
#else
            return pobj + offset;
#endif
        }

        [Obsolete("Do not use this function in PRODUCTION environment. GC may change the pointer and can not be listened.")]
        public static bool AreSame<T>(T obj1, T obj2) where T : class => AddressOf(obj1) == AddressOf(obj2);

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
