namespace NStandard.Evaluators
{
    public class Node
    {
        public NodeType NodeType { get; internal set; }
        public int Index { get; internal set; } = -1;
        public string Value { get; internal set; }
    }
}
