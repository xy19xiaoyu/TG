using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cpic.Cprs2010.Engine
{
    public class Result
    {
        public List<int> Content;
        public Result()
        {
            Content = new List<int>();
        }
        /// <summary>
        /// 并集
        /// </summary>
        /// <param name="sp1"></param>
        /// <param name="sp2"></param>
        /// <returns></returns>
        public static List<int> operator +(Result rs1, Result rs2)
        {
            return rs1.Content.Union(rs2.Content).ToList<int>();
        }
        /// <summary>
        /// 差集
        /// </summary>
        /// <param name="sp1"></param>
        /// <param name="sp2"></param>
        /// <returns></returns>
        public static List<int> operator -(Result rs1, Result rs2)
        {
            return rs1.Content.Except(rs2.Content).ToList<int>();
        }
        /// <summary>
        /// 交集
        /// </summary>
        /// <param name="sp1"></param>
        /// <param name="sp2"></param>
        /// <returns></returns>
        public static List<int> operator *(Result rs1, Result rs2)
        {
            return rs1.Content.Intersect(rs2.Content).ToList<int>();
        }

        public override string ToString()
        {
            return string.Format("共：{0} 条",Content.Count);
        }
    }
}
