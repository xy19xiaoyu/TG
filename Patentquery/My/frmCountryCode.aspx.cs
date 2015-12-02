using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Patentquery.My
{
    public partial class frmCountryCode : System.Web.UI.Page
    {
        public int recordCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void grvRsData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvRsData.PageIndex = e.NewPageIndex;
            btnSearch_Click(null, null);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string sql = "select top 10 DaiMa CCode,MingCheng CName from CountryConfig where DaiMa like '%{0}%' or MingCheng like '%{0}%'";
            DataTable dt = new DataTable();
            dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, string.Format(sql, this.TextBox1.Text.Trim()), null);
            recordCount = dt.Rows.Count;
            grvRsData.DataSource = dt;
            grvRsData.DataBind();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {

                grvRsData.PageIndex = int.Parse(((TextBox)grvRsData.BottomPagerRow.FindControl("txtGoPagestock")).Text) - 1;
            }
            catch
            {
                return;
            }
            btnSearch_Click(null, null);
        }
    }
}