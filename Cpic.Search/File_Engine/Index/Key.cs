using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cpic.Cprs2010.Index
{
    public struct Key
    {
        /// <summary>
        /// 索引键字的名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 索引键字的长度
        /// </summary>
        public int Length;
        /// <summary>
        /// 索引键字的编码
        /// </summary>
        public string Encoder;

        /// <summary>
        /// 索引值的
        /// </summary>
        public Value value;

        public override string ToString()
        {

            return string.Format("Name:{0};Length:{1};Encoder:{2};Value:{3}", this.Name, this.Length, this.Encoder.ToString(), value.Type.ToString());
        }
    }
}
