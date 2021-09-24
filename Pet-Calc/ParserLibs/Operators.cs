using System;
using System.Collections.Generic;
using System.Text;

namespace Pet_Calc.ParserLibs
{
    public class OpenBracketOperator : BaseOperator
    {
        public OpenBracketOperator()
        {
            value = '(';
            _priority = byte.MinValue;
        }
    }

    public class CloseBracketOperator : BaseOperator
    {
        public CloseBracketOperator()
        {
            value = ')';
            _priority = byte.MaxValue;
        }
    }

    public class AdOperator : BaseOperator
    {
        public AdOperator()
        {
            value = '+';
            _priority = 2;
        }
    }

    public class SubOperator : BaseOperator
    {
        public SubOperator()
        {
            value = '-';
            _priority = 2;
        }
    }

    public class DvOperator : BaseOperator
    {
        public DvOperator()
        {
            value = '/';
            _priority = 3;
        }
    }

    public class MpOperator : BaseOperator
    {
        public MpOperator()
        {
            value = '*';
            _priority = 3;
        }
    }

    public class PowOperator : BaseOperator
    {
        public PowOperator()
        {
            value = '^';
            _priority = 4;
        }
    }

    public class FlagOperator : BaseOperator
    {
        public FlagOperator()
        {
            value = '`';
            _priority = 255;
        }
    }
}
