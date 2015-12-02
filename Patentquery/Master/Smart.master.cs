using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using TLC.BusinessLogicLayer;
using ProXZQDLL;

public partial class Master_Smart : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserID"] != null)
            {
                TbUser user = (TbUser)HttpContext.Current.Session["USerInfo"];
                if (user.YongHuLeiXing.Equals("游客"))
                {
                    LiteralUserName.Text = "欢迎您 [" + user.YongHuLeiXing.Trim() + "用户]:" + user.RealName.Trim();
                }
                else
                {

                    LiteralUserName.Text = "欢迎您 [" + user.YongHuLeiXing.Trim() + "用户]:<a href='/My/EditUser.aspx'>" + user.RealName.Trim() +"</a>";
                }
            }
            else
            {
                //LiteralUserName.Text = "请<a href='../Default.aspx' target='_blank'>登录</a>，游客！|<a href='../SysAdmin/frmRegedit.aspx' target='_blank'>注册</a> ";
                //LiteralUserName.Text = "游客,您好！ | <a href='../SysAdmin/frmRegedit.aspx' target=''>注册</a> ";
                Response.Redirect("~/frmLogin.aspx");
            }
        }
    }


}
