using System;

namespace NStandard.Trees
{
    public class BinaryTree<TModel> : IBinaryTree<BinaryTree<TModel>, TModel> where TModel : class
    {
        public BinaryTree() { }
        public BinaryTree(TModel model)
        {
            Model = model;
        }

        public TModel Model { get; set; }

        public BinaryTree<TModel> Parent { get; set; }
        public BinaryTree<TModel> LeftNode { get; set; }
        public BinaryTree<TModel> RightNode { get; set; }

    }
}
