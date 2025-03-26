namespace NStandard;

public interface ISliceCount<TRet>
{
    int Count { get; }
    TRet Slice(int start, int length);
}
