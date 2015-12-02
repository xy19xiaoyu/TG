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
    public partial class frmNewsInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<NewsInfo> lsnews = new List<NewsInfo>();
            int newscount;
            if (!Page.IsPostBack)
            {
                lsnews = NewsDB.GetNewsList(1, 20, out newscount);
                gvNewsInfo.DataSource = lsnews;
                gvNewsInfo.DataBind();
                Session["pageindex"] = 1;
            }

        }

        protected void gvNewsInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int index = e.NewPageIndex;
            gvNewsInfo.PageIndex = e.NewPageIndex;
            List<NewsInfo> lsnews = new List<NewsInfo>();
            int newscount;
            lsnews = NewsDB.GetNewsList(index+1, 20, out newscount);
            gvNewsInfo.DataSource = lsnews;
            gvNewsInfo.DataBind();
            Session["pageindex"] = index + 1;
        }


        protected void gvNewsInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = gvNewsInfo.Rows[e.RowIndex].Cells[0].Text.ToString().Trim();
            string sql = "Delete From  newsinfo Where NID='" + ID + "'";
            DBA.DbAccess.ExecNoQuery(CommandType.Text, sql);
            int newscount;
            int pageindex = int.Parse(Session["pageindex"].ToString());
            List<NewsInfo> lsnews = new List<NewsInfo>();
            lsnews = NewsDB.GetNewsList(pageindex + 1, 20, out newscount);
            gvNewsInfo.DataSource = lsnews;
            gvNewsInfo.DataBind();
            MSG.AlertMsg(Page, "操作成功！");
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmNewsInfoDetails.aspx");
        }
    }
}