using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Patentquery.My
{
    public partial class frmDoSQ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["Query"]))
                {
                    hidSearchTxt.Value = "";
                }
                else
                {
                    hidSearchTxt.Value = Request.QueryString["Query"].ToString().Trim();
                }
            }
        }
    }
}