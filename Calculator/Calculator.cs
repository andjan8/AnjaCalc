using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Calculator : ICalculator
    {
        public string Expression { get; private set; }
        private Dictionary<Operators, string> _operatorDictionary;
        private ExpressionParser parser;

        public Calculator()
        {
            SetUpOperatorDictionary();
            parser = new ExpressionParser();
            this.Expression = string.Empty;
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

        public void Append(int numericValue)
        {
            Expression = Format(Expression + numericValue.ToString());
        }

        public void Append(Operators _operator)
        {
            Expression = Format(Expression + _operatorDictionary[_operator]);
        }

        public void Erase()
        {
            var tempExpression = Expression.Replace(" ", "");
            tempExpression = tempExpression.Substring(0, tempExpression.Length - 1);
            Expression = Format(tempExpression);

        }

        private string Format(string _expression)
        {
            return _expression.Replace(" ", "").Trim().Replace("+", " + ").Replace("-", " - ");
        }

        public void Clear()
        {
            this.Expression = string.Empty;
        }

        public void Calculate()
        {
            try
            {
                double result = parser.Parse(this.Expression.Replace(" ", ""));
                if (double.IsInfinity(result))
                {
                    this.Expression = "Infinity";
                }
                else
                {
                    int nrOfDecimals = BitConverter.GetBytes(decimal.GetBits((Decimal)result)[3])[2];
                    if (nrOfDecimals < 8)
                    {
                        this.Expression = result.ToString(string.Format("N{0}", nrOfDecimals)).Replace(",", " ");
                    }
                    else
                    {
                        this.Expression = result.ToString("N8").Replace(",", " ");
                    }
                }
                
            }
            catch (DivideByZeroException)
            {
                this.Expression = "NaN";
            }
            catch (Exception)
            {
                this.Expression = "Err";
            }

        }
    }
}
