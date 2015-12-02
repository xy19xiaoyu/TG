#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2010 @ CPIC Corp
*	CLR 版本: 2.0.50727.3603
*	文 件 名: ClassIfication.cs
*	创 建 人: xiwenlei(xiwl) $ chenxiaoyu(xy)
*	创建日期: 2010-10-11 13:37:13
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
using System.Net;
using log4net;
using System.Reflection;
using System.Xml;

namespace Cpic.Cprs2010.Cfg.Port
{
    /// <summary>
    /// 获取Ucla或Ecla等分类信息
    /// </summary>
    /// <remarks>获取Ucla或Ecla等分类信息</remarks>
    public class ClassIfication
    {
        /// <summary>
        /// Logger
        /// </summary>
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Ucal的请求地址
        /// </summary>
        private string _strUclaUrl;

        /// <summary>
        /// Ucla的请求地址
        /// </summary>
        public string StrUclaUrl
        {
            get { return _strUclaUrl; }
            set { _strUclaUrl = value; }
        }
        /// <summary>
        /// Ecla的请求地址
        /// </summary>
        private string _strEclaUrl = "http://10.75.8.114/invokexml.do?sf=QueryTab&spn={0}";

        /// <summary>
        /// Ecla的请求地址
        /// </summary>
        public string StrEclaUrl
        {
            get { return _strEclaUrl; }
            set { _strEclaUrl = value; }
        }
        /// <summary>
        /// 获取Ucla分类号信息
        /// </summary>
        /// <param name="_strApNo">申请号</param>
        public string GetUcla(string _strApNo)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 获取Ecla分类号信息
        /// </summary>
        /// <param name="_strApNo">申请号</param>
        public string GetEcla(string _strApNo)
        {
            logger.DebugFormat("***获取Ecla分类号:[{0}]**开始", _strApNo);

            string strXmlText = GetHttpStram(string.Format(_strEclaUrl, "CN" + _strApNo.Trim()));
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(strXmlText);
            XmlNodeList xmlNodeLst = xmlDoc.SelectNodes("//Result//ICLList//ICL//Key");

            string strResult = "";

            foreach (XmlNode xmlNodeItem in xmlNodeLst)
            {
                strResult += strResult + xmlNodeItem.InnerText.Trim()+";";
            }

            logger.DebugFormat("***获取Ecla分类号:[{0}]**结束,共[{1}]个", _strApNo, strResult);

            return strResult;
        }

        /// <summary>
        /// 发送Http请求，返回Http文本流
        /// </summary>
        /// <param name="_strUrl">URL请求地址</param>
        private string GetHttpStram(string _strUrl)
        {
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            string strResutl = client.DownloadString(_strUrl);
            return strResutl;
        }
    }
}
