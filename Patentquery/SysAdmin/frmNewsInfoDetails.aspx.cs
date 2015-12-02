using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProNewsDll;
using System.Data;
namespace Patentquery.SysAdmin
{
    public partial class frmNewsInfoDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text;
            string content = txtA.Text;
            string summary = txtSummary.Text;
            DataSet ds = new DataSet();
            string sql = "select * from TbUser Where ID='" + Session["UserID"] + "'";
            ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

            if (ds.Tables[0].Rows.Count <= 0)
            {
                return;
            }

            string realname = ds.Tables[0].Rows[0]["RealName"].ToString().Trim();
            if (NewsDB.InsertNews(title, summary, content, System.DateTime.Now, realname) > 0)
            {
                MSG.AlertMsg(Page, "操作成功！");
            }
            else
            {
                MSG.AlertMsg(Page, "操作失败！");
            }

        }

        protected void btnfanhui_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmNewsInfo.aspx");
        }
    }
}