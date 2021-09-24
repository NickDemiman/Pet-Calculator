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
        private const char _multiply = '*';
        private const char _devide = '/';
        private const char _plus = '+';
        private const char _minus = '-';
        private List<char> _operators;
        private List<char> _numbers;

        public StringValidator()
        {
            _numbers = new List<char>("0123456789");
            _operators = new List<char>("+-*/^");
        }

        public void FixMultipleOperatorBetweenNumberAndBracket(ref string value)
        {
            StringBuilder str = new StringBuilder(value);

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == _bracketOpen)
                    if (i - 1 >= 0)
                        if (_numbers.Contains(str[i - 1]))
                            str.Insert(i, _multiply);

                if (str[i] == _bracketClose)
                    if (i + 1 < str.Length)
                        if (_numbers.Contains(str[i + 1]))
                            str.Insert(i + 1, _multiply);
            }
            value = str.ToString();
        }

        public bool CheckBracketsCorrectNotation(string str)
        {
            char buff;
            Stack<char> brackets_queue = new Stack<char>();

            foreach (char elem in str)
            {
                switch (elem)
                {
                    case _bracketOpen:
                        brackets_queue.Push(elem);
                        break;

                    case _bracketClose:
                        if (!brackets_queue.TryPop(out buff))
                            return false;
                        break;
                }
            }

            if (brackets_queue.Count() != 0) return false;
            return true;
        }

        private bool Check1priorityOpNotation(ref string str, int index)
        {
            //List<char> numbersbrackets = new List<char>("0123456789(");

            if (index + 1 >= str.Length)
                return false;
            else
                if (!(_numbers.Contains(str[index + 1]) || str[index + 1] == _bracketOpen ))
                    return false;

            return true;
        }

        private bool Check2priorityOpNotation(ref string str, int index)
        {
            if (index + 1 >= str.Length || index - 1 < 0)
                return false;
            else
            {
                bool left = "0123456789)".Contains(str[index - 1]);
                bool right = "0123456789(+-".Contains(str[index + 1]);

                if (!(left && right))
                    return false;
            }

            return true;
        }

        public bool CheckCorrectOperatorsNotation(string str)
        {
            Dictionary<char, byte> operators_priority = new Dictionary<char, byte>
            {
                ['+'] = 1,
                ['-'] = 1,
                ['*'] = 2,
                ['/'] = 2,
                ['^'] = 2,
            };
            byte priority;

            for (int i = 0; i < str.Length; i++)
            {
                if (operators_priority.TryGetValue(str[i], out priority))
                {
                    switch (priority)
                    {
                        case 1:
                            if (!Check1priorityOpNotation(ref str, i))
                                return false;
                            break;

                        case 2:
                            if (!Check2priorityOpNotation(ref str, i))
                                return false;
                            break;
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
