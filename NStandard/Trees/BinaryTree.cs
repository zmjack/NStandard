using System;

namespace NStandard.Trees
{
    public class BinaryTree<TModel> : IBinaryTree<BinaryTree<TModel>, TModel>
    {
        public BinaryTree() { }
        public BinaryTree(TModel model)
        {
            Model = model;
        }

        public TModel Model { get; set; }
        public BinaryTree<TModel> Parent { get; set; }

        private BinaryTree<TModel> _LeftNode;
        public BinaryTree<TModel> LeftNode
        {
            get => _LeftNode;
            set
            {
                if (value is not null)
                {
                    value.Parent = this;
                    _LeftNode = value;
                }
                else
                {
                    _LeftNode.Parent = null;
                    _LeftNode = null;
                }
            }
        }

        private BinaryTree<TModel> _RightNode;
        public BinaryTree<TModel> RightNode
        {
            get => _RightNode;
            set
            {
                if (value is not null)
                {
                    value.Parent = this;
                    _RightNode = value;
                }
                else
                {
                    _RightNode.Parent = null;
                    _RightNode = null;
                }
            }
        }

    }
}
