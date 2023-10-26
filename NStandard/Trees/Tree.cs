using System;
using System.Collections.Generic;
using System.Linq;

namespace NStandard.Trees;

public static class Tree
{
    public static Tree<TModel> Parse<TModel>(TModel model, Func<TModel, ICollection<TModel>> childrenGetter) where TModel : class
    {
        void AddChildren(Tree<TModel> tree)
        {
            var children = childrenGetter(tree.Model);

            if (children?.Any() ?? false)
            {
                tree.AddChildren(children);
                foreach (var child in tree.Children)
                    AddChildren(child);
            }
        }

        var tree = new Tree<TModel>(model);
        AddChildren(tree);
        return tree;
    }

    public static Tree<TModel>[] Parse<TModel>(IEnumerable<TModel> models, Func<TModel, ICollection<TModel>> childrenGetter) where TModel : class
    {
        return models.Select(x => Parse(x, childrenGetter)).ToArray();
    }

    public static Tree<TModel>[] ParseRange<TModel, TKey>(IEnumerable<TModel> models, Func<TModel, TKey> keySelector, Func<TModel, TKey> parentGetter) where TModel : class
    {
        if (!typeof(TKey).IsNullable()) throw new ArgumentException($"The argument({nameof(parentGetter)} must return a nullable type.");

        void AddChildren(Tree<TModel> tree)
        {
            var children = models.Where(x => parentGetter(x).Equals(keySelector(tree.Model)));

            if (children?.Any() ?? false)
            {
                tree.AddChildren(children);
                foreach (var child in tree.Children)
                    AddChildren(child);
            }
        }

        var trees = models.Select(x => new Tree<TModel>(x)).ToArray();
        foreach (var tree in trees) AddChildren(tree);
        return trees;
    }
}

public class Tree<TModel> where TModel : class
{
    private readonly HashSet<Tree<TModel>> _innerChildren = new();

    public Tree<TModel> Parent { get; private set; }
    public IEnumerable<Tree<TModel>> Children => _innerChildren.AsEnumerable();

    public TModel Model { get; set; }

    public Tree() { }
    public Tree(TModel model)
    {
        Model = model;
    }

    public Tree<TModel> AddChild(TModel model)
    {
        var tree = new Tree<TModel>(model);
        tree.Parent = this;
        _innerChildren.Add(tree);
        return tree;
    }

    public Tree<TModel>[] AddChildren(IEnumerable<TModel> models)
    {
        var list = new List<Tree<TModel>>();
        foreach (var model in models) list.Add(AddChild(model));
        return list.ToArray();
    }

    public bool RemoveChild(Tree<TModel> tree) => _innerChildren.Remove(tree);
    public int RemoveChildWhere(Predicate<Tree<TModel>> match) => _innerChildren.RemoveWhere(match);

    public IEnumerable<Tree<TModel>> GetNodes()
    {
        foreach (var node in _innerChildren)
        {
            yield return node;

            if (node._innerChildren.Any())
            {
                foreach (var node_ in node.GetNodes())
                    yield return node_;
            }
        }
    }

    public IEnumerable<Tree<TModel>> SelectNonLeafs()
    {
        foreach (var node in _innerChildren.Where(x => x._innerChildren.Any()))
        {
            yield return node;

            foreach (var node_ in node.SelectNonLeafs())
                yield return node_;
        }
    }

    public IEnumerable<Tree<TModel>> SelectLeafs()
    {
        foreach (var node in _innerChildren)
        {
            if (node._innerChildren.Any())
            {
                foreach (var leaf in node.SelectLeafs())
                    yield return leaf;
            }
            else yield return node;
        }
    }

    public Tree<TModel> Filter(Func<Tree<TModel>, bool> predicate)
    {
        void CopyChildren(Tree<TModel> root, IEnumerable<Tree<TModel>> sourceChildren)
        {
            var children = sourceChildren.Where(predicate);
            foreach (var child in children)
            {
                var subTree = root.AddChild(child.Model);
                CopyChildren(subTree, child.Children);
            }
        }

        var root = new Tree<TModel>(Model);
        CopyChildren(root, Children);
        return root;
    }

}
