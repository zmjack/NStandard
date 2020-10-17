using System;

namespace NStandard
{
    public static class X
    {
        public static T Create<T>(Func<T> create)
        {
            return create();
        }

    }
}
