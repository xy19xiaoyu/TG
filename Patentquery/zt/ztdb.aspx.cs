using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ProXZQDLL;

public partial class TH_ztdb : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string type;

        if (IsPostBack)
        {
            return;
        }
        try
        {
            TbUser user = (TbUser)HttpContext.Current.Session["USerInfo"];
            if (string.IsNullOrEmpty(Request["type"]))
            {
                if (HttpContext.Current.Session["USerInfo"] == null)
                {
                    Response.Redirect("../frmError.htm");
                }
                //判断用户是否为企业用户
                //得到企业用户的企业现在数据库id
                if (user.YongHuLeiXing.Trim() == "企业")
                {
                    hidthid.Value = ztHelper.setqyztid();
                    yonghuleixing.Value = "企业";
                    ProXZQDLL.ClsLog.LogInsertLanMu(this, "企业在线数据库"); //"企业在线数据库");

                }
                else
                {
                    Response.Redirect("../frmError.htm");
                }
            }


            int userid = Convert.ToInt32(HttpContext.Current.Session["Userid"].ToString());

            this.rightlist.Value = UserRight.getstrRightCode(userid);
        }
        catch (Exception ex)
        {
            Console.Out.WriteLine(ex.ToString());
        }
    }
}