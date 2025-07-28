using System;
using System.Collections.Generic;
using System.Linq;

namespace NStandard.Trees;

public static class OldTree
{
    public static OldTree<TModel> Parse<TModel>(TModel? model, Func<TModel?, ICollection<TModel?>> childrenGetter) where TModel : class
    {
        void AddChildren(OldTree<TModel> tree)
        {
            var children = childrenGetter(tree.Model);

            if (children?.Any() ?? false)
            {
                tree.AddChildren(children);
                foreach (var child in tree.Children)
                    AddChildren(child);
            }
        }

        var tree = new OldTree<TModel>(model);
        AddChildren(tree);
        return tree;
    }

    public static OldTree<TModel>[] Parse<TModel>(IEnumerable<TModel?> models, Func<TModel?, ICollection<TModel?>> childrenGetter) where TModel : class
    {
        return models.Select(x => Parse(x, childrenGetter)).ToArray();
    }

    public static OldTree<TModel>[] ParseRange<TModel, TKey>(IEnumerable<TModel?> models, Func<TModel?, TKey> keySelector, Func<TModel?, TKey> parentGetter) where TModel : class
    {
        var keyType = typeof(TKey);
        if (keyType.IsValueType && !typeof(TKey).IsNullableValue()) throw new ArgumentException($"The argument({nameof(parentGetter)} must return a nullable type.");

        void AddChildren(OldTree<TModel> tree)
        {
            var children = models.Where(x => Equals(parentGetter(x), keySelector(tree.Model)));

            if (children?.Any() ?? false)
            {
                tree.AddChildren(children);
                foreach (var child in tree.Children)
                    AddChildren(child);
            }
        }

        var trees = models.Select(x => new OldTree<TModel>(x)).ToArray();
        foreach (var tree in trees) AddChildren(tree);
        return trees;
    }
}

public class OldTree<TModel> where TModel : class
{
    private readonly HashSet<OldTree<TModel>> _innerChildren = [];

    public OldTree<TModel>? Parent { get; private set; }
    public IEnumerable<OldTree<TModel>> Children => _innerChildren.AsEnumerable();

    public TModel? Model { get; set; }

    public OldTree() { }
    public OldTree(TModel? model)
    {
        Model = model;
    }

    public OldTree<TModel> AddChild(TModel? model)
    {
        var tree = new OldTree<TModel>(model)
        {
            Parent = this
        };
        _innerChildren.Add(tree);
        return tree;
    }

    public OldTree<TModel>[] AddChildren(IEnumerable<TModel?> models)
    {
        var list = new List<OldTree<TModel>>();
        foreach (var model in models) list.Add(AddChild(model));
        return [.. list];
    }

    public bool RemoveChild(OldTree<TModel> tree) => _innerChildren.Remove(tree);
    public int RemoveChildWhere(Predicate<OldTree<TModel>> match) => _innerChildren.RemoveWhere(match);

    public IEnumerable<OldTree<TModel>> GetNodes()
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

    public IEnumerable<OldTree<TModel>> SelectNonLeafs()
    {
        foreach (var node in _innerChildren.Where(x => x._innerChildren.Count != 0))
        {
            yield return node;

            foreach (var node_ in node.SelectNonLeafs())
                yield return node_;
        }
    }

    public IEnumerable<OldTree<TModel>> SelectLeafs()
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

    public OldTree<TModel> Filter(Func<OldTree<TModel>, bool> predicate)
    {
        void CopyChildren(OldTree<TModel> root, IEnumerable<OldTree<TModel>> sourceChildren)
        {
            var children = sourceChildren.Where(predicate);
            foreach (var child in children)
            {
                var subTree = root.AddChild(child.Model);
                CopyChildren(subTree, child.Children);
            }
        }

        var root = new OldTree<TModel>(Model);
        CopyChildren(root, Children);
        return root;
    }

}
