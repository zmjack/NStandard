using System;
using System.Collections.Generic;
using System.Text;

namespace NStandard
{
    public interface ISliceCount<TSliceRet>
    {
        int Count { get; }
        TSliceRet Slice(int start, int length);
    }
}
