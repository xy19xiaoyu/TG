#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2011 @ CPIC Corp
*	CLR 版本: 2.0.50727.3615
*	文 件 名: cnXmlResultData.cs
*	创 建 人: xiwenlei(xiwl) $ chenxiaoyu(xy)
*	创建日期: 2011-9-16 9:20:10
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
using System.Collections;
using Cpic.Cprs2010.Search;
using System.Data.Linq;
using log4net;
using System.Reflection;
using Cpic.Cprs2010.Cfg;
using ParseXml;
using System.IO;
using Cpic.Cprs2010.Cfg.Data;
using System.Xml;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using Util;


/// <summary>
/// 为CPRS2010组件，提供对检索、检索历史及用户的相关操作类
/// </summary>
namespace Cpic.Cprs2010.Search.ResultData
{
    /// <summary>
    ///cnXmlResultData 的摘要说明
    /// </summary>
    public class cnXmlResultData
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public cnXmlResultData()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }



        /// <summary>
        /// 得到某一结果文件的某一页数据 并按某一字段排序
        /// </summary>
        /// <param name="sp">检索式</param>
        /// <param name="PageIndex">要取的页数</param>
        /// <param name="PageSize">页数大小</param>
        /// <param name="SortExpression">排序字段</param>
        /// <returns>list(int)</int></returns>
        public IEnumerable GetResultList(SearchPattern sp, int PageSize, int PageIndex, string SortExpression)
        {
            try
            {

                ResultServices res = new ResultServices();
                List<int> lstNo = res.GetResultListByEnd(sp, PageSize, PageIndex, SortExpression);
                IEnumerable ien = GetResult(lstNo);
                return ien;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 得到给定号单的结果数据
        /// </summary>
        /// <param name="_lstNo"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SortExpression"></param>
        /// <returns></returns>
        public IEnumerable GetResultList(List<int> _lstNo, int PageSize, int PageIndex, string SortExpression)
        {
            try
            {
                List<int> lstNo = (_lstNo.Skip((PageIndex - 1) * PageSize).Take(PageSize)).ToList();

                return GetResult(lstNo);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstNo"></param>
        /// <returns></returns>
        private IEnumerable GetResult(List<int> lstNo)
        {
            try
            {
                ResultDataManagerDataContext db = new ResultDataManagerDataContext();
                Table<CnGeneral_Info> tbCnDocInfo = db.CnGeneral_Info;

                var result = from item in tbCnDocInfo
                             where lstNo.Contains(Convert.ToInt32(item.SerialNo))
                             orderby item.SerialNo descending
                             select new xmlDataInfo
                             {
                                 StrTitle = item.Title,
                                 StrApNo = item.ApNo,
                                 StrApDate = item.ApDate,
                                 StrANX = UrlParameterCode_DE.encrypt(item.ApNo.Trim()),  //add by xiwl
                                 StrIpc = item.Ipc1,
                                 StrSerialNo = item.SerialNo.ToString(),
                                 StrFtUrl = string.Format("http://search.patentstar.com.cn/bns/comm/getimg.aspx?idx={0}&Ty=CNG", UrlParameterCode_DE.encrypt(item.ApNo)),
                                 CPIC = item.SerialNo.ToString(),
                             };

                XmlParser xmlParser = new XmlParser("", "GB2312");
                CnIndexExtract cnIndexExtract = new CnIndexExtract();
                List<xmlDataInfo> lstxml = new List<xmlDataInfo>();
                //循环读取对应的XML补充其它字段数据信息
                foreach (var item in result)
                {
                    item.StrApNo = "198710002005";
                    String xmlpath = cnDataService.getAbsXmlFile(item.StrApNo);
                    // String xmlContent = FileChoose.EncryptString(System.IO.File.ReadAllText(xmlpath, System.Text.Encoding.GetEncoding("gb2312")), FileChoose.key);
                    using (StreamReader xmlreader = new StreamReader(xmlpath, Encoding.GetEncoding("gb2312")))
                    {
                        String xmlContent = xmlreader.ReadToEnd();
                        xmlContent = Util.StringUtil.ReplaceLowOrderASCIICharacters(xmlContent);
                        using (StringReader xmlString = new StringReader(xmlContent))
                        {
                            using (XmlReader reader = XmlReader.Create(xmlString, xmlParser.Settings, xmlParser.Context))
                            {
                                XDocument xRoot = XDocument.Load(reader, LoadOptions.None);
                                item.StrApNo = string.Format("{0}.{1}", item.StrApNo.Trim(), CnAppLicationNo.getValidCode(item.StrApNo)); //add by xiwl;                                
                                item.StrAgency = cnIndexExtract.getAgency(xRoot);
                                item.StrAnnDate = cnIndexExtract.getAnnouncementDate(xRoot);
                                item.StrAnnNo = cnIndexExtract.getAnnouncementNo(xRoot);
                                item.StrApply = cnIndexExtract.getApply(xRoot);
                                item.StrCountryCode = cnIndexExtract.getProvinceCode(xRoot);
                                item.StrFiled = cnIndexExtract.getField(xRoot);
                                item.StrInventor = cnIndexExtract.getInventor(xRoot);
                                item.StrPri = cnIndexExtract.getPriorNo(xRoot);
                                item.StrPubDate = getPubApdDate(cnIndexExtract.getPublicDate(xRoot), cnIndexExtract.getAnnouncementDate(xRoot));
                                item.StrPubNo = getPubApdNo(cnIndexExtract.getPublicNo(xRoot), cnIndexExtract.getAnnouncementNo(xRoot));
                                item.StrAbstr = cnIndexExtract.getAbstract(xRoot).Length >= 140 ? cnIndexExtract.getAbstract(xRoot).Substring(0, 140) : string.IsNullOrEmpty(cnIndexExtract.getAbstract(xRoot)) ? "无" : cnIndexExtract.getAbstract(xRoot);
                                item.StrClaim = cnIndexExtract.getMainClaim(xRoot);
                                item.Brief = GetBriefInfo(item.StrApNo);
                            }
                        }
                    }
                    lstxml.Add(item);
                }

                //paging... (LINQ)
                //IEnumerable ien = result.Skip((PageNumber - 1) * PageSize).Take(PageSize);
                IEnumerable ien = lstxml.DefaultIfEmpty();
                return ien;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }


        private string GetBriefInfo(string appNo)
        {
            string breif = "";
            Regex reg = new Regex("(^.{2}3.{5}$)|(^.{2}3.{5}\\..$)|(^.{4}3.{7}$)|(^.{4}3.{7}\\..$)");
            Match match = reg.Match(appNo);
            breif = match.Success ? "简要说明:" : "摘要:";
            return breif;
        }

        private string getPubApdNo(string _strPubNo, string _strApdNo)
        {
            string strRet = "";
            try
            {
                if ((string.IsNullOrEmpty(_strPubNo) || _strPubNo.StartsWith("000")))
                {
                    strRet = string.Format("[公告]{0}", _strApdNo);
                }
                else if (string.IsNullOrEmpty(_strApdNo) && !_strApdNo.StartsWith("000"))
                {
                    strRet = string.Format("[公开]{0}", _strPubNo);
                }
                else
                {
                    strRet = string.Format("[公告]{0}", _strApdNo);
                }
            }
            catch (Exception ex)
            {
            }
            return strRet;
        }

        private string getPubApdDate(string _strPubDate, string _strApdDate)
        {
            string strRet = "";
            try
            {
                if ((string.IsNullOrEmpty(_strPubDate) || _strPubDate.StartsWith("000")))
                {
                    strRet = string.Format("[公告]{0}", _strApdDate);
                }
                else if (string.IsNullOrEmpty(_strApdDate) && !_strApdDate.StartsWith("000"))
                {
                    strRet = string.Format("[公开]{0}", _strPubDate);
                }
                else
                {
                    strRet = string.Format("[公告]{0}", _strApdDate);
                }
            }
            catch (Exception ex)
            {
            }
            return strRet;
        }
    }
}
