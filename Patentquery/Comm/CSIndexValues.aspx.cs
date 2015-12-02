using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using TLC;
using System.Web.Services;
using Cpic.Cprs2010.Cfg;
using TLC.BusinessLogicLayer;
using System.Data;
using SearchInterface;
using Cpic.Cprs2010.Search.ResultData;
using System.Net;
using System.Collections;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using System.Data.Linq;

namespace Patentquery.Comm
{
    public partial class CSIndexValues : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["id"] == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(Request["id"]))
            {
                return;
            }
            string id = Request["id"].ToString();
            DataTable result = CSIndex.CSIndex.getIndexValues(id);
            Response.Write(JsonHelper.DatatTableToJson(result, "rows"));
        }
    }

}