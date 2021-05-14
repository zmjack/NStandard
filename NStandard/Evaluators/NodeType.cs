using System;
using System.Collections.Generic;
using System.Text;

namespace NStandard.Evaluators
{
    public enum NodeType
    {
        Unspecified = 0,
        Operand = 1,
        Parameter = 2,
        UnaryOperator = 3,
        BinaryOperator = 4,
        StartBracket = 5,
        EndBracket = 6,
    }
}
