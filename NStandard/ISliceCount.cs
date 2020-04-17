using System;
using System.Collections.Generic;
using System.Text;

namespace NStandard
{
    public interface ISliceLength<TSliceRet>
    {
        int Length { get; }
        TSliceRet Slice(int start, int length);
    }
}
