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
    /// UserPackage用户套餐相关
    /// </summary>
    public class UserPackage
    {
        /// <summary>
        /// 根据用户名得到套餐明细
        /// </summary>
        /// <param name="usercode"></param>
        /// <returns></returns>
        public static List<TbPackageDetailInfo> GetPackageDetailByUser(string usercode)
        {
            DataTable dt = new DataTable();
            List<TbPackageDetailInfo> lst = new List<TbPackageDetailInfo>();
            string sql = "select c.* from TbUserPackage a left join Tbpackage b "
                        + " on a.packagecode=b.packagecode "
                        + " left join tbpackagedetail c "
                        + "on b.packagecode=c.packagecode "
                        + " where getdate()<=EndDate and user_code='" + usercode + "'";

            dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql, null);
            for (int i = 0; i < dt.Rows.Count;i++ )
            {
                TbPackageDetailInfo package = new TbPackageDetailInfo();
                package.ID = Convert.ToInt32(dt.Rows[i]["id"].ToString());
                package.PackageCode = dt.Rows[i]["PackageCode"].ToString();
                package.EachLimit = Convert.ToInt32(dt.Rows[i]["EachLimit"].ToString());
                package.MonthLimit = Convert.ToInt32(dt.Rows[i]["MonthLimit"].ToString());
                lst.Add(package);
            }
            return lst;
        }

        /// <summary>
        /// 根据用户名得到套餐
        /// </summary>
        /// <param name="usercode"></param>
        /// <returns></returns>
        public static DataTable GetPackageByUser(string usercode)
        {
            DataTable dt = new DataTable();
            string sql = "select * from TbUserPackage where User_code='" + usercode + "'";
            dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql, null);
            return dt;

        }

        /// <summary>
        /// 设置用户的套餐
        /// </summary>
        /// <param name="usercode"></param>
        /// <param name="packagecode"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public bool SetUserPackage(string usercode,string packagecode,string enddate)
        {
            string sql = "select * from TbUserPackage where User_Code='"+usercode +"' ";
            DataTable dt = new DataTable();
            dt = SqlDbAccess.GetDataTable(CommandType.Text, sql, null);

            if (dt.Rows.Count > 0)
            {
                sql = "update TbUserPageage set PackageCode='" + packagecode + "',EndDate='" + enddate + "' where User_Code='" + usercode + "'";
                if (SqlDbAccess.ExecNoQuery(CommandType.Text, sql, null) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            sql="insert into TbUserPackage values (@UserCode,@PackageCode,getdate(),@EndDate)";
            SqlParameter[] param ={
                                     new SqlParameter("@UserCode",usercode),
                                     new SqlParameter("@PackageCode",packagecode),
                                     new SqlParameter("@EndDate",enddate)         
            };
            if (SqlDbAccess.ExecNoQuery(CommandType.Text, sql, param) > 0)
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
