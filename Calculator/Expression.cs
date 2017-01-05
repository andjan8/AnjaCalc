using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Expression
    {
        public string FullExpression { get; set; }
        public string Text { get; set; }
        public Guid Id { get; private set; }

        public Expression()
        {
            FullExpression = string.Empty;
            Text = string.Empty;
            Id = Guid.NewGuid();
        }

        internal void Append(string value)
        {
            this.Text = Format(this.Text + value);
        }

        private string Format(string _expression)
        {
            return _expression.Replace(" ", "").Trim().Replace("+", " + ").Replace("-", " - ");
        }

        internal void Erase()
        {
            var tempExpression = Text.Replace(" ", "");
            tempExpression = tempExpression.Substring(0, tempExpression.Length - 1);
            Text = Format(tempExpression);
        }

        internal void SetResult(double result)
        {
            this.FullExpression = this.Text;
            if (double.IsInfinity(result))
            {
                this.Text = "Infinity";
            }
            else
            {
                int nrOfDecimals = BitConverter.GetBytes(decimal.GetBits((Decimal)result)[3])[2];
                if (nrOfDecimals < 8)
                {
                    this.Text = result.ToString(string.Format("N{0}", nrOfDecimals)).Replace(",", " ");
                }
                else
                {
                    this.Text = result.ToString("N8").Replace(",", " ");
                }
            }
            this.FullExpression += " = " +this.Text;
        }

        internal void SetResult(string result)
        {
            this.FullExpression = this.Text;
            this.Text = result;
            this.FullExpression += " = " + this.Text;
        }
    }
}
