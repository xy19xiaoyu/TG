using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProXZQDLL;

namespace Patentquery.SysAdmin
{
    public partial class frmZZJGDMZ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string ID=Request.QueryString["ID"];
                if(System.String.IsNullOrEmpty(ID))
                {
                    return;
                }
                DataClasses1DataContext db = new DataClasses1DataContext();
                var result = from item in db.TbZhuZhiJGDMZ
                             where item.UserID == int.Parse(ID)
                             select item;

                if (result.Count() <= 0)
                {
                    return;
                }

                Response.Redirect("ZZJGDMZ/" + result.ToList()[0].Path.ToString().Trim());
            }
        }
    }
}