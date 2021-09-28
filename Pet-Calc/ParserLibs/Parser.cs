using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pet_Calc.ParserLibs
{
    public class Parser
    {
        private const char _null = '\0';
        private const char _plus = '+';
        private const char _minus = '-';
        private const char _multiply = '*';
        private const char _devide = '/';
        private const char _power = '^';
        private const char _openBracket = '(';
        private const char _closeBracket = ')';

        private readonly List<char> _operators;
        private readonly List<char> _numbers;

        private void FormNumber(ref StringBuilder number_str, ref List<BaseElements> result)
        {
            double number;

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
                case _plus:
                    return new AdOperator();

                case _minus:
                    return new SubOperator();

                case _multiply:
                    return new MpOperator();

                case _devide:
                    return new DvOperator();

                case _power:
                    return new PowOperator();

                case _openBracket:
                    return new OpenBracketOperator();

                default:
                    return new CloseBracketOperator();
            }
        }

        /// <summary>
        /// Выгружать все операторы из стека до тех пор, пока не будет встречена открывающая скобка
        /// </summary>
        /// <param name="result">Ссылка на список-результат обратной польской нотации</param>
        /// <param name="queue">Ссылка на стек операторов</param>
        private void TakeAllOperatorsBetweenBrackets(ref List<BaseElements> result, ref Stack<BaseOperator> queue)
        {
            while (queue.Count != 0)
            {
                if (queue.Peek()._priority == byte.MinValue)    // Встречена открывающая скобка
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

        /// <summary>
        /// Поместить оператор в стек операторов, если приоритет этого оператора больше 0 или приоритета оператора в шапке стека.
        /// Иначе, выгружать операторы из стека до тех пор, пока приоритет оператора в шапке стека не будет меньше или стек-пустой. 
        /// </summary>
        /// <param name="result">Ссылка на список-результат обратной польской нотации</param>
        /// <param name="queue">Ссылка на стек операторов</param>
        /// <param name="operator">Оператор, который нужно поместить в стек</param>
        /// <param name="top_priority">Приоритет оператора, находящийся в шапке стека</param>
        private void TakeAllOperatorsWithLowerPriority(ref List<BaseElements> result, ref Stack<BaseOperator> queue, BaseOperator @operator, byte top_priority)
        {
            BaseOperator poped;

            while (@operator._priority <= top_priority)
            {
                if (queue.TryPop(out poped))
                    result.Add(poped);

                if (queue.TryPeek(out poped))
                    top_priority = poped._priority;
                else
                    break;
            }

            queue.Push(@operator);
        }

        private void PushOperatorToStack(ref List<BaseElements> result, ref Stack<BaseOperator> queue, char elem)
        {
            BaseOperator operator_obj = FormOperator(elem);
            BaseOperator poped;
            byte top_priority;

            top_priority = queue.TryPeek(out poped) ? poped._priority : Byte.MinValue;

            if (operator_obj._priority == byte.MaxValue)
            {
                TakeAllOperatorsBetweenBrackets(ref result, ref queue);
                return;
            }

            if (operator_obj._priority > top_priority || operator_obj is OpenBracketOperator)
            {
                queue.Push(operator_obj);
            }
            else
            {
                TakeAllOperatorsWithLowerPriority(ref result, ref queue, operator_obj, top_priority);
            }
        }

        private void defineIterationElements(ref char prev, ref char elem, ref char next, string expression, int i)
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
            _numbers = new List<char>("0123456789,");
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
                    if (elem == _minus || elem == _plus)
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
