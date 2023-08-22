using System.Collections;
using System.Collections.Generic;

namespace NStandard.Collections
{
    public class Flated<T> : IEnumerable<T>
    {
        private readonly List<IEnumerable<T>> list = new();

        public void Add(T value)
        {
            list.Add(new[] { value });
        }

        public void Add(IEnumerable<T> value)
        {
            list.Add(value);
        }

        public void Add(IEnumerable<IEnumerable<T>> value)
        {
            foreach (var item in value)
            {
                list.Add(item);
            }
        }

        private IEnumerable<T> GetItems()
        {
            foreach (var enumerable in list)
            {
                foreach (var item in enumerable)
                {
                    yield return item;
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetItems().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
