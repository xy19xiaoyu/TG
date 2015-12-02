using System;
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
using System.Collections;
using System.Net;
using System.Diagnostics;
using System.Text.RegularExpressions;
using ProXZQDLL;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserID"] == null)
            {
                getUserInfo();
            }
            else
            {
                LogingRedirect();
            }
        }
    }

    private void LogingRedirect()
    {
        string sql = "Select c.ID,c.PageName,c.PageDes From UserRole a,RoleRight b, TbRight c Where c.XianShiFlag=1 And a.UserID='" + Session["UserID"] + "' And a.RoleID=b.RoleID And b.RightID=c.ID And c.NodeLevel=0 Order BY c.XianShiShunXu ASC,c.ID ASC";

        DataSet ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

        if (ds.Tables[0].Rows.Count <= 0)
        {
            MSG.AlertMsg(Page, "您的账户未授权对后台的访问，请与管理员联系！");
            return;
        }
        sql = "Select c.PageName,c.PageDes From UserRole a,RoleRight b, TbRight c Where c.XianShiFlag=1 And a.UserID='" + Session["UserID"] + "' And a.RoleID=b.RoleID And b.RightID=c.ID And c.NodeLevel='" + ds.Tables[0].Rows[0]["ID"].ToString().Trim() + "' Order BY c.XianShiShunXu ASC,c.ID ASC";

        ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

        if (ds.Tables[0].Rows.Count <= 0)
        {
            MSG.AlertMsg(Page, "您的账户未授权对后台的访问，请与管理员联系！");
            return;
        }

        if (Session["ReturnUrl"] == null)
        {
            Response.Redirect(ds.Tables[0].Rows[0]["PageName"].ToString().Trim());
        }
        else
        {
            Response.Redirect(Session["ReturnUrl"].ToString());
            Session["ReturnUrl"] = null;
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        string sql = "select * from TbUser Where UserName='" + txtUserName.Text.ToString().Trim() + "'";
        ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);



        if (ds.Tables[0].Rows.Count <= 0)
        {
            MSG.AlertMsg(Page, "您输入的用户名不存在，请重新输入！");
            return;
        }
        string PWD = ds.Tables[0].Rows[0]["UserPWD"].ToString().Trim();
        if (PWD.ToLower() != txtPWD.Text.ToString().Trim().ToLower())
        {
            MSG.AlertMsg(Page, "您输入的用户名或密码错误，请重新输入！");
            return;
        }

        Session["UserID"] = ds.Tables[0].Rows[0]["ID"].ToString().Trim();
        Session["RealName"] = ds.Tables[0].Rows[0]["RealName"].ToString().Trim();

        Session["UserInfo"] = ProXZQDLL.UserRight.getUserInfo(Session["UserID"].ToString().Trim());


        string strIP = Page.Request.UserHostAddress;
        //// 取得客户端的MAC地址
        //strIP = GetCustomerMac(strIP);
        UserRight.WriteUserInfo(strIP, txtUserName.Text.ToString().Trim(), txtPWD.Text.ToString().Trim());

        LogingRedirect();
    }

    /// <summary>
    /// 获取保存的用户名密码
    /// </summary>
    private void getUserInfo()
    {
        string strIP = Page.Request.UserHostAddress;

        // strIP = GetCustomerMac(strIP);
        ArrayList UserInfo = UserRight.getLoginInfo(strIP);
        if (UserInfo == null)
        {
            txtUserName.Focus();
            return;
        }

        txtUserName.Text = UserInfo[0].ToString();
        //txtPWD.Text = UserInfo[1].ToString();

        //txtPWD.Attributes.Add("value", UserInfo[1].ToString());

    }

    private string GetMac()
    {
        System.Net.IPAddress[] addressList = Dns.GetHostByName(Dns.GetHostName()).AddressList;
        string IP1, IP2;
        if (addressList.Length > 1)
        {
            IP1 = addressList[0].ToString();
            IP2 = addressList[1].ToString();
        }
        else
        {
            IP1 = addressList[0].ToString();
            IP2 = "没有可用的连接";
        }

        IP1 = GetCustomerMac(IP1);
        return IP1;
    }

    //这里是关键函数了
    public string GetCustomerMac(string IP)
    {
        string dirResults = "";
        ProcessStartInfo psi = new ProcessStartInfo();
        Process proc = new Process();
        psi.FileName = "nbtstat";
        psi.RedirectStandardInput = false;
        psi.RedirectStandardOutput = true;
        psi.Arguments = "-a " + IP;
        psi.UseShellExecute = false;
        proc = Process.Start(psi);
        dirResults = proc.StandardOutput.ReadToEnd();
        proc.WaitForExit();

        //匹配mac地址
        Match m = Regex.Match(dirResults, "\\w+\\-\\w+\\-\\w+\\-\\w+\\-\\w+\\-\\w\\w");

        //若匹配成功则返回mac，否则返回找不到主机信息
        if (m.ToString() != "")
        {
            return m.ToString();
        }
        else
        {
            return "找不到主机信息";
        }

    }

}

