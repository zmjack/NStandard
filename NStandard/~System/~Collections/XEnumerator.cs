using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XEnumerator
    {
        public static object TakeElement(this IEnumerator @this)
        {
            if (@this.MoveNext())
                return @this.Current;
            else return null;
        }

        public static TElement TakeElement<TElement>(this IEnumerator<TElement> @this)
        {
            if (@this.MoveNext())
                return @this.Current;
            else return default;
        }

    }
}
