using System;
using System.Linq;

namespace NStandard.Trees;

public class AvlTree<TModel> : IBinaryTree<AvlTree<TModel>, TModel>
{
    public AvlTree() { }
    public AvlTree(TModel model) => Model = model;

    public TModel Model { get; set; }
    public int Height { get; private set; } = 1;
    public int BalanceFactor { get; private set; }
    public BinaryNodeType NodeType { get; private set; }

    private AvlTree<TModel> _LeftNode;
    private AvlTree<TModel> _RightNode;

    private int LeftHeight => _LeftNode?.Height ?? 0;
    private int RightHeight => _RightNode?.Height ?? 0;

    public AvlTree<TModel> Parent { get; private set; }
    public AvlTree<TModel> LeftNode
    {
        get => _LeftNode;
        set
        {
            if (value is not null)
            {
                value.Parent = this;
                _LeftNode = value;
                _LeftNode.NodeType = BinaryNodeType.LeftNode;
            }
            else
            {
                _LeftNode.Parent = null;
                _LeftNode = null;
            }
            Update();
        }
    }
    public AvlTree<TModel> RightNode
    {
        get => _RightNode;
        set
        {
            if (value is not null)
            {
                value.Parent = this;
                _RightNode = value;
                _RightNode.NodeType = BinaryNodeType.RightNode;
            }
            else
            {
                _RightNode.Parent = null;
                _RightNode = null;
            }
            Update();
        }
    }

    private void Update()
    {
        var leftHeight = LeftHeight;
        var rightHeight = RightHeight;
        var height = new[] { leftHeight, rightHeight }.Max() + 1;
        BalanceFactor = rightHeight - leftHeight;

        if (Height != height)
        {
            Height = new[] { leftHeight, rightHeight }.Max() + 1;
            var increment = Height - height;
            for (var parent = Parent; parent != null; parent = parent.Parent)
            {
                parent.Height += increment;
                parent.BalanceFactor = parent.RightHeight - parent.LeftHeight;
            }
        }
    }

    public void LeftRotate()
    {
        if (NodeType != BinaryNodeType.RightNode) throw new InvalidOperationException("Only the right node is allowed to do a left rotation.");

        var root = Parent;
        switch (root.NodeType)
        {
            case BinaryNodeType.RootNode: Parent = null; break;
            case BinaryNodeType.LeftNode: root.Parent.LeftNode = this; break;
            case BinaryNodeType.RightNode: root.Parent.RightNode = this; break;
        }
        root.RightNode = LeftNode;
        LeftNode = root;
    }
    public void RightTotate()
    {
        if (NodeType != BinaryNodeType.LeftNode) throw new InvalidOperationException("Only the left node is allowed to do a right rotation.");

        var root = Parent;
        switch (root.NodeType)
        {
            case BinaryNodeType.RootNode: Parent = null; break;
            case BinaryNodeType.LeftNode: root.Parent.LeftNode = this; break;
            case BinaryNodeType.RightNode: root.Parent.RightNode = this; break;
        }
        root.LeftNode = RightNode;
        RightNode = root;
    }

}
