using System;

namespace NStandard
{
    public static class Runtime
    {
        public unsafe static IntPtr AddressOf(object obj)
        {
            var typeRef = __makeref(obj);
            var pTypeRef = (IntPtr**)(&typeRef);
            return **pTypeRef;
        }

        public static bool AreSame(object obj1, object obj2) => AddressOf(obj1) == AddressOf(obj2);

    }
}
