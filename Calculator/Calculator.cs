using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Calculator : ICalculator
    {
        public Expression CurrentExpression { get; set; }
        public List<Expression> ExpressionHistory { get; set; }
        

        private Dictionary<Operators, string> _operatorDictionary;
        private ExpressionParser parser;

        public Calculator()
        {
            SetUpOperatorDictionary();
            parser = new ExpressionParser();
            this.CurrentExpression = new Expression();
            this.ExpressionHistory = new List<Expression>();


            for (int i = 1; i <= 10; i++)
            {
                var expression = new Expression();
                expression.FullExpression = string.Format("{0} + {1} = {2}", i, i, i + i);
                expression.Text = string.Format("{0} + {1}", i, i);
                ExpressionHistory.Add(expression);
            }

        }

        private void SetUpOperatorDictionary()
        {
            _operatorDictionary = new Dictionary<Operators, string>();
            _operatorDictionary.Add(Operators.Add, "+");
            _operatorDictionary.Add(Operators.Subtract, "-");
            _operatorDictionary.Add(Operators.Multiply, "*");
            _operatorDictionary.Add(Operators.Divide, "/");
            _operatorDictionary.Add(Operators.LeftParanthesis, "(");
            _operatorDictionary.Add(Operators.RightParanthesis, ")");
            _operatorDictionary.Add(Operators.Comma, ".");
        }

        public void RemoveFromHistory(Expression e)
        {
            this.ExpressionHistory.Remove(e);
        }

        public void Append(int numericValue)
        {
            CurrentExpression.Append(numericValue.ToString());
        }

        public void Append(Operators _operator)
        {
            CurrentExpression.Append(_operatorDictionary[_operator]);
        }

        public void Erase()
        {
            CurrentExpression.Erase();
        }

        public void Clear()
        {
            CurrentExpression = new Expression();
        }

        public void Calculate()
        {
            try
            {
                double result = parser.Parse(this.CurrentExpression.Text.Replace(" ", ""));
                CurrentExpression.SetResult(result);
            }
            catch (DivideByZeroException)
            {
                CurrentExpression.SetResult("NaN");
            }
            catch (Exception)
            {
                CurrentExpression.SetResult("Err");
            }

            this.ExpressionHistory.Add(CurrentExpression);

        }
    }
}
