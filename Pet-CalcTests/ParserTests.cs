using System;
using Xunit;
using Pet_Calc.ParserLibs;
using System.Collections.Generic;

namespace Pet_CalcTests
{
    public class ParserTests
    {
        private bool IsEqual(List<BaseElements> left, List<BaseElements> right)
        {
            if (left.Count == right.Count)
            {
                for (int i = 0; i< left.Count; i++)
                {
                    if (left[i] is Number)
                    {
                        if (!left[i].Equals(right[i]))
                            return false;
                    }

                    if (left[i] is BaseOperator)
                    {
                        if (!left[i].Equals(right[i]))
                            return false;
                    }
                }
                return true;
            }
            else
                return false;
        }

        [Fact]
        public void ParseTests()
        {
            // Arrange
            string expr1 = "-8+(1-2)";
            string expr2 = "-8+(1-2)/-1";
            string expr3 = "5/(9-3^2)";
            string expr4 = "+1+2";
            string expr5 = "1*2+(3/+4)";

            Parser parser = new Parser();

            List<BaseElements> expected1 = new List<BaseElements>()
            {
                new Number(-8),
                new Number(1),
                new Number(2),
                new SubOperator(),
                new AdOperator()
            };

            List<BaseElements> expected2 = new List<BaseElements>()
            {
                new Number(-8),
                new Number(1),
                new Number(2),
                new SubOperator(),
                new Number(-1),
                new DvOperator(),
                new AdOperator()
            };

            List<BaseElements> expected3 = new List<BaseElements>()
            {
                new Number(5),
                new Number(9),
                new Number(3),
                new Number(2),
                new PowOperator(),
                new SubOperator(),
                new DvOperator()
            };

            List<BaseElements> expected4 = new List<BaseElements>()
            {
                new Number(1),
                new Number(2),
                new AdOperator()
            };
            
            List<BaseElements> expected5 = new List<BaseElements>()
            {
                new Number(1),
                new Number(2),
                new MpOperator(),
                new Number(3),
                new Number(4),
                new DvOperator(),
                new AdOperator()
            };

            // Act
            var actual1 = parser.Parse(expr1);
            var actual2 = parser.Parse(expr2);
            var actual3 = parser.Parse(expr3);
            var actual4 = parser.Parse(expr4);
            var actual5 = parser.Parse(expr5);

            var answer1 = IsEqual(expected1, actual1);
            var answer2 = IsEqual(expected2, actual2);
            var answer3 = IsEqual(expected3, actual3);
            var answer4 = IsEqual(expected4, actual4);
            var answer5 = IsEqual(expected5, actual5);

            // Assert
            Assert.True(answer1);
            Assert.True(answer2);
            Assert.True(answer3);
            Assert.True(answer4);
            Assert.True(answer5);
        }
    }
}
