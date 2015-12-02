using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ExcelLib;
using TLC.BusinessLogicLayer;
using System.IO;

namespace Patentquery.Comm
{
    public partial class ExportExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["filename"] == null) return;
            if (string.IsNullOrEmpty(Request["filename"].ToString().Trim()))
            {
                Response.Write("<script>alert('数据加载错误，请重试！');</script>");
                return;
            }
            string name = Request["filename"].ToString();
            string strFileName = "/Images/Export/Patent" + name;
            string fullpath = System.Web.HttpContext.Current.Server.MapPath(strFileName);
            Page.Response.Clear();
            bool success = NPOIHelper.ResponseFile(Page.Request, Page.Response, name, fullpath, 1024000);
            if (!success)
                Response.Write("<script>alert('下载文件出错！');</script>");
            Page.Response.End();
        }

    }
}
