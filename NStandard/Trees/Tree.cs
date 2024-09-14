using System.Diagnostics;

namespace NStandard.Trees;

public static class Tree
{
    public static Tree<T> From<T>(T model, Func<T, ICollection<T>> childrenSelector) where T : class
    {
        void AddChildren(TreeNode<T> node)
        {
            var model = node.Model;
            if (model is not null)
            {
                var children = childrenSelector(model);
                if (children is not null)
                {
                    foreach (var child in children)
                    {
                        var subNode = new TreeNode<T>(child);
                        node.Add(subNode);
                        AddChildren(subNode);
                    }
                }

            }
        }

        var root = new TreeNode<T>(model);
        AddChildren(root);
        return new Tree<T>(root);
    }

    public static IList<Tree<T>> From<T, TKey>(IEnumerable<T> models, Func<T, TKey> keySelector, Func<T, TKey?> parentKeySelector)
        where T : class
        where TKey : notnull
    {
        var keyType = typeof(TKey);
        if (keyType.IsValueType && !keyType.IsNullableValue()) throw new ArgumentException($"The argument({nameof(parentKeySelector)} must return a nullable type.");

        var roots = new Dictionary<TKey, TreeNode<T>>();
        var nonRoots = new List<T>();

        foreach (var model in models)
        {
            var parentKey = parentKeySelector(model);

            if (parentKey is null)
            {
                roots.Add(keySelector(model), new(model));
            }
            else nonRoots.Add(model);
        }

        void Init(TKey key, TreeNode<T> node)
        {
            var items = nonRoots.Where(x => key.Equals(parentKeySelector(x))).ToArray();
            foreach (var item in items)
            {
                var itemNode = new TreeNode<T>(item);
                node.Add(itemNode);
                nonRoots.Remove(item);
                Init(keySelector(item), itemNode);
            }
        }

        var list = new List<Tree<T>>();
        foreach (var root in roots)
        {
            var node = root.Value;
            Init(root.Key, node);
            list.Add(new(node));
        }
        return list;
    }
}

[DebuggerDisplay("{Root}")]
public class Tree<T>(TreeNode<T> root) : ITreeVisitable<Tree<T>, TreeNode<T>>
{
    public TreeNode<T> Root { get; } = root;

    public IEnumerable<TreeNode<T>> RecursiveNodes
    {
        get
        {
            yield return Root;
            foreach (var item in Root.RecursiveNodes)
            {
                yield return item;
            }
        }
    }

    public IEnumerable<TreeNode<T>> NonLeaves
    {
        get
        {
            if (!Root.IsLeaf)
            {
                yield return Root;
                foreach (var item in Root.NonLeaves)
                {
                    yield return item;
                }
            }
        }
    }

    public IEnumerable<TreeNode<T>> Leaves
    {
        get
        {
            if (Root.IsLeaf) yield return Root;
            else
            {
                foreach (var item in Root.Leaves)
                {
                    yield return item;
                }
            }
        }
    }

    public Tree<T> Visit(Func<TreeNode<T>, bool> predicate)
    {
        return new(Root.Visit(predicate));
    }
}
