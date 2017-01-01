using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public interface ICalculator
    {
        string Expression { get; }
        void Append(int numericValue);
        void Append(Operators _operator);
        void Erase();
        void Clear();
        void Calculate();
    }
}
