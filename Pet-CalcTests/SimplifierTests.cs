using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Pet_Calc.ParserLibs;

namespace Pet_CalcTests
{
    public class SimplifierTests
    {
        [Theory]
        [InlineData("-((-1+2)+8)","((1-2)-8)")]
        [InlineData("-(-(-1+2)+8)","((-1+2)-8)")]
        [InlineData("-8+99^1-1", "-8+99^1-1")]
        [InlineData("-(-1-3)/-1", "(1+3)/-1")]
        public void PutMinusInBracketsTests(string expression, string expected)
        {
            // Arrange
            string actual;
            Simplifier simplifier = new Simplifier();

            // Act
            actual = simplifier.PutMinusInBrackets(expression);


            // Assert
            Assert.Equal(expected, actual);
        }

    }
}
