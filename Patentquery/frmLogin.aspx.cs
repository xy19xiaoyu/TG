using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ProXZQDLL;

namespace Patentquery
{
    public partial class frmLogin : System.Web.UI.Page
    {
        public static Random rdYkID = new Random();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ImageButtonLogin_Click(object sender, ImageClickEventArgs e)
        {
            DataSet ds = new DataSet();
            string sql = "select * from TbUser Where UserName='" + TextBoxAccount.Text.ToString().Trim() + "'";
            ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);


            if (ds.Tables[0].Rows.Count <= 0)
            {
                MSG.AlertMsg(Page, "您输入的用户名不存在，请重新输入！");
                return;
            }
            string PWD = ds.Tables[0].Rows[0]["UserPWD"].ToString().Trim();
            if (PWD.ToLower() != Password.Text.ToString().Trim().ToLower())
            {
                MSG.AlertMsg(Page, "您输入的用户名或密码错误，请重新输入！");
                return;
            }

            Session["UserID"] = ds.Tables[0].Rows[0]["ID"].ToString().Trim();

            Session["RealName"] = ds.Tables[0].Rows[0]["RealName"].ToString().Trim();

            Session["UserInfo"] = ProXZQDLL.UserRight.getUserInfo(Session["UserID"].ToString().Trim());

            string IP = HttpContext.Current.Request.UserHostAddress.ToString(); ;//HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
            ProXZQDLL.ClsLog.LogInsert(IP, Session["RealName"].ToString().Trim(), ds.Tables[0].Rows[0]["YongHuLeiXing"].ToString().Trim());

            Response.Redirect("My/SmartQuery.aspx");


            //Response.Redirect("ssssssss");
        }

        protected void imgBtnYk_Click(object sender, ImageClickEventArgs e)
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
                Response.Redirect("My/SmartQuery.aspx");
            }
            else
            {
                Response.Write("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>"
                    + "<script>javascript:alert('服务器资源不足，请稍后再试');window.location.href = 'UserLogin.aspx';</script>");
            }
        }
    }
}