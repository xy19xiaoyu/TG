using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using Cpic.Cprs2010.Search.ResultData;
using TLC.BusinessLogicLayer;
using ProXZQDLL;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Response.Redirect("~/frmLogin.aspx");
        }
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
}
