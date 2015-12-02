#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2010 @ CPIC Corp
*	CLR 版本: 2.0.50727.3615
*	文 件 名: DocdbDataService.cs
*	创 建 人: xiwenlei(xiwl)
*	创建日期: 2010-12-14 9:53:36
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
using System.Configuration;
using System.IO;
using log4net;
using System.Reflection;

/// <summary>
/// Cpic.Cprs2010.Cfg.Data 的摘要说明
/// </summary>
namespace Cpic.Cprs2010.Cfg.Data
{
    /// <summary>
    ///cnDataService 的摘要说明
    /// </summary>
    public class DocdbDataService
    {
        /// <summary>
        /// Logger
        /// </summary>
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        #region 属性
        /// <summary>
        /// DOCDB数据XML根目录
        /// </summary>
        private static string strXmlBasePath;

        /// <summary>
        /// DOCDB数据XML根目录
        /// </summary>
        public static string StrXmlBasePath
        {
            get { return strXmlBasePath; }
            set { strXmlBasePath = value; }

        }

        /// <summary>
        /// 美国全文xml文件根目录
        /// </summary>
        private static string strEnTxtXmlBasePath;
        public static string StrEnTxtXmlBasePath
        {
            get { return strEnTxtXmlBasePath; }
            set { strEnTxtXmlBasePath = value; }
        }


        /// <summary>
        /// DOCDB数据PDF根目录
        /// </summary>
        private static string strImgBasePath;

        /// <summary>
        /// DOCDB数据PDF根目录
        /// </summary>
        public static string StrImgBasePath
        {
            get { return DocdbDataService.strImgBasePath; }
            set { DocdbDataService.strImgBasePath = value; }
        }

        #endregion

        #region 静态构造及初始化
        /// <summary>
        /// 静态构造
        /// </summary>
        static DocdbDataService()
        {
            Init();
        }

        /// <summary>
        /// 初始配置
        /// </summary>
        private static void Init()
        {
            //strXmlBasePath = @"\\10.75.8.118\DOCDB_Service\DOCDB_DATASOURCE_DIR\";     //ConfigurationManager.AppSettings["AbsXmlBasePath"].ToString();  

            strXmlBasePath = ConfigurationManager.AppSettings["DocdbXmlBasePath"].ToString();
            strImgBasePath = ConfigurationManager.AppSettings["DocdbImgBasePath"].ToString();
            strEnTxtXmlBasePath = ConfigurationManager.AppSettings["EnTxtXmlBasePath"].ToString();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 得到指定公开号得到对应的XML文件
        /// </summary>
        /// <param name="_strPubNo">公开号</param>
        public static string getXmlFile(string _strPubNo)
        {
            string strFilePath = "";
            try
            {
                //TBD: 提到2位国别
                string country = _strPubNo.Substring(0, 2);

                strFilePath = string.Format(@"{0}\{1}\{2}\{3}.xml", strXmlBasePath, country, GetFilePathByPubNo(_strPubNo), _strPubNo);

            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                strFilePath = "";
            }

            return strFilePath;
        }

        /// <summary>
        /// 获得美国全文xml文件路径
        /// </summary>
        /// <param name="_strPubNo"></param>
        /// <returns></returns>
        public static string getUSTxtXmlFile(string _strPubNo)
        {
            string strFilePath = "";
            try
            {
                //TBD: 提到2位国别
                string country = _strPubNo.Substring(0, 2);

                strFilePath = string.Format(@"{0}\{1}\{2}\{3}.xml", strEnTxtXmlBasePath, country, GetFilePathByPubNo(_strPubNo), _strPubNo);

            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                strFilePath = "";
            }

            return strFilePath;
        }

        /// <summary>
        /// 根据公开号获得文件相对路径
        /// </summary>
        /// <param name="pubNo"></param>
        /// <returns></returns>
        public static string GetFilePathByPubNo(string pubNo)
        {
            string filePath = string.Empty;
            pubNo = pubNo.PadLeft(16, '0');//'0'            
            filePath = pubNo.Substring(0, 4) + "\\" + pubNo.Substring(4, 4) + "\\" + pubNo.Substring(8, 4) + "\\" + pubNo.Substring(12, 4);
            return filePath;
        }

        /// <summary>
        /// 根据公开号获得文件目录的相对路径[含国别]
        /// </summary>
        /// <param name="pubNo"></param>
        /// <returns></returns>
        public static string GetRelativePathByPN(string pubNo)
        {
            pubNo = pubNo.Trim();
            //TBD: 提到2位国别
            string country = pubNo.Substring(0, 2);
            string filePath = string.Empty;
            pubNo = pubNo.PadLeft(16, '0');//'0'            
            filePath = country + "\\" + pubNo.Substring(0, 4) + "\\" + pubNo.Substring(4, 4) + "\\" + pubNo.Substring(8, 4) + "\\" + pubNo.Substring(12, 4);
            return filePath;
        }

        /// <summary>
        /// 得取PDF文件路径
        /// </summary>
        /// <param name="_strPubNo">公开号</param>
        /// <returns>文件路径</returns>
        public static string[] getImgPdfFile(string _strPubNo)
        {
            string[] resultFiel = new string[0] { };
            string strFilePath = "";
            try
            {
                strFilePath = getImgPdfFilePath(_strPubNo);

                if (Directory.Exists(strFilePath))
                {
                    resultFiel = Directory.GetFiles(strFilePath, "*.pdf");

                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());

            }
            return resultFiel;
        }

        /// <summary>
        /// 得取PDF文件根路径
        /// </summary>
        /// <param name="_strPubNo">公开号</param>
        /// <returns>根路径</returns>
        public static string getImgPdfFilePath(string _strPubNo)
        {
            string strFilePath = "";
            try
            {
                //TBD: 提到2位国别
                string country = _strPubNo.Substring(0, 2);

                strFilePath = string.Format(@"{0}\{1}\{2}\", strImgBasePath, country, GetFilePathByPubNo(_strPubNo));
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());

            }
            return strFilePath;
        }

        #endregion

    }
}
