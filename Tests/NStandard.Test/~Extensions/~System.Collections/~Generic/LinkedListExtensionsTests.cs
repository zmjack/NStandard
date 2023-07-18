using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NStandard.Test
{
    public class LinkedListExtensionsTests
    {
        [Fact]
        public void Test1()
        {
            var arr = new[] { 1, 2, 3, 4, 5 };
            var list = new LinkedList<int>(arr);
            LinkedListNode<int> node;

            node = list.GetNodes().First(x => x.Value == 1);
            Assert.Null(node.Previous);
            Assert.Equal(2, node.Next.Value);

            node = list.GetNodes().First(x => x.Value == 3);
            Assert.Equal(2, node.Previous.Value);
            Assert.Equal(4, node.Next.Value);

            node = list.GetNodes().First(x => x.Value == 5);
            Assert.Equal(4, node.Previous.Value);
            Assert.Null(node.Next);
        }

    }
}
