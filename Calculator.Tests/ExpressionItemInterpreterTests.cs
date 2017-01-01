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
    public class ExpressionItemInterpreterTests
    {

        [TestMethod]
        public void CanGetNextToken_NumberOnly()
        {
            ExpressionPartInterpreter l = new ExpressionPartInterpreter("123");
            ExpressionPart t = l.GetNextExpressionPart();
            Assert.AreEqual("123", t.Value);
            Assert.AreEqual(ExpressionPartType.Number, t.Type);
        }

        [TestMethod]
        public void CanGetNextToken_SimpleExpression()
        {
            ExpressionPartInterpreter l = new ExpressionPartInterpreter("1+2");
            ExpressionPart t = l.GetNextExpressionPart();
            Assert.AreEqual("1", t.Value);
            Assert.AreEqual(ExpressionPartType.Number, t.Type);

            t = l.GetNextExpressionPart();
            Assert.AreEqual(string.Empty, t.Value);
            Assert.AreEqual(ExpressionPartType.Plus, t.Type);

            t = l.GetNextExpressionPart();
            Assert.AreEqual("2", t.Value);
            Assert.AreEqual(ExpressionPartType.Number, t.Type);
        }

        [TestMethod]
        public void CanGetTokens()
        {
            ExpressionPartInterpreter l = new ExpressionPartInterpreter("55+2*3-(-1)");
            ExpressionPart t = l.GetNextExpressionPart();
            Assert.AreEqual("55", t.Value);
            Assert.AreEqual(ExpressionPartType.Number, t.Type);

            t = l.GetNextExpressionPart();
            Assert.AreEqual(string.Empty, t.Value);
            Assert.AreEqual(ExpressionPartType.Plus, t.Type);

            t = l.GetNextExpressionPart();
            Assert.AreEqual("2", t.Value);
            Assert.AreEqual(ExpressionPartType.Number, t.Type);

            t = l.GetNextExpressionPart();
            Assert.AreEqual(string.Empty, t.Value);
            Assert.AreEqual(ExpressionPartType.Multiply, t.Type);

            t = l.GetNextExpressionPart();
            Assert.AreEqual("3", t.Value);
            Assert.AreEqual(ExpressionPartType.Number, t.Type);

            t = l.GetNextExpressionPart();
            Assert.AreEqual(string.Empty, t.Value);
            Assert.AreEqual(ExpressionPartType.Minus, t.Type);

            t = l.GetNextExpressionPart();
            Assert.AreEqual(string.Empty, t.Value);
            Assert.AreEqual(ExpressionPartType.LParen, t.Type);

            t = l.GetNextExpressionPart();
            Assert.AreEqual("-1", t.Value);
            Assert.AreEqual(ExpressionPartType.Number, t.Type);

            t = l.GetNextExpressionPart();
            Assert.AreEqual(string.Empty, t.Value);
            Assert.AreEqual(ExpressionPartType.RParen, t.Type);

        }

    }
}
