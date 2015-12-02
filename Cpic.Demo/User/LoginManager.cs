#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2011 @ CPIC Corp
*	CLR 版本: 2.0.50727.3615
*	文 件 名: LoginManager.cs
*	创 建 人: xiwenlei(xiwl) $ chenxiaoyu(xy)
*	创建日期: 2011-4-6 11:15:16
*	版    本: V1.0
*	备注描述: $Myparameter1$           
*
* 修改历史: 
*   ****NO_1:
*	修 改 人: 
*	修改日期: 
*	描    述: $Myparameter1$           
******************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using DBA;
using System.Data;

/// <summary>
/// 为CPRS2010组件，提供对检索、检索历史及用户的相关操作类
/// </summary>
namespace Cpic.Cprs2010.User
{
    /// <summary>
    ///LoginManager 的摘要说明
    /// </summary>
    public class LoginManager
    {
        public LoginManager()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }


        /// <summary>
        /// 验证登陆用户是否已在其它机器登陆
        /// </summary>
        public static void CheckUID_SessionID()
        {
            try
            {
                string strSelectSql = "SELECT SessionID from TbLoginSSID  where USER_ID={0}";

                User userInfo = (User)HttpContext.Current.Session["UserInfo"];

                string strUSID = SqlDbAccess.ExecuteScalar(CommandType.Text, string.Format(strSelectSql, userInfo.ID)).ToString();

                if (strUSID != HttpContext.Current.Session.SessionID)
                {
                    HttpContext.Current.Session.Clear();
                    HttpContext.Current.Response.Redirect("~/SessionTimeOut.aspx?reSLid=0426&RequestUrl=" + HttpContext.Current.Server.UrlEncode(HttpContext.Current.Request.Url.ToString()));
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 更新数据库中的登陆用户对应的sessionID
        /// </summary>
        public static void User_Login()
        {
            try
            {
                User userInfo = (User)HttpContext.Current.Session["UserInfo"];

                string strQuSql = "select * from TbLoginSSID where USER_ID={0}";
                DataTable dt = SqlDbAccess.GetDataTable(CommandType.Text, string.Format(strQuSql, userInfo.ID));
                if (dt != null && dt.Rows.Count > 0)
                {
                    string strUpSql = "update TbLoginSSID set SessionID='{2}',LoginTime='{0}',Xtdm=0  where USER_ID={1}";
                    SqlDbAccess.ExecNoQuery(CommandType.Text, string.Format(strUpSql, DateTime.Now.ToString(), userInfo.ID, HttpContext.Current.Session.SessionID));

                }
                else
                {
                    string strUpSql = "  insert into TbLoginSSID (USER_ID,SessionID,LoginTime,Xtdm) values({0},'{1}','{2}',0)";
                    SqlDbAccess.ExecNoQuery(CommandType.Text, string.Format(strUpSql, userInfo.ID, HttpContext.Current.Session.SessionID, DateTime.Now.ToString()));
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 注销登陆用户
        /// </summary>
        public static void User_LogOut()
        {
            try
            {
                string strUpSql = "update TbLoginSSID set SessionID=null,LoginTime=null,LougOutTime='{0}' where SessionID='{1}'";
                SqlDbAccess.ExecNoQuery(CommandType.Text, string.Format(strUpSql, DateTime.Now.ToString(), HttpContext.Current.Session.SessionID));

                //string strDelSql = "delete from TbLoginSSID where SessionID='{0}' ";
                //SqlDbAccess.ExecNoQuery(CommandType.Text, string.Format(strDelSql, HttpContext.Current.Session.SessionID));
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// 记录登录历史
        /// </summary>
        public static void User_logHistory()
        {
            try
            {
                string insertSql = "insert into dbo.TbLoginHistory (USER_ID,SessionID,LoginTime,Xtdm,LougOutTime) "
                                + "select USER_ID,SessionID,LoginTime,Xtdm,'{0}' from TbLoginSSID where SessionID='{1}'";
                SqlDbAccess.ExecNoQuery(CommandType.Text, string.Format(insertSql, DateTime.Now.ToString(), HttpContext.Current.Session.SessionID));
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// 更新在线状态
        /// </summary>
        public static void UpdateLoginStatus(string xtdm, string comid, string ssid)
        {
            try
            {
                //更新在线表
                string strUpSql = "update TbLoginSSID set Xtdm={0}  where USER_ID={1} and SessionID='{2}'";
                SqlDbAccess.ExecNoQuery(CommandType.Text, string.Format(strUpSql, xtdm, comid, ssid));
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// 登录校验
        /// </summary>
        public static bool ValidateLogin(string userid, string comid, string ssid, string xtdm)
        {
            try
            {
                //    string query = "select * from dbo.TbUserRelation u,dbo.TbLoginSSID l "
                //+ "where u.ComID = l.USER_ID and u.ComID={0} and u.UserID = '{1}' and u.Xtdm={2} and l.SessionID='{3}' and l.LougOutTime is null";

                string query = "select * from dbo.TbUserRelation u,dbo.TbLoginSSID l "
            + "where u.ComID = l.USER_ID and u.ComID={0} and u.UserID = '{1}' and u.Xtdm={2} and l.SessionID='{3}'";

                //SqlDbAccess.ExecNoQuery(CommandType.Text, string.Format(query, comid, userid,xtdm, ssid));
                DataTable dt = SqlDbAccess.GetDataTable(CommandType.Text, string.Format(query, comid, userid, xtdm, ssid));
                if (dt != null && dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
