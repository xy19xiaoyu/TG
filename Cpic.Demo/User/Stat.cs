using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using DBA;
[assembly: log4net.Config.DOMConfigurator(Watch = true)]

namespace Cpic.Cprs2010.User
{
    public class Stat
    {
        public static DataTable AllPagesConfig;
        public static Dictionary<string, string> _StatPageList = new Dictionary<string, string>();
        public static Dictionary<string, string> StatPageList
        {
            get
            {
                return _StatPageList;
            }
            set
            {
                _StatPageList = value;
            }
        }
        public static DataTable GetItems()
        {
            string sql = "select * from ddlitem order by storid";
            return SqlDbAccess.GetDataTable(CommandType.Text, sql);

        }
        public static void IniStatPage()
        {
            string sqlSelectRight = "SELECT ROW_NUMBER() OVER(ORDER BY id) as ��� ,PageFileName as ҳ��·��, ModuleName as ����ģ��,IsStata as �Ƿ�ͳ�� FROM TbStatConfig ";
            try
            {

                AllPagesConfig = SqlDbAccess.GetDataTable(CommandType.Text, sqlSelectRight);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Session.Add("ERROR_MESSAGE", "���ݿ��������!");
                HttpContext.Current.Session.Add("ERROR_Exception", ex);
                HttpContext.Current.Response.Redirect("~/frmErrInfor.aspx");
            }

            //ɾ�������ڵ�ҳ��
            for (int i = AllPagesConfig.Rows.Count - 1; i >= 0; i--)
            {
                string filePath;
                if (AllPagesConfig.Rows[i]["ҳ��·��"].ToString() == "����")
                {
                    continue;
                }
                if (AllPagesConfig.Rows[i]["ҳ��·��"].ToString().IndexOf("?") > 0)
                {
                    filePath = AllPagesConfig.Rows[i]["ҳ��·��"].ToString().Substring(0, AllPagesConfig.Rows[i]["ҳ��·��"].ToString().IndexOf("?"));
                }
                else
                {
                    filePath = AllPagesConfig.Rows[i]["ҳ��·��"].ToString();
                }
                try
                {
                    if (!File.Exists(HttpContext.Current.Server.MapPath(filePath)))
                    {
                        DeleteStat(AllPagesConfig.Rows[i]["ҳ��·��"].ToString());
                        AllPagesConfig.Rows.RemoveAt(i);

                    }
                }
                catch (Exception ex)
                {
                    DeleteStat(AllPagesConfig.Rows[i]["ҳ��·��"].ToString());
                    AllPagesConfig.Rows.RemoveAt(i);

                }
            }

            foreach (DataRow row in Right.AllPagesConfig.Rows)
            {
                bool exisit = false;
                foreach (DataRow row1 in AllPagesConfig.Rows)
                {
                    if (row["ҳ��·��"].ToString() == row1["ҳ��·��"].ToString())
                    {
                        exisit = true;
                        break;
                    }
                }
                if (exisit == false)
                {
                    DataRow newrow = AllPagesConfig.NewRow();
                    newrow["���"] = row["���"].ToString();
                    newrow["ҳ��·��"] = row["ҳ��·��"].ToString();
                    newrow["����ģ��"] = string.Empty;
                    newrow["�Ƿ�ͳ��"] = false;
                    AllPagesConfig.Rows.Add(newrow);
                }
            }


        }

