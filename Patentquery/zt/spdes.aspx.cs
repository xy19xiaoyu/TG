using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace th.TH
{
    public partial class spdes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["NodeId"] != null)
            {
                Response.Write("spdes-NodeId:" + Request["NodeId"].ToString());
            }
           
        }
    }
}
