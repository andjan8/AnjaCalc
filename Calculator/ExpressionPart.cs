using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class ExpressionPart
    {
        public ExpressionPartType Type { get; set; }
        public string Value { get; set; }

        public ExpressionPart()
        {
            Type = ExpressionPartType.End;
            Value = string.Empty;
        }
    }
}
