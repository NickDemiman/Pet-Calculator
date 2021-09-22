using System;
using System.Collections.Generic;
using System.Text;

namespace Pet_Calc.ParserLibs
{
    public static class Comparer
    {
        public static bool IsEqual(BaseElements left, BaseElements right)
        {
            return left.value == right.value;
        }
    }
}
