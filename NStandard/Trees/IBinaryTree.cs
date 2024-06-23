using System.Collections.Generic;

namespace NStandard.Trees;

public interface IBinaryTree<TNode, TModel> where TNode : class, IBinaryTree<TNode, TModel>
{
    TModel? Model { get; set; }

    TNode? Parent { get; }
    TNode? LeftNode { get; set; }
    TNode? RightNode { get; set; }
}

public static class XIBinaryTree
{
    public static IEnumerable<TNode?> GetPreOrderNodes<TNode, TModel>(this IBinaryTree<TNode, TModel> @this)
        where TNode : class, IBinaryTree<TNode, TModel>
    {
        yield return @this as TNode;
        if (@this.LeftNode is not null)
        {
            var nodes = GetPreOrderNodes(@this.LeftNode);
            foreach (var node in nodes) yield return node;
        }
        if (@this.RightNode is not null)
        {
            var nodes = GetPreOrderNodes(@this.RightNode);
            foreach (var node in nodes) yield return node;
        }
    }

    public static IEnumerable<TNode?> GetInOrderNodes<TNode, TModel>(this IBinaryTree<TNode, TModel> @this)
        where TNode : class, IBinaryTree<TNode, TModel>
    {
        if (@this.LeftNode is not null)
        {
            var nodes = GetInOrderNodes(@this.LeftNode);
            foreach (var node in nodes) yield return node;
        }
        yield return @this as TNode;
        if (@this.RightNode is not null)
        {
            var nodes = GetInOrderNodes(@this.RightNode);
            foreach (var node in nodes) yield return node;
        }
    }

    public static IEnumerable<TNode?> GetPostOrderNodes<TNode, TModel>(this IBinaryTree<TNode, TModel> @this)
        where TNode : class, IBinaryTree<TNode, TModel>
    {
        if (@this.LeftNode is not null)
        {
            var nodes = GetPostOrderNodes(@this.LeftNode);
            foreach (var node in nodes) yield return node;
        }
        if (@this.RightNode is not null)
        {
            var nodes = GetPostOrderNodes(@this.RightNode);
            foreach (var node in nodes) yield return node;
        }
        yield return @this as TNode;
    }
}
