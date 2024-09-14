namespace NStandard.Trees;

public interface ITreeVisitable<TSelf, TNode>
{
    IEnumerable<TNode> RecursiveNodes { get; }
    IEnumerable<TNode> NonLeaves { get; }
    IEnumerable<TNode> Leaves { get; }
    TSelf Visit(Func<TNode, bool> predicate);
}
