using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ProXZQDLL;

namespace Patentquery.SysAdmin
{
    public partial class frmRegedit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtPWD.Attributes["value"] = txtPWD.Text.ToString().Trim();
            txtQueRen.Attributes["value"] = txtQueRen.Text.ToString().Trim();

            if (!IsPostBack)
            {
                ZZJGDMZ1.Visible = false;
                ZZJGDMZ2.Visible = false;

                QYMC1.Visible = false;
                QYMC2.Visible = false;

            }
        }

        private string UserInsert()
        {
            string sql = "";
            DataSet ds = new DataSet();

            if (this.txtUserName.Text.ToString().Trim() == "")
            {
                return "请输入登录名称！";
            }
            sql = "Select * From TbUser Where UserName='" + txtUserName.Text.ToString().Trim() + "' ";

            ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return "您录入的登录名已存在，请重新输入！";
            }
            if (this.txtRealName.Text.ToString().Trim() == "")
            {
                return "请输入真实姓名！";
            }
            if (this.txtPWD.Text.ToString().Trim() == "")
            {

                return "请输入密码";
            }
            if (txtPWD.Text.ToString().Trim().Length > 50)
            {
                return "密码超长，请重新输入！";
            }

            if (txtPWD.Text.ToString().Trim() != txtQueRen.Text.ToString().Trim())
            {
                return "您两次输入的密码不一致,请重新输入！";
            }

            if (txtYouXiang.Text.ToString().Trim() == "")
            {
                return "请输入您的邮箱地址！";
            }
            string ZZJGDMZ = "";
            if (rbtYongHuLeiXing.SelectedValue == "企业")
            {
                if (txtQiYeMingCheng.Text.ToString().Trim() == "")
                {
                    return "请输入企业名称！";
                }
                if (!fl.HasFile)
                {
                    return "请选择要上传的组织结构代码证！";
                }

                if (System.IO.Path.GetExtension(fl.FileName).ToLower().Trim() != ".jpg")
                {

                }

                ZZJGDMZ = Guid.NewGuid() + System.IO.Path.GetExtension(fl.FileName).ToLower().Trim();
                fl.SaveAs(Server.MapPath("ZZJGDMZ") + "\\" + ZZJGDMZ);
            }
            TbUser user = new TbUser();

            user.UserName = txtUserName.Text.ToString().Trim();
            user.UserPWD = txtPWD.Text.ToString().Trim();
            user.RealName = txtRealName.Text.ToString().Trim();
            user.YongHuLeiXing = rbtYongHuLeiXing.SelectedValue.ToString().Trim();
            user.LianXiDianHua = txtDianHua.Text.ToString().Trim();
            user.ShouJi = txtShouJi.Text.ToString().Trim();
            user.TongXinDiZhi = txtDiZhi.Text.ToString().Trim();
            user.EMail = txtYouXiang.Text.ToString().Trim();
            user.DepartMentID = 0;
            user.SHFlag = 0;
            user.QiYeMingCheng = txtQiYeMingCheng.Text.ToString().Trim();

            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                db.Log = Console.Out;
                db.TbUser.InsertOnSubmit(user);
                db.SubmitChanges();
            }


            Session["UserID"] = user.ID.ToString().Trim();

            Session["RealName"] = user.RealName.ToString().Trim();

            Session["UserInfo"] = ProXZQDLL.UserRight.getUserInfo(Session["UserID"].ToString().Trim());

            if (ZZJGDMZ.Trim() != "")
            {
                InsertZZJG(int.Parse(Session["UserID"].ToString()), ZZJGDMZ);
            }
            return "";
        }

        protected void LinkButtonSave_Click(object sender, EventArgs e)
        {
            string flag = "";

            flag = UserInsert();

            if (flag == "")
            {
                if (rbtYongHuLeiXing.SelectedValue == "企业")
                {
                    string sqlZTK = "insert into ZtDbList(ztDbName,dbType,createUserId) values('企业在线数据库','1','" + Session["UserID"].ToString().Trim() + "')";
                    DBA.DbAccess.ExecNoQuery(CommandType.Text, sqlZTK);
                }
                string sqlShouCang = "insert into TLC_Albums (UserId,ParentId,Title,live,isdel,isparent) values('" + Session["UserID"].ToString().Trim() + "',0,'收藏夹',0,0,0)";
                DBA.DbAccess.ExecNoQuery(CommandType.Text, sqlShouCang);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('注册成功，请您注意查看邮箱关注审核结果！');document.location.href='../my/'", true);

                //MSG.AlertMsg(Page, "操作成功", "../my/");
                //MSG.AlertReturn(Page, "操作成功！", "frmUserInfo.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('" + flag + "！');", true);

            }
        }

        protected void rbtYongHuLeiXing_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtYongHuLeiXing.SelectedValue == "企业")
            {
                ZZJGDMZ1.Visible = true;
                ZZJGDMZ2.Visible = true;
                QYMC1.Visible = true;
                QYMC2.Visible = true;
                Label2.Text = "*(请输入组织机构代码)";
            }
            else
            {
                ZZJGDMZ1.Visible = false;
                ZZJGDMZ2.Visible = false;
                QYMC1.Visible = false;
                QYMC2.Visible = false;
                Label2.Text = "*";
            }
        }

        private void InsertZZJG(int UserID, string filePath)
        {
            TbZhuZhiJGDMZ tb = new TbZhuZhiJGDMZ();
            tb.UserID = UserID;
            tb.Path = filePath;

            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                db.TbZhuZhiJGDMZ.InsertOnSubmit(tb);
                db.SubmitChanges();
            }
        }

    }
}