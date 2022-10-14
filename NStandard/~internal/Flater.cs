using System;
using System.Collections;
using System.Collections.Generic;

namespace NStandard
{
    internal class Flater<T> : IEnumerator
    {
        private readonly Stack<IEnumerator> Stack = new();
        private object _current;

        /// <summary>
        /// The sources for flatting.
        /// </summary>
        public IEnumerator Source { get; }

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        public object Current => _current;

        public Flater(IEnumerator source)
        {
            Source = source;
            Stack.Push(Source);
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            while (Stack.Count > 0)
            {
                var peekElement = Stack.Peek();
                if (peekElement.MoveNext())
                {
                    var current = peekElement.Current;

                    if (current is T element)
                    {
                        _current = element;
                        return true;
                    }
                    else if (current is IEnumerable enumerator)
                    {
                        Stack.Push(enumerator.GetEnumerator());
                        continue;
                    }
                    else throw new InvalidCastException($"Can not cast {current.GetType()} to {typeof(T)}.");
                }
                else
                {
                    Stack.Pop();
                }
            }
            return false;
        }

        /// <summary>
        /// Sets all the source enumerators to its initial position, which is before the first element
        ///     in the collection.
        /// </summary>
        public void Reset()
        {
            Source.Reset();
            Stack.Clear();
            Stack.Push(Source);
        }
    }

}