        public static void IniStatPageList()
        {
            foreach (DataRow row in AllPagesConfig.Rows)
            {
                string filePath;
                if (Convert.ToBoolean(row["�Ƿ�ͳ��"].ToString()) == true)
                {
                    if (row["ҳ��·��"].ToString().IndexOf("?") > 0)
                    {
                        filePath = row["ҳ��·��"].ToString().Substring(0, row["ҳ��·��"].ToString().ToUpper().IndexOf("?"));
                    }
                    else
                    {
                        filePath = row["ҳ��·��"].ToString().ToUpper();
                    }
                    if (_StatPageList.ContainsKey(filePath))
                    {
                        _StatPageList[filePath] = row["����ģ��"].ToString().Trim();
                    }
                    else
                    {
                        _StatPageList.Add(filePath, row["����ģ��"].ToString().Trim());
                    }
                }
            }
        }
        /// <summary>
        /// ����Ȩ������
        /// </summary>
        /// <returns></returns>
        public static bool SaveConfig()
        {
            using (SqlConnection conn = SqlDbAccess.GetSqlConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //1.ɾ�����е�����
                    string sql = "delete from TbStatConfig";
                    //string sql = "truncate table TbStatConfig";
                    SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sql);
                    foreach (DataRow row in AllPagesConfig.Rows)
                    {
                        sql = "Insert Into TbStatConfig(PageFileName,ModuleName,IsStata) values (@PageFileName,@ModuleName,@IsStata)";
                        SqlParameter[] parms = {
                            new SqlParameter("@PageFileName",row["ҳ��·��"].ToString()),
                            new SqlParameter("@ModuleName",row["����ģ��"].ToString()),
                            new SqlParameter("@IsStata",row["�Ƿ�ͳ��"].ToString())};
                        DBA.SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sql, parms);
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    HttpContext.Current.Session.Add("ERROR_MESSAGE", "���ݿ��������!");
                    HttpContext.Current.Session.Add("ERROR_Exception", ex);
                    HttpContext.Current.Response.Redirect("~/frmErrInfor.aspx");
                    return false;
                }
                trans.Commit();
            }
            return true;
        }

        /// <summary>
        /// ����һ�������ʷ����
        /// </summary>
        public static void AddRequest(string FileName)
        {
            try
            {
                string sql = "INSERT TbLog (IP,URL,LanMu,ShengFen)  values (@IP,@URL,@LanMu,@ShengFen)";
                string strUrl = HttpContext.Current.Request.Url.ToString();
                strUrl = strUrl.Length > 280 ? strUrl.Remove(280) : strUrl;
                SqlParameter[] parms = {
                            new SqlParameter("@IP",HttpContext.Current.Request.UserHostAddress.ToString()),
                            new SqlParameter("@URL",strUrl),
                            new SqlParameter("@LanMu",StatPageList[FileName]),
                            new SqlParameter("@ShengFen",getShenFen(HttpContext.Current.Request.UserHostAddress.ToString()))};
                DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, sql, parms);
            }
            catch (Exception ex)
            {
            }
        }


        /// <summary>
        ///  ɾ��һ������
        /// </summary>
        public static void DeleteStat(string PageFileName)
        {
            string sql = "DELETE TbStatConfig WHERE PageFileName=@PageFileName ";
            SqlParameter[] parms = {
                            new SqlParameter("@PageFileName",PageFileName)};
            DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, sql, parms);
        }
        /// <summary>
        /// �õ�IP����ʡ��
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string getShenFen(string ip)
        {
            string sql = "select top 1 shengfen from iptable where StartIPNum<=@ipnum and EndIPNum >= @ipnum order by id desc";
            SqlParameter[] parms = new SqlParameter[1] { new SqlParameter("@ipnum", IP2IPNum(ip)) };
            return DBA.SqlDbAccess.ExecuteScalar(CommandType.Text, sql, parms).ToString();
        }
        /// <summary>
        /// IP��ַ2IP��ַ����
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        public static double IP2IPNum(string IP)
        {
            return Convert.ToDouble(IP.Split(".".ToCharArray()[0])[0]) * 255d * 255d * 255d + Convert.ToDouble(IP.Split(".".ToCharArray()[0])[1]) * 255d * 255d + Convert.ToDouble(IP.Split(".".ToCharArray()[0])[2]) * 255d + Convert.ToDouble(IP.Split(".".ToCharArray()[0])[3]);
        }

        /// <summary>
        /// �õ�IP ���ڵ�
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        public static string GetLocal(string IP)
        {
            string sql = "select top 1 Country+ ' ' +Local as Local from iptable where StartIPNum<=@ipnum and EndIPNum >= @ipnum order by id desc";
            SqlParameter[] parms = new SqlParameter[1] { new SqlParameter("@ipnum", IP2IPNum(IP)) };
            return DBA.SqlDbAccess.ExecuteScalar(CommandType.Text, sql, parms).ToString();
        }
        /// <summary>


        /// <summary>
        /// ������Ŀ��ʱ��� ����������Сʱͳ��
        /// </summary>
        /// <param name="LanMu">��Ŀ</param>
        /// <param name="StartDate">��ʼʱ��</param>
        /// <param name="EndDate">����ʱ��</param>
        /// <param name="StatType">ͳ������</param>
        /// <returns></returns>
        public static DataTable getStat(string LanMu, DateTime StartDate, DateTime EndDate, string StatType)
        {
            string sql = string.Empty;
            switch (StatType)
            {
                case "Сʱ":
                    sql = @"select convert(varchar(10),riqi,120)+ ' ' +Convert(varchar(2),xiaoshi) + '��' as ʱ��,sum(fangwenliang)  as ������ from logstat where lanmu =@LanMu and  riqi between @StartDate and @EndDate group by riqi,xiaoshi order by riqi,xiaoshi";
                    break;
                case "��":
                    sql = @"select convert(varchar(10),riqi,120) as ʱ��,sum(fangwenliang) as ������ from logstat where lanmu=@lanmu and  riqi between  '" + StartDate.ToString("yyyy-MM-dd") + "' and '" + EndDate.ToString("yyyy-MM-dd") + "' group by riqi";
                    break;
                case "��":
                    sql = @"select'��' +convert(nvarchar(2),datepart(week,riqi))+'��' as ʱ��,sum(fangwenliang)  as ������ from logstat where lanmu =@LanMu and  riqi between @StartDate and @EndDate group by datepart(week,riqi)";
                    break;
                case "��":
                    sql = @"select convert(varchar(4),year(riqi))+'��' + convert(varchar(2),Month(riqi)) +'��' as ʱ��,sum(fangwenliang) as ������ from logstat where riqi between @StartDate and @EndDate group by convert(varchar(4),year(riqi))+'��' + convert(varchar(2),Month(riqi)) +'��'";
                    break;
                case "��":
                    sql = @"select convert(varchar(4),year(riqi))+'��' as ʱ��,sum(fangwenliang) as ������ from logstat where lanmu=@lanmu and  riqi between @StartDate and @EndDate group by year(riqi)";
                    break;
            }
            SqlParameter[] parms = new SqlParameter[3] { 
                                new SqlParameter("@LanMU",SqlDbType.VarChar,20),
                                new SqlParameter("@StartDate",SqlDbType.DateTime),
                                new SqlParameter("@EndDate",SqlDbType.DateTime)};
            parms[0].Value = LanMu;
            parms[1].Value = StartDate;
            parms[2].Value = EndDate;
            return DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql, parms);
        }


        /// <summary>
        /// ����ʱ��� ������ͳ��
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static DataTable StatGroupByDiQu(DateTime StartDate, DateTime EndDate)
        {
            string sql = @"select diqu as ����,sum(fangwenliang) as ������ from logstat where riqi between @StartDate and @EndDate group by diqu";
            SqlParameter[] parms = new SqlParameter[2] {                                
                                new SqlParameter("@StartDate",StartDate),
                                new SqlParameter("@EndDate",EndDate)};
            return DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql, parms);
        }


    }
}

