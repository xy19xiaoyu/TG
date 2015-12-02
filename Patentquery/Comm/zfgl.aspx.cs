using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLC;
using System.IO;

namespace Patentquery.Comm
{
    public partial class zfgl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int Userid = Convert.ToInt32(Session["Userid"].ToString());
            //ifst.Attributes["src"] = getTjRul() + "PatentAnalyze/jsp/home3.jsp?UserId=" + Userid;
            ifst.Attributes["src"] = getTjRul() + "PatentAnalyze/jsp/home_gov.jsp?UserId=0";

            ProXZQDLL.ClsLog.LogInsertLanMu(this, "政符管理"); //"企业在线数据库");, "政符管理");
        }

        private string getTjRul()
        {
            string strRs = "http://192.168.131.15:8081/";
            try
            {

                string strUrl = Request.Url.ToString().ToUpper();

                foreach (string strItem in SearchInterface.XmPatentComm.strDicTJSeverURL.Keys)
                {
                    if (strUrl.Contains(strItem))
                    {
                        strRs = "http://" + SearchInterface.XmPatentComm.strDicTJSeverURL[strItem] + "/";
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return strRs;
        }
    }
}