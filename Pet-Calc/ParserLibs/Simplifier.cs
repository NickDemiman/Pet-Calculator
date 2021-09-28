using System;
using System.Collections.Generic;
using System.Text;

namespace Pet_Calc.ParserLibs
{
    public class Simplifier
    {
        private const char _openBracket = '(';
        private const char _closeBracket = ')';
        private const char _plus = '+';
        private const char _minus = '-';
        private const char _null = '\0';
        private int _minusCount;

        private bool defineIterationElements(ref char prev, ref char elem, ref char next, string expression, int i)
        {
            elem = expression[i];

            if (i - 1 < 0)
                prev = _null;
            else
                prev = expression[i - 1];

            if (i + 1 >= expression.Length)
                next = _null;
            else
                next = expression[i + 1];

            if (_minusCount != 0 && _minusCount % 2 != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public Simplifier()
        {
            _minusCount = 0;
        }

        public string PutMinusInBrackets(string expr)
        {
            StringBuilder result = new StringBuilder();
            Stack<bool> flags = new Stack<bool>();
            List<char> numbersWithOpBracket = new List<char>("0123456789)");
            bool flag;

            char prev = new char();
            char elem = new char();
            char next = new char();

            for (int i = 0; i < expr.Length; i++)
            {
                flag = defineIterationElements(ref prev, ref elem, ref next, expr, i);

                switch (elem)
                {
                    case _closeBracket:
                        if (flags.Pop()) _minusCount--;
                        result.Append(elem);
                        break;

                    case _openBracket:
                        flags.Push(false);
                        result.Append(elem);
                        break;

                    case _plus:
                        if (flag)   result.Append(_minus);
                        else        result.Append(_plus);
                        break;

                    case _minus:
                        if (next == _openBracket)
                        {
                            _minusCount++;
                            i++;

                            flags.Push(true);
                            if (numbersWithOpBracket.Contains(prev))
                                result.Append(_plus);
                            result.Append(next);
                        }
                        else
                        {
                            if (flag)
                            {
                                if (prev != _openBracket)
                                {
                                    result.Append(_plus);
                                }
                            }
                            else
                            {
                                result.Append(_minus);
                            }
                        }
                        break;

                    default:
                        result.Append(elem);
                        break;
                }
            }
            return result.ToString();
        }

        public string Simplify(string expression)
        {
            var result = PutMinusInBrackets(expression);

            return result;
        }
    }
}
