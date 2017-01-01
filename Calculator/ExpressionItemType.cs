using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal enum ExpressionPartType
    {
        Plus = 0,
        Minus = 1,
        Multiply = 2,
        Divide = 3,
        Number = 4,
        LParen = 5,
        RParen = 6,
        End = 7,
        Unknown = 8
    }
}
