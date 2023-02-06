namespace NStandard.ValueTuples
{
    public struct IndexAndValue<TValue>
    {
        public int Index;
        public TValue Value;

        public IndexAndValue(int index, TValue value)
        {
            Index = index;
            Value = value;
        }

        public void Deconstruct(out int index, out TValue value)
        {
            index = Index;
            value = Value;
        }
    }
}
