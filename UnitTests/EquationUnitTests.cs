using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EquationReducer;

namespace UnitTestProject1
{
    [TestClass]
    public class EquationUnitTests
    {
        [TestMethod]
        public void CorrectEquation_SimpleTerms()
        {
            String input = "10x + 30y^2 = -10x";

            Equation equation = new Equation(input);

            Assert.AreEqual("20x + 30y^2 = 0", equation.ReducedEquation);
        }

        [TestMethod]
        public void CorrectEquation_MoreTerms()
        {
            String input = "10x + 30y + 40xy - 100x^2yz^2 = -10x";

            Equation equation = new Equation(input);

            Assert.AreEqual("20x + 30y + 40xy - 100x^2yz^2 = 0", equation.ReducedEquation);
        }

        [TestMethod]
        public void CorrectEquation_TermsWithoutVariables()
        {
            String input = "10x + 30 = -9";

            Equation equation = new Equation(input);

            Assert.AreEqual("10x + 39 = 0", equation.ReducedEquation);
        }

        [TestMethod]
        public void CorrectEquation_SimplfyOnOneSide()
        {
            String input = "10x + 30x - 100x = 0";

            Equation equation = new Equation(input);

            Assert.AreEqual("-60x = 0", equation.ReducedEquation);
        }

        [TestMethod]
        public void CorrectEquation_VariableSimplifyToZero()
        {
            String input = "10a^2b^3c^4defgh + 30x = 10a^2b^3c^4defgh";

            Equation equation = new Equation(input);

            Assert.AreEqual("30x = 0", equation.ReducedEquation);
        }

        [TestMethod]
        public void CorrectEquation_AllSimplifyToZero()
        {
            String input = "x + 2y + 3z = x + 2y + 3z";

            Equation equation = new Equation(input);

            Assert.AreEqual("0 = 0", equation.ReducedEquation);
        }

        [TestMethod]
        public void CorrectEquation_Doubles()
        {
            String input = "1.45231x = -0.001x";

            Equation equation = new Equation(input);

            Assert.AreEqual("1.45331x = 0", equation.ReducedEquation);
        }

        [TestMethod]
        public void CorrectEquation_OneInFinalEquation()
        {
            String input = "10x = 9x";

            Equation equation = new Equation(input);

            Assert.AreEqual("x = 0", equation.ReducedEquation);
        }

        [TestMethod]
        public void CorrectEquation_UnorderedVariables()
        {
            String input = "10x^2y = 9yx^2";

            Equation equation = new Equation(input);

            Assert.AreEqual("x^2y = 0", equation.ReducedEquation);
        }

        [TestMethod]
        public void CorrectEquation_SortVariableNames()
        {
            String input = "10cba = 0";

            Equation equation = new Equation(input);

            Assert.AreEqual("10abc = 0", equation.ReducedEquation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidInput_NullString()
        {
            String input = null;

            Equation equation = new Equation(input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidInput_InvalidDouble()
        {
            String input = "10.000.000x = 99x";

            Equation equation = new Equation(input);
        }
    }
}
