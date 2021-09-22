using System;
using System.Collections.Generic;
using System.Text;

namespace Pet_Calc.ParserLibs
{
    public class Simplifier
    {
        private List<char> _operators;
        private List<char> _numbers;
        private const char _openBracket = '(';
        private const char _closeBracket = ')';
        private const char _plus = '+';
        private const char _minus = '-';
        private int _minusCount;
        private int _bracketCount;

        private bool defineIterationElements(ref char prev, ref char elem, ref char next, string expression, int i)
        {
            elem = expression[i];

            if (i - 1 < 0)
                prev = ',';
            else
                prev = expression[i - 1];

            if (i + 1 >= expression.Length)
                next = ',';
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
            _bracketCount = 0;
            _operators = new List<char>("0123456789");
            _numbers = new List<char>("0123456789");
        }

        public string PutMinusInBrackets(string expr)
        {
            StringBuilder result = new StringBuilder();
            Stack<bool> flags = new Stack<bool>();
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
                        flags.Pop();
                        result.Append(elem);
                        break;

                    case _openBracket:
                        _bracketCount++;
                        result.Append(elem);
                        flags.Push(false);
                        break;

                    case _plus:
                        if (flag)
                        {
                            result.Append(_minus);
                        }
                        else
                        {
                            result.Append(_plus);
                        }
                        break;

                    case _minus:
                        if (next == _openBracket)
                        {
                            _minusCount++;
                            _bracketCount++;
                            i++;

                            flags.Push(true);
                            if ("0123456789)".Contains(prev))
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
            
            StringBuilder result = new StringBuilder();
            string res;

            res = PutMinusInBrackets(expression);

            result.AppendLine(res);

            return result.ToString();
        }
    }
}
