using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Patentquery.YJ
{
    public partial class frmYJItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string C_ID=Request.QueryString["CID"];
     

                if(string.IsNullOrEmpty(C_ID))
                {
                    return;
                }
                grvInfo.DataSource = ProYJDLL.YJDB.getYJItem(int.Parse(C_ID));
                grvInfo.DataBind();
            }
        }
    }
}