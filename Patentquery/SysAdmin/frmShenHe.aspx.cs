using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProXZQDLL;
using System.Data;

namespace Patentquery.SysAdmin
{
    public partial class frmShenHe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                RefGrv();
            }
        }
        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmUserInfoDetails.aspx");
        }

        /// <summary>
        /// 刷新用户列表
        /// </summary>
        private void RefGrv()
        {
            DataSet ds = new DataSet();
            string sql = "";


            sql = "select *, '' AS RoleName from TbUser WHERE SHFlag=0 ";

            if (txtZhangHao.Text.ToString().Trim() != "")
            {
                sql += " AND UserName Like '%" + txtZhangHao.Text.ToString().Trim() + "%' ";
            }
            if (txtXingMing.Text.ToString().Trim() != "")
            {
                sql += " And RealName Like '%" + txtXingMing.Text.ToString().Trim() + "%' ";
            }

            sql += " Order By ID DESC";

            ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

            if (ViewState["PgData"] != null)
            {
                ViewState["PgData"] = ds;
            }
            else
            {
                ViewState.Add("PgData", ds);
            }
            grvInfo.DataSource = ds;
            grvInfo.DataBind();
        }


        private void BindRole()
        {
            DataSet ds = new DataSet();
            string strRight = "";
            for (int i = 0; i < grvInfo.Rows.Count; i++)
            {
                strRight = "";
                string sql = "select RoleName From UserRole a,TbRole b Where a.RoleID=b.ID And a.UserID='" + grvInfo.Rows[i].Cells[0].Text.ToString().Trim() + "' Order By a.ID";
                ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    strRight += ds.Tables[0].Rows[j]["RoleName"].ToString().Trim() + "；";
                }

                grvInfo.Rows[i].Cells[8].Text = strRight;
            }
        }

        /// <summary>
        /// 隐藏ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grvInfo_DataBound(object sender, EventArgs e)
        {
            if (grvInfo.Rows.Count <= 0)
            {
                return;
            }

            LinkButton btn;

            for (int i = 0; i < grvInfo.Rows.Count; i++)
            {
                btn = (LinkButton)(grvInfo.Rows[i].Cells[11].Controls[0]);
                btn.Attributes.Add("onclick", "return confirm('您确定删除当前记录吗？')");
                grvInfo.Rows[i].Cells[0].Visible = false;

                if (grvInfo.Rows[i].Cells[3].Text.ToString().Trim() != "企业")
                {
                    grvInfo.Rows[i].Cells[9].Text = "";
                }
                else
                {
                    //grvInfo.Rows[i].Cells[9].Enabled = true;
                }
            }
            grvInfo.HeaderRow.Cells[0].Visible = false;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grvInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = grvInfo.Rows[e.RowIndex].Cells[0].Text.ToString().Trim();
            string sql = "Delete From TbUser Where ID='" + ID + "'";
            DBA.DbAccess.ExecNoQuery(CommandType.Text, sql);
            RefGrv();
            MSG.AlertMsg(Page, "操作成功！");
        }

        protected void btnChaXun_Click(object sender, EventArgs e)
        {
            RefGrv();
        }

        protected void grvInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvInfo.PageIndex = e.NewPageIndex;
            grvInfo.DataSource = ViewState["PgData"];
            grvInfo.DataBind();
        }
    }
}