using System;
using System.Collections.Generic;
using System.Text;

namespace NStandard.Utils
{
    public static class LinkedUtil
    {
        public static IEnumerable<TSelf> Take<TSelf>(this TSelf @this, Func<TSelf, TSelf> selector, int count)
        {
            var select = @this;
            for (int i = 0; i < count; i++)
            {
                select = selector(select);
                if (select is not null) yield return select;
                else break;
            }
        }

        public static IEnumerable<TSelf> Take<TSelf>(this TSelf @this, Func<TSelf, TSelf> selector, Func<TSelf, int, bool> until)
        {
            var select = @this;
            for (int i = 0; !until(select, i); i++)
            {
                select = selector(select);
                if (select is not null) yield return select;
                else break;
            }
        }

    }
}
