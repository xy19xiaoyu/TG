using System;
using System.Collections.Generic;
using System.Text;
using DBA;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Cpic.Cprs2010.User
{
    public class Role
    {

        /// <summary>
        /// 得到所有角色的代码跟角色名称
        /// </summary>
        /// <returns></returns>
        public static DataTable GetRoles()
        {
            string sql = "SELECT RoleCode,RoleName FROM TbRole";
            return SqlDbAccess.GetDataTable( CommandType.Text, sql);
        }
        /// <summary>
        /// 根据子系统得到子系统角色列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetRoleList()
        {
            DataTable dt;
            string sql = "SELECT '' as 序号, a.RoleName 角色名称,c.RightName 详细权限,c.RightCode,a.RoleCode FROM TbRole as a,TbRoleRight as b,TbRight as c where  a.RoleCode = b.RoleCode and b.RightCode=c.RightCode Order By a.RoleName";
            sql = "SELECT '' as 序号, a.RoleName 角色名称,c.RightName 详细权限,c.RightCode,a.RoleCode FROM TbRole as a left join(select c1.RightName,b1.* from  TbRoleRight as b1,TbRight as c1 where  b1.RightCode=c1.RightCode) as c  on  a.RoleCode = c.RoleCode  Order By a.RoleName";
            dt = SqlDbAccess.GetDataTable(CommandType.Text, sql);

            for (int i = dt.Rows.Count - 1; i >= 1; i--)
            {
                if (dt.Rows.Count > 1)
                {

                    if (dt.Rows[i]["角色名称"].ToString() == dt.Rows[i - 1]["角色名称"].ToString())
                    {

                        dt.Rows[i - 1]["详细权限"] = dt.Rows[i - 1]["详细权限"].ToString() + ";" + dt.Rows[i]["详细权限"].ToString();
                        dt.Rows[i - 1]["RightCode"] = dt.Rows[i - 1]["RightCode"].ToString() + ";" + dt.Rows[i]["RightCode"].ToString();
                        dt.Rows.RemoveAt(i);
                    }

                }
            }

            //初始序号
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                dt.Rows[i]["序号"] = i + 1;
            }
            return dt;
        }
        /// <summary>
        /// 判读角色名字是否已经存在
        /// </summary>
        /// <returns></returns>
        public static bool CheckRoleExsit(string RoleName)
        {
            RoleName = HttpContext.Current.Server.HtmlEncode(RoleName);
          
            string sql = "SELECT COUNT(*) FROM TbRole WHERE RoleName=@RoleName";
            SqlParameter parms = new SqlParameter("@RoleName", SqlDbType.NVarChar, 50);
            parms.Value = RoleName;
            if (Convert.ToInt32(SqlDbAccess.ExecuteScalar(CommandType.Text, sql, parms)) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="RoleCode"></param>
        /// <param name="RoleName"></param>
        /// <param name="SubSysCode"></param>
        /// <param name="Rights"></param>
        /// <returns></returns>
        public static bool AddRole(string RoleName, string Rights)
        {
            RoleName = HttpContext.Current.Server.HtmlEncode(RoleName);
            int MaxId;
            object objMax;
            string sqlSelectRight = "select max(id) from TbRole";
            string sql;
            SqlParameter[] parms;
            objMax = SqlDbAccess.ExecuteScalar(CommandType.Text, sqlSelectRight);
            if (DBNull.Value == objMax)
            {
                MaxId = 0;
            }
            else
            {
                MaxId = Convert.ToInt32(objMax);
            }


            string RoleCode = (MaxId + 1).ToString();

            do
            {
                RoleCode = "0" + RoleCode;

            } while (RoleCode.Length < 4);


            string[] right = Rights.Split(";".ToCharArray()[0]);

            using (SqlConnection conn = SqlDbAccess.GetSqlConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {

                        foreach (string r in right)
                        {
                            sql = "Insert Into TbRoleRight values(@RoleCode,@RightCode)";
                            parms = new SqlParameter[2]{
                                                    new System.Data.SqlClient.SqlParameter("@RoleCode", SqlDbType.NVarChar, 16),
                                                    new System.Data.SqlClient.SqlParameter("@RightCode", SqlDbType.NVarChar, 50)};
                            parms[0].Value = RoleCode;
                            parms[1].Value = r.Trim();
                            SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sql, parms);
                        }

                        string strsql = "Insert INTO TbRole values (@RoleCode,@RoleName)";
                        parms = new SqlParameter[2]{
                                                    new System.Data.SqlClient.SqlParameter("@RoleCode", SqlDbType.NVarChar, 4),
                                                    new System.Data.SqlClient.SqlParameter("@RoleName", SqlDbType.NVarChar, 50)};

                        parms[0].Value = RoleCode;
                        parms[1].Value = RoleName;
                        SqlDbAccess.ExecNoQuery(trans, CommandType.Text, strsql, parms);
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception("插入数据库失败,事务已经回滚", ex);
                    }
                };
            };
            return true;

        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="RoleCode">角色</param>
        /// <returns></returns>
        public static bool DelRole(string RoleCode)
        {
            string sqlDelRole = "Delete TbRole Where RoleCode=@RoleCode";
            string sqlDelRightCode = "Delete TbRoleRight Where RoleCode=@RoleCode";
            SqlParameter parms;
            using (SqlConnection conn = SqlDbAccess.GetSqlConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        parms = new System.Data.SqlClient.SqlParameter("@RoleCode", SqlDbType.NVarChar, 4);
                        parms.Value = RoleCode;
                        SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sqlDelRole, parms);
                        parms = parms = new System.Data.SqlClient.SqlParameter("@RoleCode", SqlDbType.NVarChar, 4);
                        parms.Value = RoleCode;
                        SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sqlDelRightCode, parms);
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception("插入数据库失败,事务已经回滚", ex);

                    }
                };
            };
            return true;

        }
        /// <summary>
        /// 更新一个角色的权限、角色的名称
        /// </summary>
        /// <param name="RoleCode"></param>
        /// <param name="RightCodes"></param>
        /// <returns></returns>
        public static bool UpdateRole(string RoleCode, string RoleName, string RightCodes)
        {
            RoleName = HttpContext.Current.Server.HtmlEncode(RoleName);

            SqlParameter[] parms;
            string sql;
            using (SqlConnection conn = SqlDbAccess.GetSqlConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //1.如果修改了角色名称
                        string updateRole = "Update TbRole Set RoleName =@RoleName Where RoleCode=@RoleCode";
                        parms = new SqlParameter[2]{
                            new System.Data.SqlClient.SqlParameter("@RoleName", SqlDbType.NVarChar, 50),
                            new System.Data.SqlClient.SqlParameter("@RoleCode", SqlDbType.NVarChar, 4)};
                        parms[0].Value = RoleName;
                        parms[1].Value = RoleCode;
                        SqlDbAccess.ExecNoQuery(trans, CommandType.Text, updateRole, parms);

                        //2.删除以前所有的权限配置
                        string sqlDelRightCode = "Delete TbRoleRight Where RoleCode=@RoleCode";
                        parms = new SqlParameter[1]{
                        new System.Data.SqlClient.SqlParameter("@RoleCode", SqlDbType.NVarChar, 4)};
                        parms[0].Value = RoleCode;
                        SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sqlDelRightCode, parms);

                        //3.添加现在最新的权限配置
                        string[] right = RightCodes.Split(";".ToCharArray()[0]);
                        foreach (string r in right)
                        {
                            sql = "Insert Into TbRoleRight values(@RoleCode,@RightCode)";
                            parms = new SqlParameter[2]{
                                                    new System.Data.SqlClient.SqlParameter("@RoleCode", SqlDbType.NVarChar, 4),
                                                    new System.Data.SqlClient.SqlParameter("@RightCode", SqlDbType.NVarChar,50)};
                            parms[0].Value = RoleCode;
                            parms[1].Value = r;
                            SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sql, parms);
                        }

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception("插入数据库失败,事务已经回滚", ex);
                    }
                
                };
            };
            //4.这叫快刀斩乱麻 ：）
            return true;
        }
    }
}
