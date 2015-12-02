using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;
using System.ComponentModel;
using System.Web.UI;
using log4net;
using System.Reflection;


namespace Cpic.Cprs2010.User
{
    /// <summary>
    /// 网站请求过滤
    /// 1.用户登陆以及权限的判断
    /// </summary>
    public static class RequestFilter
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static string WebSiteRootPaht = HttpContext.Current.Server.MapPath("~/");
        private static List<string> _CheckLogInList = new List<string>();
        private static List<string> _CheckRightList = new List<string>();
        // private static Dictionary<string, string> QueryHas = new Dictionary<string, string>();

        /// <summary>
        /// 验证登陆页面列表
        /// </summary>
        public static List<string> CheckLogInList
        {
            get { return _CheckLogInList; }
        }
        public static List<string> CheckRightList
        {
            get { return _CheckRightList; }
        }


        /// <summary>
        /// 得到需要验证登陆验证权限的列表
        /// </summary>
        public static void IniCheckList()
        {
            _CheckLogInList.Clear();
            _CheckRightList.Clear();

            foreach (DataRow dr in Right.AllPagesConfig.Rows)
            {
                if (Convert.ToBoolean(dr["是否验证登陆"].ToString()) == true)
                {
                    int index;
                    string FileName;
                    //string QueryString;

                    index = dr["页面路径"].ToString().IndexOf("?");
                    if (index > 0)
                    {
                        FileName = dr["页面路径"].ToString().Substring(0, index).ToUpper();
                        if (!_CheckLogInList.Contains(FileName))
                        {
                            _CheckLogInList.Add(FileName);
                        }

                    }
                    if (!_CheckLogInList.Contains(dr["页面路径"].ToString().ToUpper()))
                    {
                        _CheckLogInList.Add(dr["页面路径"].ToString().ToUpper());
                    }
                }
                if (Convert.ToBoolean(dr["是否验证权限"].ToString()) == true)
                {
                    int index;
                    string FileName;
                    index = dr["页面路径"].ToString().IndexOf("?");
                    if (index > 0)
                    {
                        FileName = dr["页面路径"].ToString().Substring(0, index).ToUpper();
                        if (!_CheckRightList.Contains(FileName))
                        {
                            _CheckRightList.Add(FileName);
                        }

                    }
                    if (!_CheckRightList.Contains(dr["页面路径"].ToString().ToUpper()))
                    {
                        _CheckRightList.Add(dr["页面路径"].ToString().ToUpper());
                    }

                }
            }
        }
        /// <summary>
        /// 过滤每次请求
        /// </summary>
        public static void Filter()
        {

            //得到请求的路径 
            string ReqPath = HttpContext.Current.Request.PhysicalPath;


            //得到请求页面类型      
            string FileExtension = System.IO.Path.GetExtension(ReqPath);
            //如果请求的是aspx 页面
            if (FileExtension.ToUpper() != ".ASPX")
            {
                //不去做任何判断走了
                return;
            }

            //
            string ReqFile = ReqPath.Replace(WebSiteRootPaht, "~/").Replace("\\", "/").ToUpper();

            string ReqString = HttpContext.Current.Request.RawUrl.ToUpper().Replace(HttpContext.Current.Request.ApplicationPath.ToUpper() + "/", "~/");
            if (!ReqString.StartsWith("~"))
            {
                ReqString = "~" + ReqString;
            }

            ReqString = ReqFile;
            //判断请求的页面 是否需要验证登陆
            if (_CheckLogInList.Contains(ReqString))
            {
                CheckLogIn();
                LoginManager.CheckUID_SessionID();
            }

            ///判断请求的页面是否需要验证权限
            if (_CheckRightList.Contains(ReqString))
            {
                CheckUserRight(ReqString);
            }

            //判断也是是否需要统计
            if (Stat.StatPageList.ContainsKey(ReqFile))
            {
                Stat.AddRequest(ReqFile);
            }
        }
        
