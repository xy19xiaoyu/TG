#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2011 @ CPIC Corp
*	CLR 版本: 2.0.50727.3615
*	文 件 名: xmlDataInfo.cs
*	创 建 人: xiwenlei(xiwl) $ chenxiaoyu(xy)
*	创建日期: 2011-9-16 9:21:53
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
    ///xmlDataInfo 的摘要说明
    /// </summary>
    [Serializable]
    public class xmlDataInfo
    {
        public xmlDataInfo()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private string strSerialNo="";

        public string StrSerialNo
        {
            get { return strSerialNo; }
            set { strSerialNo = value; }
        }
        private string strANX = "";

        /// <summary>
        /// 加密
        /// </summary>
        public string StrANX
        {
            get { return strANX; }
            set { strANX = value; }
        }
        //申请号码
        private string strApNo = "";
        /// <summary>
        /// 申请号码
        /// </summary>
        public string StrApNo
        {
            get { return strApNo; }
            set { strApNo = value; }
        }
        //申请日
        private string strApDate = "";
        /// <summary>
        /// 申请日
        /// </summary>
        public string StrApDate
        {
            get { return strApDate; }
            set { strApDate = value; }
        }
        //公开号
        private string strPubNo = "";
        /// <summary>
        /// 公开号
        /// </summary>
        public string StrPubNo
        {
            get { return strPubNo; }
            set { strPubNo = value; }
        }
        //公开日
        private string strPubDate = "";
        /// <summary>
        /// 公开日
        /// </summary>
        public string StrPubDate
        {
            get { return strPubDate; }
            set { strPubDate = value; }
        }
        //公告号
        private string strAnnNo = "";

        public string StrAnnNo
        {
            get { return strAnnNo; }
            set { strAnnNo = value; }
        }
        //公告日
        private string strAnnDate = "";
        /// <summary>
        /// 公告日
        /// </summary>
        public string StrAnnDate
        {
            get { return strAnnDate; }
            set { strAnnDate = value; }
        }

        private string strPnOrGn = "";

        public string StrPnOrGn
        {
            get { return strPnOrGn; }
            set { strPnOrGn = value; }
        }
        private string strPdOrGd = "";

        public string StrPdOrGd
        {
            get { return strPdOrGd; }
            set { strPdOrGd = value; }
        }
        /// <summary>
        /// 主IPC
        /// </summary>
        private string strMainIPC = "";
        /// <summary>
        /// 主IPC
        /// </summary>
        public string StrMainIPC
        {
            get { return strMainIPC; }
            set { strMainIPC = value; }
        }
        //IPC
        private string strIpc = "";
        /// <summary>
        /// IPC
        /// </summary>
        public string StrIpc
        {
            get { return strIpc; }
            set { strIpc = value; }
        }
        //优先权号
        private string strPri = "";
        /// <summary>
        /// 优先权号
        /// </summary>
        public string StrPri
        {
            get { return strPri; }
            set { strPri = value; }
        }
        //发明人
        private string strInventor = "";
        /// <summary>
        /// 发明人
        /// </summary>
        public string StrInventor
        {
            get { return strInventor; }
            set { strInventor = value; }
        }
        //申请人
        private string strApply = "";

        public string StrApply
        {
            get { return strApply; }
            set { strApply = value; }
        }
        //发明名称
        private string strTitle = "";
        /// <summary>
        /// 发明名称
        /// </summary>
        public string StrTitle
        {
            get { return strTitle; }
            set { strTitle = value; }
        }

        //范畴分类
        private string strFiled = "";
        /// <summary>
        /// 范畴分类
        /// </summary>
        public string StrFiled
        {
            get { return strFiled; }
            set { strFiled = value; }
        }


        //国省代码
        private string strCountryCode = "";
        /// <summary>
        /// 国省代码
        /// </summary>
        public string StrCountryCode
        {
            get { return strCountryCode; }
            set { strCountryCode = value; }
        }

        private string strAgency = "";

        /// <summary>
        /// 代理机构
        /// </summary>
        public string StrAgency
        {
            get { return strAgency; }
            set { strAgency = value; }
        }


        //代理机构dz
        private string strAgency_Addres = "";

        /// <summary>
        /// 代理机构dz
        /// </summary>
        public string StrAgency_Addres
        {
            get { return strAgency_Addres; }
            set { strAgency_Addres = value; }
        }

        //penglei

        //权利要求
        private string strClaim = "";

        /// <summary>
        /// 权利要求
        /// </summary>
        public string StrClaim
        {
            get { return strClaim; }
            set { strClaim = value; }
        }


        //摘要
        private string strAbstr = "";
        /// <summary>
        /// 摘要
        /// </summary>
        public string StrAbstr
        {
            get { return strAbstr; }
            set { strAbstr = value; }
        }


        //附图
        private string strFtUrl = "";
        /// <summary>
        /// 附图
        /// </summary>
        public string StrFtUrl
        {
            get { return strFtUrl; }
            set { strFtUrl = value; }
        }

        private string strcpic = "";
        
        public string CPIC
        {
            get { return strcpic; }
            set { strcpic = value; }
        }


        private string strbrief = "";

        /// <summary>
        /// 简要说明/摘要
        /// </summary>
        public string Brief
        {
            get { return strbrief; }
            set { strbrief = value; }
        }
        /// <summary>
        /// 同族
        /// </summary>
        private string strTongZu = "";
        /// <summary>
        /// 同族
        /// </summary>
        public string TongZu
        {
            get { return strTongZu; }
            set { strTongZu = value; }
        }

        /// <summary>
        /// 专利类型
        /// </summary>
        private string strZhuanLiLeiXing = "";
        /// <summary>
        /// 专利类型
        /// 发明：1
        /// 实用新型：2
        /// 外观：3
        /// </summary>
        public string ZhuanLiLeiXing
        {
            get { return strZhuanLiLeiXing; }
            set { strZhuanLiLeiXing = value; }
        }

        /// <summary>
        /// 法律状态
        /// </summary>
        private string strFaLvZhuangTai="";
        /// <summary>
        /// 法律状态
        /// </summary>
        public string FaLvZhuangTai
        {
            get { return strFaLvZhuangTai; }
            set { strFaLvZhuangTai = value; }
        }

        /// <summary>
        /// 申请人地址
        /// </summary>
        private string strShenQingRenDiZhi = "";
        /// <summary>
        /// 申请人地址
        /// </summary>
        public string StrShenQingRenDiZhi
        {
            get { return strShenQingRenDiZhi; }
            set { strShenQingRenDiZhi = value; }
        }

        /// <summary>
        /// 代理人
        /// </summary>
        private string strDaiLiRen = "";
        /// <summary>
        /// 代理人
        /// </summary>
        public string StrDaiLiRen
        {
            get { return strDaiLiRen; }
            set { strDaiLiRen = value; }
        }
        /// <summary>
        /// 数据来源
        /// </summary>
        private string form;
        /// <summary>
        /// 数据来源
        /// </summary>
        public string Form
        {
            get { return form; }
            set { form = value; }
        }

        /// <summary>
        /// 核心专利标识
        /// </summary>
        private string iscore;
        /// <summary>
        /// 核心专利标识
        /// </summary>
        public string Iscore
        {
            get { return iscore; }
            set { iscore = value; }
        }

        private string _Note;
        /// <summary>
        /// 标注
        /// </summary>
        public string Note
        {
            get { return _Note; }
            set { _Note = value; }
        }
        private string _NoteDate;
        /// <summary>
        /// 标注日期
        /// </summary>
        public string NoteDate
        {
            get { return _NoteDate; }
            set { _NoteDate = value; }
        }
        private string _zlnl;
        /// <summary>
        /// 专利年龄
        /// </summary>
        public string ZLNl
        {
            get { return _zlnl; }
            set { _zlnl = value; }
        }

        ////////DOCDB数据字段
        private string strDocdbApNo = "";

        public string StrDocdbApNo
        {
            get { return strDocdbApNo; }
            set { strDocdbApNo = value; }
        }

        private string strEpoApNo = "";

        public string StrEpoApNo
        {
            get { return strEpoApNo; }
            set { strEpoApNo = value; }
        }

        private string strOriginalApNo = "";

        public string StrOriginalApNo
        {
            get { return strOriginalApNo; }
            set { strOriginalApNo = value; }
        }

        private string strDocdbPubNo = "";

        public string StrDocdbPubNo
        {
            get { return strDocdbPubNo; }
            set { strDocdbPubNo = value; }
        }

        private string strEpoPubNo = "";

        public string StrEpoPubNo
        {
            get { return strEpoPubNo; }
            set { strEpoPubNo = value; }
        }

        private string strOriginalPubNo = "";

        public string StrOriginalPubNo
        {
            get { return strOriginalPubNo; }
            set { strOriginalPubNo = value; }
        }

        private string strFmyAbs = "";

        public string StrAbsFmy
        {
            get { return strFmyAbs; }
            set { strFmyAbs = value; }
        }

        private string strRefDoc = "";

        /// <summary>
        /// 引用文献
        /// </summary>
        public string StrRefDoc
        {
            get { return strRefDoc; }
            set { strRefDoc = value; }
        }

        private string strEcla = "";

        /// <summary>
        /// ECLA
        /// </summary>
        public string StrEcla
        {
            get { return strEcla; }
            set { strEcla = value; }
        }
    }
}
