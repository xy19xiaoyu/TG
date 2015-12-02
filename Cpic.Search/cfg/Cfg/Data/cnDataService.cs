#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2010 @ CPIC Corp
*	CLR 版本: 2.0.50727.3615
*	文 件 名: cnDataService.cs
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
using System.Xml;

/// <summary>
/// Cpic.Cprs2010.Cfg.Data 的摘要说明
/// </summary>
namespace Cpic.Cprs2010.Cfg.Data
{
    /// <summary>
    ///cnDataService 的摘要说明
    /// </summary>
    public class cnDataService
    {
        /// <summary>
        /// Logger
        /// </summary>
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


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
        /// 摘要数据翻译目录
        /// </summary>
        private static string strAbsXmlTransBasePath;

        /// <summary>
        /// 摘要数据翻译目录
        /// </summary>
        public static string StrAbsXmlTransBasePath
        {
            get { return strAbsXmlTransBasePath; }
            set { strAbsXmlTransBasePath = value; }
        }

        /// <summary>
        /// 全文数据目录
        /// </summary>
        private static string strFullXmlBasePath;

        /// <summary>
        /// 全文数据目录
        /// </summary>
        public static string StrFullXmlBasePath
        {
            get { return strFullXmlBasePath; }
            set { strFullXmlBasePath = value; }
        }

        /// <summary>
        /// 全文NewData数据目录
        /// </summary>
        private static string strFullXmlNewDataBasePath;

        /// <summary>
        /// 全文NewData数据目录
        /// </summary>
        public static string StrFullXmlNewDataBasePath
        {
            get { return cnDataService.strFullXmlNewDataBasePath; }
            set { cnDataService.strFullXmlNewDataBasePath = value; }
        }

        /// <summary>
        /// 全文图形数据目录
        /// </summary>
        private static string strImgDocBasePath;

        /// <summary>
        /// 全文图形数据目录
        /// </summary>
        public static string StrImgDocBasePath
        {
            get { return strImgDocBasePath; }
            set { strImgDocBasePath = value; }
        }
        /// <summary>
        /// 附图数据目录
        /// </summary>
        private static string strFuTuBasePath;

        /// <summary>
        /// 附图数据目录
        /// </summary>
        public static string StrFuTuBasePath
        {
            get { return strFuTuBasePath; }
            set { strFuTuBasePath = value; }
        }

        /// <summary>
        /// 附图GIF数据目录
        /// </summary>
        private static string strFuTuGifBasePath;

        /// <summary>
        /// 附图GIF数据目录
        /// </summary>
        public static string StrFuTuGifBasePath
        {
            get { return strFuTuGifBasePath; }
            set { strFuTuGifBasePath = value; }
        }
        #endregion

        #region 静态构造及初始化
        /// <summary>
        /// 静态构造
        /// </summary>
        static cnDataService()
        {
            Init();
        }