        /// <summary>
        /// PAd访问检测
        /// </summary>
        /// <returns></returns>
        public static bool CheckPadRequest()
        {
            bool isRequest = false;
            try
            {
                Dictionary<string, string> clientInfos = new Dictionary<string, string>();

                string userAgent = HttpContext.Current.Request.UserAgent;

                if (userAgent != null)
                {
                    if (userAgent.ToLower().IndexOf("windows nt ") > -1)
                    {
                        if (userAgent.ToLower().IndexOf("tablet pc ") > -1)
                        {
                            clientInfos.Add("计算机/手机", "Windows 系统PAD"); //Windows 系统PAD 不能访问
                        }
                        else
                        {
                            clientInfos.Add("计算机/手机", "计算机"); //可以访问
                            isRequest = true;
                        }
                    }
                    else if (userAgent.ToLower().IndexOf("windows moble ") > -1)
                    {
                        clientInfos.Add("计算机/手机", "Windows 系统手机"); //Windows 系统手机 不能访问
                    }
                    else if (userAgent.ToLower().IndexOf("iphonet") > -1)
                    {
                        clientInfos.Add("计算机/手机", "iphon 系统手机"); //iphon 系统手机 不能访问
                    }
                    else if (userAgent.IndexOf("GPAD-N2100(1009web)") > -1)
                    {
                        clientInfos.Add("计算机/手机", "长城 Android PAD 测试");  //长城 Android PAD 测试 可以访问 pad版CPRS
                        isRequest = true;
                    }
                    else
                    {
                        clientInfos.Add("计算机/手机", "手机或其它终端"); // 不能访问
                    }
                }

            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message);
                isRequest = true;
            }
            return isRequest;
        }

        /// <summary>
        /// PAd访问检测
        /// </summary>
        /// <returns></returns>
        public static bool IsPadRequest()
        {
            bool isPad = false;
            try
            {
                Dictionary<string, string> clientInfos = new Dictionary<string, string>();

                string userAgent = HttpContext.Current.Request.UserAgent;

                if (userAgent != null)
                {
                    if (userAgent.ToLower().IndexOf("windows nt ") > -1)
                    {
                        if (userAgent.ToLower().IndexOf("tablet pc ") > -1)
                        {
                            clientInfos.Add("计算机/手机", "Windows 系统PAD"); //Windows 系统PAD 不能访问
                            isPad = true;
                        }
                        else
                        {
                            clientInfos.Add("计算机/手机", "计算机"); //可以访问                            
                        }
                    }
                    else if (userAgent.ToLower().IndexOf("windows moble ") > -1)
                    {
                        clientInfos.Add("计算机/手机", "Windows 系统手机"); //Windows 系统手机 不能访问
                        isPad = true;
                    }
                    else if (userAgent.ToLower().IndexOf("iphonet") > -1)
                    {
                        clientInfos.Add("计算机/手机", "iphon 系统手机"); //iphon 系统手机 不能访问
                        isPad = true;
                    }
                    else if (userAgent.IndexOf("GPAD-N2100(1009web)") > -1)
                    {
                        clientInfos.Add("计算机/手机", "长城 Android PAD 测试");  //长城 Android PAD 测试 可以访问 pad版CPRS
                        isPad = true;

                    }
                    else
                    {
                        clientInfos.Add("计算机/手机", "手机或其它终端"); // 不能访问
                    }
                }

            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message);               
            }
            return isPad;
        }

        /// <summary>
        /// 验证用户是否登陆
        /// </summary>
        public static void CheckLogIn()
        {
            //连Session都没有== 会话还没建立 直接跑到登陆页 
            if (HttpContext.Current.Session == null)
            {
                HttpContext.Current.Response.Redirect(HttpRuntime.AppDomainAppVirtualPath + "/SessionTimeOut.aspx?RequestUrl=" + HttpContext.Current.Server.UrlEncode(HttpContext.Current.Request.Url.ToString()));
            }
            if (HttpContext.Current.Session["UserName"] == null)
            {
                HttpContext.Current.Response.Redirect(HttpRuntime.AppDomainAppVirtualPath + "/SessionTimeOut.aspx?RequestUrl=" + HttpContext.Current.Server.UrlEncode(HttpContext.Current.Request.Url.ToString()));
            }

        }
        /// <summary>
        /// 验证用户权限
        /// </summary>
        private static void CheckUserRight(string PageName)
        {
            CheckLogIn();
            if (!UserManager.LogInCheck(PageName))
            {
                HttpContext.Current.Response.Redirect("~/RightNotEnought.aspx");
            }
        }


        /// <summary>
        /// 电信网通双线路转换
        /// </summary>
        public static void IpTelUnicConvert()
        {
            string strDns = HttpContext.Current.Request.Url.DnsSafeHost.ToLower();

            //非域名访问不检测
            if (!(strDns.EndsWith(".com") || strDns.EndsWith(".cn")))
            {
                return;
            }

            try
            {
                //IP地址
                string strIp = HttpContext.Current.Request.UserHostAddress.ToString();
                string strIpType = Stat.GetLocal(strIp);    //IP所在地及类型                                
                string strUrl = HttpContext.Current.Request.Url.ToString();

                logger.DebugFormat("IP:{0}[{1}]->[{2}]", strIp, strIpType,strUrl);

                //生产环境下
                //电信线路:  tel.域名.xxx,   二级域名为xxxtel.域名.xxx   
                //网通线络：域名.xxx,  二级域名为 xxx.域名.xxxx
                if (strIpType.Contains("电信") && !strDns.Contains("tel."))
                {
                    //转向电信线路
                    strUrl = ConvertUrl_TelUnic(HttpContext.Current.Request.Url.DnsSafeHost, strUrl, "tel");
                    HttpContext.Current.Response.Redirect(strUrl);
                }
                else if (!strIpType.Contains("电信") && strDns.Contains("tel."))
                {
                    //转向网通线路                 
                    strUrl = ConvertUrl_TelUnic(HttpContext.Current.Request.Url.DnsSafeHost, strUrl, "unic");
                    HttpContext.Current.Response.Redirect(strUrl);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_strDns"></param>
        /// <param name="strUrl"></param>
        /// <param name="_strGetUrlType"></param>
        /// <returns></returns>
        private static string ConvertUrl_TelUnic(string _strDns, string strUrl, string _strGetUrlType)
        {
            string strNewDns = "";
            string strNewUrl = strUrl;
            try
            {
                switch (_strGetUrlType)
                {
                    case "tel":
                        //转向电信线路
                        strNewDns = "searchtel.patentstar.com.cn";
                        strNewUrl = strUrl.Replace(_strDns, strNewDns);
                        break;
                    case "unic":
                        //转向网通线路
                        strNewDns = "search.patentstar.com.cn";
                        strNewUrl = strUrl.Replace(_strDns, strNewDns);
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return strNewUrl;
        }
    }


}

