#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2010 @ CPIC Corp
*	CLR 版本: 2.0.50727.3615
*	文 件 名: DwpiDataService.cs
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

/// <summary>
/// Cpic.Cprs2010.Cfg.Data 的摘要说明
/// </summary>
namespace Cpic.Cprs2010.Cfg.Data
{
    /// <summary>
    ///cnDataService 的摘要说明
    /// </summary>
    public class DwpiDataService
    {
        #region 属性
        /// <summary>
        /// 摘要数据目录
        /// </summary>
        private static string strAbsXmlBasePath;

        /// <summary>
        /// 摘要数据目录
        /// </summary>
        public static string StrAbsXmlBasePath
        {
            get { return strAbsXmlBasePath; }
            set { strAbsXmlBasePath = value; }
        }

        /// <summary>
        /// 摘要数据目录
        /// </summary>
        private static string strFuTuBasePath;


        /// <summary>
        /// 摘要数据目录
        /// </summary>
        public static string StrFuTuBasePath
        {
            get { return strFuTuBasePath; }
            set { strFuTuBasePath = value; }
        }
        #endregion

        #region 静态构造及初始化
        /// <summary>
        /// 静态构造
        /// </summary>
        static DwpiDataService()
        {
            Init();
        }

        /// <summary>
        /// 初始配置
        /// </summary>
        private static void Init()
        {
            //strAbsXmlBasePath = @"\\10.1.11.36\userdir\xml\";     //ConfigurationManager.AppSettings["AbsXmlBasePath"].ToString();
            //strFuTuBasePath = @"\\10.75.8.122\Format_Data\";                                //ConfigurationManager.AppSettings["FullXmlBasePath"].ToString();           
            
            strAbsXmlBasePath = ConfigurationManager.AppSettings["dwpiAbsXmlBasePath"].ToString();
            strFuTuBasePath = ConfigurationManager.AppSettings["dwpiFuTuBasePath"].ToString();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 得到指定入藏号的获取文件的全路径
        /// </summary>
        /// <param name="_strPan">入藏号为10位,6位内部转换</param>
        public static string getAbsXmlFile(string _strPan)
        {
            string strFilePath = "";
            try
            {
                //TBD: 
                string strDirPath = strAbsXmlBasePath + string.Format(@"{0}\{1}\", _strPan.Substring(0, 4), _strPan.Substring(4, 3));
                string fileRegex = _strPan + "*.xml";
                string[] strFilesPath = Directory.GetFiles(strDirPath, fileRegex);
                strFilePath = strFilesPath[0];

            }
            catch (Exception ex)
            {
                strFilePath = "";
            }

            return strFilePath;
        }

        /// <summary>
        /// 得到指定相对路径的附图全路径
        /// </summary>
        /// <param name="_relatedPath">相对路径</param>
        public static string getFuTuPath(string _relatedPath)
        {
            string strFilePath = "";
            try
            {
                //TBD: 
                //eg:\\10.75.8.122\Format_Data\2009\20001\003\200920001003c.xml
                strFilePath = strFuTuBasePath + _relatedPath;
            }
            catch (Exception ex)
            {
                strFilePath = "";
            }

            return strFilePath;
        }

        #endregion

    }
}
