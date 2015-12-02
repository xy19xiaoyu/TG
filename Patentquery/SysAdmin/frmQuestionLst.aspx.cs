using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProNewsDll;

namespace Patentquery.SysAdmin
{
    public partial class frmQuestionLst : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<QuestionsInfo> lsnews = new List<QuestionsInfo>();
            
            if (!Page.IsPostBack)
            {
                lsnews = QuestionDB.GetQuestionList(1, 20, int.Parse(ddlStatus.SelectedValue));
                gvNewsInfo.DataSource = lsnews;
                gvNewsInfo.DataBind();
                Session["pageindex"] = 1;
            }

        }

        protected void gvNewsInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int index = e.NewPageIndex;
            gvNewsInfo.PageIndex = e.NewPageIndex;
            List<QuestionsInfo> lsnews = new List<QuestionsInfo>();
            
            lsnews = QuestionDB.GetQuestionList(index + 1, 20, int.Parse(ddlStatus.SelectedValue));
            gvNewsInfo.DataSource = lsnews;
            gvNewsInfo.DataBind();
            Session["pageindex"] = index + 1;
        }

       
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            GridViewRow gr = (GridViewRow)((LinkButton)sender).Parent.Parent;
            string id = gr.Cells[0].Text;

            Response.Redirect("frmQuestionAnser.aspx?qid=" + id );
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<QuestionsInfo> lsnews = new List<QuestionsInfo>();
            lsnews = QuestionDB.GetQuestionList(1, 20, int.Parse(ddlStatus.SelectedValue));
            gvNewsInfo.DataSource = lsnews;
            gvNewsInfo.DataBind();
            Session["pageindex"] = 1;
        }

        protected void gvNewsInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[3].Text.Trim() == "0")
                {
                    e.Row.Cells[3].Text = "未回复";
                }
                else
                {
                    e.Row.Cells[3].Text = "已回复";
                }
            }
        }
    }
}