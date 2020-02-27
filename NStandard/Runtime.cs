using System;

namespace NStandard
{
    public static class Runtime
    {
        public unsafe static IntPtr AddressOf(object obj)
        {
            var oref = __makeref(obj);
            var pref = (IntPtr**)&oref;
            return **pref;
        }

        public static bool AreSame(object obj1, object obj2) => AddressOf(obj1) == AddressOf(obj2);

    }
}
