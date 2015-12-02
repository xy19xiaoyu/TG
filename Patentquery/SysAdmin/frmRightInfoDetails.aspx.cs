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

public partial class frmRightInfoDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //UserRight.Check(Page);
        if (!IsPostBack)
        {
            BindDdlUp();
            string ID = Request.QueryString["ID"];

            if (ID != null && ID != "")
            {
                BindData(ID);
            }
        }
    }
    private void BindData(string ID)
    {

        DataClasses1DataContext db = new DataClasses1DataContext();
        var result = from item in db.TbRight
                     where item.ID.ToString() == ID
                     select item;

        if (result.Count() <= 0)
        {
            return;
        }

        txtRightName.Text = result.ToList()[0].PageDes.ToString().Trim();
        txtRightCode.Text = result.ToList()[0].PageName.ToString().Trim();
        ddlUp.SelectedValue = result.ToList()[0].Nodelevel.ToString().Trim();
        txtShunXu.Text = result.ToList()[0].XianShiShunXu.ToString().Trim();
        if (result.ToList()[0].XianShiFlag.ToString().Trim() == "1")
        {
            chkXianShi.Checked = true;
        }
        else
        {
            chkXianShi.Checked = false;
        }
    }

    private void BindDdlUp()
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        var result = from item in db.TbRight
                     where item.Nodelevel == 0
                     select item;
        ddlUp.Items.Clear();
        ddlUp.Items.Add("根目录");
        ddlUp.Items[0].Value = "0";
        for (int i = 0; i < result.Count(); i++)
        {
            ddlUp.Items.Add(result.ToList()[i].PageDes);
            ddlUp.Items[i + 1].Value = result.ToList()[i].ID.ToString();
        }

    }


    private string RoleInsert()
    {
        string sql = "";
        DataSet ds = new DataSet();

        if (txtRightName.Text.ToString().Trim() == "")
        {
            return "请输入权限名称！";
        }
        if (txtRightCode.Text.ToString().Trim() == "")
        {
            return "请输入权限/URL！";
        }

        TbRight right = new TbRight();
        right.PageName = txtRightCode.Text.ToString().Trim();
        right.PageUrl  = txtRightCode.Text.ToString().Trim();
        right.PageDes = txtRightName.Text.ToString().Trim();
        right.Nodelevel = int.Parse(ddlUp.SelectedValue.ToString().Trim());
        right.XianShiFlag = chkXianShi.Checked ? 1 : 0;
        right.XianShiShunXu = int.Parse(txtShunXu.Text.ToString().Trim());
        using (DataClasses1DataContext db = new DataClasses1DataContext())
        {
            db.Log = Console.Out;
            db.TbRight.InsertOnSubmit(right);
            db.SubmitChanges();
        }

        return "";
    }



    private string RoleUpdate(string ID)
    {
        string sql;
        DataSet ds = new DataSet();
        if (txtRightName.Text.ToString().Trim() == "")
        {
            return "请输入权限名称！";

        }

        if (txtRightCode.Text.ToString().Trim() == "")
        {
            return "请输入权限/URL！";
        }

        using (DataClasses1DataContext db = new DataClasses1DataContext())
        {
            db.Log = Console.Out;
            //取出

            var right = db.TbRight.SingleOrDefault<TbRight>(s => s.ID.ToString() == ID);

            if (right == null)
            {
                return "未查询到符合条件的数据!";
            }

            right.PageDes = txtRightName.Text.ToString().Trim();
            right.PageName = txtRightCode.Text.ToString().Trim();
            right.PageUrl = txtRightCode.Text.ToString().Trim();

            right.Nodelevel = int.Parse(ddlUp.SelectedValue.ToString().Trim());
            right.XianShiFlag = chkXianShi.Checked ? 1 : 0;
            right.XianShiShunXu = int.Parse(txtShunXu.Text.ToString().Trim());

            //执行更新操作
            db.SubmitChanges();
        }

        return "";
    }
    /// <summary>
    /// 插入新权限
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
            MSG.AlertReturn(Page, "操作成功!", "frmRightInfo.aspx");
        }
        else
        {
            MSG.AlertMsg(Page, msg);
        }

    }


    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmRightInfo.aspx");
    }



}
