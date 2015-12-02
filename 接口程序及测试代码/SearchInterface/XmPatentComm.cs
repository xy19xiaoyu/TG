using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;

namespace SearchInterface
{
    public class XmPatentComm
    {
        public static string strUrlDome = string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["CPRSDomeURL"]) ?
                                            "http://202.106.92.181/cprs2010/" : System.Configuration.ConfigurationManager.AppSettings["CPRSDomeURL"];

        public static Dictionary<string, string> strDicTJSeverURL = new Dictionary<string, string>();
        public static Dictionary<string, string> strDicLegUrl = new Dictionary<string, string>();

        public static string strWebSearchGroupName = "";

        static XmPatentComm()
        {
            try
            {
                string strURLCfg = string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["TJServerURL"]) ?
                                           "192.168.1.1>192.168.1.7:8080" : System.Configuration.ConfigurationManager.AppSettings["TJServerURL"]; ;
                foreach (string strKeys in strURLCfg.Split("|".ToArray(), StringSplitOptions.RemoveEmptyEntries))
                {
                    string[] strArry = strKeys.Split('>');
                    foreach (string strKey in strArry[0].Split(";".ToArray(), StringSplitOptions.RemoveEmptyEntries))
                    {
                        strDicTJSeverURL.Add(strKey.ToUpper(), strArry[1].Trim());
                    }
                }
            }
            catch (Exception ex)
            {
            }
            InitLegUrlCfg();
        }

        public static void InitLegUrlCfg()
        {
            try
            {

                DataTable dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, "select CO,LegUrl from tblegalUrl_cfg order by ID desc");
                strDicLegUrl.Clear();
                foreach (DataRow rowItem in dt.Rows)
                {
                    if (!strDicLegUrl.ContainsKey(rowItem["CO"].ToString().Trim().ToUpper()))
                    {
                        strDicLegUrl.Add(rowItem["CO"].ToString().Trim().ToUpper(), rowItem["LegUrl"].ToString().Trim());
                    }
                }
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        /// 外观分类号还原,加-[dd-dd-sssssss]
        /// </summary>
        /// <param name="strDegIpc"></param>
        /// <returns></returns>
        public static string RevertDegIpc(string strDegIpc)
        {
            string strRs = strDegIpc;
            try
            {
                if (!strDegIpc.Contains('-'))
                {
                    if (strDegIpc.Length > 4)
                    {
                        strRs = strDegIpc.Insert(4, "-").Insert(2, "-");
                    }
                    else if (strDegIpc.Length > 2)
                    {
                        strRs = strDegIpc.Insert(2, "-");
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return strRs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_strDt"></param>
        /// <returns></returns>
        public static string FormatDateVlue(string _strDt)
        {
            string strRs = "";
            try
            {
                _strDt = _strDt.Trim();
                if (_strDt == "" || _strDt.StartsWith("000"))
                {
                    _strDt = "";
                }
                else
                {
                    if (_strDt.IndexOf("]") > 0)
                    {
                        _strDt = _strDt.Substring(_strDt.IndexOf("]") + 1);
                    }
                    if (_strDt.Trim().Length == 8)
                    {
                        strRs = string.Format("{0}.{1}.{2}", _strDt.Substring(0, 4), _strDt.Substring(4, 2), _strDt.Substring(6, 2));
                    }
                    else
                    {
                        strRs = Convert.ToDateTime(_strDt).ToString("yyyy.MM.dd"); //"yyyy年MM月dd日
                    }
                }
            }
            catch (Exception ex)
            {
                strRs = _strDt;
            }
            return strRs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strTxt"></param>
        /// <param name="strEnter"></param>
        /// <param name="strDbType"></param>
        /// <returns></returns>
        public static string getSplitString(string strTxt, string strEnter, string strDbType)
        {
            string strRs = "";
            try
            {
                string strFormatUrl = string.Format("<a href='frmDoSq.aspx?db={0}&Query=F XX ({2}/{1})' target='_blank'>{3}</a>; ",
                                                    strDbType, strEnter, "{0}", "{1}");
                string strUrlItem = "";
                foreach (string strItem in strTxt.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                {
                    strUrlItem = strDbType == "CN" && strEnter == "IC" ? Util.FormatUtil.FormatIPC(strItem) : strItem;
                    strRs += string.Format(strFormatUrl, System.Web.HttpContext.Current.Server.UrlEncode(strUrlItem).Replace("+", "%20"), strItem);
                }

                strRs = strRs.TrimEnd("; ".ToCharArray());
            }
            catch (Exception ex)
            {
                strRs = strTxt;
            }
            return strRs;
        }

        public static string Format_LegalStatusDetailInfo(string _strDetailInfo)
        {
            string strRs = _strDetailInfo;
            try
            {
                strRs = strRs.Replace("|", "<BR>");
                Regex reg = new Regex(@"无效宣告决定号：(\d+)<BR>");
                //strRs = reg.Replace(strRs, "无效宣告决定号：<a href='http://app.sipo-reexam.gov.cn/reexam_out/searchdoc/decidedetail.jsp?jdh=$1&lx=wx' target='_blank'>$1</a><BR>");
                strRs = reg.Replace(strRs, "无效宣告决定号：<a href='decidedetail.html?jdh=$1' target='_blank'>$1</a><BR>");
            }
            catch (Exception ex)
            {

            }
            return strRs;
        }

        public static string getLegalWebUrl(string strPubId)
        {
            //"以下信息仅供参考，如需更多信息，请前往[专利受理局官网]查询。" +strDicLegUrl
            string strRs = "以下信息仅供参考，如需更多信息，请前往[专利受理局官网]查询。<BR/>";
            try
            {
                string strCC = strPubId.Substring(0, 2).ToUpper();
                if (strDicLegUrl.ContainsKey(strCC))
                {
                    strRs = string.Format("以下信息仅供参考，如需更多信息，请前往[<a href='{0}' style='text-decoration:underline;' target='_blank'><b>专利受理局官网</b></a>]查询。<BR/>", strDicLegUrl[strCC]);
                }
            }
            catch (Exception ex)
            {
            }
            return strRs;
        }
    }
}
