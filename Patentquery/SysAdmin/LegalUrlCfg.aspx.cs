using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Patentquery.SysAdmin
{
    public partial class LegalUrlCfg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                refGrv();
            }
        }

        private void refGrv()
        {
            List<ProXZQDLL.TbLegalUrl_Cfg> lst = new List<ProXZQDLL.TbLegalUrl_Cfg>();
            lst = ProXZQDLL.ClsLog.getTbLegal();
            grvInfo.DataSource = lst;
            grvInfo.DataBind();
        }

        protected void btnXuanZe_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow grv = (GridViewRow)(btn.Parent.Parent);

            txtGuoBie.Text = grv.Cells[0].Text.ToString().Trim().Replace("&nbsp;", "");
            txtMiaoShu.Text = grv.Cells[1].Text.ToString().Trim().Replace("&nbsp;", "");
            txtWangZhi.Text = grv.Cells[2].Text.ToString().Trim().Replace("&nbsp;", "");
        }

        protected void btnBaoCun_Click(object sender, EventArgs e)
        {
            if (txtGuoBie.Text.ToString().Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('请输入国别！');", true);
                return;
            }

            bool flag = false;
            flag = ProXZQDLL.ClsLog.TbLegalOperate(txtGuoBie.Text.ToString().Trim(), txtMiaoShu.Text.ToString().Trim(), txtWangZhi.Text.ToString().Trim());
           
            refGrv();

            if (flag)
            {
                txtGuoBie.Text = "";
                txtMiaoShu.Text = "";
                txtWangZhi.Text = "";
                SearchInterface.XmPatentComm.InitLegUrlCfg();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('操作成功！');", true);                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('操作失败，请检查输入内容是否正确后重试！');", true);
            }

        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow grv = (GridViewRow)(btn.Parent.Parent);

            bool flag = false;
            flag = ProXZQDLL.ClsLog.TbLegalDel(grv.Cells[0].Text.ToString().Trim());            
            refGrv();
            if (flag)
            {
                SearchInterface.XmPatentComm.InitLegUrlCfg();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('操作成功！');", true);                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('操作失败，请检查输入内容是否正确后重试！');", true);
            }
        }
    }
}