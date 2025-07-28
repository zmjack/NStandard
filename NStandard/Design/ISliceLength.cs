namespace NStandard.Design;

public interface ISliceLength<TRet>
{
    int Length { get; }
    TRet Slice(int start, int length);
}
