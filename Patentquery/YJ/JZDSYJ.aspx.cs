using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Patentquery.YJ
{
    public partial class JZDSYJ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["UserInfo"] == null)
                {
                    Response.Redirect("../default.aspx");
                }

                ProXZQDLL.ClsLog.LogInsertLanMu(this, "专利预警"); // "专利预警");

                List<ProYJDLL.ShengShi> lst = ProYJDLL.YJDB.getSheng();

                ddlSheng.DataSource = lst;
                ddlSheng.DataTextField = "Sheng";
                ddlSheng.DataValueField = "DaiMaID";
                ddlSheng.DataBind();
                ddlshichang.DataSource = lst;
                ddlshichang.DataTextField = "Sheng";
                ddlshichang.DataValueField = "DaiMaID";
                ddlshichang.DataBind();

                lst = ProYJDLL.YJDB.getGuoJia();

                ddlGuoJia.DataSource = lst;
                ddlGuoJia.DataTextField = "Sheng";
                ddlGuoJia.DataValueField = "DaiMaID";
                ddlGuoJia.DataBind();


                lst = ProYJDLL.YJDB.getShiJie();

                ddlShiJie.DataSource = lst;
                ddlShiJie.DataTextField = "Sheng";
                ddlShiJie.DataValueField = "DaiMaID";
                ddlShiJie.DataBind();

                ddlShiJie1.DataSource = lst;
                ddlShiJie1.DataTextField = "Sheng";
                ddlShiJie1.DataValueField = "DaiMaID";
                ddlShiJie1.DataBind();
                
                int userid = int.Parse(System.Web.HttpContext.Current.Session["UserID"].ToString());
                DataTable res = ztHelper.getName(userid.ToString());
                ddlfanwei.DataSource = res;
                ddlfanwei.DataTextField = "ztDbName";
                ddlfanwei.DataValueField = "zid";
                ddlfanwei.DataBind();
                ddlfanwei.Items.Add("平台数据");


            }
        }

    }
}