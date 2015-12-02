using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProYJDLL;
using System.Data;

namespace Patentquery.Comm
{
    public partial class yjitems : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string C_ID = Request.QueryString["CID"];


            if (string.IsNullOrEmpty(C_ID))
            {
                return;
            }
            DataTable res = ProYJDLL.YJDB.getYJItemxy(int.Parse(C_ID));
            Response.Write(JsonHelper.DatatTableToJson1(res, "rows", res.Rows.Count));          
        }
    }
}