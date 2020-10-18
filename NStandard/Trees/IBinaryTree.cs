using System;
using System.Collections.Generic;
using System.Text;

namespace NStandard.Trees
{
    public interface IBinaryTree<TNode, TModel> where TNode : class, IBinaryTree<TNode, TModel>
    {
        TModel Model { get; set; }

        TNode Parent { get; set; }
        TNode LeftNode { get; set; }
        TNode RightNode { get; set; }
    }

    public static class XIBinaryTree
    {
        public static IEnumerable<TNode> GetPreOrderNodes<TNode, TModel>(this IBinaryTree<TNode, TModel> @this)
            where TNode : class, IBinaryTree<TNode, TModel>
        {
            yield return @this as TNode;
            if (@this.LeftNode is not null)
            {
                var nodes = GetPreOrderNodes<TNode, TModel>(@this.LeftNode);
                foreach (var node in nodes) yield return node;
            }
            if (@this.RightNode is not null)
            {
                var nodes = GetPreOrderNodes<TNode, TModel>(@this.RightNode);
                foreach (var node in nodes) yield return node;
            }
        }

        public static IEnumerable<TNode> GetInOrderNodes<TNode, TModel>(this IBinaryTree<TNode, TModel> @this)
            where TNode : class, IBinaryTree<TNode, TModel>
        {
            if (@this.LeftNode is not null)
            {
                var nodes = GetInOrderNodes<TNode, TModel>(@this.LeftNode);
                foreach (var node in nodes) yield return node;
            }
            yield return @this as TNode;
            if (@this.RightNode is not null)
            {
                var nodes = GetInOrderNodes<TNode, TModel>(@this.RightNode);
                foreach (var node in nodes) yield return node;
            }
        }

        public static IEnumerable<TNode> GetPostOrderNodes<TNode, TModel>(this IBinaryTree<TNode, TModel> @this)
            where TNode : class, IBinaryTree<TNode, TModel>
        {
            if (@this.LeftNode is not null)
            {
                var nodes = GetPostOrderNodes<TNode, TModel>(@this.LeftNode);
                foreach (var node in nodes) yield return node;
            }
            if (@this.RightNode is not null)
            {
                var nodes = GetPostOrderNodes<TNode, TModel>(@this.RightNode);
                foreach (var node in nodes) yield return node;
            }
            yield return @this as TNode;
        }
    }

}
