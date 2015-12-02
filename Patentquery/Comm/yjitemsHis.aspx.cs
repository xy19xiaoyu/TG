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
    public partial class yjitemsHis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string W_ID = Request.QueryString["WID"];


            if (string.IsNullOrEmpty(W_ID))
            {
                return;
            }
            DataTable res = ProYJDLL.YJDB.getYJItemHis(int.Parse(W_ID));
            Response.Write(JsonHelper.DatatTableToJson1(res, "rows", res.Rows.Count));          
        }
    }
}