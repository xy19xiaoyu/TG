using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Patentquery.Comm
{
    public partial class AutoCollects : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string strSql = "select a.CollectId,a.AlbumId, b.Title as floder,a.Note,a.NoteDate from TLC_Collects a, TLC_Albums b where a.Pid={0} and a.AlbumId=b.AlbumId  and a.UserId={1}";

                    string strPid = Request.QueryString["PID"].Trim(); //8779247



                    GridView1.DataSource = DBA.SqlDbAccess.GetDataTable(CommandType.Text, string.Format(strSql, strPid, Convert.ToUInt32(Session["UserID"])));
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox txNote = (TextBox)GridView1.Rows[e.RowIndex].Controls[0].Controls[1];

            string strUpSql = "update TLC_Collects set Note='{1}' where CollectId={0}";

            DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, string.Format(strUpSql, e.Keys[0], txNote.Text));
        }
    }
}