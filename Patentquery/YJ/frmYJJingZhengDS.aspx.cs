using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProYJDLL;
using System.Web.Services;

namespace Patentquery.YJ
{
    public partial class frmYJJingZhengDS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnChaXun_Click(object sender, EventArgs e)
        {
            //grvInfo.DataSource= YJDB.getYJ(txtKeyWord.Text.ToString().Trim(), 1);
            //grvInfo.DataBind();
        }
        [WebMethod]
        public static string GetAvailableTickets(string ID)
        {
            return "";
        }
    }
}