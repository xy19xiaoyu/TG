#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2010 @ CPIC Corp
*	CLR 版本: 2.0.50727.3603
*	文 件 名: SearchStatus.cs
*	创 建 人: xiwenlei(xiwl) $ chenxiaoyu(xy)
*	创建日期: 2010-9-27 13:22:07
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
    ///SearchStatus 的摘要说明
    /// </summary>
    public enum SearchStatus
    {
        /// <summary>
        /// 空闲
        /// </summary>
        Free = 0,
        /// <summary>
        /// 检索中
        /// </summary>
        Searching = 1,
        /// <summary>
        /// 关闭
        /// </summary>
        Closeed = 2,

        /// <summary>
        /// 错误
        /// </summary>
        Error = 3,
        /// <summary>
        /// 已登陆
        /// </summary>
        LogIn = 4,

        /// <summary>
        /// 已注销
        /// </summary>
        LogOut = 5,

    }
}
