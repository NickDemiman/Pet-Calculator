using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pet_Calc.ParserLibs
{
    public class StringValidator
    {
        private const char _bracketOpen = '(';
        private const char _bracketClose = ')';
        private List<char> _operators;
        private List<char> _numbers;

        public StringValidator()
        {
            _numbers = new List<char>("0123456789");
            _operators = new List<char>("+-*/^");
        }

        // Недописана
        public void FixMultipleOperatorBetweenNumberAndBracket(ref string value)
        {
            StringBuilder str = new StringBuilder(value);

            for (int i = 0; i < str.Length; i++)
            {
                try
                {
                    switch (str[i])
                    {
                        case '(':
                            if (_numbers.Contains(str[i - 1]))
                            {
                                str.Insert(i, '*');
                            }
                            break;

                        case ')':
                            if (_numbers.Contains(str[i + 1]))
                            {
                                str.Insert(i + 1, '*');
                            }
                            break;
                    }
                }
                catch (IndexOutOfRangeException)
                {

                }
            }
            value = str.ToString();
        }

        public bool CheckBracketsCorrectNotation(string str)
        {
            Stack<char> brackets_queue = new Stack<char>();

            foreach (char elem in str)
            {
                switch (elem)
                {
                    case _bracketOpen:
                        brackets_queue.Push(elem);
                        break;

                    case _bracketClose:
                        try
                        {
                            brackets_queue.Pop();
                        }
                        catch (InvalidOperationException)
                        {
                            return false;
                        }
                        break;
                }
            }

            if (brackets_queue.Count() != 0) return false;
            return true;
        }

        public bool CheckCorrectOperatorsNotation(string str)
        {
            List<char> numbersbrackets = new List<char>("0123456789(");

            for (int i = 0; i < str.Length; i++)
            {
                if (_operators.Contains(str[i]))
                {
                    if (str[i] == '+' || str[i] == '-')
                    {
                        if (i+1 >= str.Length)
                        {
                            return false;
                        }
                        else
                        {
                            if (!numbersbrackets.Contains(str[i + 1])){
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (i+1>=str.Length || i - 1 < 0)
                        {
                            return false;
                        }
                        else
                        {
                            bool left = "0123456789)".Contains(str[i - 1]);
                            bool right = "0123456789(+-".Contains(str[i + 1]);

                            if (!(left && right))
                                return false;
                        }
                    }
                }
            }
            return true;
        }

        public bool Validate(ref string str)
        {
            bool q1 = CheckBracketsCorrectNotation(str);
            FixMultipleOperatorBetweenNumberAndBracket(ref str);
            bool q2 = CheckCorrectOperatorsNotation(str);

            return q1 && q2;
        }
    }
}
