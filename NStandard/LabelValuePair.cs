using System;

namespace NStandard
{
    [Obsolete("May be removed in the future.")]
    public class LabelValuePair<TValue>
    {
        public string Label { get; set; }
        public TValue Value { get; set; }
    }
}
