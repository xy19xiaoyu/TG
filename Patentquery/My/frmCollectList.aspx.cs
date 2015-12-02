using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProXZQDLL;

namespace Patentquery.My
{
    public partial class frmCollectList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["Userid"] == null)
            {
                return;
            }
            TbUser user = (TbUser)HttpContext.Current.Session["USerInfo"];
            int userid = user.ID;
            string rightlist = UserRight.getstrRightCode(userid);
            yonghuleixing.Value = user.YongHuLeiXing.Trim();
            if (user.YongHuLeiXing.Trim() == "企业")
            {
                if (rightlist.IndexOf("qy_adddata") > 0)
                {
                    string ztid = ztHelper.setqyztid();
                    zttype.Items.Clear();
                    zttype.Items.Add(new ListItem("企业在线数据库", ztid));
                }
            }
            else
            {
                if (rightlist.IndexOf("zt_adddata") > 0)
                {
                    string ztid = ztHelper.setqyztid();
                    zttype.Items.Add(new ListItem("企业在线数据库", ztid));
                    zttype.DataSource = ztHelper.getztName();
                    zttype.DataTextField = "ztdbname";
                    zttype.DataValueField = "ZID";
                    zttype.DataBind();
                }
            }
            this.rightlist.Value = rightlist;
        }
    }
}