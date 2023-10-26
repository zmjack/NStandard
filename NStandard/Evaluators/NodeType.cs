using System;

namespace NStandard.Evaluators;

[Flags]
public enum NodeType
{
    Unspecified = 0,
    Operand = 1,
    Parameter = 2,
    UnaryOperator = 4,
    BinaryOperator = 8,
    StartBracket = 1024,
    EndBracket = 2048,
}
