using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Patentquery.My
{
    public partial class frmSearchIPCIndex : System.Web.UI.Page
    {
        public int recordCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlClassifyType.SelectedValue == "ECLA")
            {
                SearchIPCEcla(txtIpc.Text);
            }
            else if (ddlClassifyType.SelectedValue == "UC")
            {
                SearchIPCUC(txtIpc.Text);
            }
            else if (ddlClassifyType.SelectedValue == "FT")
            {
                SearchIPCFT(txtIpc.Text);
            }
            else if (ddlClassifyType.SelectedValue == "FI")
            {
                SearchIPCFI(txtIpc.Text);
            }
        }
        private void SearchIPCEcla(string ipc)
        {
            string sql = "select ipc,ecla as class from ipc_ecla where ipc like '" + ipc + "%'";
            DataTable dt = new DataTable();
            dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql, null);
            recordCount = dt.Rows.Count;
            grvIPC.DataSource = dt;
            grvIPC.DataBind();
        }

        private void SearchIPCUC(string ipc)
        {
            string sql = "select uc  as class,ipcs as ipc from ipc_uc where ipc like '" + ipc + "%'";
            DataTable dt = new DataTable();
            dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql, null);
            recordCount = dt.Rows.Count;
            grvIPC.DataSource = dt;
            grvIPC.DataBind();
        }
        private void SearchIPCFI(string ipc)
        {
            string sql = "select * from ipcFI where FI like '" + ipc + "%'";
            DataTable dt = new DataTable();
            dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql, null);
            recordCount = dt.Rows.Count;
            grvIPC.DataSource = dt;
            grvIPC.DataBind();
        }

        private void SearchIPCFT(string ipc)
        {
            string sql = "select ipc,FT as class,FTEDESC as des from ipc_FT where ipc like '" + ipc + "%'";
            DataTable dt = new DataTable();
            dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql, null);
            recordCount = dt.Rows.Count;
            grvIPC.DataSource = dt;
            grvIPC.DataBind();
        }

        protected void grvIPC_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvIPC.PageIndex = e.NewPageIndex;
            if (ddlClassifyType.SelectedValue == "ECLA")
            {
                SearchIPCEcla(txtIpc.Text);
            }
            else if (ddlClassifyType.SelectedValue == "UC")
            {
                SearchIPCUC(txtIpc.Text);
            }
            else if (ddlClassifyType.SelectedValue == "FT")
            {

            }
            else if (ddlClassifyType.SelectedValue == "FI")
            {
                SearchIPCFI(txtIpc.Text);
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {

                grvIPC.PageIndex = int.Parse(((TextBox)grvIPC.BottomPagerRow.FindControl("txtGoPagestock")).Text) - 1;
            }
            catch
            {
                return;
            }
            if (ddlClassifyType.SelectedValue == "ECLA")
            {
                SearchIPCEcla(txtIpc.Text);
            }
            else if (ddlClassifyType.SelectedValue == "UC")
            {
                SearchIPCUC(txtIpc.Text);
            }
            else if (ddlClassifyType.SelectedValue == "FT")
            {

            }
            else if (ddlClassifyType.SelectedValue == "FI")
            {
                SearchIPCFI(txtIpc.Text);
            }
        }

    }
}