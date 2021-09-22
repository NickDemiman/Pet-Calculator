using System;
using Xunit;
using Pet_Calc.ParserLibs;
using System.Text;

namespace Pet_CalcTests
{
    public class StringValidatorTests
    {
        [Theory]
        [InlineData("8(32-2)/8", "8*(32-2)/8")]
        [InlineData("(1+2)/(9+6)", "(1+2)/(9+6)")]
        [InlineData("8/(32-2)8", "8/(32-2)*8")]
        public void FixMultipleOperatorBetweenNumberAndBracketTests(string expr, string expected)
        {
            // Arrange
            var validator = new StringValidator();

            // Act
            validator.FixMultipleOperatorBetweenNumberAndBracket(ref expr);

            // Assert
            Assert.Equal(expected, expr);

        }

        [Theory]
        [InlineData("2+3()", true)]
        [InlineData("2+3)", false)]
        [InlineData(")(", false)]
        [InlineData("12)(+2", false)]
        [InlineData("1-)(+2", false)]
        [InlineData("1*((3-4)-5)", true)]
        [InlineData("1+2", true)]
        [InlineData("1+2)", false)]
        [InlineData("(1+2)", true)]
        [InlineData("(1+2)/(9+6)", true)]
        [InlineData("1+2+3", true)]
        [InlineData("1*((3-4)-5", false)]
        public void CheckBracketsCorrectNotationTests(string expr, bool expected)
        {
            //Arrange
            var str_val = new StringValidator();

            // Act
            bool answer = str_val.CheckBracketsCorrectNotation(expr);

            // Assert
            Assert.Equal(expected, answer);
        }

        [Theory]
        [InlineData("1+2+3++4", false)]
        [InlineData("1+2+3/4/5", true)]
        [InlineData("1+2+3/4//5", false)]
        [InlineData("1+2+3**4", false)]
        [InlineData("1+2+3--4", false)]
        [InlineData("1+*2-3", false)]
        [InlineData("-",false)]
        [InlineData("1+2-",false)]
        [InlineData("8+(1-2)",true)]
        [InlineData("8+(1-)",false)]
        [InlineData("8+(1-2)-3",true)]
        [InlineData("8/(1-2+)-3",false)]
        public void CheckCorrectOperatorsNotationTest(string expr, bool expected)
        {
            // Arrange
            var validator = new StringValidator();

            // Act
            bool answer = validator.CheckCorrectOperatorsNotation(expr);

            // Assert
            Assert.Equal(expected, answer);
        }
    }
}
