using System;
using System.Collections.Generic;
using System.Text;

namespace NStandard
{
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

        public BinaryTree<TModel> SetLeft(TModel model)
        {
            var tree = new BinaryTree<TModel>(model)
            {
                Parent = this,
            };
            Left = tree;
            return tree;
        }

        public BinaryTree<TModel> SetRight(TModel model)
        {
            var tree = new BinaryTree<TModel>(model)
            {
                Parent = this,
            };
            Right = tree;
            return tree;
        }

    }
}
