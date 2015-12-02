using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace Patentquery.Comm
{
    public partial class PatentInfo : System.Web.UI.Page
    {
        private SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
        protected void Page_Load(object sender, EventArgs e)
        {

            string id = string.Empty;
            string type = string.Empty;

            resutBase ret = new resutBase() { ret = false, err = "参数错误" };
            
            if (Request["id"] == null || Request["type"] == null)
            {
                Response.Write(ret.ToString());
                return;
            }
            if (string.IsNullOrEmpty(Request["id"].ToString().Trim()) || string.IsNullOrEmpty(Request["type"].ToString().Trim()))
            {
                Response.Write(ret.ToString());
                return;
            }

            id = Request["id"].ToString();
            type = Request["type"].ToString();
            object data = null;
            switch (type.ToUpper())
            {
                case "B":
                    data = "b";
                    //著录项目
                    break;
                case "C":
                    //string xmltext = search.getInfoByPatentID(Request.QueryString["Id"], "CN", "0");
                    //if (xmltext.StartsWith("ERROR："))
                    //{
                    //    data = xmltext;
                    //}
                    //else
                    //{
                    //    MSXML2.DOMDocument30Class xml = new MSXML2.DOMDocument30Class();
                    //    MSXML2.DOMDocument30Class xslt = new MSXML2.DOMDocument30Class();
                    //    //xmltext=xmltext.Replace("<![CDATA[<math>", "<math>").Replace("</math>]]>", "</math>");
                    //    xml.loadXML(xmltext);

                    //    XmlDocument doc = new XmlDocument();
                    //    doc.Load(Server.MapPath("~") + "\\newcss\\claims.xsl");
                    //    string xsltext = doc.InnerXml;

                    //    xslt.loadXML(xsltext);
                    //    LiteralRights.Text = xml.transformNode(xslt).Replace("charset=UTF-16", "charset=GB2312");
                    //}
                    //权利要求
                    break;
                case "D":
                    data = "d";
                    //说明书
                    break;
                case "PDF":
                    //PDF                    
                    data = "<div id=\"123\">hello json</div>";
                    break;
                case "P":
                    //图片
                    break;
                case "L":
                    //法律状态
                    break;
                case "R":
                    //引文
                    break;
                default:
                    data = null;
                    break;
            }
            ret.data = data;
            ret.ret = true;
            ret.err = "";
            Response.Write(ret.ToString());
        }
    }
    public class resutBase
    {
        public bool ret;
        public string err;
        public object data;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);

        }
    }
}