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

public partial class frmDepartMent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RefGrv();
        }
    }
    protected void btnQueDing_Click(object sender, EventArgs e)
    {
        if (txtDepartMent.Text.ToString().Trim() == "")
        {
            MSG.AlertMsg(Page, "请输入部门名称！");
            return;
        }
        string sql = "Insert Into TbDepartMent(DepartMent) Values('"+ txtDepartMent.Text.ToString().Trim() +"')";

        DBA.DbAccess.ExecNoQuery(CommandType.Text, sql);
        RefGrv();
        MSG.AlertMsg(Page, "操作成功");
    }

    /// <summary>
    /// 刷新页面
    /// </summary>
    private void RefGrv()
    {
        DataSet ds = new DataSet();
        string sql = "Select * From TbDepartMent";

        ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

        grvDepartMent.DataSource = ds;
        grvDepartMent.DataBind();
    }
}
