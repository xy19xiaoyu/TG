using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Patentquery.YJ
{
    public partial class frmGaiYao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string WID = Request.QueryString["WID"];
                if (!string.IsNullOrEmpty(WID))
                {
                    ProYJDLL.YJDB.getYJItemByWID(int.Parse(WID),0,1,1);
                }
            }

        }
    }
}