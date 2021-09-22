using System;
using System.Collections.Generic;
using System.Text;

namespace Pet_Calc.ParserLibs
{
    public abstract class BaseElements
    {
        public object value;
    }

    public abstract class BaseOperand : BaseElements
    {

    }

    public abstract class BaseOperator : BaseElements
    {
        public byte _priority;

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is BaseOperator))
                return false;
            else
                return (char)this.value == (char)((BaseOperator)obj).value;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
