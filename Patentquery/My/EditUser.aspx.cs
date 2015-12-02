using System;
using System.Collections;
using System.Collections.Generic;
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
using TLC.BusinessLogicLayer;
using ProXZQDLL;

public partial class My_EditUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Session["UserInfo"] != null)
            {
                ProXZQDLL.TbUser tb = (ProXZQDLL.TbUser)Session["UserInfo"];
                TextBoxTrueName.Text = tb.RealName;
                TextBoxMobile.Text = tb.ShouJi;
                TextBoxTel.Text = tb.LianXiDianHua;
                TextBoxAdds.Text = tb.TongXinDiZhi;
                txtEmail.Text = tb.EMail;
            }
            else
            {
                Response.Redirect("Hint.aspx?Message=" + Server.UrlEncode("用户不存在"));
            }
        }
    }

    protected void LinkButtonSave_Click(object sender, EventArgs e)
    {
        if (TextBoxTrueName.Text.ToString().Trim().Length > 50)
        {
            MSG.AlertMsg(Page, "您输入的姓名格式错误，请检查后输入！");
            return;
        }
        if (Session["UserID"] != null)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                db.Log = Console.Out;
                //取出

                var user = db.TbUser.SingleOrDefault<TbUser>(s => s.ID.ToString() == Session["UserID"].ToString().Trim());

                if (user == null)
                {
                    return;
                }


                user.RealName = TextBoxTrueName.Text.ToString().Trim();

                user.LianXiDianHua = TextBoxTel.Text.ToString().Trim();
                user.ShouJi = TextBoxMobile.Text.ToString().Trim();
                user.TongXinDiZhi = TextBoxAdds.Text.ToString().Trim();
                user.EMail = txtEmail.Text.ToString().Trim();
                /// user.EMail = txtYouXiang.Text.ToString().Trim();
                /// 
                if (TextBoxPassword.Text != "")
                {
                    user.UserPWD = TextBoxPassword.Text.ToString().Trim();
                }

                //执行更新操作
                db.SubmitChanges();
                Session["UserInfo"] = user;
            }

        }
        MSG.AlertMsg(Page, "修改成功！");
        //PanelView.Visible = false;
        //PanelResult.Visible = true;
    }   
}
