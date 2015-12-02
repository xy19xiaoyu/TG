using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ProXZQDLL;

namespace Patentquery.My
{
    public partial class frmTreeQuery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                return;
            }
            try
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
                        zttype.DataValueField = "dbid";
                        zttype.DataBind();
                    }
                }
                this.rightlist.Value = rightlist;

                // 初始化代理机构代码
                string strSql = "select DAILIJGDM, DAILIJGMC, DAILIJGLXDH,DAILIJGDZ from ZPT_SJWH_DLJGPZB";

                DataTable dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, strSql);

                string res = "";
                foreach (DataRow dr in dt.Rows)
                {
                    string line = "(" + dr["DAILIJGDM"].ToString() + ")" + dr["DAILIJGMC"].ToString() + ";";
                    res += line;
                }
                res.TrimEnd(';');

                //给前台的hidden变量赋代理机构代码值
                this.hfValue.Value = res;

                // 初始化国省代码
                string strSqlCo = "select DaiMa, MingCheng from countryconfig";

                DataTable dtCo = DBA.SqlDbAccess.GetDataTable(CommandType.Text, strSqlCo);

                string resCo = "";
                foreach (DataRow dr in dtCo.Rows)
                {
                    string line = "(" + dr["DaiMa"].ToString().Trim() + ")" + dr["MingCheng"].ToString().Trim() + ";";
                    resCo += line;
                }
                resCo.TrimEnd(';');
                // 给前台变量赋国省代码值
                this.hfValueCountryCode.Value = resCo;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
            }

        }
    }
}