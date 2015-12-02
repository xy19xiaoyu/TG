#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2010 @ CPIC Corp
*	CLR 版本: 2.0.50727.3603
*	文 件 名: SearchPoolType.cs
*	创 建 人: xiwenlei(xiwl) $ chenxiaoyu(xy)
*	创建日期: 2010-9-27 14:45:23
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
namespace Cpic.Cprs2010.Search.SearchManager
{
    /// <summary>
    ///SearchPoolType 的摘要说明
    /// </summary>
    public enum SearchPoolType
    {
        /// <summary>
        /// 
        /// </summary>
        DefaultCn=-1,

        /// <summary>
        /// 
        /// </summary>
        DefaultDocDB=-2,

        /// <summary>
        /// 
        /// </summary>
        DefaultDwpi=-3,

        /// <summary>
        /// Socket 中文检索
        /// </summary>
        SocketCn=0,

        /// <summary>
        /// Socket DOCDB检索
        /// </summary>
        SocketDocDB=1,

        /// <summary>
        /// Socket DWPI检索
        /// </summary>
        SocketDwpi=2,

        /// <summary>
        /// 内存 中文检索
        /// </summary>
        MeeryCn=10,

        /// <summary>
        /// 
        /// </summary>
        FileCN=20,

        /// <summary>
        /// 
        /// </summary>
        FileDocdb,

        /// <summary>
        /// 
        /// </summary>
        FileDwpi,
    }
}
