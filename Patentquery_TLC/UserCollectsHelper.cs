using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using TLC;

namespace TLC
{
    public class UserCollectsHelper
    {
        /// <summary>
        /// 得到某一用户的，某一节点的分页数据
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="AlbumId"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public static List<int> GetResultList(string type, int AlbumId, int PageIndex, int PageSize, out int ItemCount, string filter)
        {
            type = type.ToUpper();

            int userid = Convert.ToInt32(HttpContext.Current.Session["Userid"].ToString());
            List<int> list = null;
            try
            {
                PQDataContext db = new PQDataContext();
                switch (filter.ToUpper())
                {
                    case "ALL":

                        list = (from th in db.TLC_Collects
                                where th.AlbumId == AlbumId && th.Type == type && th.UserId == userid && th.isdel == false
                                orderby th.CollectId descending
                                select th.Pid.Value).Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList<int>();
                        ItemCount = (from th in db.TLC_Collects
                                     where th.AlbumId == AlbumId && th.Type == type && th.UserId == userid && th.isdel == false
                                     select th).Count();
                        break;
                    case "YES":
                        list = (from th in db.TLC_Collects
                                where th.AlbumId == AlbumId && th.Type == type && th.UserId == userid && th.NoteDate.Value != null && th.isdel == false
                                orderby th.CollectId descending
                                select th.Pid.Value).Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList<int>();
                        ItemCount = (from th in db.TLC_Collects
                                     where th.AlbumId == AlbumId && th.Type == type && th.UserId == userid && th.NoteDate.Value != null && th.isdel == false
                                     select th).Count();
                        break;
                    case "NO":
                        list = (from th in db.TLC_Collects
                                where th.AlbumId == AlbumId && th.Type == type && th.UserId == userid && th.NoteDate.Value == null && th.isdel == false
                                orderby th.CollectId descending
                                select th.Pid.Value).Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList<int>();
                        ItemCount = (from th in db.TLC_Collects
                                     where th.AlbumId == AlbumId && th.Type == type && th.UserId == userid && th.NoteDate.Value == null && th.isdel == false
                                     select th).Count();
                        break;
                    default:
                        ItemCount = 0;
                        break;
                }

                return list;
            }
            catch (Exception ex)
            {
                //logger.Error(ex.ToString());
                ItemCount = 0;
                return null;
            }
        }

        /// <summary>
        /// 得到某一用户的，某一节点的分页数据
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="AlbumId"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public static List<int> GetResultList(string type, int AlbumId, int PageIndex, int PageSize, out int ItemCount, string filter, string Query)
        {
            type = type.ToUpper();

            int userid = Convert.ToInt32(HttpContext.Current.Session["Userid"].ToString());
            List<int> list = null;
            try
            {
                PQDataContext db = new PQDataContext();
                switch (filter.ToUpper())
                {
                    case "ALL":

                        list = (from th in db.TLC_Collects
                                where th.AlbumId == AlbumId && th.Type == type && th.UserId == userid && th.Note.Contains(Query) && th.isdel == false
                                orderby th.CollectId descending
                                select th.Pid.Value).Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList<int>();
                        ItemCount = (from th in db.TLC_Collects
                                     where th.AlbumId == AlbumId && th.Type == type && th.UserId == userid && th.Note.Contains(Query) && th.isdel == false
                                     select th).Count();
                        break;
                    case "YES":
                        list = (from th in db.TLC_Collects
                                where th.AlbumId == AlbumId && th.Type == type && th.UserId == userid && th.NoteDate.Value != null && th.Note.Contains(Query) && th.isdel == false
                                orderby th.CollectId descending
                                select th.Pid.Value).Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList<int>();
                        ItemCount = (from th in db.TLC_Collects
                                     where th.AlbumId == AlbumId && th.Type == type && th.UserId == userid && th.NoteDate.Value != null && th.Note.Contains(Query) && th.isdel == false
                                     select th).Count();
                        break;
                    case "NO":
                        list = (from th in db.TLC_Collects
                                where th.AlbumId == AlbumId && th.Type == type && th.UserId == userid && th.NoteDate.Value == null && th.Note.Contains(Query) && th.isdel == false
                                orderby th.CollectId descending
                                select th.Pid.Value).Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList<int>();
                        ItemCount = (from th in db.TLC_Collects
                                     where th.AlbumId == AlbumId && th.Type == type && th.UserId == userid && th.NoteDate.Value == null && th.Note.Contains(Query) && th.isdel == false
                                     select th).Count();
                        break;
                    default:
                        ItemCount = 0;
                        break;
                }

                return list;
            }
            catch (Exception ex)
            {
                //logger.Error(ex.ToString());
                ItemCount = 0;
                return null;
            }
        }

