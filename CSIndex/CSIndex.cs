using System;
using System.Collections.Generic;
using System.Text;
using DBA;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Linq;
namespace CSIndex
{
    public class CSIndex
    {

        /// <summary>
        /// 判读标引项是否已经存在
        /// </summary>
        /// <returns></returns>
        public static bool CheckIndexExsit(string itemname, int uid)
        
        {
            itemname = HttpContext.Current.Server.HtmlEncode(itemname);

            string sql = "SELECT COUNT(*) FROM CSIndex_Item WHERE itemname=@itemname and isdel=0 and createuserid=" + uid;
            SqlParameter[] parms = new SqlParameter[]{
                    new SqlParameter("@itemname",itemname)
            };
            if (Convert.ToInt32(SqlDbAccess.ExecuteScalar(CommandType.Text, sql, parms)) > 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public static DataTable GetIndexs(string itemname, int uid)
        {
            string sql = "SELECT itemname,'' as 'itemvalues',id,'' as ids FROM CSIndex_Item where isdel=0 and createuserid=" + uid;

            string where = "";
            if (itemname != "")
            {
                where += " and itemname like '%" + itemname + "%' ";
            }
            sql = sql + where;
            DataTable dt = SqlDbAccess.GetDataTable(CommandType.Text, sql);
            foreach (DataRow row in dt.Rows)
            {

                DataTable tmpdt = getIndexValues(row["id"].ToString());

                string values = "";
                string ids = "";
                foreach (DataRow subdt in tmpdt.Rows)
                {
                    values += subdt["valuename"].ToString() + ";";
                    ids += subdt["id"].ToString() + ";";

                }
                values = values.Trim(';');
                ids = ids.Trim(';');
                row["itemvalues"] = values;


                row["ids"] = ids;
            }
            return dt;
        }
        public static string AddIndex(string itemname, int createuserid, params string[] values)
        {
            itemname = HttpContext.Current.Server.HtmlEncode(itemname);
            SqlParameter[] parms;

            string strsql = "insert into csindex_item (itemname,createuserid) values (@itemname,@createuserid)";
            parms = new SqlParameter[]{
                        new System.Data.SqlClient.SqlParameter("@itemname", itemname),                                                                                                        
                        new System.Data.SqlClient.SqlParameter("@createuserid", createuserid)
            };

            using (SqlConnection con = DBA.SqlDbAccess.GetSqlConnection())
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                DBA.SqlDbAccess.ExecNoQuery(trans, CommandType.Text, strsql, parms);
                string newid = DBA.SqlDbAccess.ExecuteScalar(trans, CommandType.Text, "select @@identity from csindex_item").ToString();
                string sqlvalues = @"insert into CSIndex_Value (itemid,valuename,createuserid) values ";
                string sqlvalueitems = "";
                if (values != null)
                {
                    foreach (var value in values)
                    {
                        if (string.IsNullOrEmpty(value)) continue;

                        sqlvalueitems += string.Format("({0},'{1}',{2}),", newid, HttpContext.Current.Server.HtmlEncode(value), createuserid);
                    }
                    sqlvalueitems = sqlvalueitems.Trim(',');
                }
                if (!string.IsNullOrEmpty(sqlvalueitems))
                {
                    DBA.SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sqlvalues + sqlvalueitems);
                }

                trans.Commit();
                return newid;

            }

        }
        public static bool DelIndexItem(string id)
        {
            string sqlDelIndex = "update CSIndex_Item set isdel=1 Where id=@id";
            SqlParameter parms;
            using (SqlConnection conn = SqlDbAccess.GetSqlConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        parms = new System.Data.SqlClient.SqlParameter("@id", id);
                        SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sqlDelIndex, parms);
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        //throw new Exception("插入数据库失败,事务已经回滚", ex);
                        return false;

                    }
                };
            };
            return true;

        }
        public static bool UpdateIndexItem(string itemname, string valuenames, string ids, string id)
        {
            itemname = HttpContext.Current.Server.HtmlEncode(itemname);
            SqlParameter[] parms;
            string updateIndex = "Update CSIndex_Item Set itemname =@itemname Where id=@id";

            parms = new SqlParameter[]{
                            new System.Data.SqlClient.SqlParameter("@itemname", itemname),
                            new System.Data.SqlClient.SqlParameter("@id", id)
                        };
            using (SqlConnection con = DBA.SqlDbAccess.GetSqlConnection())
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                SqlDbAccess.ExecNoQuery(trans, CommandType.Text, updateIndex, parms);
                List<string> listids = ids.Split(';').ToList<string>();
                List<string> listvaluenames = valuenames.Split(';').ToList<string>();

                for (int i = 0; i < listids.Count; i++)
                {
                    string sql = "";
                    if (listvaluenames[i].Trim() == "")
                    {
                        sql = "update CSIndex_Value set isdel =1 where id=" + listids[i];
                    }
                    else
                    {
                        sql = "update CSIndex_Value set valuename='" + listvaluenames[i] + "' where id=" + listids[i];
                    }
                    SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sql);
                }
                trans.Commit();
            }


            return true;
        }
        public static string addIndexValue(string valuename, string itemid, string zid, string createuserid)
        {
            valuename = HttpContext.Current.Server.HtmlEncode(valuename);
            SqlParameter[] parms;

            string strsql = "insert into CSIndex_Value (itemid,valuename,zid,createuserid) values (@itemid,@valuename,@zid,@createuserid)";
            parms = new SqlParameter[]{
                            new System.Data.SqlClient.SqlParameter("@itemid", itemid),                                                    
                            new System.Data.SqlClient.SqlParameter("@valuename", valuename),                                                    
                            new System.Data.SqlClient.SqlParameter("@zid", zid),
                            new System.Data.SqlClient.SqlParameter("@createuserid", createuserid)
            };

            using (SqlConnection con = DBA.SqlDbAccess.GetSqlConnection())
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                DBA.SqlDbAccess.ExecNoQuery(trans, CommandType.Text, strsql);
                string newid = DBA.SqlDbAccess.ExecuteScalar(trans, CommandType.Text, "select @@identity from CSIndex_Value").ToString();
                trans.Commit();
                return newid;

            }
        }
        public static bool DelIndexValue(string id)
        {
            string sqlDelIndex = "update CSIndex_Value set isdel=1 Where id=@id";
            SqlParameter parms;
            using (SqlConnection conn = SqlDbAccess.GetSqlConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        parms = new System.Data.SqlClient.SqlParameter("@id", id);
                        SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sqlDelIndex, parms);
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        //throw new Exception("插入数据库失败,事务已经回滚", ex);
                        return false;

                    }
                };
            };
            return true;

        }
        public static bool UpdateIndexValue(string valuename, string id)
        {
            valuename = HttpContext.Current.Server.HtmlEncode(valuename);
            SqlParameter[] parms;
            string updateIndex = "Update CSIndex_Value Set valuename =@valuename Where id=@id";
            parms = new SqlParameter[]{
                            new System.Data.SqlClient.SqlParameter("@valuename", valuename)
                        };
            SqlDbAccess.ExecNoQuery(CommandType.Text, updateIndex, parms);


            return true;
        }
        public static DataTable getIndexValues(string itemid)
        {
            SqlParameter pram = new SqlParameter("@itemid", itemid);
            string sql = "select id,valuename from CSIndex_Value where itemid=@itemid";
            return SqlDbAccess.GetDataTable(CommandType.Text, sql, pram);
        }

        public static List<string> getUserIndexResults(string zid, string pid, string userid)
        {
            string sql = string.Format("select valueid from CSIndex_Result as a,ZtDbList as b where a.zid = b.DbID and b.zid='{0}' and pid={1} and a.createuserid={2}", zid, pid, userid);
            DataTable dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql);
            List<string> indexResults = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                indexResults.Add(row[0].ToString());
            }
            return indexResults;
        }
        public static DataTable getUserIndexItems(string userid)
        {
            string sql = string.Format("select a.itemname,b.valuename,b.itemid,b.id as valueid from csindex_item as a join CSIndex_Value as b on a.id  = b.itemid where a.createuserid={0} and a.isdel=0 order by a.id,b.id", userid);
            return DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql);
        }
        public static string getShowIndexItmesJSON(string userid, string zid, string pid)
        {
            DataTable DT = getUserIndexItems(userid);
            List<string> listindexs = getUserIndexResults(zid, pid, userid);
            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in DT.Rows)
            {
                string valuename = row["valuename"].ToString();
                string itemname = row["itemname"].ToString();
                string valueid = row["valueid"].ToString();
                string itemid = row["itemid"].ToString();
                string value = "";
                if (listindexs.Contains(row["valueid"].ToString()))
                {
                    value = "是";
                }
                sb.Append(string.Format("{5}\"name\":\"{0}\",\"value\":\"{1}\",\"group\":\"{2}\",\"itemid\":\"{3}\",\"valueid\":\"{4}\",\"editor\":{5}\"type\":\"checkbox\",\"options\":{5}\"on\":\"是\",\"off\":\"\"{6}{6}{6},", valuename, value, itemname, itemid, valueid, "{", "}"));
            }
            return string.Format("{2}\"total\":{0},\"rows\":[{1}]{3}", DT.Rows.Count, sb.ToString(0, sb.Length - 1), "{", "}");
        }
        public static string AddToResult(string zid, string pid, string ids, string type)
        {
            int uid = Convert.ToInt32(HttpContext.Current.Session["Userid"].ToString());
            string result = "succ";
            try
            {
                using (SqlConnection con = DBA.SqlDbAccess.GetSqlConnection())
                {
                    con.Open();
                    SqlTransaction trans = con.BeginTransaction();

                    string dbid = DBA.SqlDbAccess.ExecuteScalar(trans, CommandType.Text, "select dbid from ztdblist where zid='" + zid + "'").ToString();
                    DBA.SqlDbAccess.ExecNoQuery(trans, CommandType.Text, string.Format("delete from CSIndex_Result where zid={0} and pid={1} and createuserid={2}", dbid, pid, uid));
                    string insertsql = "insert into CSIndex_Result (zid,type,pid,itemid,valueid,createuserid) values";
                    string values = " ({0},'{1}',{2},{3},{4}),";
                    string tmpSQL = "";
                    List<string> listids = ids.Split(';').ToList<string>();
                    StringBuilder sb = new StringBuilder();

                    if (ids != "")
                    {
                        foreach (var id in listids)
                        {
                            if (id.ToString().Trim() == "") continue;
                            sb.Append(string.Format(values, dbid, type, pid, id, uid));
                        }
                        tmpSQL = insertsql + sb.ToString(0, sb.Length - 1);

                        DBA.SqlDbAccess.ExecNoQuery(trans, CommandType.Text, tmpSQL);
                    }
                    trans.Commit();
                }
            }
            catch (Exception ex)
            {
                result = "failed";
            }

            return result;
        }
    }
}