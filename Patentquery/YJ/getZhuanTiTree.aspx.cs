using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using TLC;

namespace Patentquery.YJ
{
    public partial class getZhuanTiTree : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int userid = int.Parse(System.Web.HttpContext.Current.Session["UserID"].ToString());
            DataTable res = ztHelper.getName(userid.ToString());
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < res.Rows.Count; i++)
            {
                sb.Append("{");
                string zid = res.Rows[i]["zid"].ToString().Trim();
                //sb.Append(getNodes(zid,"",userid));
                //sb.Append("},");
                //sb.Append("{");
                //int thid = int.Parse(res.Rows[i]["dbid"].ToString().Trim());
                //sb.Append(string.Format("\"id\":\"{1}\",\"text\":\"{2}\"", zid, res.Rows[i]["ztdbname"]));
                sb.Append(string.Format("\"id\":\"{1}\",\"text\":\"{2}\"", "{", zid, res.Rows[i]["ztdbname"]));
                string result = getNodes(zid, "", userid);
                sb.Append(result);
                sb.Append("},");               
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            Response.ContentType = "text/plain";
            Response.Write(sb.ToString());            
        }
        private string getNodes(string zid, string PId,int userid)
        {
            StringBuilder sb = new StringBuilder();
            PQDataContext db = new PQDataContext();
            IQueryable<ztTree> tree;
            if (PId == "")
            {
                tree = from x in db.ztTree
                       where x.PNid == PId  && x.CreateUserId == userid && x.isdel == false && x.zid==zid 
                       orderby x.NodeId ascending
                       select x;

                if (tree.Count() > 0)
                {
                    sb.Append(",\"children\":[");
                    //sb.Append("[");
                    foreach (var x in tree)
                    {
                        sb.Append("{");
                        sb.Append(string.Format("\"id\":\"{1}\",\"text\":\"{2}\"", "{", x.Nid, x.NodeName.Trim()));

                        if (x.IsParent == 1)
                        {
                            string result = getNodes(zid, x.Nid, userid);
                            sb.Append(result);
                        }
                        sb.Append("},");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("]");
                }

            }
            else
            {
   
                    tree = from x in db.ztTree
                           where x.zid==zid && x.PNid == PId && x.isdel == false
                           orderby x.NodeId ascending
                           select x;

                    if (tree.Count() > 0)
                    {
                        sb.Append(",\"children\":[");
                        foreach (var x in tree)
                        {
                            sb.Append("{");
                            sb.Append(string.Format("\"id\":\"{1}\",\"text\":\"{2}\"", "{", x.Nid, x.NodeName.Trim()));

                            if (x.IsParent == 1)
                            {
                                string result = getNodes(zid, x.Nid, userid);
                                sb.Append(result);
                            }
                            sb.Append("},");
                        }
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append("]");

                    }
                
            }
            return sb.ToString();
        }
    }
}