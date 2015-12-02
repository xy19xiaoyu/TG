using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class getNodes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string PId;
        string zid;
        string clstype;
        try
        {

            zid = Request["ztid"].ToString();
        }
        catch (Exception ex)
        {
            zid = "";
        }


        try
        {

            PId = Request["Id"].ToString();
        }
        catch (Exception ex)
        {
            PId = "";
        }      
        string fileter;

        if (string.IsNullOrEmpty(Request["fileter"]))
        {
            fileter = "";
        }
        else
        {
            fileter = Request["fileter"].ToString().Trim();
        }
        //Response.Write("[{\"id\":\"1\",\"text\":\"Node 1\",\"state\":\"closed\"},{\"id\":\"2\",\"text\":\"Node 2\",\"state\":\"open\"},{\"id\":\"3\",\"text\":\"Node 3\",\"state\":\"open\"},{\"id\":\"4\",\"text\":\"Node 4\",\"state\":\"open\"}]");
        if (string.IsNullOrEmpty(fileter))
        {
            Response.Write(ztHelper.GetNodes(zid, PId));
        }
        else
        {
            Response.Write(ztHelper.GetNodes(zid, PId, fileter));
        }

    }

}