using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pet_Calc.ParserLibs
{
    public class Parser
    {
        private List<char> _operators;
        private List<char> _numbers;

        private void FormNumber(ref StringBuilder number_str, ref List<BaseElements> result)
        {
            double number = new double();

            if (double.TryParse(number_str.ToString(), out number))
            {
                Number result_number = new Number(number);

                result.Add(result_number);

                number_str.Clear();
            }            
        }

        private BaseOperator FormOperator(char operator_ch)
        {
            switch (operator_ch)
            {
                case '+':
                    return new AdOperator();

                case '-':
                    return new SubOperator();

                case '*':
                    return new MpOperator();

                case '/':
                    return new DvOperator();

                case '^':
                    return new PowOperator();

                case '(':
                    return new OpenBracketOperator();

                default:
                    return new CloseBracketOperator();
            }
        }

        private void TakeAllOperatorsBetweenBrackets(ref List<BaseElements> result, ref Stack<BaseOperator> queue)
        {
            while (queue.Count != 0)
            {
                if (queue.Peek()._priority == 1)
                {
                    queue.Pop();
                    break;
                }
                else
                {
                    result.Add(queue.Pop());
                }
            }
        }

        private void TakeAllOperatorsWithLowerPriority(ref List<BaseElements> result, ref Stack<BaseOperator> queue, BaseOperator @operator, byte top_priority)
        {
            while (@operator._priority <= top_priority && queue.Count != 0)
            {
                result.Add(queue.Pop());

                if (queue.Count != 0)
                {
                    top_priority = queue.Peek()._priority;
                }
                else
                {
                    break;
                }
            }
            queue.Push(@operator);
        }

        private void PushOperatorToStack(ref List<BaseElements> result, ref Stack<BaseOperator> queue, char elem)
        {
            BaseOperator operator_obj = FormOperator(elem);
            byte top_priority = 0;

            if (queue.Count != 0)
            {
                top_priority = queue.Peek()._priority;
            }

            if (operator_obj is CloseBracketOperator)
            {
                TakeAllOperatorsBetweenBrackets(ref result, ref queue);
            }
            else
            {
                if (operator_obj._priority > top_priority || operator_obj is OpenBracketOperator)
                {
                    queue.Push(operator_obj);
                }
                else
                {
                    TakeAllOperatorsWithLowerPriority(ref result, ref queue, operator_obj, top_priority);
                }
            }
        }

        private void defineIterationElements(ref char prev, ref char elem, ref char next, string expression, int i)
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
        }

        private void ClearQueue(ref List<BaseElements> result, ref Stack<BaseOperator> queue, ref StringBuilder number)
        {
            if (number.Length != 0) FormNumber(ref number, ref result);

            while (queue.Count != 0)
            {
                result.Add(queue.Pop());
            }
        }

        public Parser()
        {
            _numbers = new List<char>("0123456789.");
            _operators = new List<char>("+-*/^()");
        }

        public List<BaseElements> Parse(string expression)
        {
            List<BaseElements> result = new List<BaseElements>();
            Stack<BaseOperator> op_queue = new Stack<BaseOperator>();
            StringBuilder number = new StringBuilder();

            char prev = new char();
            char elem = new char();
            char next = new char();

            for (int i = 0; i < expression.Length; i++)
            {
                defineIterationElements(ref prev, ref elem, ref next, expression, i);

                if (_operators.Contains(elem))
                {
                    if (elem == '-' || elem == '+')
                    {
                        if (!_numbers.Contains(prev))
                            number.Insert(0, elem);
                        else
                        {
                            FormNumber(ref number, ref result);
                            PushOperatorToStack(ref result, ref op_queue, elem);
                        }
                    }
                    else
                    {
                        FormNumber(ref number, ref result);
                        PushOperatorToStack(ref result, ref op_queue, elem);
                    }
                }
                else
                {
                    number.Append(elem);
                }
            }

            ClearQueue(ref result, ref op_queue, ref number);
            return result;
        }
    }
}
