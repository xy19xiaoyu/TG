using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Patentquery.Comm
{
    public partial class getCSIndexValues : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //            {"total":7,"rows":[
            //    {"name":"Name","value":"Bill Smith","group":"ID Settings","editor":"text"},
            //    {"name":"Address","value":"","group":"ID Settings","editor":"text"},
            //    {"name":"Age","value":"40","group":"ID Settings","editor":"numberbox"},
            //    {"name":"Birthday","value":"01/02/2012","group":"ID Settings","editor":"datebox"},
            //    {"name":"SSN","value":"123-456-7890","group":"ID Settings","editor":"text"},
            //    {"name":"Email","value":"bill@gmail.com","group":"Marketing Settings","editor":{
            //        "type":"validatebox",
            //        "options":{
            //            "validType":"email"
            //        }
            //    }},
            //    {"name":"FrequentBuyer","value":"false","group":"Marketing Settings","editor":{
            //        "type":"checkbox",
            //        "options":{
            //            "on":true,
            //            "off":false
            //        }
            //    }}
            //]}

            try
            {
                if (string.IsNullOrEmpty(Request["zid"]) || string.IsNullOrEmpty(Request["zid"]))
                {
                    return;
                }
                string uid = HttpContext.Current.Session["Userid"].ToString();
                string zid = Request["zid"].ToString();
                string pid = Request["pid"].ToString();
                Response.Write(CSIndex.CSIndex.getShowIndexItmesJSON(uid, zid, pid));
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}