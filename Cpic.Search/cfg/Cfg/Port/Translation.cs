#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2010 @ CPIC Corp
*	CLR 版本: 2.0.50727.3603
*	文 件 名: Translation.cs
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
using log4net;
using log4net.Core;
using System.Reflection;
using Cpic.Cprs2010.Cfg.TranslationC2EService;
using Cpic.Cprs2010.Cfg.TranslationE2CService;
using System.Text.RegularExpressions;
using System.Net;
using System.Web;

namespace Cpic.Cprs2010.Cfg.Port
{
   
    
    /// <summary>
    /// 英汉互译
    /// </summary>
    /// <remarks>英汉互译</remarks>
    public class Translation
    {
        /// <summary>
        /// Singleton instance
        /// </summary>
        private static Translation ins = new Translation();

        /// <summary>
        /// 私有构造
        /// </summary>
        private Translation() { }

        /// <summary>
        /// 取得类实例
        /// </summary>
        /// <returns>单例实例</returns>
        public static Translation getInstance()
        {
            return ins;
        }

        /// <summary>
        /// Logger
        /// </summary>
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 常量
        /// </summary>
        private static int INDEX_OF_RESSTR_START = 4;

        /// <summary>
        /// 常量
        /// </summary>
        private static int LENGTH_OF_FILTER_STR = 9;

        ///// <summary>
        ///// 英译汉网址
        ///// </summary>
        //private string _strEnToCnUrl;

        ///// <summary>
        ///// 汉译英网址
        ///// </summary>
        //private string _strCnToEnUrl;


        #region CPRS翻译引擎        
       
        /// <summary>
        /// 英译汉
        /// </summary>
        /// <param name="_strText">英文文本</param>
        public string EnToCn(string _strText)
        {
            logger.Debug("------ CnToEn In Msg ([para1] _strText: " + _strText + ")------");
            string resStr = null;

            TranslationE2CService.ServiceSoapClient client = new TranslationE2CService.ServiceSoapClient();
            resStr = client.TranslateETC(_strText, 0);

            if (!resStr.Equals(""))
            {
                if (resStr.Substring(1, 5).Equals("error"))
                {
                    logger.Error("Get resStr Error:" + resStr);
                    logger.Error("Error Input: " + _strText);
                    logger.Debug("------ EnToCn Out Msg (return value: null)------");
                    return null;
                }
                logger.Debug("Get resStr Success: " + resStr);
                resStr = resStr.Substring(INDEX_OF_RESSTR_START, resStr.Length - LENGTH_OF_FILTER_STR);
            }

            logger.Debug("------ EnToCn Out Msg (return value: " + resStr + ")------");
            return resStr;
        }

        /// <summary>
        /// 汉译英
        /// </summary>
        /// <param name="_strText">中文文本</param>
        public string CnToEn(string _strText)
        {
            logger.Debug("------ CnToEn In Msg ([para1] _strText: " + _strText + ")------");
            string resStr = null;

            TranslationC2EService.ServiceSoapClient client = new TranslationC2EService.ServiceSoapClient();

            do
            {
                resStr = client.WebServiceC2E(_strText);
                if (resStr.Substring(1, 5).Equals("error"))
                {
                    resStr = "";
                }
            } while (resStr.Equals(""));
            //if (!resStr.Equals(""))
            //{
            //    if (resStr.Substring(1, 5).Equals("error"))
            //    {
            //        logger.Error("Get resStr Error:" + resStr);
            //        logger.Error("Error Input: " + _strText);
            //        logger.Debug("------ EnToCn Out Msg (return value: null)------");
            //        return null;
            //    }
            logger.Debug("Get resStr Success: " + resStr);
            resStr = resStr.Substring(INDEX_OF_RESSTR_START, resStr.Length - LENGTH_OF_FILTER_STR);
            //}

            logger.Debug("------ CnToEn Out Msg ([return] value: " + resStr + ")------");
            return resStr;
        }


