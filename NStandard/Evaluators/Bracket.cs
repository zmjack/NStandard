using System.ComponentModel;

namespace NStandard.Evaluators;

public struct Bracket(string start, string end)
{
    public string Start = start;
    public string End = end;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void Deconstruct(out string start, out string end)
    {
        start = Start;
        end = End;
    }
}
