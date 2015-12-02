using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;

namespace Patentquery.Comm
{
    public partial class ipcSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string GetMainNodesList(string type)
        {
            string sql = "select nodename,[des] from [ipcTreeTable] where [type]='" + type + "' and live=0";
            DataTable dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql);
            return JsonHelper.DatatTableToJson(dt, "rows");
        }
        /// <summary>
        /// 类型检索
        /// </summary>
        /// <param name="type">IPC，ADM,ARE</param>
        /// <param name="stype">key,ipc</param>
        /// <param name="svale">word or ipc</param>
        /// <returns></returns>
        [WebMethod]
        public static string GetNodesList(string type, string svale)
        {
            //判断是IPC 检索 还是关键词检索
            DataTable dt = SysTreeHelper.GetIPCListByNodeName(type, svale);
            return JsonHelper.DatatTableToJson(dt, "rows");

        }
        /// <summary>
        /// 类型检索
        /// </summary>
        /// <param name="type">IPC，ADM,ARE</param>
        /// <param name="stype">key,ipc</param>
        /// <param name="svale">word or ipc</param>
        /// <returns></returns>
        [WebMethod]
        public static string Search(string type, string stype, string svale)
        {

            //判断是IPC 检索 还是关键词检索
            DataTable dt;
            dt = SysTreeHelper.GetIPCListByIPC(type, stype, svale);
            return JsonHelper.DatatTableToJson(dt, "rows");
        }
    }
}