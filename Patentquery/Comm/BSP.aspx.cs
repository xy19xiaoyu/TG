using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Web.Services;


public partial class BSP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int ztid;
        string type = string.Empty;

        if (!string.IsNullOrEmpty(Request["ztid"]))
        {
            if (!Int32.TryParse(Request["ztid"].ToString(), out ztid))
            {
                return;
            }
        }
        else
        {
            return;
        }
        if (!string.IsNullOrEmpty(Request["type"]))
        {
            type = Request["type"].ToString();
        }
        else
        {
            type="cn";
        }

        Response.Write(JsonHelper.DatatTableToJson(ztHelper.GetSearchPattern(ztid.ToString(), type), "rows"));
    }

    [WebMethod]
    public static string addSearchPattern(string ztid,string spNum, string SearchPattern, string Hit, string type)
    {
        return ztHelper.addSearchPattern(ztid, spNum, SearchPattern, Hit, type);
    }


    [WebMethod]
    public static string deleteSearchPattern(string id)
    {
        return ztHelper.deleteSearchPattern(id);
    }

    [WebMethod]
    public static string UpdateSearchPattern(string id, string SearchPattern)
    {
        return ztHelper.UpdateSearchPattern(id, SearchPattern);
    }
    [WebMethod]
    public static string SPBoundNode(string spid, string nodeid)
    {
        return ztHelper.SPBoundNode(spid, nodeid);
    }
    [WebMethod]
    public static string RemoveBindToZT(string spid)
    {
        return ztHelper.RemoveBindToZT(spid);
    }
}

