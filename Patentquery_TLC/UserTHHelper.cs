using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Cpic.Cprs2010.Search;
using TLC;
using ProXZQDLL;

public class ztresult
{
    public int pid;
    public int star;
    public string strForm;
}
/// <summary>
/// asdf
/// </summary>
public class ztHelper
{
    public static string xituConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SqlServerStr"].ConnectionString;


    /// <summary>
    /// 得到某一用户的，某一节点的分页数据
    /// </summary>
    /// <param name="NodeId">节点id</param>
    /// <param name="PageSize">每页显示的数据量</param>
    /// <param name="PageIndex">显示的第几页</param>
    /// <returns></returns>
    public static List<ztresult> GetResultList(string NodeId, string type, int PageSize, int PageIndex, string isupdate, out int count)
    {
        try
        {
            List<int> list;
            List<int> sp;
            if (isupdate.ToUpper() != "UP")
            {
                sp = GetResultList(NodeId, type, "0");
            }
            else
            {
                sp = GetResultList(NodeId, type, "3");
            }
            List<int> add = GetResultList(NodeId, type, "1");
            List<int> del = GetResultList(NodeId, type, "2");
            Dictionary<string, string> listsr = getStars(NodeId);
            sp.AddRange(add);
            list = sp.Except(del).ToList<int>();
            list.Sort();
            list.Reverse();
            count = list.Count;
            List<int> tmp = list.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList<int>();
            List<ztresult> result = new List<ztresult>();
            foreach (var x in tmp)
            {
                int star = 0;
                string from = "检索式";
                if (add.Contains(x))
                {
                    from = "人工添加";
                }
                if (listsr.ContainsKey(x.ToString()))
                {
                    star = Convert.ToInt32(listsr[x.ToString()]);
                }
                result.Add(new ztresult() { pid = x, star = star, strForm = from });

            }
            return result;

        }
        catch (Exception ex)
        {
            count = 0;
            //logger.Error(ex.ToString());
            return null;
        }
    }

