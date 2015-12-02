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

public partial class frmRoleInfoDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //UserRight.Check(Page);
        if (!IsPostBack)
        {
            hfUserLeiXing.Value = UserRight.getUserLeiXing(Session["UserID"].ToString().Trim());
            RefTV();
            string ID = Request.QueryString["ID"];

            if (ID != null && ID != "")
            {
                BindData(ID);
            }
        }
    }
    private void BindData(string ID)
    {
        DataSet ds = new DataSet();

        string sql = "select ID, RoleName, '' AS PageDes from TbRole Where ID='" + ID + "'";
        ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);
        if (ds.Tables[0].Rows.Count <= 0)
        {
            return;
        }

        txtProName.Text = ds.Tables[0].Rows[0]["RoleName"].ToString().Trim();

        sql = "Select * From RoleRight Where RoleID='" + ID + "'";
        ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            for (int k = 0; k < tTV.Nodes.Count; k++)
            {
                NodeCheck(tTV.Nodes[k], ds.Tables[0].Rows[i]["RightID"].ToString().Trim());
            }
        }
    }

    private void NodeCheck(TreeNode node, string ID)
    {
        if (node.Value == ID)
        {
            node.Checked = true;
        }
        for (int i = 0; i < node.ChildNodes.Count; i++)
        {
            NodeCheck(node.ChildNodes[i], ID);
        }
    }

    /// <summary>
    /// 角色信息
    /// </summary>
    private void BindRight()
    {
        DataSet ds = new DataSet();
        string sql = "select ID,PageName,pageDes from TbRight Where NodeLevel<>0";
        ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

        chkRight.DataSource = ds;
        chkRight.DataTextField = "pageDes";
        chkRight.DataValueField = "ID";
        chkRight.DataBind();
    }

    private string TreeNodeInsert(string sql, TreeNode node)
    {
        if (node.Checked)
        {
            sql += "Insert Into RoleRight(RightID,RoleID) Values('" + node.Value.ToString().Trim() + "','@@@'); ";
        }
        for (int i = 0; i < node.ChildNodes.Count; i++)
        {
            sql = TreeNodeInsert(sql, node.ChildNodes[i]);
        }
        return sql;
    }

    private string RoleInsert()
    {
        string sql = "";
        DataSet ds = new DataSet();

        if (txtProName.Text.ToString().Trim() == "")
        {
            return "请输入角色名称！";
        }

        string sqlInsert = "";

        for (int i = 0; i < tTV.Nodes.Count; i++)
        {
            sqlInsert = TreeNodeInsert(sqlInsert, tTV.Nodes[i]);
        }

        if (sqlInsert == "")
        {
            //return "请给角色分配至少一个权限！";
        }
        sql = "Select * From TbRole Where RoleName='" + txtProName.Text.ToString().Trim() + "' ";
        if (hfUserLeiXing.Value.ToString().Trim() == "企业")
        {
            sql += "And DepartMentID='" + Session["UserID"] + "' ";
        }
        else
        {
            sql += "And DepartMentID IS NULL ";
        }

        ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

        if (ds.Tables[0].Rows.Count > 0)
        {
            return "您录入的角色已存在，请重新输入！";
        }
        //插入数据库项目名称，并返回当前插入行的ID
        if (hfUserLeiXing.Value.ToString().Trim() == "企业")
        {
            sql = "Insert Into TbRole(RoleName,DepartMentID) Values('" + txtProName.Text.ToString().Trim() + "','" + Session["UserID"] + "')   select @@identity";
        }
        else
        {
            sql = "Insert Into TbRole(RoleName) Values('" + txtProName.Text.ToString().Trim() + "')   select @@identity";
        }
        ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

        if (sqlInsert != "")
        {
            sqlInsert = sqlInsert.Replace("@@@", ds.Tables[0].Rows[0][0].ToString().Trim());
            DBA.DbAccess.ExecNoQuery(CommandType.Text, sqlInsert);
        }
        return "";
    }

    private void TreeNodeUpdate(TreeNode node, string ID)
    {
        if (node.Checked)
        {
            string sql = "Insert Into RoleRight(RightID,RoleID) Values('" + node.Value.ToString().Trim() + "','" + ID + "')  ";
            DBA.DbAccess.ExecNoQuery(CommandType.Text, sql);
        }
        for (int i = 0; i < node.ChildNodes.Count; i++)
        {
            TreeNodeUpdate(node.ChildNodes[i], ID);
        }
    }


    private string RoleUpdate(string ID)
    {
        string sql;
        DataSet ds = new DataSet();
        if (txtProName.Text.ToString().Trim() == "")
        {
            return "请输入角色名称！";

        }

        sql = "Delete From RoleRight Where RoleID='" + ID + "'";
        DBA.DbAccess.ExecNoQuery(CommandType.Text, sql);

        for (int i = 0; i < tTV.Nodes.Count; i++)
        {
            TreeNodeUpdate(tTV.Nodes[i], ID);
        }


        sql = "Update TbRole Set RoleName='" + txtProName.Text.ToString().Trim() + "' Where ID='" + ID + "'";

        DBA.DbAccess.ExecNoQuery(CommandType.Text, sql);

        return "";
    }
    /// <summary>
    /// 插入新角色
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnQueDing_Click(object sender, EventArgs e)
    {
        string ID = Request.QueryString["ID"];
        string msg = "";

        if (ID != null && ID != "")
        {
            msg = RoleUpdate(ID);
        }
        else
        {
            msg = RoleInsert();
        }

        if (msg == "")
        {
            MSG.AlertReturn(Page, "操作成功!", "frmRoleInfo.aspx");
        }
        else
        {
            MSG.AlertMsg(Page, msg);
        }

    }

    /// <summary>
    /// 绑定权限
    /// </summary>
    private void RefTV()
    {
        DataSet ds = new DataSet();
        string sql = "";
        if (hfUserLeiXing.Value.ToString().Trim() == "企业")
        {
            sql = "Select ID,PageName,PageUrl,PageDes From TbRight Where NodeLevel=0 And ID IN (select distinct RightID from RoleRight where RoleID in (select RoleID from UserRole where UserID='" + Session["UserID"] + "')) Order BY XianShiShunXu ASC,ID ASC";
        }
        else
        {
            sql = "Select * From TbRight Where NodeLevel=0";
        }
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
            if (hfUserLeiXing.Value.ToString().Trim() == "企业")
            {
                sql = "Select ID,PageName,PageUrl,PageDes From TbRight Where NodeLevel='" + tTV.Nodes[i].Value.ToString().Trim() + "' And ID IN (select distinct RightID from RoleRight where RoleID in (select RoleID from UserRole where UserID='" + Session["UserID"] + "') )Order BY XianShiShunXu ASC,ID ASC";
            }
            else
            {
                sql = "Select * From TbRight Where NodeLevel=" + tTV.Nodes[i].Value.ToString().Trim();
            }
            ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                TreeNode node = new TreeNode(ds.Tables[0].Rows[j]["PageDes"].ToString().Trim(), ds.Tables[0].Rows[j]["ID"].ToString().Trim());
                tTV.Nodes[i].ChildNodes.Add(node);
            }
        }
    }

    protected void tTV_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        TreeNode currentNode = e.Node;
        bool ischecked = currentNode.Checked;
        checkChildNode(currentNode, ischecked);
    }
    private void checkChildNode(TreeNode node, bool isChecked)
    {
        foreach (TreeNode child in node.ChildNodes)
        {
            child.Checked = isChecked;
            if (child.ChildNodes.Count != 0)
            {
                checkChildNode(child, isChecked);
            }
        }
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmRoleInfo.aspx");
    }



    protected void tTV_TreeNodeCheckChanged1(object sender, TreeNodeEventArgs e)
    {
        TreeNodeCollection node = e.Node.ChildNodes;
        if (e.Node.Parent != null)
        {
            CheckNodeParent(e.Node, e.Node.Checked);
        }
        foreach (TreeNode item in node)
        {
            item.Checked = e.Node.Checked;
            CheckNode(item.ChildNodes, e.Node.Checked);
        }
        e.Node.ExpandAll();

    }

    private void CheckNodeParent(TreeNode node, bool selected)
    {
        if (!selected)
        {
            return;
        }
        TreeNode parNode = node.Parent;
        if (parNode != null)
        {
            parNode.Checked = selected;
            CheckNodeParent(parNode, selected);
        }
    }

    public void CheckNode(TreeNodeCollection node, bool selected)
    {
        foreach (TreeNode item in node)
        {
            item.Checked = selected;
            CheckNode(item.ChildNodes, selected);
        }
    }
}
