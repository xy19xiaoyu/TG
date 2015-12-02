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

public partial class MainLeft : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RefTV();
        }
    }

    private void RefTV()
    {
        DataSet ds = new DataSet();
        //string sql = "Select * From TbRight Where NodeLevel=0";
        string sql = "Select c.ID,c.PageName,c.PageDes From UserRole a,RoleRight b, TbRight c Where a.UserID='" + Session["UserID"] + "' And a.RoleID=b.RoleID And b.RightID=c.ID And c.NodeLevel=0";
        ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            TreeNode node = new TreeNode(ds.Tables[0].Rows[i]["PageDes"].ToString().Trim(), ds.Tables[0].Rows[i]["ID"].ToString().Trim());
            tTV.Nodes.Add(node);
        }

        AddNode2TV();
    }

    private void AddNode2TV()
    {
        DataSet ds = new DataSet();
        string sql = "";

        for (int i = 0; i < tTV.Nodes.Count; i++)
        {
            //sql = "Select * From TbRight Where NodeLevel=" + tTV.Nodes[i].Value.ToString().Trim();
            sql = "Select c.PageName,c.PageDes From UserRole a,RoleRight b, TbRight c Where a.UserID='" + Session["UserID"] + "' And a.RoleID=b.RoleID And b.RightID=c.ID And c.NodeLevel=" + tTV.Nodes[i].Value.ToString().Trim();
            ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                TreeNode node = new TreeNode(ds.Tables[0].Rows[j]["PageDes"].ToString().Trim(), ds.Tables[0].Rows[j]["PageName"].ToString().Trim());
                node.NavigateUrl = ds.Tables[0].Rows[j]["PageName"].ToString().Trim();
                tTV.Nodes[i].ChildNodes.Add(node);
            }
        }
    }
}
