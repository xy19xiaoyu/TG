using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using ProXZQDLL;

public partial class editNodes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static string addNode(string thid, string parent, string name, string clsType, string des, string live)
    {
        des = des.Replace("\n", "").Replace("\r", "");
        return ztHelper.addNode(thid, parent, name, clsType, des, live);

    }

    [WebMethod]
    public static string deleteNode(string id)
    {
        return ztHelper.deleteNode(id);
    }

    [WebMethod]
    public static string Rename(string id, string name, string des)
    {
        des = des.Replace("\n", "").Replace("\r", "");
        return ztHelper.Rename(id, name, des);
    }
    [WebMethod]
    public static string AddToTH(string pids, string nodids, string type)
    {
        TbUser user = (TbUser)HttpContext.Current.Session["USerInfo"];
        string username = user.RealName;
        pids = pids.Replace(",undefined,", ",");
        return ztHelper.AddToTH(pids, nodids, type, username);

    }
    [WebMethod]
    public static string MoveToTH(string pids, string nodids, string type,string OldNodeId)
    {
        TbUser user = (TbUser)HttpContext.Current.Session["USerInfo"];
        string username = user.RealName;
        pids = pids.Replace(",undefined,", ",");
        return ztHelper.AddToTH(pids, nodids, type, OldNodeId, username);

    }

    [WebMethod]
    public static string DelToZT(string pid, string nodid,string type)
    {
        pid = pid.Replace(",undefined,", ",");
        return ztHelper.DelToTH(pid, nodid, type);

    }

    [WebMethod]
    public static string getNodeDes(string nodid)
    {
        return ztHelper.getNodeDes(nodid);

    }

    [WebMethod]
    public static string getztName(string id)
    {
        return ztHelper.getztName(id);

    }
    [WebMethod]
    public static string setStars(string pid, string nodeid, string value,string type)
    {
        return ztHelper.setStars(pid, nodeid, value, type);

    }    
    [WebMethod]
    public static string getqyztid()
    {
        return ztHelper.setqyztid();

    }
}
