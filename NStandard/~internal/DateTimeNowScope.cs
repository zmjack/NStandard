using System;
using System.Collections.Generic;
using System.Text;

namespace NStandard
{
    public class NowScope : Scope<NowScope>
    {
        public DateTime Now;
        public NowScope() { Now = DateTime.Now; }
        public NowScope(Func<DateTime, DateTime> store) { Now = store(DateTime.Now); }
    }
}
