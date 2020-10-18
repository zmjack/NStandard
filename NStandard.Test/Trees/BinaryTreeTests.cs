using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace NStandard.Trees.Test
{
    public class BinaryTreeTests
    {
        private readonly int[] InOrder = new[] { 4, 7, 2, 1, 5, 3, 8, 6 };
        private readonly int[] PreOrder = new[] { 1, 2, 4, 7, 3, 5, 6, 8 };
        private readonly int[] PostOrder = new[] { 7, 4, 2, 5, 8, 6, 3, 1 };
        /*
         *              1
         *            /   \
         *           2     3
         *          /     / \
         *         4     5   6
         *          \       /
         *           7     8
         */

        [Fact]
        public void Test1()
        {
            var tree = BinaryTreeConstructor.ConstructFrom_InPreOrder(InOrder, PreOrder);
            Assert.Equal(InOrder, tree.GetInOrderNodes().Select(x => x.Model));
            Assert.Equal(PreOrder, tree.GetPreOrderNodes().Select(x => x.Model));
            Assert.Equal(PostOrder, tree.GetPostOrderNodes().Select(x => x.Model));
        }

        [Fact]
        public void Test2()
        {
            var tree = BinaryTreeConstructor.ConstructFrom_InPostOrder(InOrder, PostOrder);
            Assert.Equal(InOrder, tree.GetInOrderNodes().Select(x => x.Model));
            Assert.Equal(PreOrder, tree.GetPreOrderNodes().Select(x => x.Model));
            Assert.Equal(PostOrder, tree.GetPostOrderNodes().Select(x => x.Model));
        }
    }
}
