using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Text;
using System.Web.Services;

namespace Patentquery.My
{
    public partial class CompareClaims : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }
        [WebMethod]
        public static string[] getClaims(string Ids,string _type)
        {
            SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
            StringBuilder sb = new StringBuilder();
            string[] lst = new string[2];
            if (!string.IsNullOrEmpty(Ids))
            {
                if (string.IsNullOrEmpty(_type))
                {
                    return null;
                }
                
                
                //sb.Append(" <table cellspacing='1' class='compare'>");
                //sb.Append("<tr>");
                //sb.Append("<td width='49%' style='border-right: #6595d6 1px solid;'>");

                //LiteralRights.Text = search.getInfoByPatentID(Request.QueryString["Id"], "CN", "0");
                string[] arrayId = Ids.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                string xmltext = search.getInfoByPatentID(arrayId[0].ToString(), _type, "0");
                MSXML2.DOMDocument30Class xml = new MSXML2.DOMDocument30Class();
                MSXML2.DOMDocument30Class xslt = new MSXML2.DOMDocument30Class();
                xml.loadXML(xmltext);

                XmlDocument doc = new XmlDocument();
                doc.Load(System.Web.HttpContext.Current.Server.MapPath("~") + "\\newcss\\claims.xsl");
                string xsltext = doc.InnerXml;

                xslt.loadXML(xsltext);
                string claimsA = xml.transformNode(xslt).Replace("charset=UTF-16", "charset=GB2312");
                int start=claimsA.IndexOf("<table");
                int end=claimsA.IndexOf("</table>")-start+8;
                claimsA=claimsA.Substring(start, end);
                //{["name":"张三","age":18],["name":"李四","age":19]}
                //sb.Append("{[ClaimsA:");
                //sb.Append(claimsA);
                //sb.Append("],[ClaimsB:");
                //sb.Append("</td>");
                //sb.Append("<td>");
                
                xmltext = search.getInfoByPatentID(arrayId[1].ToString(), _type, "0");
                xml = new MSXML2.DOMDocument30Class();
                //MSXML2.DOMDocument30Class xslt = new MSXML2.DOMDocument30Class();
                xml.loadXML(xmltext);

                //XmlDocument doc = new XmlDocument();
                doc.Load(System.Web.HttpContext.Current.Server.MapPath("~") + "\\newcss\\claims.xsl");
                xsltext = doc.InnerXml;

                xslt.loadXML(xsltext);
                string claimsB = xml.transformNode(xslt).Replace("charset=UTF-16", "charset=GB2312");
                start = claimsB.IndexOf("<table") ;
                end = claimsB.IndexOf("</table>") - start+8;
                claimsB = claimsB.Substring(start, end);
                //sb.Append(claimsB);
                //sb.Append("]}");
                //sb.Append("</td>");
                //sb.Append("</tr>");
                //sb.Append("</table>");
                //Response.Write(sb.ToString());
                lst[0] = claimsA.Replace("document.write(", "//document.write(");
                lst[1] = claimsB.Replace("document.write(", "//document.write(");
            }
            return lst;
        }

        [WebMethod]
        public static string[] getDes(string Ids, string _type)
        {
            SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
            StringBuilder sb = new StringBuilder();
            string[] lst = new string[2];
            if (!string.IsNullOrEmpty(Ids))
            {
                if (string.IsNullOrEmpty(_type))
                {
                    return null;
                }


                //sb.Append(" <table cellspacing='1' class='compare'>");
                //sb.Append("<tr>");
                //sb.Append("<td width='49%' style='border-right: #6595d6 1px solid;'>");

                //LiteralRights.Text = search.getInfoByPatentID(Request.QueryString["Id"], "CN", "0");
                string[] arrayId = Ids.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                string xmltext = search.getInfoByPatentID(arrayId[0].ToString(), _type, "1");
                MSXML2.DOMDocument30Class xml = new MSXML2.DOMDocument30Class();
                MSXML2.DOMDocument30Class xslt = new MSXML2.DOMDocument30Class();
                xml.loadXML(xmltext);

                XmlDocument doc = new XmlDocument();
                doc.Load(System.Web.HttpContext.Current.Server.MapPath("~") + "\\newcss\\des.xsl");
                string xsltext = doc.InnerXml;

                xslt.loadXML(xsltext);
                string claimsA = xml.transformNode(xslt).Replace("charset=UTF-16", "charset=GB2312");
                int start = claimsA.IndexOf("<table");
                int end = claimsA.IndexOf("</table>") - start + 8;
                claimsA = claimsA.Substring(start, end);
                //{["name":"张三","age":18],["name":"李四","age":19]}
                //sb.Append("{[ClaimsA:");
                //sb.Append(claimsA);
                //sb.Append("],[ClaimsB:");
                //sb.Append("</td>");
                //sb.Append("<td>");
                xmltext = search.getInfoByPatentID(arrayId[1].ToString(), _type, "1");
                xml = new MSXML2.DOMDocument30Class();               
                xml.loadXML(xmltext);
               
                string claimsB = xml.transformNode(xslt).Replace("charset=UTF-16", "charset=GB2312");
                start = claimsB.IndexOf("<table");
                end = claimsB.IndexOf("</table>") - start + 8;
                claimsB = claimsB.Substring(start, end);
                //sb.Append(claimsB);
                //sb.Append("]}");
                //sb.Append("</td>");
                //sb.Append("</tr>");
                //sb.Append("</table>");
                //Response.Write(sb.ToString());
                lst[0] = claimsA.Replace("document.write(", "//document.write(");
                lst[1] = claimsB.Replace("document.write(", "//document.write(");
            }
            return lst;
        }

        [WebMethod]
        public static string getSingleClaims(string Id, string type)
        {
            SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
            String claims = "";
            if (!string.IsNullOrEmpty(Id))
            {
                if (string.IsNullOrEmpty(type))
                {
                    return null;
                }

                string xmltext = search.getInfoByPatentID(Id, type, "0");
                MSXML2.DOMDocument30Class xml = new MSXML2.DOMDocument30Class();
                MSXML2.DOMDocument30Class xslt = new MSXML2.DOMDocument30Class();
                xml.loadXML(xmltext);

                XmlDocument doc = new XmlDocument();
                doc.Load(System.Web.HttpContext.Current.Server.MapPath("~") + "\\newcss\\claims.xsl");
                string xsltext = doc.InnerXml;

                xslt.loadXML(xsltext);
                string claimsA = xml.transformNode(xslt).Replace("charset=UTF-16", "charset=GB2312");
                int start = claimsA.IndexOf("<table");
                int end = claimsA.IndexOf("</table>") - start + 8;
                claimsA = claimsA.Substring(start, end);

                claims = claimsA.Replace("document.write(", "//document.write(");
            }
            return claims;
        }

        [WebMethod]
        public static string getSingleDes(string Id, string type)
        {
            SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
            String des = "";
            if (!string.IsNullOrEmpty(Id))
            {
                if (string.IsNullOrEmpty(type))
                {
                    return null;
                }

                string xmltext = search.getInfoByPatentID(Id, type, "1");
                MSXML2.DOMDocument30Class xml = new MSXML2.DOMDocument30Class();
                MSXML2.DOMDocument30Class xslt = new MSXML2.DOMDocument30Class();
                xml.loadXML(xmltext);

                XmlDocument doc = new XmlDocument();
                doc.Load(System.Web.HttpContext.Current.Server.MapPath("~") + "\\newcss\\des.xsl");
                string xsltext = doc.InnerXml;

                xslt.loadXML(xsltext);
                string desA = xml.transformNode(xslt).Replace ("charset=UTF-16", "charset=GB2312");
                int start = desA.IndexOf("<table");
                int end = desA.IndexOf("</table>") - start + 8;
                desA = desA.Substring(start, end);

                des = desA.Replace("document.write(", "//document.write(");
            }
            return des;
        }
    }
    
}