﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NStandard
{
    public static class XLinkedList
    {
        public static IEnumerable<LinkedListNode<T>> GetNodes<T>(this LinkedList<T> @this)
        {
            for (var node = @this.First; node != null; node = node.Next)
            {
                yield return node;
            }
        }

    }
}
