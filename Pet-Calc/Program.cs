/**
 * \mainpage Консольный калькулятор выражений с поддержкой переменных и больших вычислений
 * # 1. Содержание
 * Данная программа состоит и следующих модулей:
 * - \ref BaseElements.cs | *Незадокументирован*
 * - \ref Calculator.cs
 * - \ref Operands.cs | **Задокументирован**
 * - \ref Operators.cs
 * - \ref Parser.cs
 * - \ref Simplifier.cs
 * - \ref StringValidator.cs
 * ## 1.1 Краткое описание
 * $$T_1=\sum\limits_{i=1}^{n}{T_i}$$
 */

using System;
using System.Collections.Generic;
using Pet_Calc.ParserLibs;

namespace Pet_Calc
{
    class Program
    {
        private static void Procces(ref string expression, int roundCount)
        {
            StringValidator validator = new StringValidator();
            Simplifier simplifier = new Simplifier();
            Parser parser = new Parser();
            Calculator calculator = new Calculator(roundCount);

            bool check = validator.Validate(ref expression);

            if (check)
            {
                expression = simplifier.Simplify(expression);

                var parsed = parser.Parse(expression);
                var answer = calculator.Calc(parsed);

                Console.WriteLine(answer);
                Console.WriteLine('\n');
            }
            else
            {
                Console.WriteLine("Введенная строка не валидна");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите ваше выражение:\n");

            while (true)
            {
                string expression = Console
                                        .ReadLine()
                                        .Replace(" ","");

                Procces(ref expression, 3);
            }
        }
    }
}
