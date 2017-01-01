using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class ExpressionPartInterpreter
    {
        private char[] expressionCharacters;
        private ExpressionPart previousPart;
        private ExpressionPart part;
        private bool returnPreviousPart;
        private int index;

        public ExpressionPartInterpreter(string expression)
        {
            this.returnPreviousPart = false;
            this.expressionCharacters = expression.ToCharArray();
        }

        public ExpressionPart GetNextExpressionPart()
        {
            return returnPreviousPart ? ReturnPreviousPart() : GetNextPart();
        }

        private ExpressionPart ReturnPreviousPart()
        {
            returnPreviousPart = false;
            return previousPart;
        }

        private ExpressionPart GetNextPart()
        {
            previousPart = null;
            part = new ExpressionPart();
            bool stop = false;

            while (!stop)
            {
                if (index == expressionCharacters.Length)
                {
                    stop = true;
                }
                else
                {
                    char c = expressionCharacters[index];
                    stop = ParseCharacter(stop, c);
                }
            }

            previousPart = previousPart == null ? part : previousPart;
            return part;
        }

        private bool ParseCharacter(bool stop, char c)
        {
            
            switch (c)
            {
                case '+':
                    part = GetOperatorToken(ExpressionPartType.Plus, part);
                    stop = true;
                    break;
                case '-':
                    if (part.Type == ExpressionPartType.Number)
                    {
                        part = GetOperatorToken(ExpressionPartType.Minus, part);
                        stop = true;
                    }
                    else
                    {
                        part.Type = ExpressionPartType.Number;
                        part.Value += c;
                        index++;
                    }
                    break;
                case '*':
                    part = GetOperatorToken(ExpressionPartType.Multiply, part);
                    stop = true;
                    break;
                case '/':
                    part = GetOperatorToken(ExpressionPartType.Divide, part);
                    stop = true;
                    break;
                case '(':
                    part = GetOperatorToken(ExpressionPartType.LParen, part);
                    stop = true;
                    break;
                case ')':
                    part = GetOperatorToken(ExpressionPartType.RParen, part);
                    stop = true;
                    break;
                default:
                    part.Type = ExpressionPartType.Number;
                    part.Value += c;
                    index++;
                    break;
            }

            return stop;
        }
    

        private ExpressionPart GetOperatorToken(ExpressionPartType tKind, ExpressionPart t)
        {
            index++;
            if (t.Type == ExpressionPartType.End && t.Value == string.Empty)
            {
                return new ExpressionPart { Type = tKind, Value = string.Empty };
            }
            else
            {

                returnPreviousPart = true;
                previousPart = new ExpressionPart { Type = tKind, Value = string.Empty };
                return t;
            }
        }

        public void Revert()
        {
            // startIndex--;
            returnPreviousPart = true;
        }
    }
}