        /// <summary>
        /// 初始配置
        /// </summary>
        private static void Init()
        {
            strAbsXmlBasePath = @"\\10.1.11.36\userdir\xml\";     //ConfigurationManager.AppSettings["AbsXmlBasePath"].ToString();
            strFullXmlBasePath = @"\\10.75.8.122\Format_Data\";                                //ConfigurationManager.AppSettings["FullXmlBasePath"].ToString();
            strImgDocBasePath = @"\\10.1.2.134\imgdoc\";        //ConfigurationManager.AppSettings["ImgDocBasePath"].ToString();
            strFuTuBasePath = @"\\10.1.1.8\userdir\futu\";       //ConfigurationManager.AppSettings["FuTuBasePath"].ToString();

            strAbsXmlBasePath = ConfigurationManager.AppSettings["Cn_AbsXmlBasePath"].ToString();
            strFullXmlBasePath = ConfigurationManager.AppSettings["Cn_FullXmlBasePath"].ToString();
            strImgDocBasePath = ConfigurationManager.AppSettings["Cn_ImgDocBasePath"].ToString();
            strFuTuBasePath = ConfigurationManager.AppSettings["Cn_FuTuBasePath"].ToString();

            strFuTuGifBasePath = ConfigurationManager.AppSettings["Cn_FuTuGifBasePath"].ToString();

            strAbsXmlTransBasePath = @"\\10.75.8.122\CPRS2010_Data_UpList\英文样例（XML）\";
            strAbsXmlTransBasePath = ConfigurationManager.AppSettings["Cn_AbsXmlTransBasePath"].ToString();
            if (ConfigurationManager.AppSettings["Cn_FullXmlNewDataBasePath"] != null)
            {
                StrFullXmlNewDataBasePath = ConfigurationManager.AppSettings["Cn_FullXmlNewDataBasePath"].ToString();
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 得到指定申请号的摘数文件的全路径
        /// </summary>
        /// <param name="_strApNo">申请号为12位,8位内部转换</param>
        public static string getAbsXmlFile(string _strApNo)
        {
            string strFilePath = "";
            try
            {
                //TBD: 8位转12位
                _strApNo = ApNo8To12(_strApNo);
                strFilePath = strAbsXmlBasePath + string.Format(@"{0}\{1}\{2}.xml", _strApNo.Substring(0, 4), _strApNo.Substring(4, 5), _strApNo);
            }
            catch (Exception ex)
            {
                strFilePath = "";
            }

            return strFilePath;
        }

        /// <summary>
        /// 得到指定申请号的摘要翻译数据文件的全路径
        /// </summary>
        /// <param name="_strApNo">申请号为12位,8位内部转换</param>
        public static string getAbsXmlTransFile(string _strApNo)
        {
            string strFilePath = "";
            try
            {
                //TBD: 8位转12位
                if (_strApNo.IndexOf('.') > 0)
                {
                    _strApNo = _strApNo.Substring(0, _strApNo.IndexOf('.'));
                }
                _strApNo = ApNo8To12(_strApNo);
                strFilePath = strAbsXmlTransBasePath + string.Format(@"{0}\{1}\{2}.xml", _strApNo.Substring(0, 4), _strApNo.Substring(4, 5), _strApNo);
            }
            catch (Exception ex)
            {
                strFilePath = "";
            }

            return strFilePath;
        }

        /// <summary>
        /// 得到指定申请号的权利要求文件的全路径
        /// </summary>
        /// <param name="_strApNo">申请号为12位,8位内部转换</param>
        public static string getClamXmlFile(string _strApNo)
        {
            string strFilePath = "";
            try
            {
                //TBD: 8位转12位
                _strApNo = ApNo8To12(_strApNo);
                //eg:\\10.75.8.122\Format_Data\2009\20001\003\200920001003c.xml
                strFilePath = strFullXmlBasePath + string.Format(@"{0}\{1}\{2}\{3}C.xml", _strApNo.Substring(0, 4), _strApNo.Substring(4, 5), _strApNo.Substring(9, 3), _strApNo);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                strFilePath = "";
            }

            return strFilePath;
        }

        #region lidonglei
        /// <summary>
        /// 得到指定申请号的权利要求文件的内容
        /// </summary>
        /// <param name="_strApNo">申请号为12位,8位内部转换</param>
        public static string GetClaimContent(string appNo, string LeiXing)
        {
            string transContent = "";
            try
            {
                //TBD: 8位转12位
                appNo = ApNo8To12(appNo);
                //根据申请号获得公告日(APPD)或公开日(PUD)
                string appDate = GetXmlContent(appNo, "APPD");
                if (string.IsNullOrEmpty(appDate))
                    appDate = GetXmlContent(appNo, "PUD");
                if (string.IsNullOrEmpty(appDate))
                    return transContent;
                //获得内容
                transContent = GetNewDataContent(appNo, appDate, LeiXing);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                transContent = "";
            }

            return transContent;
        }

        /// <summary>
        /// 根据申请号和节点标记获得节点内容
        /// </summary>
        /// <param name="appNo"></param>
        /// <param name="mark"></param>
        /// <returns></returns>
        private static string GetXmlContent(string appNo, string mark)
        {
            string xmlPath = getAbsXmlFile(appNo);
            if (!File.Exists(xmlPath))
                return "";
            XmlDocument doc = new XmlDocument();
            string result = string.Empty;
            try
            {
                doc.Load(xmlPath);
                if (doc != null)
                {
                    if (doc.ChildNodes.Count > 1)
                    {
                        XmlNodeList list = doc.ChildNodes[1].SelectNodes(mark);
                        if (list.Count <= 0)
                        {
                            return result;
                        }
                        else
                        {
                            foreach (XmlNode item in list)
                            {
                                result += item.InnerText;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = string.Empty;
            }
            return result;
        }


        /// <summary>
        /// 新加工整理的数据
        /// </summary>
        /// <param name="appNo"></param>
        /// <param name="date"></param>
        /// <param name="LeiXing"></param>
        /// <returns></returns>
        private static string GetNewDataContent(string appNo, string date, string LeiXing)
        {
            date = DateTime.Parse(date).Year + DateTime.Parse(date).Month.ToString().PadLeft(2, '0') + DateTime.Parse(date).Day.ToString().PadLeft(2, '0');
            string translated = string.Empty;
            string tmp = "Claims";
            if (LeiXing == "D")
            {
                tmp = "ShuoMingShu";
            }
            //先从63服务器上找文件，若存在读取内容并返回
            string parentFolder = StrFullXmlNewDataBasePath + "\\" + tmp + "\\";
            string subFolder = appNo.Substring(0, 4) + @"\" + appNo.Substring(4, 5) + @"\" + appNo.Substring(9) + @"\";
            string fullPath = parentFolder + date + @"\" + subFolder;

            fullPath += appNo + LeiXing + ".xml";
            if (File.Exists(fullPath))
            {
                translated = System.IO.File.ReadAllText(fullPath, System.Text.Encoding.GetEncoding("gb2312"));
            }
            else
            {
                translated = "";
                logger.Debug(fullPath + "-----No File");
            }

            return translated;
        }

        #endregion

        /// <summary>
        /// 得到指定申请号的说明书文件的全路径
        /// </summary>
        /// <param name="_strApNo">申请号为12位,8位内部转换</param>
        public static string getDesXmlFile(string _strApNo)
        {
            string strFilePath = "";
            try
            {
                //TBD: 8位转12位
                _strApNo = ApNo8To12(_strApNo);
                strFilePath = strFullXmlBasePath + string.Format(@"{0}\{1}\{2}\{3}D.xml", _strApNo.Substring(0, 4), _strApNo.Substring(4, 5), _strApNo.Substring(9, 3), _strApNo);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                strFilePath = "";
            }

            return strFilePath;
        }

        /// <summary>
        /// 得到指定图形的全路径,申请号为8位或12位
        /// </summary>
        /// <param name="_strApNo">申请号.图形编号</param>
        /// <param name="_strVol">卷期号</param>
        public static string getImgDocFile(string _strApNo, string _strVol)
        {
            string strFilePath = "";
            try
            {
                _strApNo = _strApNo.Trim();

                //TBD: 8位+.D

                //8910\img\85100\85100108.1
                //strFilePath = strImgDocBasePath + string.Format(@"{0}\img\{1}\{2}",_strVol, _strApNo.Substring(0, 5), _strApNo);
                if (_strApNo.IndexOf('.') == 8)
                {
                    strFilePath = strImgDocBasePath + string.Format(@"{0}\img\{1}\{2}", _strVol, _strApNo.Substring(0, 5), _strApNo);
                }

                //TDB:12位+.D
                if (_strApNo.IndexOf('.') == 12)
                {
                    strFilePath = strImgDocBasePath + string.Format(@"{0}\img\{1}\{2}\{3}", _strVol, _strApNo.Substring(0, 4), _strApNo.Substring(4, 5), _strApNo);
                }


                if (!File.Exists(strFilePath))
                {
                    strFilePath = strFilePath.Replace("imgdoc", "imgdoc1");
                    if (!File.Exists(strFilePath))
                    {
                        strFilePath = strFilePath.Replace("imgdoc1", "imgdoc2");
                    }
                }

                //test
                //strFilePath = strImgDocBasePath + @"8910\img\85100\85100108.1";
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                strFilePath = "";
            }

            return strFilePath;
        }

        /// <summary>
        /// 得到指定附图的全路径
        /// </summary>
        /// <param name="_strApNo">申请号为8位或12位</param>
        public static string getFuTuFile(string _strApNo)
        {
            string strFilePath = "";
            bool isL8To12 = false;
            try
            {
                _strApNo = _strApNo.Trim();

                //TBD: 8位 00103\00103001.dat
                if (_strApNo.Length == 8)
                {
                    strFilePath = strFuTuBasePath + string.Format(@"{0}\{1}.dat", _strApNo.Substring(0, 5), _strApNo);

                    if (File.Exists(strFilePath))
                    {
                        return strFilePath;
                    }
                    else
                    {
                        _strApNo = ApNo8To12(_strApNo);
                        isL8To12 = true;
                    }
                }

                //TDB:12位 2008\10000\200810000001.dat 
                if (_strApNo.Length == 12)
                {
                    strFilePath = strFuTuBasePath + string.Format(@"{0}\{1}\{2}.dat", _strApNo.Substring(0, 4), _strApNo.Substring(4, 5), _strApNo);

                    //如果原始号为12位且文件不存在时，转为8位路径
                    if ((!File.Exists(strFilePath)) && !isL8To12)
                    {
                        strFilePath = strFuTuBasePath + string.Format(@"{0}{1}\{0}{2}.dat", _strApNo.Substring(2, 3), _strApNo.Substring(7, 2), _strApNo.Substring(7, 5));
                    }
                }

                //test
                //strFilePath = strFuTuBasePath + @"00103\00103001.dat";
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                strFilePath = "";
            }

            return strFilePath;
        }

        /// <summary>
        /// 得到指定附图的全路径(GIF格式)
        /// </summary>
        /// <param name="_strApNo">申请号为8位或12位</param>
        /// <returns></returns>
        public static string getFuTuGifFile(string _strApNo)
        {
            string strFilePath = "";
            try
            {
                _strApNo = _strApNo.Trim();

                switch (_strApNo.Length)
                {
                    ////TBD: 8位 00103\00103001.dat
                    case 8:
                        strFilePath = StrFuTuGifBasePath + string.Format(@"{0}\{1}.gif", _strApNo.Substring(0, 5), _strApNo);
                        break;
                    //TDB:12位 2008\10000\200810000001.dat 
                    case 12:
                        strFilePath = StrFuTuGifBasePath + string.Format(@"{0}\{1}\{2}.gif", _strApNo.Substring(0, 4), _strApNo.Substring(4, 5), _strApNo);
                        break;

                }
                //test
                //strFilePath = strFuTuBasePath + @"00103\00103001.dat";
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                strFilePath = "";
            }

            return strFilePath;
        }


        /// <summary>
        /// 得取PDF文件路径
        /// </summary>
        /// <param name="_strApNo">申请号</param>
        /// <returns>文件路径</returns>
        public static string[] getImgPdfFile(string _strApNo)
        {
            string[] resultFiel = new string[0] { };
            string strFilePath = "";
            try
            {
                _strApNo = _strApNo.Trim();

                strFilePath = getImgPdfFilePath2(_strApNo);

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
        /// <param name="_strApNo">申请号</param>
        /// <returns>根路径\5\4\</returns>
        public static string getImgPdfFilePath(string _strApNo)
        {
            string strFilePath = "";
            try
            {
                _strApNo = _strApNo.Trim();

                //8910\img\85100\85100108.1
                //strFilePath = strImgDocBasePath + string.Format(@"{0}\img\{1}\{2}",_strVol, _strApNo.Substring(0, 5), _strApNo);
                if (_strApNo.Length == 8)
                {
                    strFilePath = strImgDocBasePath + string.Format(@"{0}\{1}", _strApNo.Substring(0, 5), _strApNo.Substring(5));
                }

                //TDB:12位+.D
                if (_strApNo.Length == 12)
                {
                    strFilePath = strImgDocBasePath + string.Format(@"{0}\{1}\", _strApNo.Substring(0, 4), _strApNo.Substring(4, 5));
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());

            }
            return strFilePath;
        }

        /// <summary>
        /// 得取PDF文件根路径
        /// </summary>
        /// <param name="_strApNo">申请号</param>
        /// <returns>根路径\\5\4\3\</returns>
        public static string getImgPdfFilePath2(string _strApNo)
        {
            string strFilePath = "";
            try
            {

                //TBD: 8位转12位
                _strApNo = ApNo8To12(_strApNo);
                strFilePath = strFullXmlBasePath + string.Format(@"{0}\{1}\{2}\", _strApNo.Substring(0, 4), _strApNo.Substring(4, 5), _strApNo.Substring(9, 3));
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());

            }
            return strFilePath;
        }


        /// <summary>
        /// 申请号8位转12位
        /// </summary>
        /// <param name="_strApNo">申请号为8位或12位</param>
        /// <returns></returns>
        public static string ApNo8To12(string _strApNo)
        {
            string strRetu = "";
            try
            {
                strRetu = _strApNo.Trim();
                if (strRetu.Length < 8)
                    strRetu = strRetu.PadLeft(8, '0');
                if (strRetu.Length == 8)
                {
                    if (strRetu.Substring(0, 1) == "0")
                    {
                        strRetu = string.Format("20{0}00{1}", strRetu.Substring(0, 3), _strApNo.Substring(3, 5));
                    }
                    else
                    {
                        strRetu = string.Format("19{0}00{1}", strRetu.Substring(0, 3), _strApNo.Substring(3, 5));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return strRetu;
        }

        /// <summary>
        /// 软换epo中文申请号为原始申请号
        /// </summary>
        /// <param name="_strApNo"></param>
        /// <returns></returns>
        public static string FormatEpoToCNApNo(string _strApNo)
        {
            string strRetu = _strApNo;
            try
            {
                strRetu = System.Text.RegularExpressions.Regex.Replace(strRetu.ToUpper(), "[A-Z]", "");
                if (int.Parse(strRetu.Substring(0, 4)) < 2004)
                {
                    strRetu = strRetu.Substring(2).Remove(3, 1);
                    if (strRetu.Length == 9)
                    {
                        strRetu = strRetu.Remove(3, 1);
                    }
                }
                else
                {
                    strRetu = strRetu.Insert(5, "0");
                    if (strRetu.Length == 11)
                    {
                        strRetu = strRetu.Insert(5, "0");
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return strRetu;
        }


        /// <summary>
        /// 申请号转11位位欧局格式
        /// </summary>
        /// <param name="_strApNo">申请号为8位或12位</param>
        /// <returns></returns>
        public static string FormatApNoTo11(string _strApNo)
        {
            string strRetu = "";
            try
            {
                strRetu = ApNo8To12(_strApNo).Remove(5, 1);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return strRetu;
        }


        /// <summary>
        /// 申请号转11位欧局格式
        /// </summary>
        /// <param name="_strApNo">申请号为8位或12位</param>
        /// <returns></returns>
        public static string FormatApNoTo112(string _strApNo)
        {
            string strRetu = "";
            try
            {
                strRetu = strRetu = ApNo8To12Epo(_strApNo).Remove(5, 1);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return strRetu;
        }

        /// <summary>
        /// 申请号8位转12位
        /// </summary>
        /// <param name="_strApNo">申请号为8位或12位</param>
        /// <returns></returns>
        public static string ApNo8To12Epo(string _strApNo)
        {
            string strRetu = "";
            try
            {
                strRetu = _strApNo.Trim();
                if (strRetu.Length == 8)
                {
                    if (strRetu.Substring(0, 1) == "0")
                    {
                        strRetu = string.Format("20{0}00{1}", _strApNo.Substring(0, 2), _strApNo.Substring(2, 6));
                    }
                    else
                    {
                        strRetu = string.Format("19{0}00{1}", _strApNo.Substring(0, 2), _strApNo.Substring(2, 6));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return strRetu;
        }
        #endregion

    }
}
