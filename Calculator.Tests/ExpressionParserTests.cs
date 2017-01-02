using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator;

namespace Calculator.Tests
{
    [TestClass]
    public class ExpressionParserTests
    {
        ExpressionParser p;

        [TestInitialize]
        public void Init()
        {
            p = new ExpressionParser();
        }

        [TestMethod]
        public void CanParseExpressions()
        {
        
            double result = p.Parse("100+(5*5)/5-5+(100-200)+10*10");
            Assert.AreEqual(100, result);

            result = p.Parse("55+2*3-(-1)");
            Assert.AreEqual(62, result);

            result = p.Parse("2*2*2");
            Assert.AreEqual(8, result);

            result = p.Parse("4/2");
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void MultiplicationsTest()
        {
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    var expectedResult = i * j;
                    var expression = i.ToString() + "*" + j.ToString();
         
                    double result = p.Parse(expression);
                    Assert.AreEqual(expectedResult, result);
                }
            }
        }

        [TestMethod]
        public void AddingTests()
        {
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    var expectedResult = i + j;
                    var expression = i.ToString() + "+" + j.ToString();
        
                    double result = p.Parse(expression);
                    Assert.AreEqual(expectedResult, result);
                }
            }
        }

        [TestMethod]
        public void SubtractionTests()
        {
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    var expectedResult = i - j;
                    var expression = i.ToString() + "-" + j.ToString();
                   
                    double result = p.Parse(expression);
                    Assert.AreEqual(expectedResult, result);
                }
            }
        }

        [TestMethod]
        public void DivisionTests()
        {
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    var expectedResult = (double)i / (double)j;
                    var expression = i.ToString() + "/" + j.ToString();
         
                    double result = p.Parse(expression);
                    Assert.AreEqual(expectedResult, result);
                }
            }
        }

      
        [TestMethod]
        public void HandlesDivisionWithZero()
        {
            double result = p.Parse("10/0");
            Assert.IsTrue(double.IsInfinity(result));
        }

        [TestMethod]
        public void CanHandleNonIntegerExpressions()
        {
            double result = p.Parse("2.5+2.5");
            Assert.AreEqual(5, result);
        }
    }
}
