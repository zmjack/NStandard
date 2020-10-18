using System;
using System.Collections.Generic;
using System.Text;

namespace NStandard.Trees
{
    public abstract class BalancedBinaryTree<TModel> : IBinaryTree<BalancedBinaryTree<TModel>, TModel> where TModel : class
    {
        public BalancedBinaryTree() { }
        public BalancedBinaryTree(TModel model)
        {
            Model = model;
        }

        public TModel Model { get; set; }
        public int Height { get; private set; }
        public int BalanceFactor { get; private set; }

        private BalancedBinaryTree<TModel> _LeftNode;
        private BalancedBinaryTree<TModel> _RightNode;

        public BalancedBinaryTree<TModel> Parent { get; set; }
        public BalancedBinaryTree<TModel> LeftNode
        {
            get => _LeftNode;
            set
            {
                _LeftNode = value;
                BalanceFactor = (_RightNode?.Height ?? 0) - (_LeftNode?.Height ?? 0);
            }
        }
        public BalancedBinaryTree<TModel> RightNode
        {
            get => _RightNode;
            set
            {
                _RightNode = value;
                BalanceFactor = (_RightNode?.Height ?? 0) - (_LeftNode?.Height ?? 0);
            }
        }

        protected abstract void Balance();

    }
}
