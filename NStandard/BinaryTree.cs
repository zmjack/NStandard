using System.Collections.Generic;

namespace NStandard
{
    public static class BinaryTree
    {
        public static IEnumerable<BinaryTree<TModel>> GetPreOrderNodes<TModel>(BinaryTree<TModel> tree)
        {
            yield return tree;
            if (tree.Left is not null)
            {
                var nodes = GetPreOrderNodes(tree.Left);
                foreach (var node in nodes) yield return node;
            }
            if (tree.Right is not null)
            {
                var nodes = GetPreOrderNodes(tree.Right);
                foreach (var node in nodes) yield return node;
            }
        }

        public static IEnumerable<BinaryTree<TModel>> GetInOrderNodes<TModel>(BinaryTree<TModel> tree)
        {
            if (tree.Left is not null)
            {
                var nodes = GetInOrderNodes(tree.Left);
                foreach (var node in nodes) yield return node;
            }
            yield return tree;
            if (tree.Right is not null)
            {
                var nodes = GetInOrderNodes(tree.Right);
                foreach (var node in nodes) yield return node;
            }
        }

        public static IEnumerable<BinaryTree<TModel>> GetPostOrderNodes<TModel>(BinaryTree<TModel> tree)
        {
            if (tree.Left is not null)
            {
                var nodes = GetPostOrderNodes(tree.Left);
                foreach (var node in nodes) yield return node;
            }
            if (tree.Right is not null)
            {
                var nodes = GetPostOrderNodes(tree.Right);
                foreach (var node in nodes) yield return node;
            }
            yield return tree;
        }
    }

    public class BinaryTree<TModel>
    {
        public BinaryTree<TModel> Parent { get; private set; }
        public BinaryTree<TModel> Left { get; private set; }
        public BinaryTree<TModel> Right { get; private set; }

        public TModel Model { get; set; }

        public BinaryTree() { }
        public BinaryTree(TModel model)
        {
            Model = model;
        }

        public void ClearLeft() => Left = null;
        public BinaryTree<TModel> SetLeft(TModel model) => SetLeft(new BinaryTree<TModel>(model));
        public BinaryTree<TModel> SetLeft(BinaryTree<TModel> tree)
        {
            tree.Parent = this;
            Left = tree;
            return tree;
        }

        public void ClearRight() => Right = null;
        public BinaryTree<TModel> SetRight(TModel model) => SetRight(new BinaryTree<TModel>(model));
        public BinaryTree<TModel> SetRight(BinaryTree<TModel> tree)
        {
            tree.Parent = this;
            Right = tree;
            return tree;
        }

    }
}
