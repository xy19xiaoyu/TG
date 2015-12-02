using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProXZQDLL;
using System.Data;

namespace Patentquery.My
{
    public partial class WebLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["UserID"] == null)
            {
                Cpic.Cprs2010.User.User userInfo = Cpic.Cprs2010.User.UserManager.getGuestUser(Session.SessionID);
                if (userInfo != null)
                {
                    TbUser tbXmUser = new TbUser();
                    tbXmUser.ID = userInfo.ID;
                    tbXmUser.RealName = userInfo.Use_Name + userInfo.ID.ToString();
                    tbXmUser.YongHuLeiXing = "游客";

                    Session["UserID"] = tbXmUser.ID.ToString();
                    Session["RealName"] = tbXmUser.RealName;
                    Session["UserInfo"] = tbXmUser;
                    Response.Redirect("SmartQuery.aspx");
                }
                else
                {
                    Response.Write("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>"
                        + "<script>javascript:alert('服务器资源不足，请稍后再试');window.location.href = 'UserLogin.aspx';</script>");
                }
            }else
            {
                DataSet ds = new DataSet();
                string userid = Request.QueryString["UserID"].ToString();

                string sql = "select * from TbUser Where UserName='" + userid.Trim() + "'";
                ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);


                if (ds.Tables[0].Rows.Count <= 0)
                {
                    Response.Redirect("../frmLogin.aspx");
                }
                
                Session["UserID"] = ds.Tables[0].Rows[0]["ID"].ToString().Trim();

                Session["RealName"] = ds.Tables[0].Rows[0]["RealName"].ToString().Trim();

                Session["UserInfo"] = ProXZQDLL.UserRight.getUserInfo(Session["UserID"].ToString().Trim());

                string IP = HttpContext.Current.Request.UserHostAddress.ToString(); ;//HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
                ProXZQDLL.ClsLog.LogInsert(IP, Session["RealName"].ToString().Trim(), ds.Tables[0].Rows[0]["YongHuLeiXing"].ToString().Trim());

                Response.Redirect("SmartQuery.aspx");

            }

        }
    }
}