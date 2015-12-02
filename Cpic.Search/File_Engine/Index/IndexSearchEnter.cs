using System;
using System.Collections.Generic;
using System.Text;

namespace Cpic.Cprs2010.Index
{
    public class IndexFileConfig
    {
        /// <summary>
        /// 键字长度
        /// </summary>
        public int Length;
        /// <summary>
        /// 索引名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 排序规则
        /// </summary>
        public string SortExpress;
        /// <summary>
        /// 每项在数据文件中的长度
        /// </summary>
        public int ItemLenth
        {
            get
            {
                int ix = SortExpress.IndexOf(",");
                if ( ix> 0)
                {
                    return Convert.ToInt32(ix);
                }
                else
                {
                    throw new Exception("Config文件错误");
                }
            }
        }
    }
}
