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

public partial class frmSHRight : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //UserRight.Check(Page);
        if (!IsPostBack)
        {
            BindList();
        }
    }

    private void BindList()
    {
        DataSet ds = new DataSet();
        string sql = "Select ID,RealName from TbUser Where ID IN (Select a.UserID From UserRole a,RoleRight b, TbRight c Where a.RoleID=b.RoleID And b.RightID=c.ID And c.PageName='frmLogSH.aspx')";
        ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

        listUserInfo.DataSource = ds;
        listUserInfo.DataTextField = "RealName";
        listUserInfo.DataValueField = "ID";
        listUserInfo.DataBind();

        sql = "Select ID,RealName from TbUser";
        ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

        chkUserInfo.DataSource = ds;
        chkUserInfo.DataTextField = "RealName";
        chkUserInfo.DataValueField = "ID";
        chkUserInfo.DataBind();
    }

    private void BindCHK(string UserID)
    {
        DataSet ds = new DataSet();
        string sql = "Select ID,RealName from TbUser Where ID='" + UserID + "'";
        ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);
    }

    protected void listUserInfo_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        string sql = "Select BSHID From SHRight Where SHID='" + listUserInfo.SelectedValue.ToString().Trim() + "'";
        ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

        for (int i = 0; i < chkUserInfo.Items.Count; i++)
        {
            chkUserInfo.Items[i].Selected = false;
        }
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            for (int j = 0; j < chkUserInfo.Items.Count; j++)
            {
                if (chkUserInfo.Items[j].Value.Trim() == ds.Tables[0].Rows[i]["BSHID"].ToString().Trim())
                {
                    chkUserInfo.Items[j].Selected = true;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBaoCun_Click(object sender, EventArgs e)
    {
        string sql = "Delete From SHRight Where SHID='" + listUserInfo.SelectedValue.ToString().Trim() + "'";
        DBA.DbAccess.ExecNoQuery(CommandType.Text, sql);
        for (int i = 0; i < chkUserInfo.Items.Count; i++)
        {
            if (chkUserInfo.Items[i].Selected)
            {
                sql = "Insert Into SHRight(SHID,BSHID) Values('" + listUserInfo.SelectedValue.ToString().Trim() + "','" + chkUserInfo.Items[i].Value.Trim() + "')";
                DBA.DbAccess.ExecNoQuery(CommandType.Text, sql);
            }
        }

        MSG.AlertMsg(Page, "操作成功！");
    }
}
