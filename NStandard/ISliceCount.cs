namespace NStandard
{
    public interface ISliceLength<TSliceRet>
    {
        int Length { get; }
        TSliceRet Slice(int start, int length);
    }
}
