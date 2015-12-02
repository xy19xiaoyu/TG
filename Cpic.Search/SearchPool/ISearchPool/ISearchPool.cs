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


/// <summary>
/// 为CPRS2010组件，提供对检索、检索历史及用户的相关操作类
/// </summary>
namespace Cpic.Cprs2010.SearchPool
{
    /// <summary>
    ///ISearchPool 的摘要说明
    /// </summary>
    public interface ISearchPool : IDisposable
    {

        /// <summary>
        /// 最多几个Items
        /// </summary>
        int Max
        {
            get;
            set;
        }
        /// <summary>
        /// 空闲的Items
        /// </summary>
        Queue<Search.ISearch> freeItems
        {
            get;
            set;
        }

        /// <summary>
        /// 已使用的Items
        /// </summary>
        List<Search.ISearch> UsedItems
        {
            get;
            set;
        }

        /// <summary>
        /// 最少几个Items
        /// </summary>
        int Min
        {
            get;
            set;
        }

        /// <summary>
        /// 增长值
        /// </summary>
        int Growth
        {
            get;
            set;
        }

        /// <summary>
        /// 所有的Items总数
        /// </summary>
        int Count
        {
            get;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        void Ini();

        /// <summary>
        /// 得到一个Items
        /// </summary>     
        Search.ISearch getItem();
        /// <summary>
        /// 使用一个Items
        /// </summary>
        Search.ISearch useItem();

        /// <summary>
        /// 释放用户所使用的Items
        /// </summary>
        /// <param name="UserId">用户唯一标识</param>
        bool freeItem(Search.ISearch tmpIs);

        /// <summary>
        /// 增长
        /// </summary>
        bool Grow();
    }
}
