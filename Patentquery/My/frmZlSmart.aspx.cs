using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProXZQDLL;

namespace Patentquery.My
{
    public partial class frmZlSmart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    Cpic.Cprs2010.User.User userInfo = Cpic.Cprs2010.User.UserManager.getGuestUser(Session.SessionID);
                    if (userInfo != null)
                    {
                        TbUser tbXmUser = new TbUser();
                        tbXmUser.ID = userInfo.ID;
                        tbXmUser.RealName = userInfo.Use_Name;
                        tbXmUser.YongHuLeiXing = "游客";

                        Session["UserID"] = tbXmUser.ID.ToString();
                        Session["RealName"] = tbXmUser.RealName;
                        Session["UserInfo"] = tbXmUser;
                    }
                    else
                    {
                        //Response.Write("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>"
                        //    + "<script>javascript:alert('服务器资源不足，请稍后再试');window.location.href = 'UserLogin.aspx';</script>");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}