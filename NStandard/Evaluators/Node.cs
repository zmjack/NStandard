namespace NStandard.Evaluators;

public class Node(NodeType type, string value, int index = -1)
{
    public NodeType NodeType { get; internal set; } = type;
    public int Index { get; internal set; } = index;
    public string Value { get; internal set; } = value;
}
