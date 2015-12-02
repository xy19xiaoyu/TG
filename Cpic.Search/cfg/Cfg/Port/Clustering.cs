#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2010 @ CPIC Corp
*	CLR 版本: 2.0.50727.3603
*	文 件 名: Clustering.cs
*	创 建 人: xiwenlei(xiwl) $ chenxiaoyu(xy)
*	创建日期: 2010-9-27 13:37:13
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

namespace Cpic.Cprs2010.Cfg.Port
{
    /// <summary>
    /// 聚类
    /// </summary>
    /// <remarks>聚类</remarks>
    class Clustering
    {
        /// <summary>
        /// 申请度请求地址
        /// </summary>
        private string _strSimDocUrl;

        public string StrSimDocUrl
        {
            get { return _strSimDocUrl; }
            set { _strSimDocUrl = value; }
        }
        /// <summary>
        /// 根据传入的申请号，获取其相似文本
        /// </summary>
        /// <param name="_strApNo">申请号</param>
        public Queue<string> GetSimilarDoc(string _strApNo)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 根据传入的申请号，获取其相似文本
        /// </summary>
        /// <param name="_strApNo">申请号</param>
        /// <param name="_douSvalue">过滤条件，大于等于此相似度值的</param>
        public Queue<string> GetSimilarDoc(string _strApNo, double _douSvalue)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 发送Http请求，返回Http文本流
        /// </summary>
        public string GetHttpStram()
        {
            throw new System.NotImplementedException();
        }
    }
}