    /// <summary>
    /// 得到某一用户的，某一节点的分页数据
    /// </summary>
    /// <param name="NodeId">节点ID</param>
    /// <param name="type">数据类型cn en</param>
    /// <returns></returns>
    public static List<int> GetResultList(string NodeId, string type)
    {
        string sql = "select nid from zttree where pnid='" + NodeId + "'";
        DataTable dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql);
        if (dt.Rows.Count == 0)
        {
            try
            {
                List<int> list;
                List<int> sp = GetResultList(NodeId, type, "0");
                List<int> add = GetResultList(NodeId, type, "1");
                List<int> del = GetResultList(NodeId, type, "2");
                sp.AddRange(add);
                list = sp.Except(del).ToList<int>();
                list.Sort();
                list.Reverse();
                return list;
            }
            catch (Exception ex)
            {
                //logger.Error(ex.ToString());
                return null;
            }
        }
        else
        {
            List<int> list = new List<int>();
            foreach (DataRow row in dt.Rows)
            {
                list.AddRange(GetResultList(row["nid"].ToString(), type));
            }

            return list.Distinct<int>().ToList<int>();
        }

    }
    /// </summary>
    /// <param name="zid">专题库id</param>
    /// <param name="type">数据类型cn en</param>
    /// <returns></returns>
    public static List<int> GetZTResultList(string zid, string type)
    {
        string sql1 = "select count(*) from ztdblist where zid='" + zid + "'";
        List<int> list = new List<int>();
        if (DBA.SqlDbAccess.ExecuteScalar(CommandType.Text, sql1).ToString() == "0")
        {
            list.AddRange(GetResultList(zid, type));
        }
        else
        {
            string sql = "select nid from zttree where zid='" + zid + "' and live=0";
            DataTable dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql);

            
            foreach (DataRow row in dt.Rows)
            {
                list.AddRange(GetResultList(row["nid"].ToString(), type));
            }
        }

        return list.Distinct<int>().ToList<int>();
    }
    /// <summary>
    /// 得到某一个用户，某一节点的数据总数
    /// </summary>
    /// <param name="CreateUserId"></param>
    /// <param name="NodeId"></param>
    /// <returns></returns>
    public static int GetItemCountByNodeId(int NodeId, string type)
    {
        PQDataContext db = new PQDataContext();
        return (from th in db.ztdb
                where th.NodeId == NodeId && th.type == type && th.isdel == false
                select th.Pid.Value).Count();
    }
    /// <summary>
    /// 得到某一个用户，某一节点的数据总数
    /// </summary>
    /// <param name="CreateUserId"></param>
    /// <param name="NodeId"></param>
    /// <returns></returns>
    public static int GetItemCountByNodeId(int NodeId, string type, string isupdate)
    {
        bool isup = false;
        if (isupdate.ToUpper() == "UP") isup = true;
        PQDataContext db = new PQDataContext();
        return (from th in db.ztdb
                where th.NodeId == NodeId && th.type == type && th.isdel == false && th.isUpdate == isup
                select th.Pid.Value).Count();
    }

    /// <summary>
    /// 得到专题库某一节点的子节点
    /// </summary>
    /// <param name="zid"></param>
    /// <param name="PId"></param>
    /// <returns></returns>
    public static string GetNodes(string zid, string PId)
    {
        StringBuilder sb = new StringBuilder();
        PQDataContext db = new PQDataContext();
        IQueryable<ztTree> tree;
        if (PId == "")
        {
            tree = from x in db.ztTree
                   where x.zid == zid && x.PNid == PId && x.isdel == false
                   orderby x.NodeId ascending
                   select x;

            if (tree.Count() > 0)
            {
                sb.Append("[");
                foreach (var x in tree)
                {
                    sb.Append(string.Format("{0}\"id\":\"{1}\",\"text\":\"{2}\",\"state\":\"{3}\",\"attributes\":{0}\"live\":\"{4}\",\"des\":\"{5}\",\"Nid\":\"{7}\"{6}", "{", x.NodeId, x.NodeName.Trim(), "open", x.live, x.des, "}", x.Nid));
                    //sb.Append(string.Format("{0}\"id\":\"{1}\",\"name\":\"{2}\",\"open\":\"{3}\"", "{", x.NodeId, x.NodeName, "true"));
                    var subnodes = from node in db.ztTree
                                   where node.PNid == x.Nid && node.isdel == false
                                   orderby x.NodeId ascending
                                   select node;
                    if (subnodes.Count() > 0)
                    {
                        sb.Append(",\"children\": [");
                        foreach (var sbn in subnodes)
                        {
                            sb.Append(string.Format("{0}\"id\":\"{1}\",\"text\":\"{2}\",\"state\":\"{3}\",\"attributes\":{0}\"live\":\"{4}\",\"des\":\"{5}\",\"Nid\":\"{8}\"{6}{7}", "{", sbn.NodeId, sbn.NodeName.Trim(), (sbn.IsParent == 0 ? "open" : "closed"), sbn.live, sbn.des, "}", "},", sbn.Nid));
                            //sb.Append(string.Format("{0}\"id\":\"{1}\",\"name\":\"{2}\",\"isParent\":\"{3}\"{4}", "{", sbn.NodeId, sbn.NodeName, (sbn.IsParent == 0 ? "false" : "true"), "},"));
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
            var tmp = from y in db.ztTree
                      where y.NodeId.ToString() == PId
                      select y.Nid;
            if (tmp.Count() > 0)
            {
                PId = tmp.First();

                tree = from x in db.ztTree
                       where x.PNid == PId && x.isdel == false
                       orderby x.NodeId ascending
                       select x;

                if (tree.Count() > 0)
                {
                    sb.Append("[");
                    foreach (var x in tree)
                    {
                        sb.Append(string.Format("{0}\"id\":\"{1}\",\"text\":\"{2}\",\"state\":\"{3}\",\"attributes\":{0}\"live\":\"{4}\",\"des\":\"{5}\",\"Nid\":\"{8}\"{6}{7}", "{", x.NodeId, x.NodeName.Trim(), (x.IsParent == 0 ? "open" : "closed"), x.live, x.des, "}", "},", x.Nid));
                        //sb.Append(string.Format("{0}\"id\":\"{1}\",\"name\":\"{2}\",\"isParent\":\"{3}\"{4}", "{", x.NodeId, x.NodeName, (x.IsParent == 0 ? "false" : "true"), "},"));

                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("]");

                }
            }
        }
        return sb.ToString();
    }
    /// <summary>
    /// 得到专题库某一节点的子节点
    /// </summary>
    /// <param name="zid"></param>
    /// <param name="PId"></param>
    /// <param name="fileter"></param>
    /// <returns></returns>
    public static string GetNodes(string zid, string PId, string fileter)
    {
        StringBuilder sb = new StringBuilder();
        PQDataContext db = new PQDataContext();
        IQueryable<ztTree> tree;
        if (PId == "")
        {
            tree = from x in db.ztTree
                   where x.zid == zid && x.PNid == PId && x.isdel == false
                   orderby x.NodeId ascending
                   select x;

            if (tree.Count() > 0)
            {
                sb.Append("[");
                foreach (var x in tree)
                {
                    sb.Append(string.Format("{0}\"id\":\"{1}\",\"text\":\"{2}\",\"state\":\"{3}\",\"attributes\":{0}\"live\":\"{4}\",\"des\":\"{5}\",\"Nid\":\"{7}\"{6}", "{", x.NodeId, x.NodeName.Trim(), "open", x.live, x.des.Trim(), "}", x.Nid));
                    //sb.Append(string.Format("{0}\"id\":\"{1}\",\"name\":\"{2}\",\"open\":\"{3}\"", "{", x.NodeId, x.NodeName, "true"));
                    var subnodes = from node in db.ztTree
                                   where node.PNid == x.Nid && node.isdel == false
                                   orderby x.NodeId ascending
                                   select node;
                    if (subnodes.Count() > 0)
                    {
                        sb.Append(",\"children\": [");
                        foreach (var sbn in subnodes)
                        {
                            sb.Append(string.Format("{0}\"id\":\"{1}\",\"text\":\"{2}\",\"state\":\"{3}\",\"attributes\":{0}\"live\":\"{4}\",\"des\":\"{5}\",\"Nid\":\"{8}\"{6}{7}", "{", sbn.NodeId, sbn.NodeName.Trim(), (sbn.IsParent == 0 ? "open" : "closed"), sbn.live, sbn.des.Trim(), "}", "},", sbn.Nid));
                            //sb.Append(string.Format("{0}\"id\":\"{1}\",\"name\":\"{2}\",\"isParent\":\"{3}\"{4}", "{", sbn.NodeId, sbn.NodeName, (sbn.IsParent == 0 ? "false" : "true"), "},"));
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
            var tmp = from y in db.ztTree
                      where y.NodeId.ToString() == PId
                      select y.Nid;
            if (tmp.Count() > 0)
            {
                PId = tmp.First();

                tree = from x in db.ztTree
                       where x.PNid == PId && x.isdel == false
                       orderby x.NodeId ascending
                       select x;

                if (tree.Count() > 0)
                {
                    sb.Append("[");
                    foreach (var x in tree)
                    {
                        sb.Append(string.Format("{0}\"id\":\"{1}\",\"text\":\"{2}\",\"state\":\"{3}\",\"attributes\":{0}\"live\":\"{4}\",\"des\":\"{5}\",\"Nid\":\"{8}\"{6}{7}", "{", x.NodeId, x.NodeName.Trim(), (x.IsParent == 0 ? "open" : "closed"), x.live, x.des.Trim(), "}", "},", x.Nid));
                        //sb.Append(string.Format("{0}\"id\":\"{1}\",\"name\":\"{2}\",\"isParent\":\"{3}\"{4}", "{", x.NodeId, x.NodeName, (x.IsParent == 0 ? "false" : "true"), "},"));

                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("]");

                }
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// 添加专题库的节点
    /// </summary>
    /// <param name="zid">专题id</param>
    /// <param name="parent">父节点（没有则为0）</param>
    /// <param name="name">节点名称</param>
    /// <param name="clsType">数据类型 cn en</param>
    /// <param name="des">节点描述</param>
    /// <param name="live">级别 根节点为0 以此+1</param>
    /// <returns></returns>
    public static string addNode(string zid, string parent, string name, string clsType, string des, string live)
    {
        int CreateUserId = Convert.ToInt32(HttpContext.Current.Session["Userid"].ToString());
        int lv = Convert.ToInt32(live);
        int newid;
        string Nid = Guid.NewGuid().ToString();
        name = name.Trim();
        try
        {
            using (SqlConnection conn = DBA.SqlDbAccess.GetSqlConnection(xituConnectionString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();


                if (Convert.ToInt32(DBA.SqlDbAccess.ExecuteScalar(tran, CommandType.Text, "select count(*) from ztTree where isdel=0 and NodeName='" + name.Replace("'", "''") + "' and PNid='" + parent.Replace("'", "''") + "' and CreateUserId=" + CreateUserId + " and zid='" + zid.Replace("'", "''") + "'")) >= 1)
                {
                    return "{\"id\":\"exists\"}";
                }
                string addnodesql = "insert into ztTree (NodeName,Nid,PNid,CreateUserId,IsParent,type,des,zid,live) values (@NodeName,@Nid,@PNid,@CreateUserId,0,@type,@des,@zid,@live)";

                SqlParameter[] pars = new SqlParameter[]{
                new SqlParameter("@NodeName",SqlDbType.NVarChar),
                new SqlParameter("@Nid",SqlDbType.Char),                
                new SqlParameter("@PNid",SqlDbType.Char),                
                new SqlParameter("@CreateUserId",SqlDbType.Int),                
                new SqlParameter("@type",SqlDbType.NVarChar),
                new SqlParameter("@des",SqlDbType.NVarChar),
                new SqlParameter("@zid",SqlDbType.VarChar),
                new SqlParameter("@live",SqlDbType.Int)};

                pars[0].Value = name;
                pars[1].Value = Nid;
                pars[2].Value = parent;
                pars[3].Value = CreateUserId;
                pars[4].Value = clsType;
                pars[5].Value = des;
                pars[6].Value = zid;
                pars[7].Value = lv;
                DBA.SqlDbAccess.ExecNoQuery(tran, CommandType.Text, addnodesql, pars);
                //更新父节点标记
                DBA.SqlDbAccess.ExecNoQuery(tran, CommandType.Text, "update ztTree set IsParent=1 where NId='" + parent.Replace("'", "''") + "'");
                newid = Convert.ToInt32(DBA.SqlDbAccess.ExecuteScalar(tran, CommandType.Text, "select @@identity from ztTree"));
                tran.Commit();
            }

        }
        catch (Exception ex)
        {
            return "{\"id\":\"failed\",\"Nid\":\"failed\"}";
        }
        return "{\"id\":\"" + newid + "\",\"Nid\":\"" + Nid + "\"}";

        //  return "{\"mess\":\"" + ht["mess"] + "\",\"id\":\"" + ht["id"].ToString().Replace("\\", "\\\\") + "\",\"text\":\"" + ht["text"] + "\"}";

    }
    /// <summary>
    /// 删除节点
    /// </summary>
    /// <param name="id">节点id</param>
    /// <returns></returns>
    public static string deleteNode(string id)
    {
        string result = "succ";
        try
        {
            string pid = DBA.SqlDbAccess.ExecuteScalar(xituConnectionString, CommandType.Text, string.Format("select PNid from ztTree where Nid='{0}'", id.Replace("'", "''"))).ToString();
            int count = (int)DBA.SqlDbAccess.ExecuteScalar(xituConnectionString, CommandType.Text, "select count(*) from ztTree where PNid='" + pid.Replace("'", "''") + "' and isdel=0");
            DBA.SqlDbAccess.ExecNoQuery(xituConnectionString, CommandType.Text, "update ztTree set isdel=1 where Nid='" + id.Replace("'", "''") + "'");

            if (count == 1)
            {
                DBA.SqlDbAccess.ExecNoQuery(xituConnectionString, CommandType.Text, "update ztTree set IsParent=0 where Nid='" + pid.Replace("'", "''") + "'");
            }
        }
        catch (Exception ex)
        {
            result = "failed";
        }

        return result;
    }
    /// <summary>
    /// 修改节点名称
    /// </summary>
    /// <param name="id">节点id</param>
    /// <param name="name">节点名称</param>
    /// <param name="des">描述</param>
    /// <returns></returns>
    public static string Rename(string id, string name, string des)
    {
        int CreateUserId = Convert.ToInt32(HttpContext.Current.Session["Userid"].ToString());
        string result = "succ";
        try
        {

            if (Convert.ToInt32(DBA.SqlDbAccess.ExecuteScalar(CommandType.Text, "select count(*) from ztTree where isdel=0 and NodeName='" + name.Replace("'", "''") + "' and PNid=(select PNid from zttree where Nid='" + id.Replace("'", "''") + "') and CreateUserId=" + CreateUserId)) >= 1)
            {
                return "exists";
            }

            SqlParameter[] pars = new SqlParameter[]{
                new SqlParameter("@NodeName",SqlDbType.NVarChar),
                new SqlParameter("@des",SqlDbType.NVarChar),
                new SqlParameter("@Nid",SqlDbType.Char)
            };
            pars[0].Value = name;
            pars[1].Value = des;
            pars[2].Value = id;
            DBA.SqlDbAccess.ExecNoQuery(xituConnectionString, CommandType.Text, "update ztTree set NodeName=@NodeName,des =@des where Nid=@Nid", pars);
        }
        catch (Exception ex)
        {
            result = "failed";
        }

        return result;
    }

    /// <summary>
    /// 得到节点的描述
    /// </summary>
    /// <param name="nodid"></param>
    /// <returns></returns>
    public static string getNodeDes(string nodid)
    {
        string result = "succ";
        try
        {
            string SQL = string.Format("select des from zttree where Nid='{0}'", nodid);
            object r = DBA.SqlDbAccess.ExecuteScalar(xituConnectionString, CommandType.Text, SQL);
            if (r == null)
            {
                result = "";
            }
            else
            {
                result = r.ToString();
            }
        }
        catch (Exception ex)
        {
            result = "failed";
        }
        return result;
    }

    /// <summary>
    /// 根据专题ID 返回专题库名称
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string getztName(string zid)
    {
        string result = "succ";
        try
        {
            string SQL = string.Format("select ztDbName from ZtDbList where zid='" + zid.Replace("'", "''") + "'");
            object r = DBA.SqlDbAccess.ExecuteScalar(xituConnectionString, CommandType.Text, SQL);
            if (r == null)
            {
                result = "failed";
            }
            else
            {
                result = r.ToString();
            }
        }
        catch (Exception ex)
        {
            result = "failed";
        }
        return result;
    }

    /// <summary>
    /// 得到所有的专题库 （专题库id,专题库名称）
    /// </summary>
    /// <returns></returns>
    public static DataTable getztName()
    {

        string SQL = string.Format("select [zid],ztdbname from ztdblist where dbtype =0 and isdel =0");
        return DBA.SqlDbAccess.GetDataTable(xituConnectionString, CommandType.Text, SQL);
    }
    /// <summary>
    /// 根据用户ID得到所有的专题库 （专题库id,专题库名称）
    /// </summary>
    /// <param name="userid"></param>
    /// <returns></returns>
    public static DataTable getName(string userid)
    {

        string SQL = string.Format("select * from ztdblist where dbtype =0 and isdel =0 and CreateUserId='" + userid + "'");
        return DBA.SqlDbAccess.GetDataTable(xituConnectionString, CommandType.Text, SQL);
    }
    /// <summary>
    /// 添加到专题库
    /// </summary>
    /// <param name="pids">专利ID</param>
    /// <param name="nodids">专题库节点</param>
    /// <param name="type">数据类型（cn|en)</param>
    /// <param name="from">来源：（当前操作用户|检索式）</param>
    /// <returns></returns>
    public static string AddToTH(string pids, string nodids, string type, string from)
    {
        List<string> lstpid = pids.Trim(',').Split(',').ToList<string>();
        List<string> lstnodeid = nodids.Trim(',').Split(',').ToList<string>();
        return AddToTH(lstpid, lstnodeid, type, "", from);
    }

    /// <summary>
    /// 专利从专题库的一个节点移动到另外节点
    /// </summary>
    /// <param name="pids">专利ID</param>
    /// <param name="nodids">节点ID</param>
    /// <param name="type">数据类型 cn、en</param>
    /// <param name="OldNodeId">旧的节点ID</param>
    /// <param name="from"></param>
    /// <returns></returns>
    public static string AddToTH(string pids, string nodids, string type, string OldNodeId, string from)
    {
        List<string> lstpid = pids.Trim(',').Split(',').ToList<string>();
        List<string> lstnodeid = nodids.Trim(',').Split(',').ToList<string>();
        return AddToTH(lstpid, lstnodeid, type, OldNodeId, from);
    }

    /// <summary>
    /// 重载 把从Web端的专利id,节点id拆分成数组并进行插入
    /// </summary>
    /// <param name="lstpid"></param>
    /// <param name="lstnodeid"></param>
    /// <param name="type"></param>
    /// <param name="OldNodeId"></param>
    /// <param name="from"></param>
    /// <returns></returns>
    public static string AddToTH(List<string> lstpid, List<string> lstnodeid, string type, string OldNodeId, string from)
    {
        int CreateUserId = Convert.ToInt32(HttpContext.Current.Session["Userid"].ToString());
        // int CreateUserId = 1;
        string result = "succ";
        List<int> lstid = lstpid.ConvertAll<int>(Convert.ToInt32);
        try
        {


            foreach (string nodeid in lstnodeid)
            {
                if (nodeid == OldNodeId) continue; //如果移动到原来的节点下，不错任何操作
                AddWriteZTCNP(nodeid, type, "1", lstid);
            }

            if (!string.IsNullOrEmpty(OldNodeId))
            {
                if (!lstnodeid.Contains(OldNodeId))
                {
                    AddWriteZTCNP(OldNodeId, type, "2", lstid);

                }
            }

        }
        catch (Exception ex)
        {
            result = "failed";
        }
        return result;
    }
    /// <summary>
    /// 专题库中 某一专利的删除
    /// </summary>
    /// <param name="Pid">专利id</param>
    /// <param name="NodeId">节点id</param>
    /// <returns></returns>
    public static string DelToTH(string Pid, string NodeId, string type)
    {
        string result = "succ";
        try
        {
            List<int> lstpid = Pid.Trim(',').Split(',').ToList<string>().ConvertAll<int>(Convert.ToInt32);
            AddWriteZTCNP(NodeId, type, "2", lstpid);
        }
        catch (Exception ex)
        {
            result = "failed";
        }
        return result;

    }

    /// <summary>
    /// 添加到专题 重载，支持事务
    /// </summary>
    /// <param name="tran"></param>
    /// <param name="lstpid"></param>
    /// <param name="lstnodeid"></param>
    /// <param name="from"></param>
    /// <param name="CreateUserId"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static string AddToTH(SqlTransaction tran, List<int> lstpid, List<string> lstnodeid, string from, string CreateUserId, string type)
    {
        string result = "succ";
        foreach (string nodeid in lstnodeid)
        {
            AddWriteZTCNP(nodeid, type, "1", lstpid);
        }
        return result;

    }


    /// <summary>
    /// 获取 用户节点的检索式
    /// </summary>
    /// <param name="NodeId"></param>
    /// <returns></returns>
    public static DataTable GetSPattern(string NodeId, string type)
    {
        string sql = string.Format("select id, sp,Hit,UpdateDate,isUsed from ztsp where Nid='{0}' and isdel=0 and type='{1}'", NodeId.Replace("'", "''"), type.Replace("'", "''"));
        return DBA.SqlDbAccess.GetDataTable(xituConnectionString, CommandType.Text, sql);
    }


    /// <summary>
    /// 获取专题库下指定数据类型的所有的检索式。
    /// </summary>
    ///<param name="type">数据类型 cn en</param>
    ///<param name="zid">专题库ID</param>
    /// <returns></returns>
    public static DataTable GetSearchPattern(string zid, string type)
    {
        string sql = string.Format("select id,NodeId,NodeName, SPNum,sp,Hit,UpdateDate,st,Nid from ztsp where zid='{0}' and isdel=0 and type='{1}' order by spNum asc", zid.Replace("'", "''"), type.Replace("'", "''"));
        return DBA.SqlDbAccess.GetDataTable(xituConnectionString, CommandType.Text, sql);
    }

    /// <summary>
    /// 更新专题库节点绑定的检索式
    /// </summary>
    /// <param name="id">检索式id</param>
    /// <param name="SearchPattern">检索式</param>
    /// <param name="Hit">命中篇数</param>
    /// <param name="nodeid">节点id</param>
    /// <param name="type">数据类型 cn en</param>
    /// <returns></returns>
    public static string UpdateSearchPattern(string id, string SearchPattern, int Hit, string nodeid, string type)
    {
        string result = "succ";
        try
        {
            //检索
            int intCount = 0;
            //int userid = Convert.ToInt32(System.Web.HttpContext.Current.Session["Userid"].ToString());
            int userid = 1;
            SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
            string strHits = search.GetSearchDataHits("", SearchPattern, userid, type.ToUpper());
            if (strHits == "ERROR") return "ERROR";
            string strNo = string.Empty;
            if (strHits.IndexOf("(") >= 0 && strHits.IndexOf(")") > 0)
            {
                strNo = strHits.Substring(strHits.IndexOf("(") + 1, strHits.IndexOf(")") - strHits.IndexOf("(") - 1);
            }
            if (strHits.IndexOf("<hits:") >= 0 && strHits.IndexOf(">") > 0)
            {
                intCount = Convert.ToInt32(strHits.Substring(strHits.IndexOf("<hits:") + 6, strHits.IndexOf(">") - strHits.IndexOf("<hits:") - 6).Trim());
            }
            //判断结果是否太大
            if (intCount > 5000)
            {
                result = "对不起，检索式检索结果大于5000，请修改检索式，缩小检索结果范围";
                return result;
            }
            //获取检索结果
            SearchPattern schPatItem = new SearchPattern();
            schPatItem.SearchNo = strNo; ;  //检索编号[001-999]
            //检索式：F XX 2010/AD
            schPatItem.UserId = userid;   //用户ID
            if (type.ToUpper() == "CN")
            {
                schPatItem.DbType = SearchDbType.Cn;
            }
            else
            {
                schPatItem.DbType = SearchDbType.DocDB;
            }
            ResultServices res = new ResultServices();
            List<int> lst = res.GetResultList(schPatItem, "");
            List<string> nodids = new List<string>();
            nodids.Add(nodeid);
            //
            // 更新 除了核心专利+来源是 “检索式” 的删除标记            
            //
            using (SqlConnection conn = DBA.SqlDbAccess.GetSqlConnection())
            {
                conn.Open();
                ////删除旧的数据
                SqlTransaction trans = conn.BeginTransaction();
                //string delztdb = string.Format("update ztdb set isdel=1 where (iscore =0 and [Form] ='检索式') and Nid='{0}' and type='{1}'", nodeid, type);
                //DBA.SqlDbAccess.ExecNoQuery(trans, CommandType.Text, delztdb);

                ////添加新的数据
                //AddToTH(trans, lst, nodids, "检索式", userid.ToString(), type);
                WriteZTCNP(nodeid, type, "0", lst);

                //更新检索式
                string SQL = string.Format("update ztsp set sp ='{0}',Hit={1} where id={2}", SearchPattern.Replace("'", "''"), intCount, id.Replace("'", "''"));
                DBA.SqlDbAccess.ExecNoQuery(trans, CommandType.Text, SQL);

                trans.Commit();
            }

        }
        catch (Exception ex)
        {
            result = "failed";
        }
        return result;
    }

    /// <summary>
    /// 更新检索式
    /// </summary>
    /// <param name="id">检索式id</param>
    /// <param name="SearchPattern">检索式</param>
    /// <returns></returns>
    public static string UpdateSearchPattern(string id, string SearchPattern)
    {
        string result = "succ";
        try
        {
            //更新检索式
            string SQL = string.Format("update ztsp set sp ='{0}',Hit={1},st='待检索' where id={2}", SearchPattern.Replace("'", "''"), "NULL", id);
            DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, SQL);
        }
        catch (Exception ex)
        {
            result = "failed";
        }
        return result;
    }

    /// <summary>
    /// 删除检索式 & 检索式对应的数据 
    /// 注：保留 用户进行标星的专利，来源不是检索式的专利
    /// </summary>
    /// <param name="id">检索式id</param>
    /// <param name="nodeid">节点id</param>
    /// <param name="type">类型</param>
    /// <returns></returns>
    public static string deleteSearchPattern(string id, string nodeid, string type)
    {
        string result = "succ";
        try
        {
            //
            // 更新 除了核心专利+来源是 “检索式” 的删除标记            
            //
            using (SqlConnection conn = DBA.SqlDbAccess.GetSqlConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                //string delztdb = string.Format("update ztdb set isdel=1 where (iscore =0 and [form] ='检索式') and Nid='{0}' and type='{1}'", nodeid, type);
                string filename = GetZTCNP(nodeid, type, "0");
                if (Directory.Exists(Path.GetDirectoryName(filename)))
                {
                    if (File.Exists(filename)) File.Delete(filename);
                }
                string SQL = string.Format("update ztsp set isdel =1 where id={0}", id);
                DBA.SqlDbAccess.ExecNoQuery(trans, CommandType.Text, SQL);
                trans.Commit();
            }
        }
        catch (Exception ex)
        {
            result = "failed";
        }
        return result;
    }


    /// <summary>
    /// 删除检索式
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string deleteSearchPattern(string id)
    {
        string result = "succ";
        try
        {
            string SQL = string.Format("update ztsp set isdel =1 where id={0}", id);
            DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, SQL);

        }
        catch (Exception ex)
        {
            result = "failed";
        }
        return result;
    }

    /// <summary>
    /// 对专题库节点添加检索式&并对检索式进行检索入库。
    /// 注：大于5000条的检索式 不允许插入。
    /// </summary>
    /// <param name="NodeId">节点id</param>
    /// <param name="SearchPattern">检索式</param>
    /// <param name="Hit">命中篇数</param>
    /// <param name="type">数据类型 cn en</param>
    /// <returns></returns>
    public static string addSearchPattern1(string NodeId, string SearchPattern, string Hit, string type, string zid)
    {
        string result = "succ";

        try
        {
            //检索
            int intCount = 0;
            //int userid = Convert.ToInt32(System.Web.HttpContext.Current.Session["Userid"].ToString());
            int userid = 1;
            SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
            string strHits = search.GetSearchDataHits("", SearchPattern, userid, type.ToUpper());
            if (strHits == "ERROR") return "ERROR";
            string strNo = string.Empty;
            if (strHits.IndexOf("(") >= 0 && strHits.IndexOf(")") > 0)
            {
                strNo = strHits.Substring(strHits.IndexOf("(") + 1, strHits.IndexOf(")") - strHits.IndexOf("(") - 1);
            }
            if (strHits.IndexOf("<hits:") >= 0 && strHits.IndexOf(">") > 0)
            {
                intCount = Convert.ToInt32(strHits.Substring(strHits.IndexOf("<hits:") + 6).Trim('>').Trim());
            }
            //判断结果是否太大
            if (intCount > 50000)
            {
                result = "对不起，检索式检索结果大于50000，请缩小修改检索式，缩小检索结果范围";
                return result;
            }
            //获取检索结果
            SearchPattern schPatItem = new SearchPattern();
            schPatItem.SearchNo = strNo; ;  //检索编号[001-999]
            //检索式：F XX 2010/AD
            schPatItem.UserId = userid;   //用户ID
            if (type.ToUpper() == "CN")
            {
                schPatItem.DbType = SearchDbType.Cn;
            }
            else
            {
                schPatItem.DbType = SearchDbType.DocDB;
            }
            ResultServices res = new ResultServices();
            List<int> lst = res.GetResultList(schPatItem, "");
            List<string> nodids = new List<string>();
            nodids.Add(NodeId);
            //添加到ztdb中
            using (SqlConnection conn = DBA.SqlDbAccess.GetSqlConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                if (intCount > 0)
                {
                    //AddToTH(tran, lst, nodids, "检索式", userid.ToString(), type);
                    WriteZTCNP(NodeId, type, "0", lst);
                }
                string SQL = string.Format("insert into ztsp (Nid,sp,Hit,Type,isdel,zid) values ('{0}','{1}',{2},'{3}',0,'{4}')", NodeId, SearchPattern, intCount, type, zid);
                DBA.SqlDbAccess.ExecNoQuery(tran, CommandType.Text, SQL);
                tran.Commit();
            }


        }
        catch (Exception ex)
        {
            result = "失败:" + ex.Message;
        }
        return result;
    }

    /// <summary>
    /// 添加检索式
    /// </summary>
    /// <param name="zid"></param>
    /// <param name="SearchNo"></param>
    /// <param name="SearchPattern"></param>
    /// <param name="Hit"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static string addSearchPattern(string zid, string SearchNo, string SearchPattern, string Hit, string type)
    {
        string result = "succ";
        int CreateUserId = Convert.ToInt32(HttpContext.Current.Session["Userid"].ToString());
        try
        {
            string SQL = string.Format("insert into ztsp (zid,SPNum,sp,Hit,Type,isdel,st) values ('{0}',{1},'{2}',{3},'{4}',0,'{5}')", zid.Replace("'", "''"), SearchNo.Replace("'", "''"), SearchPattern.Replace("'", "''"), Hit, type.Replace("'", "''"), "待检索");
            DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, SQL);

        }
        catch (Exception ex)
        {
            result = "失败:" + ex.Message;
        }
        return result;
    }

    /// <summary>
    /// 添加专利数据库
    /// </summary>
    /// <param name="ZtDbname"></param>
    /// <param name="nType"></param>
    /// <param name="ztDbdes"></param>
    /// <returns></returns>
    public static string addZtDb(string ZtDbname, int nType, string ztDbdes)
    {
        return addZtDb(ZtDbname, nType, ztDbdes, "");
    }

    /// <summary>
    /// 专利数据库信息修改
    /// </summary>
    /// <param name="_nDbId"></param>
    /// <param name="ZtDbname"></param>
    /// <param name="ztDbdes"></param>
    /// <returns></returns>
    public static string UpDateZtDb(string zid, string ZtDbname, string ztDbdes)
    {
        return UpDateZtDb(zid, ZtDbname, ztDbdes, "");
    }

    /// <summary>
    /// 添加专利数据库
    /// </summary>
    /// <param name="ZtDbname"></param>
    /// <param name="nType"></param>
    /// <param name="ztDbdes"></param>
    /// <returns></returns>
    public static string addZtDb(string ZtDbname, int nType, string ztDbdes, string strImg)
    {
        int CreateUserId = Convert.ToInt32(HttpContext.Current.Session["Userid"].ToString());
        string zid = Guid.NewGuid().ToString();
        try
        {
            //DbID
            //ztDbName
            //dbType
            //CreateUserId
            //CreateTime
            //DbDes
            //IsDel

            using (SqlConnection conn = DBA.SqlDbAccess.GetSqlConnection(xituConnectionString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                SqlParameter[] pars = new SqlParameter[]{
                new SqlParameter("@ztDbName",SqlDbType.NVarChar),
                new SqlParameter("@dbType",SqlDbType.Int),
                new SqlParameter("@CreateUserId",SqlDbType.NVarChar),
                new SqlParameter("@DbDes",SqlDbType.NVarChar),
                new SqlParameter("@ztHeardImg",SqlDbType.NVarChar),
                new SqlParameter("@zid",SqlDbType.VarChar)
                };
                pars[0].Value = ZtDbname;
                pars[1].Value = nType;
                pars[2].Value = CreateUserId;
                pars[3].Value = ztDbdes;
                pars[4].Value = strImg;
                pars[5].Value = zid;

                DBA.SqlDbAccess.ExecNoQuery(tran, CommandType.Text, "insert into ZtDbList (ztDbName,dbType,CreateUserId,DbDes,ztHeardImg,zid) values (@ztDbName,@dbType,@CreateUserId,@DbDes,@ztHeardImg,@zid)", pars);

                tran.Commit();
            }
            return zid;
        }
        catch (Exception ex)
        {
            return "failed";
        }
    }

    /// <summary>
    /// 专利数据库信息修改
    /// </summary>
    /// <param name="_nDbId"></param>
    /// <param name="ZtDbname"></param>
    /// <param name="ztDbdes"></param>
    /// <returns></returns>
    public static string UpDateZtDb(string zid, string ZtDbname, string ztDbdes, string strImg)
    {
        string result = "succ";
        try
        {
            //DbID
            //ztDbName
            //dbType
            //CreateUserId
            //CreateTime
            //DbDes
            //IsDel

            SqlParameter[] pars = new SqlParameter[]{
                new SqlParameter("@ztDbName",SqlDbType.NVarChar),
                new SqlParameter("@DbDes",SqlDbType.NVarChar),
                new SqlParameter("@zid",SqlDbType.VarChar),
                new SqlParameter("@ztHeardImg",SqlDbType.NVarChar)
            };
            pars[0].Value = ZtDbname;
            pars[1].Value = ztDbdes;
            pars[2].Value = zid;
            pars[3].Value = strImg;
            DBA.SqlDbAccess.ExecNoQuery(xituConnectionString, CommandType.Text, "update ZtDbList set ZtDbname=@ZtDbname,DbDes =@DbDes,ztHeardImg=@ztHeardImg where zid=@zid", pars);
        }
        catch (Exception ex)
        {
            result = "failed";
        }

        return result;
    }
    /// <summary>
    /// 得到某一个专利，在某一节点的标星 等级
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="Nodeid"></param>
    /// <returns></returns>
    public static Dictionary<string, string> getStars(string Nodeid)
    {
        Dictionary<string, string> s = new Dictionary<string, string>();
        try
        {
            DataTable dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, string.Format("select pid,iscore from ztdb where  Nid='{0}' and isdel =0  and iscore>0", Nodeid.Replace("'", "")));
            foreach (DataRow row in dt.Rows)
            {
                if (!s.ContainsKey(row["pid"].ToString()))
                {
                    s.Add(row["pid"].ToString(), row["iscore"].ToString());
                }
            }

            return s;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    /// <summary>
    /// 对某一个节点，某一篇专利 进行标星。
    /// </summary>
    /// <param name="pid">专利id</param>
    /// <param name="nodeid">节点id</param>
    /// <param name="value">标星等级（1-5）</param>
    /// <returns></returns>
    public static string setStars(string pid, string nodeid, string value, string type)
    {
        string result = "succ";
        TbUser user = (TbUser)HttpContext.Current.Session["USerInfo"];
        string ssql = "select count(*) from ztdb where pid={0} and isdel=0 and Nid='{1}' and type='{2}'";
        string usql = "update ztdb set Iscore={2} where pid={0} and isdel=0 and Nid='{1}' and type='{3}'";
        string isql = "insert into ztdb (pid,nid,iscore,type) values ({0},'{1}',{2},'{3}')";
        try
        {
            if (DBA.SqlDbAccess.ExecuteScalar(CommandType.Text, string.Format(ssql, pid.Replace("'", "''"), nodeid.Replace("'", "''"), type.Replace("'", "''"))).ToString() == "0")
            {
                DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, string.Format(isql, pid.Replace("'", "''"), nodeid.Replace("'", "''"), value.Replace("'", "''"), type.Replace("'", "''")));
            }
            else
            {
                DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, string.Format(usql, pid.Replace("'", "''"), nodeid.Replace("'", "''"), value.Replace("'", "''"), type.Replace("'", "''")));

            }
        }
        catch (Exception ex)
        {
            result = "failed";
        }

        return result;
    }


    /// <summary>
    /// 如果用户是企业用户，得到用户的企业在线数据库的数据库id
    /// </summary>
    /// <returns></returns>
    public static string setqyztid()
    {
        string result = "succ";
        TbUser user = (TbUser)HttpContext.Current.Session["USerInfo"];
        if (user.YongHuLeiXing.Trim() == "企业")
        {
            try
            {
                string SQL = string.Format("select zid from ZtDbList where dbtype=1 and createUserid=" + user.QiYeID.Value);
                object r = DBA.SqlDbAccess.ExecuteScalar(xituConnectionString, CommandType.Text, SQL);
                if (r == null)
                {
                    result = "failed";
                }
                else
                {
                    result = r.ToString();
                }
            }
            catch (Exception ex)
            {
                result = "failed";
            }
        }
        else
        {
            result = "failed1";
        }
        return result;
    }

    /// <summary>
    /// 使一条已经录入后的检索式 与 专题库的节点进行绑定
    /// </summary>
    /// <param name="spid"></param>
    /// <param name="nodeid"></param>
    /// <returns></returns>
    public static string SPBoundNode(string spid, string nodeid)
    {
        string result = "succ";
        try
        {
            DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, string.Format("update ztsp set Nid ='{0}',nodename = (select nodename from ztTree where Nid='{0}') where id = {1}", nodeid.Replace("'", "''"), spid.Replace("'", "''")));
        }
        catch (Exception ex)
        {
            result = "failed";
        }

        return result;
    }
    /// <summary>
    /// 使一条已经录入后的检索式 与 专题库的节点进行绑定
    /// </summary>
    /// <param name="spid"></param>
    /// <param name="nodeid"></param>
    /// <returns></returns>
    public static string RemoveBindToZT(string spid)
    {
        string result = "succ";
        try
        {
            DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, string.Format("update ztsp set Nid = '',nodename = '' where id = {0}", spid.Replace("'", "''")));
        }
        catch (Exception ex)
        {
            result = "failed";
        }

        return result;
    }
    public static string basepath = System.Configuration.ConfigurationManager.AppSettings["CPRS2010UserPath"].ToString();
    public static string GetZTCNP(string Nid, string dbtype, string ftype)
    {
        return string.Format("{0}\\ZT\\{1}\\{2}\\{3}.cnp", basepath, Nid, dbtype, ftype);
    }
    /// <summary>
    /// 得到某一结果文件的全部数据
    /// </summary>
    /// <param name="sp">检索式</param>
    /// <param name="SortExpression">排序</param>
    /// <returns>list int</returns>
    public static List<int> GetResultList(string Nid, string dbtype, string ftype)
    {
        List<int> lstfml = new List<int>();
        int readlength;
        string filepath = GetZTCNP(Nid, dbtype, ftype);
        if (!File.Exists(filepath))
        {
            return lstfml;
        }
        using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read))
        {
            byte[] allnos = new byte[fs.Length];
            readlength = fs.Read(allnos, 0, (int)fs.Length);
            for (int i = 0; i < readlength; i += 4)
            {
                lstfml.Add(BitConverter.ToInt32(allnos, i));
            }
        }
        //todo:StorBy(SortExpression);
        return lstfml;
    }

    public static bool WriteZTCNP(string Nid, string dbtype, string ftype, List<int> hitlist)
    {
        string filename = ztHelper.GetZTCNP(Nid, dbtype, ftype);
        if (Directory.Exists(Path.GetDirectoryName(filename)))
        {
            if (File.Exists(filename)) File.Delete(filename);
        }
        else
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filename));
        }
        using (FileStream fsw = new FileStream(filename, FileMode.CreateNew, FileAccess.Write))
        {
            foreach (var x in hitlist)
            {
                byte[] by = BitConverter.GetBytes(x);
                fsw.Write(by, 0, by.Length);
            }
        }
        return true;
    }
    public static bool AddWriteZTCNP(string Nid, string dbtype, string ftype, List<int> hitlist)
    {
        string filename = ztHelper.GetZTCNP(Nid, dbtype, ftype);
        if (!Directory.Exists(Path.GetDirectoryName(filename)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filename));
        }
        using (FileStream fsw = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
        {
            fsw.Seek(fsw.Length, SeekOrigin.Begin);
            foreach (var x in hitlist)
            {
                byte[] by = BitConverter.GetBytes(x);
                fsw.Write(by, 0, by.Length);
            }
        }
        return true;
    }
    public static string DelZtDb(string zid)
    {

        string result = "succ";
        try
        {
            DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, string.Format("update ZtDbList set isdel=1 where zid='{0}'", zid.Replace("'", "''")));
        }
        catch (Exception ex)
        {
            result = "failed";
        }
        return result;
    }
}
