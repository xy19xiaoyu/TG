using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using TLC.BusinessLogicLayer;

public partial class My_Help : System.Web.UI.Page
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
        List<ProXZQDLL.TbHelpFiles> lst = new List<ProXZQDLL.TbHelpFiles>();
        lst = ProXZQDLL.ClsOpinion.getSysHelpFile();
        grvInfo.DataSource = lst;
        grvInfo.DataBind();
    }

    protected void grvInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grvInfo.PageIndex = e.NewPageIndex;
        refGrv();
    }
}
