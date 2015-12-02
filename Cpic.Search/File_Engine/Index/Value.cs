using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cpic.Cprs2010.Index
{
    public struct Value
    {
        public int Length;
        public ValType Type;

        public override string ToString()
        {
            return string.Format("Length:{0};Type:{1}", Length.ToString(), Type.ToString());
        }
    }
}
