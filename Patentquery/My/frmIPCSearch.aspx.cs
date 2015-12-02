using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Patentquery.My
{
    public partial class frmIPCSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProXZQDLL.ClsLog.LogInsertLanMu(this, "导航检索"); // 
        }
    }
}