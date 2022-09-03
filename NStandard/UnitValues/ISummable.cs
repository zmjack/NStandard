using System.Collections.Generic;

namespace NStandard.UnitValues
{
    public interface ISummable<TUnitValue> where TUnitValue : struct, IUnitValue
    {
        void QuickSum(IEnumerable<TUnitValue> values);
        void QuickAverage(IEnumerable<TUnitValue> values);
    }
}
