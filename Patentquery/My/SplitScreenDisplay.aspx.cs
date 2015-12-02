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
using Cpic.Cprs2010.Search.ResultData;
using TLC.BusinessLogicLayer;

public partial class My_SplitScreenDisplay : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Id"] != null && Request.QueryString["Id"] != "")
            {
                String strId = Request.QueryString["Id"].Trim();
                String type = Request.QueryString["type"];
                if (type == null || type.Equals ("")) {
                    type = "CN";
                } else {
                    type = type.Trim();
                }
                String patType = Request.QueryString["patType"];
                if (patType == null || patType.Equals ("")) {
                    patType = "0";
                }
                else {
                    patType = patType.Trim();
                }

                LiteralLeft.Text = string.Format ("<iframe id='leftFrame' src='PatentSectionShow.aspx?Id={0}&type={1}&select={2}&patType={3}' scrolling='auto' frameborder='0' width='100%' height='550px' style='z-index:0;'></iframe>",
                        strId, type, 0, patType);
                LiteralRight.Text = string.Format ("<iframe id='rightFrame' src='PatentSectionShow.aspx?Id={0}&type={1}&select={2}&patType={3}' scrolling='auto' frameborder='0' width='100%' height='550px' style='z-index:0;'></iframe>",
                        strId, type, 3, patType);
            }
        }
    }
}