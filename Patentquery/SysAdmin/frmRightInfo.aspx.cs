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

public partial class frmRightInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //UserRight.Check(Page);
        if (!IsPostBack)
        {
            BindDdlUp();
            RefGrv();
          
        
        }
    }
    /// <summary>
    /// 新增权限
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmRightInfoDetails.aspx");
    }

    /// <summary>
    /// 刷新权限列表
    /// </summary>
    private void RefGrv()
    {
        DataSet ds = new DataSet();
        string sql = "select *  from TbRight Where 1=1 ";

        if (txtRightCode.Text.ToString().Trim() != "")
        {
            sql += "And PageName Like '%" + txtRightCode.Text.ToString().Trim() + "%' ";
        }
        if (txtRightName.Text.ToString().Trim() != "")
        {
            sql += "And PageDes Like '%" + txtRightName.Text.ToString().Trim() + "%' ";
        }

        if (ddlUp.SelectedValue.ToString().Trim() != "-1")
        {
            sql += "And NodeLevel ='"+ ddlUp.SelectedValue.ToString().Trim() +"' ";
        }
        sql += " Order By Nodelevel ,XianShiShunXu";
        ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);
        grvInfo.DataSource = ds;
        grvInfo.DataBind();

        for (int i = 0; i < grvInfo.Rows.Count; i++)
        {
            sql="select PageDes from TbRight Where ID='"+ grvInfo.Rows[i].Cells[3].Text.ToString().Trim() +"' ";
            ds=DBA.DbAccess.GetDataSet(CommandType.Text,sql);
            if(ds.Tables[0].Rows.Count<=0)
            {
                grvInfo.Rows[i].Cells[3].Text = "根目录";
                continue;
            }
            
            grvInfo.Rows[i].Cells[3].Text = ds.Tables[0].Rows[0][0].ToString().Trim();
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
            btn = (LinkButton)(grvInfo.Rows[i].Cells[7].Controls[0]);
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
        string sql = "Delete From TbRight Where ID='" + ID + "'";
        DBA.DbAccess.ExecNoQuery(CommandType.Text, sql);
        RefGrv();
        MSG.AlertMsg(Page, "操作成功！");
    }

    protected void btnChaXun_Click(object sender, EventArgs e)
    {
        RefGrv();
    }

    private void BindDdlUp()
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        var result = from item in db.TbRight
                     where item.Nodelevel == 0
                     select item;
        ddlUp.Items.Clear();
        ddlUp.Items.Add("请选择");
        ddlUp.Items[0].Value = "-1";
        ddlUp.Items.Add("根目录");
        ddlUp.Items[1].Value = "0";
        for (int i = 0; i < result.Count(); i++)
        {
            ddlUp.Items.Add(result.ToList()[i].PageDes);
            ddlUp.Items[i +2].Value = result.ToList()[i].ID.ToString();
        }

    }
}
