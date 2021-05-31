using System;

namespace NStandard
{
    public class NowOffsetScope : Scope<NowScope>
    {
        public DateTimeOffset Now;
        internal NowOffsetScope() { Now = DateTimeOffset.Now; }
        internal NowOffsetScope(Func<DateTimeOffset, DateTimeOffset> store) { Now = store(DateTimeOffset.Now); }
    }
}
