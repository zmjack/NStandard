using System;
using System.Collections.Generic;
using System.Text;

namespace NStandard.Threading
{
    public enum CronFieldType
    {
        Ingore = -1,
        Any = 0,
        Specified = 1,
        Last = 2,
        Workday = 3,
        LastWorkday = 4,
    }
}
