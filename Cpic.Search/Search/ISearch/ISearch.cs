#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2010 @ CPIC Corp
*	CLR 版本: 2.0.50727.3603
*	文 件 名: ISearch.cs
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

/// <summary>
/// 为CPRS2010组件，提供对检索、检索历史及用户的相关操作类
/// </summary>
namespace Cpic.Cprs2010.Search
{
    /// <summary>
    ///ISearch 的摘要说明
    /// </summary>
    public interface ISearch
    {

        /// <summary>
        /// 唯一标识
        /// </summary>
        int Id
        {
            get;
            set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        SearchStatus Status
        {
            get;
            set;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        bool Ini();

        /// <summary>
        /// 登陆
        /// </summary>
        bool logIn();

        /// <summary>
        /// 注销
        /// </summary>
        bool logOut();

        /// <summary>
        /// 类型
        /// </summary>
        SearchDbType Type
        {
            get;
            set;
        }

        /// <summary>
        /// 检索
        /// </summary>
        ResultInfo Search(SearchPattern _searchPattern);

    }
}
