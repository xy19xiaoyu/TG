using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ProXZQDLL;
namespace Patentquery.Master
{
    public partial class index_new : System.Web.UI.MasterPage
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

                        LiteralUserName.Text = "欢迎您 [" + user.YongHuLeiXing.Trim() + "用户]:<a href='/My/EditUser.aspx'>" + user.RealName.Trim() + "</a>";
                    }
                }
                else
                {
                    //LiteralUserName.Text = "游客,您好！ | <a href='../SysAdmin/frmRegedit.aspx' target=''>注册</a> ";
                    Response.Redirect("~/frmLogin.aspx");
                }
            }
        }
    }
}