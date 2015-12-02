using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cpic.Cprs2010.Index
{
    public enum WordSplitType
    {
        /// <summary>
        /// 中文切词
        /// </summary>
        Cn = 1,
        /// <summary>
        /// 英文切词
        /// </summary>
        English = 2,
        /// <summary>
        /// 不切词
        /// </summary>
        None = 3,
    }
}
