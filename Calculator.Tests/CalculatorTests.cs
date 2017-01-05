using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        ICalculator calculator;
        [TestInitialize]
        public void Init()
        {
            calculator = new Calculator();
        }

        [TestMethod]
        public void CanAppendNumber()
        {
            calculator.Append(1);
            Assert.AreEqual("1", calculator.CurrentExpression.Text);
        }

        [TestMethod]
        public void CanAppendOperator()
        {
            calculator.Append(Operators.Add);
            Assert.AreEqual(" + ", calculator.CurrentExpression.Text);
        }

        [TestMethod]
        public void CanEnterExpression()
        {
            calculator.Append(1);
            calculator.Append(0);
            calculator.Append(0);
            calculator.Append(Operators.Add);
            calculator.Append(Operators.LeftParanthesis);
            calculator.Append(5);
            calculator.Append(Operators.Multiply);
            calculator.Append(5);
            calculator.Append(Operators.RightParanthesis);
            calculator.Append(Operators.Divide);
            calculator.Append(5);
            calculator.Append(Operators.Subtract);
            calculator.Append(5);
            calculator.Append(Operators.Add);
            calculator.Append(Operators.LeftParanthesis);
            calculator.Append(1);
            calculator.Append(0);
            calculator.Append(0);
            calculator.Append(Operators.Subtract);
            calculator.Append(2);
            calculator.Append(0);
            calculator.Append(0);
            calculator.Append(Operators.RightParanthesis);
            calculator.Append(Operators.Add);
            calculator.Append(1);
            calculator.Append(0);
            calculator.Append(Operators.Multiply);
            calculator.Append(1);
            calculator.Append(0);

            Assert.AreEqual("100 + (5*5)/5 - 5 + (100 - 200) + 10*10", calculator.CurrentExpression.Text);
        }

        [TestMethod]
        public void CanErase()
        {
            calculator.Append(1);
            calculator.Append(Operators.Add);
            calculator.Append(2);
            calculator.Append(Operators.Add);
            calculator.Append(3);
            calculator.Erase();
            Assert.AreEqual("1 + 2 + ", calculator.CurrentExpression.Text);
            calculator.Erase();
            Assert.AreEqual("1 + 2", calculator.CurrentExpression.Text);
        }

        [TestMethod]
        public void CanClear()
        {
            calculator.Append(1);
            calculator.Append(Operators.Add);
            calculator.Append(2);
            calculator.Append(Operators.Add);
            calculator.Append(3);
            Assert.AreEqual("1 + 2 + 3", calculator.CurrentExpression.Text);
            calculator.Clear();
            Assert.AreEqual(string.Empty, calculator.CurrentExpression.Text);
        }

        [TestMethod]
        public void CanCalculate()
        {
            calculator.Append(1);
            calculator.Append(0);
            calculator.Append(0);
            calculator.Append(0);
            calculator.Append(Operators.Multiply);
            calculator.Append(1);
            calculator.Append(0);
            calculator.Append(0);
            calculator.Append(0);
            calculator.Calculate();
            Assert.AreEqual("1 000 000", calculator.CurrentExpression.Text);
        }

        [TestMethod]
        public void CanGetHistory()
        {
            calculator.Append(1);
            calculator.Append(Operators.Add);
            calculator.Append(1);
            calculator.Calculate();
            var history = calculator.ExpressionHistory;
            Assert.AreEqual("1 + 1 = 2", history.First().FullExpression);
          
        }

    }
}
