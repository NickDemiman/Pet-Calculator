using System;
using System.Collections.Generic;
using System.Text;

namespace Pet_Calc.ParserLibs
{
    public class Number : BaseOperand
    {
        public Number(double x)
        {
            value = x;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Number))
                return false;
            else
                return (double)this.value == (double)((Number)obj).value;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class Variable : BaseOperand
    {
        public Variable(char x)
        {
            value = x;
        }
    }
}
