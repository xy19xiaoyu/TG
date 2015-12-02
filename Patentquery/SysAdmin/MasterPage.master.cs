using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                RefTV();
                // Mp3();
            }
        }
    }   

    private void RefTV()
    {
        DataSet ds = new DataSet();
        string sql = "Select ID,PageName,PageUrl,PageDes From TbRight Where XianShiFlag=1 And NodeLevel=0 And ID IN (select distinct RightID from RoleRight where RoleID in (select RoleID from UserRole where UserID='" + Session["UserID"] + "')) Order BY XianShiShunXu ASC,ID ASC";
        ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            TreeNode node = new TreeNode(ds.Tables[0].Rows[i]["PageDes"].ToString().Trim(), ds.Tables[0].Rows[i]["ID"].ToString().Trim());
            // node.NavigateUrl = ds.Tables[0].Rows[i]["PageUrl"].ToString().Trim();

            node.SelectAction = TreeNodeSelectAction.None;
            tTV.Nodes.Add(node);
        }

        AddNode2TV();

        for (int i = 0; i < tTV.Nodes.Count; i++)
        {
            for (int j = 0; j < tTV.Nodes[i].ChildNodes.Count; j++)
            {
                if (tTV.Nodes[i].ChildNodes[j].NavigateUrl.ToString().Trim() == Request.RawUrl.ToString().Trim().Substring(Request.RawUrl.ToString().Trim().LastIndexOf("/") + 1))
                {
                    tTV.Nodes[i].ChildNodes[j].Selected = true;
                }
            }
        }
    }

    private void AddNode2TV()
    {
        DataSet ds = new DataSet();
        string sql = "";

        for (int i = 0; i < tTV.Nodes.Count; i++)
        {
            sql = "Select ID,PageName,PageUrl,PageDes From TbRight Where XianShiFlag=1 And NodeLevel='" + tTV.Nodes[i].Value.ToString().Trim() + "' And ID IN (select distinct RightID from RoleRight where RoleID in (select RoleID from UserRole where UserID='" + Session["UserID"] + "') )Order BY XianShiShunXu ASC,ID ASC";
            ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                TreeNode node = new TreeNode(ds.Tables[0].Rows[j]["PageDes"].ToString().Trim(), ds.Tables[0].Rows[j]["PageName"].ToString().Trim());
                node.NavigateUrl = ds.Tables[0].Rows[j]["PageUrl"].ToString().Trim();

                tTV.Nodes[i].ChildNodes.Add(node);
            }
        }
    }
}
