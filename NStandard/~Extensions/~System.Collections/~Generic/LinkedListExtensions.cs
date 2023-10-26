using System.Collections.Generic;
using System.ComponentModel;

namespace NStandard;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class LinkedListExtensions
{
    public static IEnumerable<LinkedListNode<T>> GetNodes<T>(this LinkedList<T> @this)
    {
        for (var node = @this.First; node != null; node = node.Next)
        {
            yield return node;
        }
    }

}
