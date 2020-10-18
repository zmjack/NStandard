﻿using System;
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
                BalanceFactor = (_RightNode?.Height ?? 0) - (_LeftNode?.Height ?? 0);
            }
        }
        public BalancedBinaryTree<TModel> RightNode
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
                BalanceFactor = (_RightNode?.Height ?? 0) - (_LeftNode?.Height ?? 0);
            }
        }

        protected abstract void Balance();

    }
}