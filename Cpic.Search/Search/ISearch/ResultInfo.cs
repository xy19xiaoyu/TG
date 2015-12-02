#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2010 @ CPIC Corp
*	CLR 版本: 2.0.50727.3603
*	文 件 名: ResultInfo.cs
*	创 建 人: xiwenlei(xiwl) $ chenxiaoyu(xy)
*	创建日期: 2010-9-27 13:21:17
*	版    本: V1.0
*	备注描述: $Myparameter1$           
*
* 修改历史: 
*   ****NO_1:
*	修 改 人: 
*	修改日期: 
*	描    述: $Myparameter1$           
******************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Cpic.Cprs2010.Search
{
    [Serializable]
    public class ResultInfo
    {
        /// <summary>
        /// 检索式信息
        /// </summary>
        private SearchPattern _searchPattern;

        /// <summary>
        /// 检索式信息
        /// </summary>
        public SearchPattern SearchPattern
        {
            get { return _searchPattern; }
            set { _searchPattern = value; }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public ResultInfo()
        {
            //todo:nothing
        }

        /// <summary>
        /// 检索结果信息
        /// </summary>
        private string _hitMsg;

        /// <summary>
        /// 检索结果信息
        /// </summary>
        public string HitMsg
        {
            get { return _hitMsg; }
            set
            {
                try
                {
                    Regex reg = new Regex(@"\(\d*\).*\<hits:(.*)\>.*"); //(001)F TI  BOOK   <hits: 2>
                    Match match = reg.Match(value);
                    if (match.Success)
                    {                        
                        _hitCount = int.Parse(match.Groups[1].Value.Trim());
                    }
                }
                catch
                {
                    _hitCount = 0;
                }
                _hitMsg = value;
            }
        }
        /// <summary>
        /// 结果记录数
        /// </summary>
        private int _hitCount;

        /// <summary>
        /// 结果记录数
        /// </summary>
        public int HitCount
        {
            get { return _hitCount; }
            set { _hitCount = value; }
        }
    }
}
