using System;

namespace NStandard
{
    public class NowScope : Scope<NowScope>
    {
        public DateTime Now;
        public NowScope() { Now = DateTime.Now; }
        public NowScope(Func<DateTime, DateTime> store) { Now = store(DateTime.Now); }
    }
}
