using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class ExpressionParser
    {
        private ExpressionPartInterpreter lexer;
        ExpressionPartType[] additiveExpressionPartType = new ExpressionPartType[] { ExpressionPartType.Plus, ExpressionPartType.Minus };
        ExpressionPartType[] multiplicativeExpressionPartType = new ExpressionPartType[] { ExpressionPartType.Multiply, ExpressionPartType.Divide };

        public double Parse(string expression)
        {
            this.lexer = new ExpressionPartInterpreter(expression);
            double result = GetResult();

            ExpressionPart t = lexer.GetNextExpressionPart();
            if (t.Type == ExpressionPartType.End)
            {
                return result;
            }
            else
            {
                throw new Exception("End Expected");
            }
        }

     
        private double GetResult()
        {
            var component1 = Factor();
            var ExpressionPart = lexer.GetNextExpressionPart();
            while (additiveExpressionPartType.Contains(ExpressionPart.Type))
            {
                var component2 = Factor();
                if (ExpressionPart.Type == ExpressionPartType.Plus)
                {
                    component1 += component2;
                }
                else
                {
                    component1 -= component2;
                }

                ExpressionPart = lexer.GetNextExpressionPart();
            }
            lexer.Revert();
            return component1;
        }

        private double Factor()
        {
            var factor1 = Number();
            var ExpressionPart = lexer.GetNextExpressionPart();
            while (multiplicativeExpressionPartType.Contains(ExpressionPart.Type))
            {
                var factor2 = Number();
                if (ExpressionPart.Type == ExpressionPartType.Multiply)
                {
                    factor1 *= factor2;
                }
                else
                {
                    factor1 /= factor2;
                }
                ExpressionPart = lexer.GetNextExpressionPart();
            }
            lexer.Revert();
            return factor1;

        }

        private double Number()
        {
            var ExpressionPart = lexer.GetNextExpressionPart();
            double value = 0;
            if (ExpressionPart.Type == ExpressionPartType.LParen)
            {
                value = GetResult();
                var rParen = lexer.GetNextExpressionPart().Type;
                if (rParen != ExpressionPartType.RParen)
                    throw new Exception("Unbalanced parenthesis");
            }
            else if (ExpressionPart.Type == ExpressionPartType.Number)
            {
                value = Convert.ToDouble(ExpressionPart.Value);
            }
            else
            {
                throw new Exception("Not a number");
            }
            return value;

        }

       
    }
}
