using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Patentquery.My
{
    public partial class Opinion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string strSql = "Insert into TbOpinion(UID,UName,Title,LYtxt) values ({0},'{1}','{2}','{3}')";
                DBA.DbAccess.ExecNoQuery(System.Data.CommandType.Text, string.Format(strSql, Convert.ToInt32(Session["UserID"]),
                    txbUName.Text.Trim(), txbTitle.Text.Trim(), txbContent.Text.Trim()));
                ClientScript.RegisterStartupScript(this.GetType(), "js", "<script>alert('提交成功，感谢您的参与!')</script>");
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "js", "<script>alert('提交失败，请重试!')</script>");
            }
        }
    }
}