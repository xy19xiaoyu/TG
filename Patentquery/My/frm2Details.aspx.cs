using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cpic.Cprs2010.Cfg;

namespace Patentquery.My
{
    public partial class frm2Details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string ID = Request.QueryString["ID"];
                if (ID.Trim().Length % 2 != 0)
                {
                    ID = ID.Substring(0, ID.Trim().Length - 1);
                }
                ID = UrlParameterCode_DE.encrypt(ID);
                Response.Redirect("frmPatDetails.aspx?Id=" + ID);
            }
        }
    }
}