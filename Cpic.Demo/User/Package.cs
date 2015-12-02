using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBA;
using System.Data.SqlClient;

namespace Cpic.Cprs2010.User
{
    /// <summary>
    /// Package套餐设置
    /// </summary>
    public class Package
    {
        /// <summary>
        /// 根据套餐代码得到套餐信息
        /// </summary>
        /// <param name="PackageCode"></param>
        /// <returns></returns>
        public static DataTable GetPackage(string PackageCode)
        {
            DataTable dt = new DataTable();
            string sql = "select * from TbPackage where PackageCode='" + PackageCode + "'";
            dt = SqlDbAccess.GetDataTable(CommandType.Text, sql, null);
            return dt;
        }
        /// <summary>
        /// 得到所有套餐信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPackage()
        {
            DataTable dt = new DataTable();
            string sql = "select * from TbPackage ";
            dt = SqlDbAccess.GetDataTable(CommandType.Text, sql, null);
            return dt;
        }

        /// <summary>
        /// 根据套餐代码得到套餐详细信息
        /// </summary>
        /// <param name="PackageCode"></param>
        /// <returns></returns>
        public static DataTable GetPackageDetail(string PackageCode)
        {

            DataTable dt = new DataTable();
            string sql = "select * from TbPackage a left join TbPackageDetail b "
                       + " on a.PackageCode=b.PackageCode where a.PackageCode='" + PackageCode + "'";

            dt = SqlDbAccess.GetDataTable(CommandType.Text, sql, null);
            return dt;
        }
        
        /// <summary>
        /// 设置套餐
        /// </summary>
        /// <param name="packagecode"></param>
        /// <param name="packagename"></param>
        /// <param name="yearmoney"></param>
        /// <param name="monthmoney"></param>
        /// <param name="packagedes"></param>
        /// <returns></returns>
        public bool SetPackage(SqlTransaction tran, string packagecode, string packagename, int yearmoney, int monthmoney, string packagedes)
        {
            string sql = "insert into TbPackage values (@PackageCode,@PackageName,@YearMoney,@MonthMoney,@PackageDes)";
            SqlParameter[] param ={
                                     new SqlParameter("@PackageCode",packagecode),
                                     new SqlParameter("@PackageName",packagename),
                                     new SqlParameter("@YearMoney",yearmoney),
                                     new SqlParameter("@MonthMoney",monthmoney),
                                     new SqlParameter("@PackageDes",packagedes)
                                 };
            if (SqlDbAccess.ExecNoQuery(tran,CommandType.Text, sql, param) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 设置套餐详细信息
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="PackageCode"></param>
        /// <param name="FunctionCode"></param>
        /// <param name="EachLimt"></param>
        /// <param name="MonthLimit"></param>
        /// <returns></returns>
        public bool SetPackageDetail(SqlTransaction tran, string PackageCode, string FunctionCode, int EachLimt, int MonthLimit)
        {
            string sql = "insert into TbPackageDetail values (@PackageCode,@FunctionCode,@EachLimit,@MonthLimit)";
            SqlParameter[] param ={
                               new SqlParameter("@PackageCode",PackageCode),
                               new SqlParameter("@FunctionCode",FunctionCode),
                               new SqlParameter("@EachLimit",EachLimt),
                               new SqlParameter("@MonthLimit",MonthLimit)
                           };
            if (SqlDbAccess.ExecNoQuery(tran, CommandType.Text, sql, param) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除套餐
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="PackageCode"></param>
        /// <returns></returns>
        public bool DelPackage(SqlTransaction tran, string PackageCode)
        {
            string sql = "delete from TbPackage where PackageCode='" + PackageCode + "'";
            if (SqlDbAccess.ExecNoQuery(tran, CommandType.Text, sql, null) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除套餐明细
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="PackageCode"></param>
        /// <returns></returns>
        public bool DelPackageDetail(SqlTransaction tran, string PackageCode)
        {
            string sql = "delete from TbPackageDetail where PackageCode='" + PackageCode + "'";
            if (SqlDbAccess.ExecNoQuery(tran, CommandType.Text, sql, null) > 0)
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
