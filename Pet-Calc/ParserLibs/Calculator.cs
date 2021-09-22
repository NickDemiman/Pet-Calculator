using System;
using System.Collections.Generic;
using System.Text;

namespace Pet_Calc.ParserLibs
{
    public class Calculator
    {
        public int _numberCountAfterPoint;

        private int FindOperator(ref List<BaseElements> list)
        {
            int i;
            for (i = 2; i < list.Count; i++)
            {
                if (list[i] is BaseOperator)
                {
                    if (list[i - 2] is Number || list[i - 1] is Number)
                        return i;
                }
            }

            return -1;
        }

        private void PerformCorrectAction(ref List<BaseElements> parsed, int index)
        {
            var left = (double)parsed[index - 2].value;
            var right = (double)parsed[index - 1].value;

            if (parsed[index] is AdOperator)
                parsed[index - 2].value = left + right;

            if (parsed[index] is SubOperator)
                parsed[index - 2].value = left - right;

            if (parsed[index] is DvOperator)
                parsed[index - 2].value = left / right;

            if (parsed[index] is MpOperator)
                parsed[index - 2].value = left * right;

            if (parsed[index] is PowOperator)
                parsed[index - 2].value = Math.Pow(left, right);

            parsed.RemoveAt(index);
            parsed.RemoveAt(index - 1);
        }

        private string VerifyAnswer(double value)
        {
            if (double.IsInfinity(value))
                return "Inf";

            if (double.IsNaN(value))
                return "NaN";

            if (double.IsFinite(value))
            {
                var result = Math.Round(value, _numberCountAfterPoint);
                return result.ToString();
            }

            return "";
        }

        public Calculator(int value)
        {
            _numberCountAfterPoint = value;
        }

        public string Calc(List<BaseElements> parsed)
        {
            var index = FindOperator(ref parsed);

            while (index != -1)
            {
                PerformCorrectAction(ref parsed, index);
                index = FindOperator(ref parsed);
            }

            var answer = (double)parsed[0].value;

            return VerifyAnswer(answer);
        }

    }
}
