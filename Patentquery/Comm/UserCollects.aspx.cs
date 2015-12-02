using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLC;
using System.Web.Services;
using Cpic.Cprs2010.Cfg;
using TLC.BusinessLogicLayer;
using System.Data;
using SearchInterface;
using Cpic.Cprs2010.Search.ResultData;
namespace Patentquery.Comm
{
    public partial class UserCollects : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int PId;
            try
            {

                PId = Convert.ToInt32(Request["Id"].ToString());
            }
            catch (Exception ex)
            {
                PId = 0;
            }

            Response.Write(UserCollectsHelper.GetNodes(PId));

        }
        [WebMethod]
        public static string GetPageList(string Type, int NodeId, int pageindex, int rows, string filter, string Query)
        {

            ClsSearch cls = new ClsSearch();
            List<int> hitlist = new List<int>();
            int ItemCount;
            List<xmlDataInfo> ie1 = null;
            if (string.IsNullOrEmpty(Query))
            {
                hitlist = UserCollectsHelper.GetResultList(Type, NodeId, pageindex, rows, out ItemCount, filter);
            }
            else
            {
                hitlist = UserCollectsHelper.GetResultList(Type, NodeId, pageindex, rows, out ItemCount, filter, Query);
            }
            ie1 = cls.GetResult(hitlist, Type.ToUpper());

            foreach (xmlDataInfo rowData in ie1)
            {
                List<string> s = UserCollectsHelper.getNote(rowData.StrSerialNo, NodeId);
                rowData.Note = s[0];
                rowData.NoteDate = s[1];
            }
            ie1 = ie1.OrderByDescending(x => x.NoteDate).ToList<xmlDataInfo>();
            return JsonHelper.ListToJson<xmlDataInfo>(ie1, "rows", ItemCount.ToString());

        }

        [WebMethod]
        public static string GetHot(string top)
        {
            DataTable ie1 = UserCollectsHelper.GetHotTop(Convert.ToInt32(top));
            foreach (DataRow x in ie1.Rows)
            {
                int pid = Convert.ToInt32(x["Pid"].ToString());
                string type = x["type"].ToString();
                List<string> apti = UserCollectsHelper.GetAppTI(pid, type);
                x["Number"] = UrlParameterCode_DE.encrypt(apti[0].ToString());
                x["appno"] = apti[0];
                x["title"] = apti[1];
            }
            return JsonHelper.DatatTableToJson(ie1, "rows");

        }
        [WebMethod]
        public static string addNode(string parent, string name, string des, string live)
        {
            return UserCollectsHelper.addNode(parent, name, des, live);


        }

        [WebMethod]
        public static string deleteNode(string id)
        {
            return UserCollectsHelper.deleteNode(id);
        }

        [WebMethod]
        public static string Rename(string id, string name, string des)
        {
            return UserCollectsHelper.Rename(id, name, des);
        }

        [WebMethod]
        public static string AddToCO(string pids, string nodids, string type, string Note)
        {
            Note = HttpContext.Current.Server.HtmlEncode(Note).Replace("'", "''");
            return UserCollectsHelper.AddToCO(pids, nodids, type, Note);
        }
        //DelToCO
        [WebMethod]
        public static string DelToCO(string pids, string nodids)
        {
            return UserCollectsHelper.DelToCO(pids, nodids);
        }

        [WebMethod]
        public static string EditNote(string nodeid, string pid, string note)
        {
            note = HttpContext.Current.Server.HtmlEncode(note).Replace("'", "''");
            return UserCollectsHelper.EditNote(nodeid, pid, note);
        }

        [WebMethod]
        public static string AddToIndex(string pids, string zid, string ids, string type)
        {
            return CSIndex.CSIndex.AddToResult(zid, pids, ids, type);
        }

    }

}