namespace NStandard
{
    public class SlidingWindow<T>
    {
        public T[] Values { get; set; }

        public SlidingWindow(T[] values)
        {
            Values = values;
        }

    }
}
