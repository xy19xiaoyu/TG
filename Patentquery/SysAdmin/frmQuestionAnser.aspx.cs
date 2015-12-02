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
    public partial class frmQuestionDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["QID"] == null)
                    return;
                int qid = int.Parse(Request.QueryString["QID"].ToString());

                List<QuestionsInfo> lst = new List<QuestionsInfo>();
                //载入问题
                lst = QuestionDB.GetQuestionInfo(qid);
                QuestionsInfo qi = (QuestionsInfo)lst[0];
                lblTitle.Text = qi.Title;
                lblQuestion.Text = qi.Content;
                string sql = "select * from tbuser where id=" + qi.CreateUser.ToString();
                DataTable dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql, null);
                if (dt.Rows.Count <= 0)
                {
                    return;
                }
                txtAnser.Text = qi.AnserContent;
                lblUser.Text = dt.Rows[0]["RealName"].ToString();
                lblDate.Text = qi.CreateDate.ToString();
            }
        }
        //提交
        protected void txtSubmit_Click(object sender, EventArgs e)
        {
            int qid = int.Parse(Request.QueryString["QID"].ToString());
            DataSet ds = new DataSet();
            string sql = "select * from TbUser Where ID='" + Session["UserID"] + "'";
            ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

            if (ds.Tables[0].Rows.Count <= 0)
            {
                return;
            }

            string realname = ds.Tables[0].Rows[0]["RealName"].ToString().Trim();
            //提交回答
            if (QuestionDB.UpdateQuestion(qid, txtAnser.Text, System.DateTime.Now, realname) > 0)
            {
                MSG.AlertMsg(Page, "操作成功！");
            }
            else
            {
                MSG.AlertMsg(Page, "操作失败！");
            }
        }

        //返回
        protected void btnFanHui_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmQuestionLst.aspx");
        }
    }
}