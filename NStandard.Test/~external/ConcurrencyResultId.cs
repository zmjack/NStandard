namespace Dawnx.Diagnostics
{
    public struct ConcurrencyResultId
    {
        public int ThreadId { get; set; }
        public int InvokeNumber { get; set; }

        public ConcurrencyResultId(int threadId, int invokeNumber)
        {
            ThreadId = threadId;
            InvokeNumber = invokeNumber;
        }
    }
}
