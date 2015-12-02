using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Patentquery.SysAdmin
{
    public partial class HelpFileMng : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                refGrv();
            }
        }

        protected void btnBaoCun_Click(object sender, EventArgs e)
        {
            bool flag = false;
            flag = ProXZQDLL.ClsOpinion.AddHelpFile(txtHelpTitle.Text.Trim(), hdiFileName.Value.Trim());

            refGrv();

            if (flag)
            {
                txtHelpTitle.Text = "";
                hdiFileName.Value = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('保存成功！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('保存失败，请检查输入内容是否正确后重试！');", true);
            }
        }

        private void refGrv()
        {
            List<ProXZQDLL.TbHelpFiles> lst = new List<ProXZQDLL.TbHelpFiles>();
            lst = ProXZQDLL.ClsOpinion.getSysHelpFile();
            grvInfo.DataSource = lst;
            grvInfo.DataBind();
        }


        protected void grv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            bool flag = false;
            flag = ProXZQDLL.ClsOpinion.DelHelpFile(int.Parse(e.Keys[0].ToString()));

            if (flag)
            {
                refGrv();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('删除成功！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('删除失败，请稍后重试！');", true);
            }
        }
    }
}