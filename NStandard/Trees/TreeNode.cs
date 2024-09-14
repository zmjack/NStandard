using System.Collections;
using System.Diagnostics;

namespace NStandard.Trees;

[DebuggerDisplay("{Model}")]
public class TreeNode<T>(T model) : IList<TreeNode<T>>, ITreeVisitable<TreeNode<T>, TreeNode<T>>
{
    private readonly List<TreeNode<T>> _children = [];

    public TreeNode<T>? Parent { get; set; }
    public T Model { get; set; } = model;

    public TreeNode<T> this[int index]
    {
        get => _children[index];
        set => _children[index] = value;
    }

    public bool IsLeaf => _children.Count == 0;

    public int Count => _children.Count;
    public bool IsReadOnly => false;

    public void Add(TreeNode<T> item)
    {
        _children.Add(item);
    }

    public void Clear()
    {
        _children.Clear();
    }

    public bool Contains(TreeNode<T> item)
    {
        return _children.Contains(item);
    }

    public void CopyTo(TreeNode<T>[] array, int arrayIndex)
    {
        _children.CopyTo(array, arrayIndex);
    }

    public IEnumerator<TreeNode<T>> GetEnumerator()
    {
        return _children.GetEnumerator();
    }

    public int IndexOf(TreeNode<T> item)
    {
        return _children.IndexOf(item);
    }

    public void Insert(int index, TreeNode<T> item)
    {
        _children.Insert(index, item);
    }

    public bool Remove(TreeNode<T> item)
    {
        return _children.Remove(item);
    }

    public void RemoveAt(int index)
    {
        _children.RemoveAt(index);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _children.GetEnumerator();
    }

    public IEnumerable<TreeNode<T>> RecursiveNodes
    {
        get
        {
            foreach (var item in _children)
            {
                yield return item;

                foreach (var sub in item.RecursiveNodes)
                {
                    yield return sub;
                }
            }
        }
    }

    public IEnumerable<TreeNode<T>> NonLeaves
    {
        get
        {
            foreach (var item in _children)
            {
                if (!item.IsLeaf)
                {
                    yield return item;

                    foreach (var sub in item.NonLeaves)
                    {
                        yield return sub;
                    }
                }
            }
        }
    }

    public IEnumerable<TreeNode<T>> Leaves
    {
        get
        {
            foreach (var item in _children)
            {
                if (item.IsLeaf)
                {
                    yield return item;
                }
                else
                {
                    foreach (var sub in item.Leaves)
                    {
                        yield return sub;
                    }
                }
            }
        }
    }

    public TreeNode<T> Visit(Func<TreeNode<T>, bool> predicate)
    {
        void Init(TreeNode<T> root, IEnumerable<TreeNode<T>> children)
        {
            var _children = children.Where(predicate);
            foreach (var child in _children)
            {
                var subNode = new TreeNode<T>(child.Model);
                root.Add(subNode);
                Init(subNode, child);
            }
        }

        var root = new TreeNode<T>(Model);
        Init(root, this);
        return root;
    }
}
