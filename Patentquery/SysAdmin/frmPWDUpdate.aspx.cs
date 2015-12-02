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

public partial class frmPWDUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RefInfo();
        }
    }
    private void RefInfo()
    {
        DataSet ds = new DataSet();
        string sql = "select * from TbUser Where ID='" + Session["UserID"] + "'";
        ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

        if (ds.Tables[0].Rows.Count <= 0)
        {
            return;
        }

        txtRealName.Text  = ds.Tables[0].Rows[0]["RealName"].ToString().Trim();
        lblUserName.Text = ds.Tables[0].Rows[0]["UserName"].ToString().Trim();

    }
    protected void btnQueDing_Click(object sender, EventArgs e)
    {
        string sql = "Update TbUser Set RealName='" + txtRealName.Text.ToString().Trim() + "', UserPWD='" + txtPWD.Text.ToString().Trim() + "' Where UserName='" + lblUserName.Text.ToString().Trim() + "'";
        DBA.DbAccess.ExecNoQuery(CommandType.Text, sql);
        MSG.AlertMsg(Page, "操作成功！");
    }
}