        /// <summary>
        /// 汉译英
        /// </summary>
        /// <param name="_strText">中文文本</param>
        public string CnToEnSplit(string _strText)
        {
            logger.Debug("------ CnToEn In Msg ([para1] _strText: " + _strText + ")------");
            string resStr = "";

            #region 分段分句
            try
            {
                foreach (string strItemP in _strText.Split("\r\n".ToArray(), StringSplitOptions.RemoveEmptyEntries))
                {
                    string strTransP = "";
                    if (strItemP.Length > 280)
                    {
                        foreach (string strItem in strItemP.Split("。.".ToArray(), StringSplitOptions.RemoveEmptyEntries))
                        {
                            string strTrnStr = "";
                            try
                            {
                                strTrnStr = CnToEn(strItem);                              
                            }
                            catch (Exception ex)
                            {
                                strTrnStr = strItem;
                            }

                            strTransP += strTrnStr + ".";
                            logger.DebugFormat("{0}->{1}", strItem, strTrnStr);
                        }
                    }
                    else
                    {
                        try
                        {
                            strTransP = CnToEn(strItemP);
                        }
                        catch (Exception ex)
                        {
                            strTransP = strItemP;
                        }
                    }
                    resStr += strTransP+"\n";
                }

                resStr = resStr.TrimEnd('\n');
            }
            catch (Exception ex)
            {
                resStr = _strText;
            }
            #endregion

            return resStr;
        }

        #endregion



        #region Google翻译

        private static string AutoDetectLanguage = "auto"; //google自动判断来源语系
         private static string UrlTemplate = "http://translate.google.com.hk/"; 
        /// <summary>
        /// 翻译文本[自动检测源语言类型]
        /// ZhangQingFeng    2012-7-27    add
        /// </summary>
        /// <param name="sourceText">源文本</param>
        /// <param name="targetLanguageCode">目标语言类型代码，如：en、zh-CN、zh-TW、ru等</param>
        /// <returns>翻译结果</returns>
        public  string Translate(string sourceText, string targetLanguageCode)
        {
            return Translate(sourceText, AutoDetectLanguage, targetLanguageCode);
        }



        /// <summary>
        /// 翻译文本
        /// ZhangQingFeng    2012-7-27    add
        /// </summary>
        /// <param name="sourceText">源文本</param>
        /// <param name="sourceLanguageCode">源语言类型代码，如：en、zh-CN、zh-TW、ru等</param>
        /// <param name="targetLanguageCode">目标语言类型代码，如：en、zh-CN、zh-TW、ru等</param>
        /// <returns>翻译结果</returns>
        public static string Translate(string sourceText, string sourceLanguageCode, string targetLanguageCode)
        {
            if (string.IsNullOrEmpty(sourceText) || Regex.IsMatch(sourceText, @"^\s*$"))
            {
                return sourceText;
            }

            string strReturn = string.Empty;

            #region POST方式实现，无长度限制
            string url = UrlTemplate;

            //组织请求的数据
            string postData = string.Format("langpair={0}&text={1}", HttpUtility.UrlEncode(sourceLanguageCode + "|" + targetLanguageCode), HttpUtility.UrlEncode(sourceText));
            byte[] bytes = Encoding.UTF8.GetBytes(postData);

            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            client.Headers.Add("ContentLength", postData.Length.ToString());
            byte[] responseData = client.UploadData(url, "POST", bytes);
            string strResult = Encoding.UTF8.GetString(responseData);    //响应结果 
            #endregion

            #region GET方式实现，有长度限制
            //string url = string.Format(UrlTemplate, HttpUtility.UrlEncode(sourceLanguageCode + "|" + targetLanguageCode), HttpUtility.UrlEncode(sourceText));
            //WebClient wc = new WebClient();
            //wc.Encoding = Encoding.UTF8;
            //string strResult = wc.DownloadString(url);                //响应结果            
            #endregion

            //使用的正则表达式：    \s+id="?result_box"?\s+[^>]*>(.+)</span>\s*</div>\s*</div>\s*<div id=spell-place-holder\s+
            // string strReg = @"\s+id=""?result_box""?\s+[^>]*>(.+)</span>\s*</div>\s*</div>\s*<div id=spell-place-holder\s+";
            int index = strResult.IndexOf("TRANSLATED_TEXT='");

            int indexend = strResult.IndexOf("';INPUT_TOOL_PATH='");
            string result = string.Empty;
            if (index > 0)
            {

                result = strResult.Substring(index + 17, indexend - index - 17);
            }
            else
            {
                throw new Exception("翻译错误");
            }

            #region 原注释
            
            
            //Match match = Regex.Match(strResult, strReg, RegexOptions.IgnoreCase | RegexOptions.Singleline);

            //if (match.Success)
            //{
            //    strReturn = match.Groups[1].Value;
            //    //<br/>替换为换行，如为HTML翻译选项则可去除下行代码
            //    strReturn = Regex.Replace(strReturn, @"<br\s*/?>", "\n", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            //    strReturn = Regex.Replace(strReturn, @"<[^>]*>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            //    strReturn = HttpUtility.HtmlDecode(strReturn);

            //}

            #endregion
            return result;
        }

   
    }
        #endregion
}
