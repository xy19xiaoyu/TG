using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Web.Services;


public partial class GetSearchPattern : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string NodeId;
        string type = string.Empty;

        if (!string.IsNullOrEmpty(Request["NodeId"]))
        {
            NodeId = Request["NodeId"].ToString();
        }
        else
        {
            return;
        }


        if (!string.IsNullOrEmpty(Request["type"]))
        {
            type = Request["type"].ToString();            
        }

        Response.Write(JsonHelper.DatatTableToJson(ztHelper.GetSPattern(NodeId, type), "rows"));
    }

    [WebMethod]
    public static string addSearchPattern(string NodeId, string SearchPattern, string Hit, string type,string ztid)
    {
        return ztHelper.addSearchPattern1(NodeId, SearchPattern, Hit,type,ztid);
    }

    [WebMethod]
    public static string deleteSearchPattern(string id,string nodeid,string type)
    {
        return ztHelper.deleteSearchPattern(id,nodeid,type);
    }

    [WebMethod]
    public static string UpdateSearchPattern(string id, string SearchPattern, int Hit,string nodeid,string type)
    {
        return ztHelper.UpdateSearchPattern(id, SearchPattern, Hit, nodeid, type);
    }
}

