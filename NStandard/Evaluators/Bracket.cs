using System.ComponentModel;

namespace NStandard.Evaluators
{
    public struct Bracket
    {
        public string Start;
        public string End;

        public Bracket(string start, string end)
        {
            Start = start;
            End = end;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out string start, out string end)
        {
            start = Start;
            end = End;
        }
    }
}
