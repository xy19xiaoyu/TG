
#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2012 @ CPIC Corp
*	CLR 版本: 2.0.50727.3615
*	文 件 名: GeneralDataInfo.cs
*	创 建 人: xiwenlei(xiwl) $ chenxiaoyu(xy)
*	创建日期: 2012-5-18 9:17:56
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
namespace Cpic.Cprs2010.Search.ResultData
{
    /// <summary>
    ///GeneralDataInfo 的摘要说明
    /// </summary>
    [Serializable]   
    public class GeneralDataInfo
    {
        public GeneralDataInfo()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private long nID;

        /// <summary>
        /// 流水号
        /// </summary>
        public long NID
        {
            get { return nID; }
            set { nID = value; }
        }

        private long nCPIC;

        /// <summary>
        /// 族号
        /// </summary>
        public long NCPIC
        {
            get { return nCPIC; }
            set { nCPIC = value; }
        }

        private string strTI;

        /// <summary>
        /// 发明名称
        /// </summary>
        public string StrTI
        {
            get { return strTI; }
            set { strTI = value; }
        }

        private string strTrsTI;

        /// <summary>
        /// 翻译后的发明名称
        /// </summary>
        public string StrTrsTI
        {
            get { return strTrsTI; }
            set { strTrsTI = value; }
        }

        private string strAN;

        /// <summary>
        /// 申请号
        /// </summary>
        public string StrAN
        {
            get { return strAN; }
            set { strAN = value; }
        }

        private string strAD;

        /// <summary>
        /// 申请日
        /// </summary>
        public string StrAD
        {
            get { return strAD; }
            set { strAD = value; }
        }

        private string strIPC;

        /// <summary>
        /// 分类号
        /// </summary>
        public string StrIPC
        {
            get { return strIPC; }
            set { strIPC = value; }
        }

        private int nMembers = 1;


        /// <summary>
        /// 同族成员数
        /// </summary>
        public int NMembers
        {
            get { return nMembers; }
            set { nMembers = value; }
        }

        private string strPubID;

        /// <summary>
        /// 公开号
        /// </summary>
        public string StrPubID
        {
            get { return strPubID; }
            set { strPubID = value; }
        }

        private string strPtCode = "";


        /// <summary>
        /// 专利编号
        /// </summary>
        public string StrPtCode
        {
            get { return strPtCode; }
            set { strPtCode = value; }
        }
    }
}
