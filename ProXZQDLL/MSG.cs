using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace ProXZQDLL
{
    /// <summary>
    /// Message 的摘要说明
    /// </summary>
    public class MSG
    {
        public MSG()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public static void AlertMsg(System.Web.UI.Page WebPage, string msg)
        {
            string ScriptStr;
            //产生一个警告信息的Script函数
            ScriptStr = String.Empty;
            ScriptStr += "<!DOCTYPE html PUBLIC \"\"-//W3C//DTD XHTML 1.0 Transitional//EN\"\" \"\"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"\">";
            ScriptStr += "<script type='text/javascript'>" + Environment.NewLine;
            ScriptStr += "function StartUp() {" + Environment.NewLine;
            ScriptStr += "alert('";
            ScriptStr += msg.Replace("'", "\'").Replace(Environment.NewLine, "\n");
            ScriptStr += "');";
            ScriptStr += "};";
            ScriptStr += "</script>";
            ScriptStr += "<body onload='StartUp();'>";
            ScriptStr += "</body>";
            //'将此脚本发送出去
            WebPage.Response.Write(ScriptStr);

            // WebPage.RegisterClientScriptBlock("111", "<script>alert('"+ msg +"')</script>");

        }

        /// <summary>
        /// 提示一条信息后跳转
        /// </summary>
        /// <param name="WebPage"></param>
        /// <param name="msg"></param>
        /// <param name="RedirectUrl"></param>
        public static void AlertReturn(System.Web.UI.Page WebPage, string msg, string RedirectUrl)
        {
            WebPage.Response.Write("<script language=javascript>alert('" + msg + "')</script>");
            WebPage.Response.Write("<script language=javascript>window.location.href='" + RedirectUrl + "'</script>");
        }

        public static void OpenNew(System.Web.UI.Page WebPage, string strURL)
        {
            //pag.Response.Write("<Script>window.open('" + msg + "');</script> ");
            string ScriptStr;
            //产生一个新窗口的Script函数
            ScriptStr = "";
            ScriptStr += "<!DOCTYPE html PUBLIC \"\"-//W3C//DTD XHTML 1.0 Transitional//EN\"\" \"\"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"\">";
            ScriptStr += "<Script LANGUAGE='JavaScript'>";
            ScriptStr += "function StartUp() {";
            ScriptStr += "var me ;";
            ScriptStr += "me = window ;";
            ScriptStr += "msg=window.open('";
            ScriptStr += strURL;
            ScriptStr += "'); ";
            ScriptStr += "};";
            ScriptStr += "StartUp();";
            ScriptStr += "</SCRIPT>";
            //将此脚本发送出去
            WebPage.Response.Write(ScriptStr);
        }
    }
}