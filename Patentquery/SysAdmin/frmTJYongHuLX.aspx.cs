using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Patentquery.SysAdmin
{
    public partial class frmTJYongHuLX : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                refGrv();
            }
        }

        private void refGrv()
        {
            List<ProXZQDLL.ClsYongHuLX> list= ProXZQDLL.ClsTJ.getYongHuLX();

            grvInfo.DataSource = list;
            grvInfo.DataBind();
        }
    }
}