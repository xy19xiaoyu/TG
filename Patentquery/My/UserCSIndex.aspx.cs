using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;

namespace Patentquery.My
{
    public partial class UserCSIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string GetIndexs(string itemname)
        {
            int uid = Convert.ToInt32(HttpContext.Current.Session["Userid"].ToString());
            DataTable dt = CSIndex.CSIndex.GetIndexs(itemname, uid);
            return JsonHelper.DatatTableToJson(dt, "rows");
        }
        [WebMethod]
        public static string AddIndex(string itemname, string value1, string value2, string value3, string value4, string value5)
        {
            int uid = Convert.ToInt32(HttpContext.Current.Session["Userid"].ToString());
            string tmp = "{2} 'static':'{0}','msg':'{1}' {3}";
            string result = "";
            string msg = "";
            if (CSIndex.CSIndex.CheckIndexExsit(itemname, uid))
            {
                string ms = CSIndex.CSIndex.AddIndex(itemname, uid, value1, value2, value3, value4, value5);
                result = "true";
            }
            else
            {
                result = "false";
                msg = "标引项已存在！";
            }
            return string.Format(tmp, result, msg, "{", "}").Replace("'", "\"");
        }
        [WebMethod]
        public static string EditIndex(string itemname, string valuenames, string ids, string id)
        {
            string tmp = "{2} 'static':'{0}','msg':'{1}' {3}";
            string result = "";
            string msg = "";

            string Rid = CSIndex.CSIndex.UpdateIndexItem(itemname, valuenames, ids, id).ToString();
            if (Rid != "false")
            {
                result = "true";
                msg = Rid;
            }
            else
            {
                result = "false";
                msg = "修改失败";
            }

            return string.Format(tmp, result, msg, "{", "}").Replace("'", "\"");
        }
        [WebMethod]
        public static string DelIndex(string id)
        {
            string tmp = "{2} 'static':'{0}','msg':'{1}' {3}";
            string result = "";
            string msg = "";

            string Rid = CSIndex.CSIndex.DelIndexItem(id).ToString();
            if (Rid.ToLower() != "false")
            {
                result = "true";
                msg = Rid;
            }
            else
            {
                result = "false";
                msg = "删除失败";
            }
            return string.Format(tmp, result, msg, "{", "}").Replace("'", "\"");

        }
    }
}