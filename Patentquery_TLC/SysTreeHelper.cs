using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DBA;
using TLC;
using System.Xml.Linq;
using System.Collections;

public class SysTreeHelper
{
    public static PQDataContext db = new PQDataContext();
    public static int addNode(int parent, string name, string clsType, string des, bool isParent, int live)
    {
        int newid = 0;
        try
        {
            using (SqlConnection conn = DBA.SqlDbAccess.GetSqlConnection())
            {
                conn.Open();
                SqlParameter[] pars = new SqlParameter[]{
                        new SqlParameter("@NodeName",SqlDbType.NVarChar),
                        new SqlParameter("@PNodeid",SqlDbType.Int),  
                        new SqlParameter("@IsParent",SqlDbType.Bit),      
                        new SqlParameter("@type",SqlDbType.NVarChar),
                        new SqlParameter("@des",SqlDbType.NVarChar),
                        new SqlParameter("@live",SqlDbType.TinyInt)
                    };

                pars[0].Value = name;
                pars[1].Value = parent;
                pars[2].Value = isParent;
                pars[3].Value = clsType;
                pars[4].Value = des;
                pars[5].Value = live;
                DBA.SqlDbAccess.ExecNoQuery(conn, CommandType.Text, "insert into sysTree (NodeName,PNodeid,IsParent,type,des,live) values (@NodeName,@PNodeid,@IsParent,@type,@des,@live)", pars);
                newid = Convert.ToInt32(DBA.SqlDbAccess.ExecuteScalar(conn, CommandType.Text, "select @@identity from sysTree"));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return newid;

    }
    public static string GetNodes(int PId, string clstype)
    {
        StringBuilder sb = new StringBuilder();

        IQueryable<sysTree> tree;
        if (PId == 0)
        {
            tree = from x in db.sysTree
                   where x.PNodeid == PId && x.type == clstype && x.isdel == false
                   orderby x.NodeId ascending
                   select x;

            if (tree.Count() > 0)
            {
                sb.Append("[");
                foreach (var x in tree)
                {
                    sb.Append(string.Format("{0}\"id\":\"{1}\",\"text\":\"{2}\",\"state\":\"{3}\",\"attributes\":{0}\"live\":\"{4}\",\"des\":\"{5}\"{6}", "{", x.NodeId, x.NodeName, "closed", x.live, x.des, "}"));
                    //sb.Append(string.Format("{0}\"id\":\"{1}\",\"name\":\"{2}\",\"open\":\"{3}\"", "{", x.NodeId, x.NodeName, "true"));
                    //var subnodes = from node in db.sysTree
                    //               where node.PNodeid == x.NodeId && node.isdel == false
                    //               orderby x.NodeId ascending
                    //               select node;
                    //if (subnodes.Count() > 0)
                    //{
                    //    sb.Append(",\"children\": [");
                    //    foreach (var sbn in subnodes)
                    //    {
                    //        sb.Append(string.Format("{0}\"id\":\"{1}\",\"text\":\"{2}\",\"state\":\"{3}\",\"attributes\":{0}\"live\":\"{4}\",\"des\":\"{5}\"{6}{7}", "{", sbn.NodeId, sbn.NodeName, (sbn.IsParent.Value == false ? "open" : "closed"), sbn.live, sbn.des, "}", "},"));
                    //        //sb.Append(string.Format("{0}\"id\":\"{1}\",\"name\":\"{2}\",\"isParent\":\"{3}\"{4}", "{", sbn.NodeId, sbn.NodeName, (sbn.IsParent == 0 ? "false" : "true"), "},"));
                    //    }
                    //    sb.Remove(sb.Length - 1, 1);
                    //    sb.Append("]");
                    //}
                    sb.Append("},");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("]");
            }

        }
        else
        {

            tree = from x in db.sysTree
                   where x.PNodeid == PId && x.isdel == false && x.type == clstype
                   orderby x.NodeId ascending
                   select x;

            if (tree.Count() > 0)
            {
                sb.Append("[");
                foreach (var x in tree)
                {
                    sb.Append(string.Format("{0}\"id\":\"{1}\",\"text\":\"{2}\",\"state\":\"{3}\",\"attributes\":{0}\"live\":\"{4}\",\"des\":\"{5}\"{6}{7}", "{", x.NodeId, x.NodeName, (x.IsParent.Value == false ? "open" : "closed"), x.live, x.des, "}", "},"));
                    //sb.Append(string.Format("{0}\"id\":\"{1}\",\"name\":\"{2}\",\"isParent\":\"{3}\"{4}", "{", x.NodeId, x.NodeName, (x.IsParent == 0 ? "false" : "true"), "},"));

                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("]");

            }
        }
        return sb.ToString();
    }
    public string GetNodes(string clstype, string SType, string value)
    {
        StringBuilder sb = new StringBuilder();
        List<int> addedid = new List<int>();
        if (SType.ToUpper() == "KEY")
        {
            allNode = from x in db.sysTree
                      where x.type == clstype && x.isdel == false && x.NodeName.IndexOf(value) >= 0 && x.live.Value > 2
                      orderby x.live descending, x.PNodeid descending
                      select x;
        }
        else
        {
            if (clstype == "ARE")
            {

                allNode = from x in db.sysTree
                          where x.type == clstype && x.isdel == false && x.des.IndexOf(value) > 0
                          orderby x.live descending, x.PNodeid descending
                          select x;
            }
            else
            {
                allNode = from x in db.sysTree
                          where x.type == clstype && x.isdel == false && x.des == value
                          orderby x.live descending, x.PNodeid descending
                          select x;
            }

        }

        if (allNode.Count() > 0)
        {
            allNode = allNode.Take(20);
            sb.Append("[");
            foreach (var x in allNode)
            {
                var tmp = GetPNode(x);
                if (tmp != string.Empty)
                {
                    sb.Append(tmp).Append(",");
                }
            }
            if (sb.Length > 0)
            {
                if (sb[sb.Length - 1] == ',')
                {
                    sb.Remove(sb.Length - 1, 1);
                }
            }
            sb.Append("]");
        }
        return sb.ToString();
    }
    private List<sysTree> st = new List<sysTree>();
    private IQueryable<sysTree> allNode;
    private List<int> addedid = new List<int>();
    public string getSubNode(sysTree x)
    {


        StringBuilder sb = new StringBuilder();
        //添加这个节点父节点的所有子节点
        List<sysTree> sbnodes = (from node in allNode
                                 where node.PNodeid == x.NodeId
                                 orderby node.NodeId ascending
                                 select node).ToList<sysTree>();
        if (sbnodes.Count() <= 0)
        {
            sbnodes = (from node in st
                       where node.PNodeid == x.NodeId
                       orderby node.NodeId ascending
                       select node).ToList<sysTree>();
        }

        foreach (var sbn in sbnodes)
        {
            string tmpsb = "";
            string nodetxt = "";
            if (addedid.Contains(sbn.NodeId)) continue;
            nodetxt = string.Format("{0}\"id\":\"{1}\",\"text\":\"{2}\",\"attributes\":{0}\"live\":\"{4}\",\"des\":\"{5}\"{6}", "{", sbn.NodeId, sbn.NodeName.Trim(), "closed", sbn.live, sbn.des, "}");
            tmpsb = getSubNode(sbn);
            if (!string.IsNullOrEmpty(tmpsb))
            {
                nodetxt += ",\"state\":\"closed\",\"children\": [" + tmpsb + "]";
                addedid.Add(sbn.NodeId);
            }
            sb.Append(nodetxt).Append("},");
            st.Remove(sbn);
        }
        if (sb.Length > 0)
        {
            if (sb[sb.Length - 1] == ',')
            {
                sb.Remove(sb.Length - 1, 1);
            }
        }
        return sb.ToString();
    }
    //向上查找父节点的时候用堆栈，当找到根节点的时候 把堆栈里的节点都加载上去的同事加载检索结果中的节点与堆栈节点同一个父节节点的节点。
    public string GetPNode(sysTree x)
    {
        StringBuilder sb = new StringBuilder();
        //如果到根节点
        if (x.live == 0)
        {
            if (addedid.Contains(x.NodeId)) return "";
            sb.Append(string.Format("{0}\"id\":\"{1}\",\"text\":\"{2}\",\"attributes\":{0}\"live\":\"{4}\",\"des\":\"{5}\"{6}", "{", x.NodeId, x.NodeName.Trim(), "closed", x.live, x.des, "}"));
            addedid.Add(x.NodeId);
            if (st.Count > 0)
            {

                string tmpsb = getSubNode(x);
                if (!string.IsNullOrEmpty(tmpsb))
                {
                    sb.Append(",\"state\":\"closed\",\"children\": [");
                    sb.Append(tmpsb.Trim(','));
                    sb.Append("]");
                }
            }
            sb.Append("}");
        }
        else //寻找父节点
        {
            var parentNode = from pnode in db.sysTree
                             where pnode.NodeId == x.PNodeid
                             select pnode;
            st.Add(parentNode.First());
            if (parentNode.Count() > 0)
            {
                //如果有父节点 添加父节点                
                sb.Append(GetPNode(parentNode.First()));
            }
        }
        if (sb.Length > 0)
        {
            if (sb[sb.Length - 1] == ',')
            {
                sb.Remove(sb.Length - 1, 1);
            }
        }
        return sb.ToString();

    }




    static void Main(string[] args)
    {
        ImportAREAX(0);
        ImportADM(0);
        ImportIPC(0);
    }


    static void ImportAREAX(int live)
    {
        XDocument tree = XDocument.Load(@"F:\xy\源代码\Patentquery\App_Data\AREAX.xml");
        foreach (var x in tree.Root.Elements())
        {
            ImportAREAXXElement(x, 0, live);
        }
    }
    static void ImportAREAXXElement(XElement node, int ParentID, int live)
    {
        Console.WriteLine(node.Attribute("name").Value + " " + node.Attribute("des").Value + "\t" + node.Attribute("IPCX").Value);
        bool isParent = false;

        if (node.Nodes().Count() > 0)
        {
            isParent = true;
        }

        string name = node.Attribute("name").Value.Trim() + " " + node.Attribute("des").Value.Trim();
        string des = node.Attribute("IPCX").Value.Trim();
        int nodeId = SysTreeHelper.addNode(ParentID, name, "ARE", des, isParent, live);
        foreach (var x in node.Elements())
        {
            ImportAREAXXElement(x, nodeId, live + 1);
        }
    }


    static void ImportADM(int live)
    {
        XDocument tree = XDocument.Load(@"F:\xy\源代码\Patentquery\App_Data\ADMTree.xml");
        foreach (var x in tree.Root.Elements())
        {
            ImportADMXElement(x, 0, live);
        }
    }
    static void ImportADMXElement(XElement node, int ParentID, int live)
    {
        Console.WriteLine(node.Attribute("name").Value + "\t" + node.Attribute("IPC").Value);
        bool isParent = false;

        if (node.Nodes().Count() > 0)
        {
            isParent = true;
        }

        string name = node.Attribute("name").Value.Trim();
        string des = node.Attribute("IPC").Value.Trim();
        int nodeId = SysTreeHelper.addNode(ParentID, name, "ADM", des, isParent, live);
        foreach (var x in node.Elements())
        {
            ImportADMXElement(x, nodeId, live + 1);
        }
    }

    static void ImportIPC(int live)
    {
        XDocument tree = XDocument.Load(@"F:\xy\源代码\Patentquery\App_Data\IPCTree.xml");
        foreach (var x in tree.Root.Elements())
        {
            ImportIPCXElement(x, 0, live);
        }
    }
    static void ImportIPCXElement(XElement node, int ParentID, int live)
    {
        Console.WriteLine(node.Attribute("name").Value + "\t" + node.Attribute("IPC").Value);
        bool isParent = false;

        if (node.Nodes().Count() > 0)
        {
            isParent = true;
        }

        string name = node.Attribute("name").Value.Trim();
        string des = node.Attribute("IPC").Value.Trim();
        int nodeId = SysTreeHelper.addNode(ParentID, name, "IPC", des, isParent, live);
        foreach (var x in node.Elements())
        {
            ImportIPCXElement(x, nodeId, live + 1);
        }
    }
    public static DataTable GetIPCListByNodeName(string type, string value)
    {
        string sql;
        string pNodename = string.Empty;
        DataTable dt;
        List<DataTable> lsttb = new List<DataTable>();

        sql = "select *,0 as showtype from ipcTreeTable where type='" + type + "' and NodeName='" + value + "'";
        dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql);

        int live = Convert.ToInt32(dt.Rows[0]["live"].ToString());
        string NodeId = dt.Rows[0]["NodeId"].ToString();
        string PNodeId = dt.Rows[0]["PNodeId"].ToString();

        switch (live)
        {
            case 0:
                break;
            case 1:
                pNodename = dt.Rows[0]["live1"].ToString();
                break;
            case 2:
                pNodename = dt.Rows[0]["live1"].ToString() + "," + dt.Rows[0]["live2"].ToString();
                break;
            case 3:
                pNodename = dt.Rows[0]["live1"].ToString() + "," + dt.Rows[0]["live2"].ToString() + "," + dt.Rows[0]["live3"].ToString();
                break;
            default:
                pNodename = dt.Rows[0]["live1"].ToString() + "," + dt.Rows[0]["live2"].ToString() + "," + dt.Rows[0]["live3"].ToString();
                break;
        }
        pNodename = pNodename.Replace(",", "','");
        lsttb.Add(GetPNodeByPNodeName(type, pNodename));
        lsttb.Add(dt);
        if (live > 3)
        {
            lsttb.Add(GetPNodeByPid(type, PNodeId));
        }
        lsttb.Add(GetSubNodeByPid(type, NodeId));
        return FormatTable(MargeTable(lsttb));
    }
   
    public static DataTable GetIPCListByIPC(string type, string stype, string value)
    {
        List<DataTable> lsttb = new List<DataTable>();
        if (type == "ARE")
        {
            if (value.Length > 4) value = value.Substring(0, 4);
        }

        string pNodename = string.Empty;
        DataTable dt;

        string sql;
        if (stype.ToUpper() == "IPC")
        {
            if (type != "ARE")
            {
                if (value.Trim('/').Length == 7)
                {
                    sql = "select *,0 as showtype from ipcTreeTable where type='" + type + "' and NodeName like '" + value + "%'";

                }
                else
                {
                    sql = "select *,0 as showtype from ipcTreeTable where type='" + type + "' and NodeName='" + value + "'";
                }
            }
            else
            {
                sql = "select TOP 20 *,0 as showtype from ipcTreeTable where type='" + type + "' and ipcs like '%" + value + "%'";
            }
        }
        else
        {
            sql = "select TOP 20 *,0 as showtype from ipcTreeTable where type='" + type + "' and des like '%" + value + "%'";
        }
        dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql);
        lsttb.Add(dt);
        foreach (DataRow Row in dt.Rows)
        {
            int live = Convert.ToInt32(Row["live"].ToString());
            string NodeId = Row["NodeId"].ToString();
            string PNodeId = Row["PNodeId"].ToString();
            switch (live)
            {
                case 0:
                    break;
                case 1:
                    pNodename = dt.Rows[0]["live1"].ToString();
                    break;
                case 2:
                    pNodename = dt.Rows[0]["live1"].ToString() + "," + dt.Rows[0]["live2"].ToString();
                    break;
                case 3:
                    pNodename = dt.Rows[0]["live1"].ToString() + "," + dt.Rows[0]["live2"].ToString() + "," + dt.Rows[0]["live3"].ToString();
                    break;
                default:
                    pNodename = dt.Rows[0]["live1"].ToString() + "," + dt.Rows[0]["live2"].ToString() + "," + dt.Rows[0]["live3"].ToString();
                    break;
            }
            pNodename = pNodename.Replace(",", "','");
            lsttb.Add(GetPNodeByPNodeName(type, pNodename));
            if (live > 3)
            {
                lsttb.Add(GetPNodeByPid(type, PNodeId));
            }
            if (stype.ToUpper() == "IPC")
            {
                lsttb.Add(GetSubNodeByPid(type, NodeId));
            }
        }

        return FormatTable(MargeTable(lsttb));

    }


    /// <summary>
    /// 多个DataTable 合成一个 DataTable
    /// </summary>
    /// <param name="dts"></param>
    /// <returns></returns>
    public static DataTable MargeTable(List<DataTable> dts)
    {
        DataTable dt = new DataTable();
        DataColumn col1 = new DataColumn("NodeId", typeof(int));
        DataColumn col2 = new DataColumn("NodeName");
        DataColumn col3 = new DataColumn("des");
        DataColumn col4 = new DataColumn("ipcs");
        DataColumn col5 = new DataColumn("live");
        DataColumn col6 = new DataColumn("showtype");
        DataColumn col7 = new DataColumn("IsParent");

        dt.Columns.Add(col1); dt.Columns.Add(col2); dt.Columns.Add(col3); dt.Columns.Add(col4); dt.Columns.Add(col5); dt.Columns.Add(col6); dt.Columns.Add(col7);
        foreach (var dt1 in dts)
        {
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string nodeid = dt1.Rows[i]["NodeId"].ToString();
                int x = (from y in dt.AsEnumerable()
                         where y["NodeId"].ToString() == nodeid
                         select y).Count();
                if (x > 0) continue;
                DataRow row = dt.NewRow();
                row["NodeId"] = dt1.Rows[i]["NodeId"];
                row["NodeName"] = dt1.Rows[i]["NodeName"];
                row["des"] = dt1.Rows[i]["des"];
                row["ipcs"] = dt1.Rows[i]["ipcs"];
                row["live"] = dt1.Rows[i]["live"];
                row["IsParent"] = dt1.Rows[i]["IsParent"];
                row["showtype"] = dt1.Rows[i]["showtype"];
                dt.Rows.Add(row);
            }
        }

        return dt;
    }
    /// <summary>
    /// 安节点排序，并格式化点组的 IPC 如1点组，2点组
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static DataTable FormatTable(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            var x = from y in dt.AsEnumerable()
                    orderby y["NodeId"] ascending
                    select y;

            DataTable dtreuslt = x.CopyToDataTable();
            foreach (DataRow row in dtreuslt.Rows)
            {
                int tmplive = Convert.ToInt32(row["live"]);
                if (tmplive > 3)
                {
                    row["des"] = "<b>" + "".PadLeft(tmplive - 3, '.') + "</b>" + row["des"].ToString();
                }
            }
            return dtreuslt;
        }
        else
        {
            return dt;
        }
    }
    public static DataTable GetIPCListByKey(string value)
    {
        string sql = "select top 50 * from ipcTreeTable where type='IPC' and des like '%" + value + "%' order by live live1 live2 live3";
        return DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql);
    }
    public static DataTable GetADMListByIPC(string value)
    {
        string sql = "select * from ipcTreeTable where type='ADM' and ipcs='" + value + "'";
        return DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql);
    }
    public static DataTable GetADMListByKey(string value)
    {
        string sql = "select top 50 * from ipcTreeTable where type='ADM' and des like '%" + value + "%'";
        return DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql);
    }
    public static DataTable GetAREListByIPC(string value)
    {
        if (value.Length > 4) value = value.Substring(0, 4);
        string sql = "select * from ipcTreeTable where type='ARE' and ipcs='" + value + "'";
        return DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql);
    }
    public static DataTable GetAREListByKey(string value)
    {
        string sql = "select top 50 * * from ipcTreeTable where type='ARE' and des like '%" + value + "%'";
        return DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql);
    }
    public static DataTable GetPNodeByPNodeName(string type, string NodeNames)
    {
        string sql = "select *, 0 as showtype from ipcTreeTable where type='" + type + "' and NodeName in('" + NodeNames + "') order by nodeid";
        return DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql);
    }
    public static DataTable GetSubNodeByPid(string type, string pid)
    {
        string sql = "select *,1 as showtype from ipcTreeTable where type='" + type + "' and PNodeid=" + pid + " order by nodeid";
        return DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql);
    }
    public static DataTable GetPNodeByPid(string type, string pid)
    {
        string sql = "select *,0 as showtype from ipcTreeTable where type='" + type + "' and NodeId='" + pid + "'";
        DataTable dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql);
        if (dt.Rows[0]["live"].ToString().Trim() == "2")
        {
            return dt;
        }
        else
        {
            List<DataTable> dts = new List<DataTable>();
            dts.Add(dt);
            dts.Add(GetPNodeByPid(type, dt.Rows[0]["PNodeId"].ToString().Trim()));
            return MargeTable(dts);
        }
    }
}
