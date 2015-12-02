#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2010 @ CPIC Corp
*	CLR 版本: 2.0.50727.3603
*	文 件 名: ISearchPool.cs
*	创 建 人: xiwenlei(xiwl) $ chenxiaoyu(xy)
*	创建日期: 2010-9-27 12:36:50
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

namespace Cpic.Cprs2010.Search
{
    public class SearchPattern
    {
        /// <summary>
        /// 组别
        /// </summary>
        private string _GroupName = "";

        /// <summary>
        /// 组别最长4位，不足4位会在左侧自动补0,大于4位取前4位
        /// </summary>
        public string GroupName
        {
            get { return _GroupName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _GroupName = "".PadLeft(4, ' ');
                }
                else
                {
                    _GroupName = value.Length > 4 ? value.Substring(0, 4) : value.PadLeft(4, '0');
                }
            }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        private int _UserId;
        /// <summary>
        /// 检索编号
        /// </summary>
        private string _searchNo;

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        /// <summary>
        /// 检索编号
        /// </summary>
        public string SearchNo
        {
            get { return _searchNo; }
            set { _searchNo = value; }
        }
        /// <summary>
        /// 检索式
        /// </summary>
        private string _pattern;

        /// <summary>
        /// 检索式
        /// </summary>
        public string Pattern
        {
            get { return _pattern; }
            set { _pattern = value; }
        }
        /// <summary>
        /// 检索库类型
        /// </summary>
        private SearchDbType _dbType;

        /// <summary>
        /// 检索库类型
        /// </summary>
        public SearchDbType DbType
        {
            get { return _dbType; }
            set { _dbType = value; }
        }

        /// <summary>
        /// 自定义CNP文件
        /// </summary>
        private string _strCnpFile;

        /// <summary>
        /// 自定义CNP文件
        /// </summary>
        public string StrCnpFile
        {
            get { return _strCnpFile; }
            set { _strCnpFile = value; }
        }
    }
}
