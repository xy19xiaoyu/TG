using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DBA;
using System.Web.Services;

namespace Patentquery.SysAdmin
{
    public partial class RecordDownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string UserNames = ";";
           DataTable   dt = SqlDbAccess.GetDataTable(CommandType.Text,"select UserName from tbUser");
           foreach (DataRow row in dt.Rows)
           {
               UserNames += row["UserName"].ToString() +";";
           }
           this.UsreNames.Value = UserNames.Trim(';');

        }
        public static Dictionary<string, string> DBtype = new Dictionary<string, string>() { { "发明", "CN1" }, { "外观", "CN3" }, { "新型", "CN2" }, { "发明&新型", "CN1','CN2" }, { "中国", "CN1','CN2','CN3" }, { "世界", "EN" } };
        public static Dictionary<string, string> COlS = new Dictionary<string, string>() { { "部", "IPC1" }, { "大类", "IPC3" }, { "小类", "IPC4" }, { "大组", "IPC7" }, { "IPC", "IPC" } };
        [WebMethod]
        public static string Record(string dbtype, string stcol, string sdate, string edate, string UserName)
        {
            string sql = string.Format("select ISNULL({0},'合计') as {4} ,count(id) AS 下载量 from recorddownload  where  {0} <>'' and [type] in('{1}') and dtime between '{2}' and '{3}' and UserName = '{5}' group by {0} with rollup order by count(id) desc", COlS[stcol], DBtype[dbtype], sdate, edate, stcol, UserName);
            if(string.IsNullOrEmpty(UserName.Trim()))
            {
                //ISNULL(ipc1,'合计')
                sql = string.Format("select ISNULL({0},'合计') as {4} ,count(id) AS 下载量 from recorddownload  where  {0} <>'' and [type] in('{1}') and dtime between '{2}' and '{3}'  group by {0} with rollup order by count(id) desc", COlS[stcol], DBtype[dbtype], sdate, edate, stcol);
            }
            DataTable res = SqlDbAccess.GetDataTable(CommandType.Text, sql);

            if (res != null && res.Rows.Count > 0)
            {
                DataRow rowItem= res.Rows.Count == 2 ? res.Rows[1]: res.Rows[0];
                rowItem[0] = "合计";
            }

            return JsonHelper.DatatTableToJson(res,"rows");
        }
    }
}