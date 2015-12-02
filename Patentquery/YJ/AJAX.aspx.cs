using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Patentquery.YJ
{
    public partial class AJAX : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "no-cache");
            string sInput = Server.UrlDecode(Request["sColor"].Trim());
            string flag = Request["flag"];
            string topflag = Request["topflag"];
            if (sInput.Length == 0)
                return;
            string sResult = "";

            string dbtype = Request.QueryString["dbtype"];
            if (!string.IsNullOrEmpty(topflag))
            {
                switch (topflag)
                {
                    case "0"://投入
                        break;
                    case "1"://成果
                        break;
                    case "2"://市场重心
                        string Sheng = Request.QueryString["sheng"].Substring(2);
                        sResult = ProYJDLL.YJDB.getShenShi(int.Parse(Sheng), sInput);
                        break;
                    case "3"://技术重心
                        sResult = ProYJDLL.YJDB.getIPC(sInput);
                        break;
                    case "4"://申请人
                        sResult = ProYJDLL.YJDB.getApplicant(sInput);
                        break;
                    case "5"://研发人
                        sResult = ProYJDLL.YJDB.getInventor(sInput);
                        break;
                    case "6"://质量
                        sResult="有效发明公开,有效实用新型授权,有效外观设计授权,有效发明授权,失效发明公开,失效实用新型授权,失效外观设计授权,失效发明授权";
                        //专利类型
                        break;
                    case "7"://寿命
                        break;
                    case "8"://来华
                        break;
                    case "9"://自定义
                        break;
                }
            }
            else
            {
                if (dbtype == "EN")
                {
                    switch (flag)
                    {
                        case "1"://行业预警                       
                            break;
                        case "2"://申请人
                            sResult = ProYJDLL.YJDB.getApplicant(sInput);
                            break;
                        case "3"://区域分布  
                            sResult = ProYJDLL.YJDB.getIPC(sInput);
                            break;
                        case "4"://发明人

                            break;
                        case "5"://来华专利
                            //sResult = ProYJDLL.YJDB.getApplicant(sInput);
                            break;
                    }
                }
                if (dbtype == "CN")
                {
                    switch (flag)
                    {
                        case "1"://行业预警
                            //sResult = ProYJDLL.YJDB.getApplicant(sInput);
                            break;
                        case "2"://申请人
                            sResult = ProYJDLL.YJDB.getApplicant(sInput);
                            //sResult = ProYJDLL.YJDB.getIPC(sInput);
                            break;
                        case "3"://区域分布
                            string Sheng = Request.QueryString["sheng"].Substring(2);
                            sResult = ProYJDLL.YJDB.getShenShi(int.Parse(Sheng), sInput);
                            break;
                        case "4"://发明人
                            sResult = ProYJDLL.YJDB.getInventor(sInput);
                            break;
                        case "5"://来华专利
                            sResult = ProYJDLL.YJDB.getApplicant(sInput);
                            break;
                    }
                }
            }

            Response.Write(sResult);
        }
    }
}