using System;
using System.Collections.Generic;
using Pet_Calc.ParserLibs;

namespace Pet_Calc
{
    class Program
    {
        static void Main(string[] args)
        {
            StringValidator validator = new StringValidator();
            Simplifier simplifier = new Simplifier();
            Parser parser = new Parser();
            Calculator calculator = new Calculator(3);

            Console.WriteLine("Введите ваше выражение:\n");

            while (true)
            {
                string expression = Console
                                            .ReadLine()
                                            .Replace(" ","");

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
        }
    }
}
