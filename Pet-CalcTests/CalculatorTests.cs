using System;
using Xunit;
using Pet_Calc.ParserLibs;
using System.Collections.Generic;

namespace Pet_CalcTests
{
    public class CalculatorTests
    {
        [Fact]
        public void CalcTests()
        {
            // Arrange

            Calculator calculator = new Calculator(3);

            string expected1 = "90";
            string expected2 = "30";
            string expected3 = "Inf";
            string expected4 = "2,167";
            string expected5 = "NaN";

            List<BaseElements> list1 = new List<BaseElements>() // "-8+99^1-1" = 90
            {
                new Number(-8),
                new Number(99),
                new Number(1),
                new PowOperator(),
                new AdOperator(),
                new Number(1),
                new SubOperator(),
            };

            List<BaseElements> list2 = new List<BaseElements>() // "8*(32-2)/8" = 30
            {
                new Number(8),
                new Number(32),
                new Number(2),
                new SubOperator(),
                new MpOperator(),
                new Number(8),
                new DvOperator()
            };

            List<BaseElements> list3 = new List<BaseElements>() // "5/(9-3^2)" = inf
            {
                new Number(5),
                new Number(9),
                new Number(3),
                new Number(2),
                new PowOperator(),
                new SubOperator(),
                new DvOperator()
            };

            List<BaseElements> list4 = new List<BaseElements>() // "6/4+4/6" = 1
            {
                new Number(6),
                new Number(4),
                new DvOperator(),
                new Number(4),
                new Number(6),
                new DvOperator(),
                new AdOperator()
            };
            
            List<BaseElements> list5 = new List<BaseElements>() // "(2-2^1)/0" = 1
            {
                new Number(2),
                new Number(2),
                new Number(1),
                new PowOperator(),
                new SubOperator(),
                new Number(0),
                new DvOperator()
            };

            // Act
            var actual1 = calculator.Calc(list1);
            var actual2 = calculator.Calc(list2);
            var actual3 = calculator.Calc(list3);
            var actual4 = calculator.Calc(list4);
            var actual5 = calculator.Calc(list5);

            // Assert
            Assert.Equal(expected1, actual1);
            Assert.Equal(expected2, actual2);
            Assert.Equal(expected3, actual3);
            Assert.Equal(expected4, actual4);
            Assert.Equal(expected5, actual5);
        }


    }
}
