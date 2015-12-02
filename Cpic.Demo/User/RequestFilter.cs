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
    /// ��վ�������
    /// 1.�û���½�Լ�Ȩ�޵��ж�
    /// </summary>
    public static class RequestFilter
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static string WebSiteRootPaht = HttpContext.Current.Server.MapPath("~/");
        private static List<string> _CheckLogInList = new List<string>();
        private static List<string> _CheckRightList = new List<string>();
        // private static Dictionary<string, string> QueryHas = new Dictionary<string, string>();

        /// <summary>
        /// ��֤��½ҳ���б�
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
        /// �õ���Ҫ��֤��½��֤Ȩ�޵��б�
        /// </summary>
        public static void IniCheckList()
        {
            _CheckLogInList.Clear();
            _CheckRightList.Clear();

            foreach (DataRow dr in Right.AllPagesConfig.Rows)
            {
                if (Convert.ToBoolean(dr["�Ƿ���֤��½"].ToString()) == true)
                {
                    int index;
                    string FileName;
                    //string QueryString;

                    index = dr["ҳ��·��"].ToString().IndexOf("?");
                    if (index > 0)
                    {
                        FileName = dr["ҳ��·��"].ToString().Substring(0, index).ToUpper();
                        if (!_CheckLogInList.Contains(FileName))
                        {
                            _CheckLogInList.Add(FileName);
                        }

                    }
                    if (!_CheckLogInList.Contains(dr["ҳ��·��"].ToString().ToUpper()))
                    {
                        _CheckLogInList.Add(dr["ҳ��·��"].ToString().ToUpper());
                    }
                }
                if (Convert.ToBoolean(dr["�Ƿ���֤Ȩ��"].ToString()) == true)
                {
                    int index;
                    string FileName;
                    index = dr["ҳ��·��"].ToString().IndexOf("?");
                    if (index > 0)
                    {
                        FileName = dr["ҳ��·��"].ToString().Substring(0, index).ToUpper();
                        if (!_CheckRightList.Contains(FileName))
                        {
                            _CheckRightList.Add(FileName);
                        }

                    }
                    if (!_CheckRightList.Contains(dr["ҳ��·��"].ToString().ToUpper()))
                    {
                        _CheckRightList.Add(dr["ҳ��·��"].ToString().ToUpper());
                    }

                }
            }
        }
        /// <summary>
        /// ����ÿ������
        /// </summary>
        public static void Filter()
        {

            //�õ������·�� 
            string ReqPath = HttpContext.Current.Request.PhysicalPath;


            //�õ�����ҳ������      
            string FileExtension = System.IO.Path.GetExtension(ReqPath);
            //����������aspx ҳ��
            if (FileExtension.ToUpper() != ".ASPX")
            {
                //��ȥ���κ��ж�����
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
            //�ж������ҳ�� �Ƿ���Ҫ��֤��½
            if (_CheckLogInList.Contains(ReqString))
            {
                CheckLogIn();
                LoginManager.CheckUID_SessionID();
            }

            ///�ж������ҳ���Ƿ���Ҫ��֤Ȩ��
            if (_CheckRightList.Contains(ReqString))
            {
                CheckUserRight(ReqString);
            }

            //�ж�Ҳ���Ƿ���Ҫͳ��
            if (Stat.StatPageList.ContainsKey(ReqFile))
            {
                Stat.AddRequest(ReqFile);
            }
        }
        
        /// <summary>
        /// PAd���ʼ��
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
                            clientInfos.Add("�����/�ֻ�", "Windows ϵͳPAD"); //Windows ϵͳPAD ���ܷ���
                        }
                        else
                        {
                            clientInfos.Add("�����/�ֻ�", "�����"); //���Է���
                            isRequest = true;
                        }
                    }
                    else if (userAgent.ToLower().IndexOf("windows moble ") > -1)
                    {
                        clientInfos.Add("�����/�ֻ�", "Windows ϵͳ�ֻ�"); //Windows ϵͳ�ֻ� ���ܷ���
                    }
                    else if (userAgent.ToLower().IndexOf("iphonet") > -1)
                    {
                        clientInfos.Add("�����/�ֻ�", "iphon ϵͳ�ֻ�"); //iphon ϵͳ�ֻ� ���ܷ���
                    }
                    else if (userAgent.IndexOf("GPAD-N2100(1009web)") > -1)
                    {
                        clientInfos.Add("�����/�ֻ�", "���� Android PAD ����");  //���� Android PAD ���� ���Է��� pad��CPRS
                        isRequest = true;
                    }
                    else
                    {
                        clientInfos.Add("�����/�ֻ�", "�ֻ��������ն�"); // ���ܷ���
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
        /// PAd���ʼ��
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
                            clientInfos.Add("�����/�ֻ�", "Windows ϵͳPAD"); //Windows ϵͳPAD ���ܷ���
                            isPad = true;
                        }
                        else
                        {
                            clientInfos.Add("�����/�ֻ�", "�����"); //���Է���                            
                        }
                    }
                    else if (userAgent.ToLower().IndexOf("windows moble ") > -1)
                    {
                        clientInfos.Add("�����/�ֻ�", "Windows ϵͳ�ֻ�"); //Windows ϵͳ�ֻ� ���ܷ���
                        isPad = true;
                    }
                    else if (userAgent.ToLower().IndexOf("iphonet") > -1)
                    {
                        clientInfos.Add("�����/�ֻ�", "iphon ϵͳ�ֻ�"); //iphon ϵͳ�ֻ� ���ܷ���
                        isPad = true;
                    }
                    else if (userAgent.IndexOf("GPAD-N2100(1009web)") > -1)
                    {
                        clientInfos.Add("�����/�ֻ�", "���� Android PAD ����");  //���� Android PAD ���� ���Է��� pad��CPRS
                        isPad = true;

                    }
                    else
                    {
                        clientInfos.Add("�����/�ֻ�", "�ֻ��������ն�"); // ���ܷ���
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
        /// ��֤�û��Ƿ��½
        /// </summary>
        public static void CheckLogIn()
        {
            //��Session��û��== �Ự��û���� ֱ���ܵ���½ҳ 
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
        /// ��֤�û�Ȩ��
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
        /// ������ͨ˫��·ת��
        /// </summary>
        public static void IpTelUnicConvert()
        {
            string strDns = HttpContext.Current.Request.Url.DnsSafeHost.ToLower();

            //���������ʲ����
            if (!(strDns.EndsWith(".com") || strDns.EndsWith(".cn")))
            {
                return;
            }

            try
            {
                //IP��ַ
                string strIp = HttpContext.Current.Request.UserHostAddress.ToString();
                string strIpType = Stat.GetLocal(strIp);    //IP���ڵؼ�����                                
                string strUrl = HttpContext.Current.Request.Url.ToString();

                logger.DebugFormat("IP:{0}[{1}]->[{2}]", strIp, strIpType,strUrl);

                //����������
                //������·:  tel.����.xxx,   ��������Ϊxxxtel.����.xxx   
                //��ͨ���磺����.xxx,  ��������Ϊ xxx.����.xxxx
                if (strIpType.Contains("����") && !strDns.Contains("tel."))
                {
                    //ת�������·
                    strUrl = ConvertUrl_TelUnic(HttpContext.Current.Request.Url.DnsSafeHost, strUrl, "tel");
                    HttpContext.Current.Response.Redirect(strUrl);
                }
                else if (!strIpType.Contains("����") && strDns.Contains("tel."))
                {
                    //ת����ͨ��·                 
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
                        //ת�������·
                        strNewDns = "searchtel.patentstar.com.cn";
                        strNewUrl = strUrl.Replace(_strDns, strNewDns);
                        break;
                    case "unic":
                        //ת����ͨ��·
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

