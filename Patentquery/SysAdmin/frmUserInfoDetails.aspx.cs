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
using System.Text;
using System.Net;
using System.Net.Mail;

public partial class frmUserInfoDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //UserRight.Check(Page);
        if (!IsPostBack)
        {
            hfUserLeiXing.Value = UserRight.getUserLeiXing(Session["UserID"].ToString().Trim());

            if (hfUserLeiXing.Value.ToString().Trim() == "企业")
            {
                trUserLeiXing.Visible = false;
                //trJueSe.Visible = false;
                string strQyMc = ((ProXZQDLL.TbUser)Session["UserInfo"]).QiYeMingCheng;
                txtQiYeMingCheng.Text = string.IsNullOrEmpty(strQyMc) ? "" : strQyMc;
            }
            BindRole();
            string ID = Request.QueryString["ID"];

            if (ID != null && ID != "")
            {

                BindData(ID);
            }

            string ShenHe = Request.QueryString["SH"];
            if (!string.IsNullOrEmpty(ShenHe))
            {
                pwd.Visible = false;
                btnQueDing.Text = "审核通过";
            }
        }
    }

    private void BindData(string ID)
    {
        //txtPWD.Enabled = false;
        //txtRealName.Enabled = false;
        txtUserName.Enabled = false;

        DataSet ds = new DataSet();


        DataClasses1DataContext db = new DataClasses1DataContext();
        var result = from item in db.TbUser
                     where item.ID.ToString().Trim() == ID
                     select item;

        if (result.Count() <= 0)
        {
            return;
        }
        txtUserName.Text = result.ToList()[0].UserName;
        txtRealName.Text = result.ToList()[0].RealName;
        txtPWD.Text = result.ToList()[0].UserPWD;
        hfPWD.Value = result.ToList()[0].UserPWD;

        ddlYongHuLX.SelectedValue = result.ToList()[0].YongHuLeiXing.ToString().Trim();
        txtDianHua.Text = result.ToList()[0].LianXiDianHua;
        txtShouJi.Text = result.ToList()[0].ShouJi;
        txtDiZhi.Text = result.ToList()[0].TongXinDiZhi;
        txtYouXiang.Text = result.ToList()[0].EMail;

        txtQiYeMingCheng.Text = result.ToList()[0].QiYeMingCheng;

        var result2 = from item2 in db.UserRole
                      where item2.UserID == Convert.ToInt32(ID)
                      select item2;

        for (int i = 0; i < result2.Count(); i++)
        {
            for (int j = 0; j < chkRole.Items.Count; j++)
            {
                if (result2.ToList()[i].RoleID.ToString().Trim() == chkRole.Items[j].Value.ToString().Trim())
                {
                    chkRole.Items[j].Selected = true;
                }
            }
        }

        if (result2.Count() == 0)
        {
            for (int j = 0; j < chkRole.Items.Count; j++)
            {
                if (chkRole.Items[j].Text.ToString().Trim() == "注册用户")
                {
                    chkRole.Items[j].Selected = true;
                }
            }
        }
    }

    /// <summary>
    /// 角色信息
    /// </summary>
    private void BindRole()
    {
        DataSet ds = new DataSet();
        string sql = "select ID,RoleName from TbRole ";
        if (hfUserLeiXing.Value.ToString().Trim() == "企业")
        {
            sql += "Where DepartMentID='" + Session["UserID"] + "'";
            sql = @"select  ID,RoleName from TbRole where ID in 
                    (select distinct RoleID from UserRole where UserID=" + Session["UserID"] + ")";
        }
        else
        {
            sql += "Where DepartMentID IS NULL";
        }

        ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

        chkRole.DataSource = ds;
        chkRole.DataTextField = "RoleName";
        chkRole.DataValueField = "ID";
        chkRole.DataBind();
    }

    private string UserInsert()
    {
        string sql = "";
        DataSet ds = new DataSet();
        bool RoleFlag = false;

        if (this.txtUserName.Text.ToString().Trim() == "")
        {
            return "请输入登录名称！";
        }

        if (this.txtRealName.Text.ToString().Trim() == "")
        {
            return "请输入真实姓名！";
        }

        if (txtPWD.Text.ToString().Trim().Length > 50)
        {
            return "密码超长，请重新输入！";
        }

        if (this.txtPWD.Text.ToString().Trim() == "")
        {

            return "请输入密码";
        }

        sql = "Select * From TbUser Where UserName='" + txtUserName.Text.ToString().Trim() + "' ";

        ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

        if (ds.Tables[0].Rows.Count > 0)
        {
            return "您录入的登录名已存在，请重新输入！";
        }

        string sqlInsert = "";

        //插入数据库项目名称，并返回当前插入行的ID
        if (false && hfUserLeiXing.Value.ToString().Trim() == "企业")
        {

        }
        else
        {
            for (int i = 0; i < chkRole.Items.Count; i++)
            {
                if (chkRole.Items[i].Selected)
                {
                    sqlInsert += "Insert Into UserRole(RoleID,UserID) Values('" + chkRole.Items[i].Value.ToString().Trim() + "','@@@'); ";
                    RoleFlag = true;
                }
            }

            if (!RoleFlag)
            {
                return "请给用户分配至少一个角色";
            }
        }


        TbUser user = new TbUser();

        user.UserName = txtUserName.Text.ToString().Trim();
        user.UserPWD = txtPWD.Text.ToString().Trim();
        user.RealName = txtRealName.Text.ToString().Trim();
        user.YongHuLeiXing = ddlYongHuLX.SelectedValue.ToString().Trim();
        user.LianXiDianHua = txtDianHua.Text.ToString().Trim();
        user.ShouJi = txtShouJi.Text.ToString().Trim();
        user.TongXinDiZhi = txtDiZhi.Text.ToString().Trim();
        user.EMail = txtYouXiang.Text.ToString().Trim();
        user.DepartMentID = 0;
        user.SHFlag = 1;

        user.QiYeMingCheng = txtQiYeMingCheng.Text.ToString().Trim();

        //插入数据库项目名称，并返回当前插入行的ID
        if (hfUserLeiXing.Value.ToString().Trim() == "企业")
        {
            user.DepartMentID = Convert.ToInt32(Session["UserID"].ToString().Trim());
            user.YongHuLeiXing = "企业";
        }
        string DepID = UserRight.getDepartMentID(Session["UserID"].ToString());

        if (DepID != "0")
        {
            user.DepartMentID = int.Parse(DepID);
        }

        using (DataClasses1DataContext db = new DataClasses1DataContext())
        {
            db.Log = Console.Out;
            db.TbUser.InsertOnSubmit(user);
            db.SubmitChanges();
        }


        // 企业用户新建出的用户角色
        if (false && hfUserLeiXing.Value.ToString().Trim() == "企业")
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var result = from item in db.UserRole
                         where item.UserID.ToString().Trim() == Session["UserID"].ToString().Trim()
                         select item;
            foreach (var item in result)
            {
                UserRole userrole = new UserRole();
                userrole.RoleID = item.RoleID;
                userrole.UserID = user.ID;
                db.UserRole.InsertOnSubmit(userrole);
                db.SubmitChanges();
            }
        }
        else//系统用户建出的用户的角色
        {

            sqlInsert = sqlInsert.Replace("@@@", user.ID.ToString().Trim());
            DBA.DbAccess.ExecNoQuery(CommandType.Text, sqlInsert);
        }
        if (ddlYongHuLX.SelectedValue.ToString().Trim() == "企业")
        {
            string sqlZTK = "insert into ZtDbList(ztDbName,dbType,createUserId) values('企业在线数据库','1','" + user.ID + "')";
            DBA.DbAccess.ExecNoQuery(CommandType.Text, sqlZTK);
        }
        string sqlShouCang = "insert into TLC_Albums (UserId,ParentId,Title,live,isdel,isparent) values('" + user.ID + "',0,'收藏夹',0,0,0)";
        DBA.DbAccess.ExecNoQuery(CommandType.Text, sqlShouCang);
        return "";
    }


    private string UserUpdate(string ID)
    {
        string sql = "";
        DataSet ds = new DataSet();
        bool RoleFlag = false;

        if (this.txtUserName.Text.ToString().Trim() == "")
        {
            return "请输入登录名称！";
        }

        if (this.txtRealName.Text.ToString().Trim() == "")
        {
            return "请输入真实姓名！";
        }

        if (txtPWD.Text.ToString().Trim().Length > 50)
        {
            return "密码超长，请重新输入！";
        }

        if (txtPWD.Text.ToString().Trim() == "")
        {
            if (hfPWD.Value != "")
            {
                txtPWD.Text = hfPWD.Value;
            }
            else
            {
                return "请输入密码";
            }
        }

        if (hfUserLeiXing.Value.ToString().Trim() != "企业")
        {
            sql = "Delete From UserRole Where UserID='" + ID + "'";
            DBA.DbAccess.ExecNoQuery(CommandType.Text, sql);

            for (int i = 0; i < chkRole.Items.Count; i++)
            {
                if (chkRole.Items[i].Selected)
                {
                    sql = "Insert Into UserRole(RoleID,UserID) Values('" + chkRole.Items[i].Value.ToString().Trim() + "','" + ID + "'); ";
                    DBA.DbAccess.ExecNoQuery(CommandType.Text, sql);
                    RoleFlag = true;
                }
            }

            if (!RoleFlag)
            {
                return "请给用户分配至少一个角色！";
            }
        }

        using (DataClasses1DataContext db = new DataClasses1DataContext())
        {
            db.Log = Console.Out;
            //取出

            var user = db.TbUser.SingleOrDefault<TbUser>(s => s.ID.ToString() == ID);

            if (user == null)
            {
                return "未查询到符合条件的数据!";
            }

            user.UserName = txtUserName.Text.ToString().Trim();
            user.UserPWD = txtPWD.Text.ToString().Trim();
            user.RealName = txtRealName.Text.ToString().Trim();

            user.YongHuLeiXing = ddlYongHuLX.SelectedValue.ToString().Trim();

            user.LianXiDianHua = txtDianHua.Text.ToString().Trim();
            user.ShouJi = txtShouJi.Text.ToString().Trim();
            user.TongXinDiZhi = txtDiZhi.Text.ToString().Trim();
            user.EMail = txtYouXiang.Text.ToString().Trim();
            user.SHFlag = 1;
            user.QiYeMingCheng = txtQiYeMingCheng.Text.ToString().Trim();

            //执行更新操作
            db.SubmitChanges();
        }

        return "";
    }
    /// <summary>
    /// 确定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnQueDing_Click(object sender, EventArgs e)
    {
        string ID = Request.QueryString["ID"];
        string flag = "";

        if (ID != null && ID != "")
        {
            flag = UserUpdate(ID);
        }
        else
        {
            flag = UserInsert();
        }
        //发送通知邮件
        if (chkEmail.Checked)
        {
            SendMail();
        }

        if (flag == "")
        {
            MSG.AlertReturn(Page, "操作成功！", "frmUserInfo.aspx");
        }
        else
        {
            MSG.AlertMsg(Page, flag);
        }
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmUserInfo.aspx");
    }

    /// <summary>
    /// 发送邮件的程序
    /// </summary>
    /// <param name="UserName"> 登陆邮箱的用户名 </param>
    /// <param name="UserName"> 登陆邮箱的密码  </param>
    /// <returns></returns>
    private bool SendMail(string UserName, string PassWord, string ShoujianRenEmail, string body)
    {
        //System.IO.StreamReader sr = new System.IO.StreamReader(@"D:\aaaaaaaaaaa\huiyuan.htm",Encoding.Default );
        //body = sr.ReadToEnd();
        //sr.Close();

        try
        {
            //编码暂硬性规定为GB2312 
            Encoding encoding = Encoding.GetEncoding("utf-8");
            MailMessage Message = new MailMessage(
            new MailAddress(System.Configuration.ConfigurationSettings.AppSettings["email"].ToString().Trim(), System.Configuration.ConfigurationSettings.AppSettings["UserRealName"].ToString().Trim(), encoding),//第一个是发信人的地址，第二个参数是发信人
            new MailAddress(ShoujianRenEmail));//收信人邮箱
            Message.SubjectEncoding = encoding;
            Message.Subject = "厦漳泉科技基础资源服务平台[用户权限变更通知]";//标题
            Message.BodyEncoding = encoding;
            Message.Body = body; //主体

            SmtpClient smtpClient = new SmtpClient(System.Configuration.ConfigurationSettings.AppSettings["SmtpClient"].ToString().Trim());//信箱服务器
            smtpClient.Credentials = new NetworkCredential(UserName, PassWord);//信箱的用户名和密码
            smtpClient.Timeout = 999999;
            smtpClient.Send(Message);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    private void SendMail()
    {
        string role = "";
        string email = "";
        DataSet ds = new DataSet();
        string sql = "Select * From TbUser Where UserName='" + txtUserName.Text.ToString().Trim() + "'";

        try
        {
            ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);
        }
        catch (Exception ex)
        {
        }

        if (ds.Tables[0].Rows.Count <= 0)
        {
        }
        email = ds.Tables[0].Rows[0]["EMail"].ToString().Trim();

        SendMail(System.Configuration.ConfigurationSettings.AppSettings["UserName"].ToString().Trim(),
            System.Configuration.ConfigurationSettings.AppSettings["PWD"].ToString().Trim(),
            email, txbMailBody.Text);
    }

    protected void btnCreatMailBody_Click(object sender, EventArgs e)
    {
        string strRoleIds = "";
        string strRoles = "";
        try
        {
            for (int i = 0; i < chkRole.Items.Count; i++)
            {
                if (chkRole.Items[i].Selected)
                {
                    strRoleIds += "," + chkRole.Items[i].Value.ToString().Trim();
                }
            }
            strRoleIds = strRoleIds.TrimStart(',');

            if (!string.IsNullOrEmpty(strRoleIds))
            {
                string sql = string.Format("select RoleName from TbRole Where ID in ({0})", strRoleIds);
                DataTable dtRols = DBA.DbAccess.GetDataTable(CommandType.Text, sql);

                for (int i = 0; i < dtRols.Rows.Count; i++)
                {
                    strRoles += dtRols.Rows[i]["RoleName"].ToString().Trim() + ";";
                }
                strRoles = strRoles.TrimEnd(';');
            }
        }
        catch (Exception ex)
        {
        }
        string strTmpUrl = Request.Url.OriginalString.ToLower();
        strTmpUrl = strTmpUrl.Substring(0, strTmpUrl.IndexOf("/sysadmin/frmuserinfodetails.aspx"));
        txbMailBody.Text = string.Format("您的角色为：[{0}],具体信息请登陆厦漳泉科技基础资源服务平台({1}/)查询", strRoles, strTmpUrl);
    }

    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        SendMail();
        MSG.AlertMsg(Page, "发送成功！");
    }
}
