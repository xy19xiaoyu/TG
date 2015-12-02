using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Patentquery.SysAdmin
{
    public partial class frmMailLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDateStart.Text = DateTime.Now.ToShortDateString();
                txtDateEnd.Text = DateTime.Now.ToShortDateString();
            }
        }
        protected void btnChaXun_Click(object sender, EventArgs e)
        {
            RefGrv();
        }

        private void RefGrv()
        {
            grvInfo.DataSource = ProXZQDLL.ClsLog.QuerySendMailLog(txtShouJianRen.Text.ToString().Trim(), txtDateStart.Text.ToString().Trim(), txtDateEnd.Text.ToString().Trim());
            grvInfo.DataBind();
        }

        protected void grvInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvInfo.PageIndex = e.NewPageIndex;
            RefGrv();
        }
    }
}