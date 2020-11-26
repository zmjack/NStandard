using System;
using System.Collections.Generic;
using System.Text;

namespace NStandard
{
    public class NowScope : Scope<NowScope>
    {
        public DateTime Now;
        internal NowScope() { Now = DateTime.Now; }
        internal NowScope(Func<DateTime, DateTime> store) { Now = store(DateTime.Now); }
    }
}
