namespace NStandard
{
    public interface ISliceCount<TSliceRet>
    {
        int Count { get; }
        TSliceRet Slice(int start, int length);
    }
}
