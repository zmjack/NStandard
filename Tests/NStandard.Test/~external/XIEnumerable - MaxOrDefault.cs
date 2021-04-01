using System.Collections.Generic;
using System.Linq;

namespace LinqSharp
{
    public static partial class XIEnumerable
    {
        public static int MaxOrDefault(this IEnumerable<int> source, int @default = default(int)) => source.Any() ? source.Max() : @default;
    }

}
