using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace Patentquery.Comm
{
    public partial class PatentInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string appno = string.Empty;
            string type = string.Empty;
            string resultType = string.Empty;
            resutBase ret = new resutBase() { ret = false, err = "参数错误" };

            Request.ContentType = "application/x-json;charset=UTF-8";
            if (Request["appno"] == null || Request["type"] == null || Request["resultType"] == null)
            {
                Response.Write(ret.ToString());
                return;
            }
            if (string.IsNullOrEmpty(Request["appno"].ToString().Trim()) || string.IsNullOrEmpty(Request["type"].ToString().Trim()) || string.IsNullOrEmpty(Request["resultType"].ToString().Trim()))
            {
                Response.Write(ret.ToString());
                return;
            }

            appno = Request["appno"].ToString();
            type = Request["type"].ToString();
            resultType = Request["resultType"].ToString();
            object o = new object();
            switch (type.ToUpper())
            {
                case "B":
                case "C":
                case "D":
                case "P":
                    break;
            }


        }
    }
    public class resutBase
    {
        public bool ret;
        public string err;
        public object data;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);

        }
    }
}