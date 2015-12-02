using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using test.WebReference;

namespace test
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WebReference.CprsMobileExtSvc webser = new WebReference.CprsMobileExtSvc();
            
            NewsInfo[] lst= webser.GetNewsList(1, 10);
        }
    }
}
