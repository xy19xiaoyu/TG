using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Cpic.Cprs2010.User
{
    /// <summary>
    /// UserDownHis用户收费功能使用历史
    /// </summary>
    public class UserDownHis
    {
        /// <summary>
        /// 每月的历史数量统计
        /// </summary>
        /// <param name="UserCode"></param>
        /// <param name="FunctionCode"></param>
        /// <returns></returns>
        public static int GetMoonHisNum(string UserCode,string FunctionCode)
        {
            DataTable dt = new DataTable();
            string sql = "select sum(patentnum) from TbUesrDownHis where usercode='" + UserCode + "' and FunctionCode='" + FunctionCode + "' "
                       + " and datepart(yyyy,getdate())=datepart(yyyy,downtime) and datepart(mm,getdate())=datepart(mm,downtime)";

            dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql, null);
            return dt.Rows.Count;

        }
        /// <summary>
        /// 历史数量统计
        /// </summary>
        /// <param name="UserCode"></param>
        /// <param name="FunctionCode"></param>
        /// <returns></returns>
        public static int GetHisNum(string UserCode, string FunctionCode)
        {
            DataTable dt = new DataTable();
            string sql = "select sum(patentnum) from TbUesrDownHis where usercode='" + UserCode + "' and FunctionCode='" + FunctionCode + "' ";

            dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql, null);
            return dt.Rows.Count;

        }
        /// <summary>
        /// 设置用户历史
        /// </summary>
        /// <param name="UserCode"></param>
        /// <param name="FunctionCode"></param>
        /// <param name="PatentName"></param>
        /// <param name="PatentNum"></param>
        /// <returns></returns>
        public bool SetHis(string UserCode,string FunctionCode,string PatentName,int PatentNum)
        {
            string sql = "insert into TbUserDownHis values (@UserCode,@FunctionCode,@PatentName,@patentNum,getdate())";
            SqlParameter[] parms ={
                new SqlParameter("@UserCode",UserCode),
                new SqlParameter("@FunctionCode",FunctionCode),
                new SqlParameter("@PatentName",PatentName),
                new SqlParameter("@PatentNum",PatentNum)
            };
            if (DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, sql, parms) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
