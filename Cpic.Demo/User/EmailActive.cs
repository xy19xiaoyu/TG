using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DBA;
using System.Data;

namespace Cpic.Cprs2010.User
{
    public class EmailActive
    {
        /// <summary>
        /// 邮箱验证
        /// </summary>
        /// <param name="userlogname"></param>
        /// <param name="lscode"></param>
        /// <returns></returns>
        public static bool CheckActive(string userlogname, string lscode)
        {
            string sql = "select * from tbemailactive where user_logname='" + userlogname + "' and lscode='" + lscode + "' and updatetime>='" + System.DateTime.Now + "'";
            DataTable dt = SqlDbAccess.GetDataTable(CommandType.Text, sql, null);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 激活邮箱
        /// </summary>
        /// <param name="userlogname"></param>
        /// <param name="lscode"></param>
        /// <returns></returns>
        public static bool ActiveEmail(string userlogname,string lscode)
        {
            using (SqlConnection conn = SqlDbAccess.GetSqlConnection())
            {
                conn.Open();//建立数据库联接并打开
                using (SqlTransaction trans = conn.BeginTransaction())//使用事务
                {
                    string sql = "delete tbemailactive where user_logname='" + userlogname + "' and lscode='" + lscode + "'";
                    if (SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sql, null) <= 0)
                    {
                        trans.Rollback();
                        return false;
                    }
                    sql = "update tbuserinfo set checkemail=1 where user_logname='" + userlogname + "'";
                    if (SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sql, null) <= 0)
                    {
                        trans.Rollback();
                        return false;
                    }
                    trans.Commit();
                }
               
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userlogname"></param>
        /// <param name="lscode"></param>
        /// <returns></returns>
        public static bool InsertActive(string userlogname, string lscode)
        {
            string sql = "insert tbEmailActive (user_logname,lscode) values('" + userlogname + "','" + lscode + "' )";
            if (SqlDbAccess.ExecNoQuery(CommandType.Text,sql,null)>0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 激活密码
        /// </summary>
        /// <param name="userlogname"></param>
        /// <param name="lscode"></param>
        /// <returns></returns>
        public static bool ActivePassWord(string userlogname, string lscode,string password)
        {
            using (SqlConnection conn = SqlDbAccess.GetSqlConnection())
            {
                conn.Open();//建立数据库联接并打开
                using (SqlTransaction trans = conn.BeginTransaction())//使用事务
                {
                    string sql = "delete tbemailactive where user_logname='" + userlogname + "' and lscode='" + lscode + "'";
                    if (SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sql, null) <= 0)
                    {
                        trans.Rollback();
                        return false;
                    }
                    sql = "update tbuserinfo set psd='"+password+"' where user_logname='" + userlogname + "'";
                    if (SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sql, null) <= 0)
                    {
                        trans.Rollback();
                        return false;
                    }
                    trans.Commit();
                }

            }
            return true;
        }
    }
}