        /// <summary>
        /// 得到某一用户的，某一节点的分页数据
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="AlbumId"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public static List<int> GetResultList(string type, int AlbumId)
        {
            type = type.ToUpper();

            int userid = Convert.ToInt32(HttpContext.Current.Session["Userid"].ToString());
            List<int> list = null;

            PQDataContext db = new PQDataContext();
            list = (from th in db.TLC_Collects
                    where th.AlbumId == AlbumId && th.Type == type && th.UserId == userid && th.isdel == false
                    orderby th.CollectId descending
                    select th.Pid.Value).ToList<int>();

            return list;
        }


        /// <summary>
        /// 得到top 10
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="AlbumId"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public static DataTable GetHotTop(int top)
        {
            try
            {
                PQDataContext db = new PQDataContext();

                DataTable list = DBA.SqlDbAccess.GetDataTable(CommandType.Text, "select top " + top.ToString() + " pid,appno,title, [type],'' as Number,COUNT(*) as [count] from TLC_Collects group by pid,appno,Title,[type] order by COUNT(*) desc");
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static string GetNodes(int PId)
        {
            int userid = Convert.ToInt32(HttpContext.Current.Session["Userid"].ToString());
            StringBuilder sb = new StringBuilder();
            PQDataContext db = new PQDataContext();
            IQueryable<TLC_Albums> tree;
            if (PId == 0)
            {
                tree = from x in db.TLC_Albums
                       where x.UserId == userid && x.ParentId == PId && x.isdel == false
                       orderby x.AlbumId ascending
                       select x;

                if (tree.Count() > 0)
                {
                    sb.Append("[");
                    foreach (var x in tree)
                    {
                        sb.Append(string.Format("{0}\"id\":\"{1}\",\"text\":\"{2}\",\"state\":\"{3}\",\"attributes\":{0}\"live\":\"{4}\",\"des\":\"{5}\"{6}", "{", x.AlbumId, x.Title.Trim(), "open", x.live, x.Note, "}"));
                        //sb.Append(string.Format("{0}\"id\":\"{1}\",\"name\":\"{2}\",\"open\":\"{3}\"", "{", x.AlbumId, x.Title, "true"));
                        var subnoNote = from node in db.TLC_Albums
                                        where node.UserId == userid && node.ParentId == x.AlbumId && node.isdel == false
                                        orderby x.AlbumId ascending
                                        select node;
                        if (subnoNote.Count() > 0)
                        {
                            sb.Append(",\"children\": [");
                            foreach (var sbn in subnoNote)
                            {
                                sb.Append(string.Format("{0}\"id\":\"{1}\",\"text\":\"{2}\",\"state\":\"{3}\",\"attributes\":{0}\"live\":\"{4}\",\"des\":\"{5}\"{6}{7}", "{", sbn.AlbumId, sbn.Title.Trim(), (sbn.IsParent == 0 ? "open" : "closed"), sbn.live, sbn.Note, "}", "},"));
                                //sb.Append(string.Format("{0}\"id\":\"{1}\",\"name\":\"{2}\",\"isParent\":\"{3}\"{4}", "{", sbn.AlbumId, sbn.Title, (sbn.IsParent == 0 ? "false" : "true"), "},"));
                            }
                            sb.Remove(sb.Length - 1, 1);
                            sb.Append("]");
                        }
                        sb.Append("},");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("]");
                }

            }
            else
            {

                tree = from x in db.TLC_Albums
                       where x.UserId == userid && x.ParentId == PId && x.isdel == false
                       orderby x.AlbumId ascending
                       select x;

                if (tree.Count() > 0)
                {
                    sb.Append("[");
                    foreach (var x in tree)
                    {
                        sb.Append(string.Format("{0}\"id\":\"{1}\",\"text\":\"{2}\",\"state\":\"{3}\",\"attributes\":{0}\"live\":\"{4}\",\"des\":\"{5}\"{6}{7}", "{", x.AlbumId, x.Title.Trim(), (x.IsParent == 0 ? "open" : "closed"), x.live, x.Note, "}", "},"));
                        //sb.Append(string.Format("{0}\"id\":\"{1}\",\"name\":\"{2}\",\"isParent\":\"{3}\"{4}", "{", x.AlbumId, x.Title, (x.IsParent == 0 ? "false" : "true"), "},"));

                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("]");

                }
            }
            return sb.ToString();
        }

        public static List<string> getNote(string pid, int AlbumId)
        {
            int UserId = Convert.ToInt32(HttpContext.Current.Session["Userid"].ToString());
            List<string> s = new List<string>();
            try
            {
                DataTable dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, string.Format("select note,notedate from TLC_Collects where UserId ={0} and pid ={1} and AlbumId ={2} and isdel=0", UserId, pid, AlbumId));
                s.Add(dt.Rows[0][0].ToString());
                s.Add(dt.Rows[0][1].ToString());

            }
            catch (Exception ex)
            {
                s.Add("");
                s.Add("");
            }

            return s;
        }

        public static string AddToCO(string pids, string nodids, string type, string Note)
        {
            pids = pids.Replace(",undefined,", ",");
            List<string> lstpid = pids.Trim(',').Split(',').ToList<string>();
            List<string> lstAlbumId = nodids.Trim(',').Split(',').ToList<string>();
            return AddToCO(lstpid, lstAlbumId, type, Note);
        }
        public static string AddToCO(List<string> lstpid, List<string> lstAlbumId, string type, string Note)
        {
            int UserId = Convert.ToInt32(HttpContext.Current.Session["Userid"].ToString());
            Note = Note.Replace("'", "''");
            string result = "succ";
            string SQL;
            try
            {
                using (SqlConnection conn = DBA.SqlDbAccess.GetSqlConnection())
                {
                    conn.Open();
                    SqlTransaction tran = conn.BeginTransaction();

                    foreach (string AlbumId in lstAlbumId)
                    {
                        foreach (string pid in lstpid)
                        {
                            if (DBA.SqlDbAccess.ExecuteScalar(tran, CommandType.Text, string.Format("select count(1) from TLC_Collects where UserId ={0} and pid ={1} and AlbumId ={2} and isdel=0", UserId, pid, AlbumId)).ToString() == "0")
                            {
                                if (string.IsNullOrEmpty(Note))
                                {
                                    SQL = string.Format("INSERT INTO TLC_Collects (UserId ,AlbumId ,[Type] ,Pid ,CollectDate,Note) VALUES ({0},{1},'{2}',{3},GETDATE(),'')", UserId, AlbumId, type, pid);
                                }
                                else
                                {
                                    SQL = string.Format("INSERT INTO TLC_Collects (UserId ,AlbumId ,[Type] ,Pid ,CollectDate ,Note ,NoteDate) VALUES ({0},{1},'{2}',{3},GETDATE() ,'{4}',GETDATE())", UserId, AlbumId, type, pid, Note);
                                }

                            }
                            else
                            {
                                SQL = string.Format("update TLC_Collects set Note= Note +'--{3}' ,NoteDate=getdate() where UserId ={0} and pid ={1} and AlbumId ={2} and isdel=0 ", UserId, pid, AlbumId, Note);

                            }
                            DBA.SqlDbAccess.ExecNoQuery(tran, CommandType.Text, SQL);
                        }

                    }


                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                result = "failed";
            }
            return result;
        }
        public static List<string> GetAppTI(int cpic, string type)
        {
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["Cpic.Cprs2010.Search.ResultData.Properties.Settings.DataProcessConnectionString"].ConnectionString;
            string sql;
            if (type.ToUpper() == "CN")
            {
                sql = "select ApNo,Title from CnGeneral_Info where SerialNo=" + cpic;
            }
            else
            {
                sql = "select PubID,Title from DocdbDocInfo where ID=" + cpic;
            }

            List<string> s = new List<string>();
            try
            {
                DataTable dt = DBA.SqlDbAccess.GetDataTable(constr, CommandType.Text, sql);
                s.Add(dt.Rows[0][0].ToString().Trim());
                s.Add(dt.Rows[0][1].ToString().Trim());

            }
            catch (Exception ex)
            {
                s.Add("");
                s.Add("");
            }

            return s;
        }

        public static string DelToCO(string pids, string nodids)
        {
            pids = pids.Replace(",undefined,", ",");
            int UserId = Convert.ToInt32(HttpContext.Current.Session["Userid"].ToString());
            string result = "succ";
            try
            {
                using (SqlConnection conn = DBA.SqlDbAccess.GetSqlConnection())
                {
                    conn.Open();
                    SqlTransaction tran = conn.BeginTransaction();
                    List<string> lstp = pids.Split(',').ToList<string>();
                    foreach (string s in lstp)
                    {
                        if (string.IsNullOrEmpty(s)) continue;
                        DBA.SqlDbAccess.ExecNoQuery(tran, CommandType.Text, string.Format("update TLC_Collects set isdel=1 where UserId ={0} and pid ={1} and AlbumId={2}", UserId, s, nodids));
                    }
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                result = "failed";
            }
            return result;
        }
        public static string addNode(string parent, string name, string Note, string live)
        {

            int UserId = Convert.ToInt32(HttpContext.Current.Session["Userid"].ToString());
            int lv = Convert.ToInt32(live);
            int newid;
            try
            {
                using (SqlConnection conn = DBA.SqlDbAccess.GetSqlConnection())
                {
                    conn.Open();
                    SqlTransaction tran = conn.BeginTransaction();

                    if (Convert.ToInt32(DBA.SqlDbAccess.ExecuteScalar(tran, CommandType.Text, "select count(*) from TLC_Albums where isdel=0 and Title='" + name + "'and ParentId=" + parent)) >= 1)
                    {
                        return "{\"mess\":\"exists\"}";
                    }
                    SqlParameter[] pars = new SqlParameter[]{
                new SqlParameter("@Title",SqlDbType.VarChar),
                new SqlParameter("@ParentId",SqlDbType.Int),                
                new SqlParameter("@UserId",SqlDbType.Int),  
                new SqlParameter("@Note",SqlDbType.VarChar),
                new SqlParameter("@live",SqlDbType.Int)};

                    pars[0].Value = name;
                    pars[1].Value = parent;
                    pars[2].Value = UserId;
                    pars[3].Value = Note;
                    pars[4].Value = lv;
                    DBA.SqlDbAccess.ExecNoQuery(tran, CommandType.Text, "insert into TLC_Albums (Title,ParentId,UserId,IsParent,Note,live) values (@Title,@ParentId,@UserId,0,@Note,@live)", pars);
                    //更新父节点标记
                    DBA.SqlDbAccess.ExecNoQuery(tran, CommandType.Text, "update TLC_Albums set IsParent=1 where AlbumId=" + parent);

                    newid = Convert.ToInt32(DBA.SqlDbAccess.ExecuteScalar(tran, CommandType.Text, "select @@identity from TLC_Albums"));
                    tran.Commit();
                }

            }
            catch (Exception ex)
            {
                return "{\"mess\":\"failed\",\"addone:\":\"failed\"}";
            }
            return "{\"mess\":\"" + newid + "\"}";

        }
        public static string deleteNode(string id)
        {
            string result = "succ";
            try
            {
                int pid = (int)DBA.SqlDbAccess.ExecuteScalar(CommandType.Text, "select ParentId from TLC_Albums where AlbumId=" + Convert.ToInt32(id));

                int count = (int)DBA.SqlDbAccess.ExecuteScalar(CommandType.Text, "select count(*) from TLC_Albums where ParentId=" + Convert.ToInt32(pid) + " and isdel=0");
                DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, "delete TLC_Albums where AlbumId=" + id);

                if (count == 1)
                {
                    DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, "update TLC_Albums set IsParent=0 where AlbumId=" + pid);
                }
            }
            catch (Exception ex)
            {
                result = "failed";
            }

            return result;
        }
        public static string Rename(string id, string name, string Note)
        {
            string result = "succ";
            try
            {
                SqlParameter[] pars = new SqlParameter[]{
                new SqlParameter("@Title",SqlDbType.VarChar),
                new SqlParameter("@Note",SqlDbType.VarChar),
                new SqlParameter("@AlbumId",SqlDbType.Int)
            };
                pars[0].Value = name;
                pars[1].Value = Note;
                pars[2].Value = id;
                DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, "update TLC_Albums set Title=@Title,Note =@Note where AlbumId=@AlbumId", pars);
            }
            catch (Exception ex)
            {
                result = "failed";
            }

            return result;
        }

        public static string EditNote(string nodeid, string pid, string note)
        {
            int UserId = Convert.ToInt32(HttpContext.Current.Session["Userid"].ToString());
            string result = "succ";
            try
            {
                string SQL = string.Format("update TLC_Collects set Note= '{3}' ,NoteDate=getdate() where UserId ={0} and pid ={1} and AlbumId ={2} and isdel=0 ", UserId, pid, nodeid, note);


                DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, SQL);
            }
            catch (Exception ex)
            {
                result = "failed";
            }

            return result;
        }
    }



}
