using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLC;

namespace Patentquery.zt
{
    public partial class ztbatchsp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PQDataContext db = new PQDataContext();
            IQueryable<ZtDbList> result = from x in db.ZtDbList
                                          where x.dbType.Value == 0
                                          select x;
            this.ztnamelist.DataSource = result;
            
            this.ztnamelist.DataTextField = "ztDbName";
            this.ztnamelist.DataValueField = "DbID";
            this.ztnamelist.DataBind();
                
        }
    }
}