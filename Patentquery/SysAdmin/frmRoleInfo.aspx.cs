using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ProXZQDLL;

public partial class frmRoleInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //UserRight.Check(Page);
        if (!IsPostBack)
        {
            hfUserLeiXing.Value = UserRight.getUserLeiXing(Session["UserID"].ToString().Trim());
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
        Response.Redirect("frmRoleInfoDetails.aspx");
    }

    /// <summary>
    /// 刷新角色列表
    /// </summary>
    private void RefGrv()
    {
        DataSet ds = new DataSet();
        string sql = "select ID, RoleName, '' AS PageDes from TbRole Where 1=1 ";

        if (hfUserLeiXing.Value.ToString().Trim() == "企业")
        {
            sql += "And DepartMentID='" + Session["UserID"] + "' ";
        }
        else
        {
            sql += "And DepartMentID IS NULL ";
        }
        if (txtJueSe.Text.ToString().Trim() != "")
        {
            sql += "And  RoleName Like '%" + txtJueSe.Text.ToString().Trim() + "%' ";
        }


        sql += " Order By ID";
        ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);
        grvInfo.DataSource = ds;
        grvInfo.DataBind();
        BindRight();
    }

    private void BindRight()
    {
        DataSet ds = new DataSet();
        string strRight = "";
        for (int i = 0; i < grvInfo.Rows.Count; i++)
        {
            strRight = "";
            string sql = "Select PageDes From RoleRight a, TbRight b Where a.RightID=b.ID And a.RoleID='" + grvInfo.Rows[i].Cells[0].Text.ToString().Trim() + "' Order By a.ID";
            ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                strRight += ds.Tables[0].Rows[j]["PageDes"].ToString().Trim() + "；";
            }

            grvInfo.Rows[i].Cells[2].Text = strRight;
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
            btn = (LinkButton)(grvInfo.Rows[i].Cells[4].Controls[0]);
            btn.Attributes.Add("onclick", "return confirm('您确定删除当前记录吗？')");
            grvInfo.Rows[i].Cells[0].Visible = false;
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
        string sql = "Delete From TbRole Where ID='" + ID + "';";
        sql += "Delete From UserRole Where RoleID='" + ID + "';";
        sql += "Delete From RoleRight Where RoleID='" + ID + "'";
        DBA.DbAccess.ExecNoQuery(CommandType.Text, sql);
        RefGrv();
        MSG.AlertMsg(Page, "操作成功！");
    }
    protected void btnChaXun_Click(object sender, EventArgs e)
    {
        RefGrv();
    }
}
