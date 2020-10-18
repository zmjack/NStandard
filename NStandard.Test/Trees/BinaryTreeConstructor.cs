using NStandard.Trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NStandard.Trees.Test
{
    public static class BinaryTreeConstructor
    {
        public static BinaryTree<int> ConstructFrom_InPreOrder(int[] inOrder, int[] preOrder)
        {
            if (inOrder.Length != preOrder.Length) throw new ArgumentException("Invalid input.");
            if (inOrder.Length < 1) throw new ArgumentException("Invalid input.");

            var preRoot = preOrder.First();

            if (inOrder.Length == 1)
            {
                var inRoot = inOrder.First();
                if (inRoot == preRoot) return new BinaryTree<int>(preRoot);
                else throw new ArgumentException("Invalid input.");
            }
            else
            {
                var tree = new BinaryTree<int>(preRoot);

                var leftInOrder = inOrder.TakeWhile(x => x != tree.Model).ToArray();
                var rightInOrder = inOrder.Skip(1 + leftInOrder.Length).ToArray();

                var leftPreOrder = preOrder.Skip(1).Take(leftInOrder.Length).ToArray();
                var rightPreOrder = preOrder.Skip(1 + leftInOrder.Length).ToArray();

                if (leftPreOrder.Length > 0) tree.LeftNode = ConstructFrom_InPreOrder(leftInOrder, leftPreOrder);
                if (rightPreOrder.Length > 0) tree.RightNode = ConstructFrom_InPreOrder(rightInOrder, rightPreOrder);

                return tree;
            }
        }

        public static BinaryTree<int> ConstructFrom_InPostOrder(int[] inOrder, int[] postOrder)
        {
            if (inOrder.Length != postOrder.Length) throw new ArgumentException("Invalid input.");
            if (inOrder.Length < 1) throw new ArgumentException("Invalid input.");

            var postRoot = postOrder.Last();

            if (inOrder.Length == 1)
            {
                var inRoot = inOrder.First();
                if (inRoot == postRoot) return new BinaryTree<int>(postRoot);
                else throw new ArgumentException("Invalid input.");
            }
            else
            {
                var tree = new BinaryTree<int>(postRoot);

                var leftInOrder = inOrder.TakeWhile(x => x != tree.Model).ToArray();
                var rightInOrder = inOrder.Skip(1 + leftInOrder.Length).ToArray();

                var leftPostOrder = postOrder.Take(leftInOrder.Length).ToArray();
                var rightPreOrder = postOrder.Skip(leftInOrder.Length).Take(rightInOrder.Length).ToArray();

                if (leftPostOrder.Length > 0) tree.LeftNode = ConstructFrom_InPostOrder(leftInOrder, leftPostOrder);
                if (rightPreOrder.Length > 0) tree.RightNode = ConstructFrom_InPostOrder(rightInOrder, rightPreOrder);

                return tree;
            }
        }

    }
}
