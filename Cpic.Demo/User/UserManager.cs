using DBA;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Cpic.Cprs2010.Search;

namespace Cpic.Cprs2010.User
{
    /// <summary>
    /// 
    /// </summary>
    public static class UserManager
    {

        #region 注册登录相关


        /// <summary>
        /// add by lidonglei(20100419)获得所有用户
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllUsers()
        {
            DataTable dt;
            string sql = "select distinct '' as 序号,a.ID ID , a.User_logname 帐号,a.Use_Name 姓名,a.User_Tel 电话,C.RoleName 角色列表,b.RoleCode 角色代码 "
                       + "from TbUserInfo as a left join TbUserRole as b on a.User_logname = b.User_logname  "
                       + "left join TbRole as c on b.RoleCode = c.RoleCode   Order By a.Use_Name";
            dt = SqlDbAccess.GetDataTable(CommandType.Text, sql);

            for (int i = dt.Rows.Count - 1; i >= 1; i--)
            {
                if (dt.Rows.Count > 1)
                {

                    if (dt.Rows[i]["帐号"].ToString() == dt.Rows[i - 1]["帐号"].ToString())
                    {

                        dt.Rows[i - 1]["角色列表"] = dt.Rows[i - 1]["角色列表"].ToString() + ";" + dt.Rows[i]["角色列表"].ToString();
                        dt.Rows[i - 1]["角色代码"] = dt.Rows[i - 1]["角色代码"].ToString() + ";" + dt.Rows[i]["角色代码"].ToString();
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
        /// 根据用户名获取用户信息
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static DataTable GetUsersByName(string Name)
        {
            DataTable dt;
            string sql = "select distinct '' as 序号,a.ID ID , a.User_logname 帐号,a.Use_Name 姓名,a.User_Tel 电话,C.RoleName 角色列表,b.RoleCode 角色代码 "
                       + "from TbUserInfo as a left join TbUserRole as b on a.User_logname = b.User_logname  "
                       + "left join TbRole as c on b.RoleCode = c.RoleCode where a.Use_Name='" + Name + "'  Order By a.Use_Name ";
            dt = SqlDbAccess.GetDataTable(CommandType.Text, sql);

            for (int i = dt.Rows.Count - 1; i >= 1; i--)
            {
                if (dt.Rows.Count > 1)
                {

                    if (dt.Rows[i]["帐号"].ToString() == dt.Rows[i - 1]["帐号"].ToString())
                    {

                        dt.Rows[i - 1]["角色列表"] = dt.Rows[i - 1]["角色列表"].ToString() + ";" + dt.Rows[i]["角色列表"].ToString();
                        dt.Rows[i - 1]["角色代码"] = dt.Rows[i - 1]["角色代码"].ToString() + ";" + dt.Rows[i]["角色代码"].ToString();
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
        /// 根据帐号获取用户信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static DataTable GetUsersByAccount(string account)
        {
            DataTable dt;
            string sql = "select distinct '' as 序号,a.ID ID , a.User_logname 帐号,a.Use_Name 姓名,a.User_Tel 电话,C.RoleName 角色列表,b.RoleCode 角色代码 "
                       + "from TbUserInfo as a left join TbUserRole as b on a.User_logname = b.User_logname  "
                       + "left join TbRole as c on b.RoleCode = c.RoleCode where a.User_logname='" + account + "'  Order By a.Use_Name ";
            dt = SqlDbAccess.GetDataTable(CommandType.Text, sql);

            for (int i = dt.Rows.Count - 1; i >= 1; i--)
            {
                if (dt.Rows.Count > 1)
                {

                    if (dt.Rows[i]["帐号"].ToString() == dt.Rows[i - 1]["帐号"].ToString())
                    {

                        dt.Rows[i - 1]["角色列表"] = dt.Rows[i - 1]["角色列表"].ToString() + ";" + dt.Rows[i]["角色列表"].ToString();
                        dt.Rows[i - 1]["角色代码"] = dt.Rows[i - 1]["角色代码"].ToString() + ";" + dt.Rows[i]["角色代码"].ToString();
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
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public static TbUserInfoInfo GetUserInfoByID(string strID)
        {
            strID = HttpContext.Current.Server.HtmlEncode(strID);
            //拼sql语句
            string strSql = "select * from TbUserInfo where ID = @ID";
            //设置参数
            SqlParameter parms = new SqlParameter("@ID", strID);
            //执行并获得数据表
            DataTable dt = SqlDbAccess.GetDataTable(CommandType.Text, strSql, parms);
            //把数据填充到实体中
            TbUserInfoInfo userInfo = new TbUserInfoInfo();
            if (dt.Rows != null && dt.Rows.Count > 0)
            {
                int nID;
                bool bRes = int.TryParse(dt.Rows[0]["ID"].ToString(), out nID);
                if (bRes)
                {
                    userInfo.ID = nID;
                    userInfo.User_logname = dt.Rows[0]["User_logname"].ToString();
                    userInfo.PSD = dt.Rows[0]["PSD"].ToString();
                    userInfo.Use_Name = dt.Rows[0]["Use_Name"].ToString();
                    userInfo.User_Number = dt.Rows[0]["User_Number"].ToString();
                    userInfo.User_Email = dt.Rows[0]["User_Email"].ToString();
                    userInfo.User_Tel = dt.Rows[0]["User_Tel"].ToString();
                }
            }
            return userInfo;
        }

        /// <summary>
        /// 根据rolecode获得所有用户信息
        /// </summary>
        /// <param name="strRoleCode"></param>
        /// <returns></returns>
        public static DataTable GetAllUserByRoleCode(string strRoleCode)
        {
            DataTable dt;
            string sql = "select distinct '' as 序号,a.ID ID,a.User_logname 账号,a.Use_Name 姓名 from TbUserInfo a,TbUserRole b,TbRole c "
                        + "where a.User_logname = b.User_logname and b.RoleCode = c.RoleCode and c.RoleCode = '" + strRoleCode + "'";
            dt = SqlDbAccess.GetDataTable(CommandType.Text, sql);


            //初始序号
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                dt.Rows[i]["序号"] = i + 1;
            }
            return dt;
        }

        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public static bool ChcekUserName(string UserName)
        {
            UserName = HttpContext.Current.Server.HtmlEncode(UserName);
            string sql = "SELECT COUNT(*) FROM TbUserInfo WHERE User_logname=@UserName";
            SqlParameter parms = new SqlParameter("@UserName", SqlDbType.NVarChar, 20);
            parms.Value = UserName;
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
        /// 添加用户
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="ShowName"></param>
        /// <param name="Psd"></param>
        /// <param name="Tel"></param>
        /// <param name="RoleList"></param>
        /// <returns></returns>
        public static bool AddUser(string UserName, string ShowName, string Psd, string Tel, string RoleList, int ChuShiDaiMa)
        {
            UserName = HttpContext.Current.Server.HtmlEncode(UserName);
            ShowName = HttpContext.Current.Server.HtmlEncode(ShowName);
            Psd = HttpContext.Current.Server.HtmlEncode(Psd);
            Tel = HttpContext.Current.Server.HtmlEncode(Tel);

            SqlParameter[] parms;
            //string addManager = "Insert Into TbUserInfo(User_logname,Psd,Use_Name,User_Tel,ChuShiDaiMa) VALUES (@UserName,@Psd,@ShowName,@Tel,@ChuShiDaiMa)";
            string addManager = "Insert Into TbUserInfo(User_logname,Psd,Use_Name,User_Tel) VALUES (@UserName,@Psd,@ShowName,@Tel)";
            string[] roles = RoleList.Split(";".ToCharArray()[0]);
            string sqlAddRole;
            using (SqlConnection conn = SqlDbAccess.GetSqlConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {

                        foreach (string r in roles)
                        {
                            sqlAddRole = "Insert Into TbUserRole values(@UserName,@RoleCode)";
                            parms = new SqlParameter[2]{
                                                    new System.Data.SqlClient.SqlParameter("@UserName", SqlDbType.NVarChar, 10),
                                                    new System.Data.SqlClient.SqlParameter("@RoleCode", SqlDbType.NVarChar, 4)};
                            parms[0].Value = UserName;
                            parms[1].Value = r.Trim();
                            SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sqlAddRole, parms);
                        }

                        parms = new SqlParameter[4]{ 
                                new SqlParameter("@UserName",UserName),
                                new SqlParameter("@Psd",Psd),
                                new SqlParameter("@ShowName",ShowName),
                                new SqlParameter("@Tel",Tel)
                                //new SqlParameter("@ChuShiDaiMa",ChuShiDaiMa)   
                              };
                        SqlDbAccess.ExecNoQuery(trans, CommandType.Text, addManager, parms);
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
        /// 注册用户方法（add by lidonglei/2010/04/15）
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="strRoleCodes">角色名称</param>
        /// <returns></returns>
        public static bool RegisterUser(TbUserInfoInfo userInfo, string strRoleCodes)
        {
            string strLogName = HttpContext.Current.Server.HtmlEncode(userInfo.User_logname);
            string strPwd = HttpContext.Current.Server.HtmlEncode(userInfo.PSD);
            string strName = HttpContext.Current.Server.HtmlEncode(userInfo.Use_Name);
            string strEmail = HttpContext.Current.Server.HtmlEncode(userInfo.User_Email);
            string strNumber = HttpContext.Current.Server.HtmlEncode(userInfo.User_Number);
            string strTel = HttpContext.Current.Server.HtmlEncode(userInfo.User_Tel);

            SqlParameter[] parms;

            string[] roles = strRoleCodes.Split(";".ToCharArray()[0]);

            using (SqlConnection conn = SqlDbAccess.GetSqlConnection())
            {
                conn.Open();//打开连接
                //使用事务保证一致性
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        ////根据角色名称搜索RoleCode
                        //string sqlSelectRoleCode = "select RoleCode from TbRole where RoleName = @RoleName";
                        //parms = new SqlParameter[1]{
                        //                        new SqlParameter("@RoleName",strRoleName)};
                        //string strRoleCode = SqlDbAccess.ExecuteScalar(CommandType.Text, sqlSelectRoleCode, parms).ToString();                       

                        //添加用户角色关系                        
                        string sqlAddRole = "Insert Into TbUserRole values(@User_logname,@RoleCode)";
                        foreach (string r in roles)
                        {
                            parms = new SqlParameter[2]{
                                                    new System.Data.SqlClient.SqlParameter("@User_logname", SqlDbType.NVarChar, 10),
                                                    new System.Data.SqlClient.SqlParameter("@RoleCode", SqlDbType.NVarChar, 4)};
                            parms[0].Value = strLogName;
                            parms[1].Value = r.Trim();
                            SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sqlAddRole, parms);
                        }

                        //添加用户
                        //添加用户sql语句
                        string addManager = "Insert Into TbUserInfo(User_logname,PSD,Use_Name,User_Number,User_Email,User_Tel) "
                                          + "VALUES (@User_logname,@PSD,@Use_Name,@User_Number,@User_Email,@User_Tel)";
                        parms = new SqlParameter[6]{ 
                                new SqlParameter("@User_logname",strLogName),
                                new SqlParameter("@PSD",strPwd),
                                new SqlParameter("@Use_Name",strName),
                                new SqlParameter("@User_Number",strNumber),
                                new SqlParameter("@User_Email",strEmail),
                                new SqlParameter("@User_Tel",strTel)
                              };
                        SqlDbAccess.ExecNoQuery(trans, CommandType.Text, addManager, parms);
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
        /// 根据RoleName获得RoleCode
        /// </summary>
        /// <param name="strRoleName"></param>
        /// <returns></returns>
        public static string GetRoleCodeByRoleName(string strRoleName)
        {
            string strRoleCode;
            strRoleName = HttpContext.Current.Server.HtmlEncode(strRoleName);

            SqlParameter[] paras;
            using (SqlConnection conn = SqlDbAccess.GetSqlConnection())
            {
                conn.Open();//打开数据库联接
                try
                {
                    //从数据库中获得RoleCode
                    string sqlSelectRoleCode = "select RoleCode from TbRole where RoleName = @RoleName";
                    paras = new SqlParameter[1]{
                                                new SqlParameter("@RoleName",strRoleName)};
                    strRoleCode = SqlDbAccess.ExecuteScalar(CommandType.Text, sqlSelectRoleCode, paras).ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception("获得RoleCode失败", ex);
                }
                finally
                {
                    //关闭连接
                    conn.Close();
                }
            }
            return strRoleCode;
        }

        /// <summary>
        /// 根据RoleCode获得RoleName
        /// </summary>
        /// <param name="strRoleCode"></param>
        /// <returns></returns>
        public static string GetRoleNameByRoleCode(string strRoleCode)
        {
            string strRoleName;
            strRoleCode = HttpContext.Current.Server.HtmlEncode(strRoleCode);

            SqlParameter[] paras;
            using (SqlConnection conn = SqlDbAccess.GetSqlConnection())
            {
                conn.Open();//打开数据库联接
                try
                {
                    //从数据库中获得RoleCode
                    string sqlSelectRoleName = "select RoleName from TbRole where RoleCode = @RoleCode";
                    paras = new SqlParameter[1]{
                                                new SqlParameter("@RoleCode",strRoleCode)};
                    strRoleName = SqlDbAccess.ExecuteScalar(CommandType.Text, sqlSelectRoleName, paras).ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception("获得RoleName失败", ex);
                }
                finally
                {
                    //关闭连接
                    conn.Close();
                }
            }
            return strRoleName;
        }

        /// <summary>
        /// 根据登录名获得用户信息
        /// </summary>
        /// <param name="strLogName"></param>
        /// <returns></returns>
        public static TbUserInfoInfo GetUserInfoByLogName(string strLogName)
        {
            strLogName = HttpContext.Current.Server.HtmlEncode(strLogName);
            //拼sql语句
            string strSql = "select * from TbUserInfo where User_logname = @UserName";
            //设置参数
            SqlParameter parms = new SqlParameter("@UserName", strLogName);
            //执行并获得数据表
            DataTable dt = SqlDbAccess.GetDataTable(CommandType.Text, strSql, parms);
            //把数据填充到实体中
            TbUserInfoInfo userInfo = new TbUserInfoInfo();
            if (dt.Rows != null && dt.Rows.Count > 0)
            {
                int nID;
                bool bRes = int.TryParse(dt.Rows[0]["ID"].ToString(), out nID);
                if (bRes)
                {
                    userInfo.ID = nID;
                    userInfo.User_logname = dt.Rows[0]["User_logname"].ToString();
                    userInfo.PSD = dt.Rows[0]["PSD"].ToString();
                    userInfo.Use_Name = dt.Rows[0]["Use_Name"].ToString();
                    userInfo.User_Number = dt.Rows[0]["User_Number"].ToString();
                    userInfo.User_Email = dt.Rows[0]["User_Email"].ToString();
                    userInfo.User_Tel = dt.Rows[0]["User_Tel"].ToString();
                }
            }
            return userInfo;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static bool UpdateUserInfo(TbUserInfoInfo userInfo)
        {
            string strLogName = HttpContext.Current.Server.HtmlEncode(userInfo.User_logname);
            string strPwd = HttpContext.Current.Server.HtmlEncode(userInfo.PSD);
            string strName = HttpContext.Current.Server.HtmlEncode(userInfo.Use_Name);
            string strEmail = HttpContext.Current.Server.HtmlEncode(userInfo.User_Email);
            string strNumber = HttpContext.Current.Server.HtmlEncode(userInfo.User_Number);
            string strTel = HttpContext.Current.Server.HtmlEncode(userInfo.User_Tel);
            string strPro = HttpContext.Current.Server.HtmlEncode(userInfo.Province);
            string strCity = HttpContext.Current.Server.HtmlEncode(userInfo.City);
            string strCounty = HttpContext.Current.Server.HtmlEncode(userInfo.County);
            string strQQ = HttpContext.Current.Server.HtmlEncode(userInfo.QQ);
            string strCompany = HttpContext.Current.Server.HtmlEncode(userInfo.Company);
            string strSex = HttpContext.Current.Server.HtmlEncode(userInfo.Sex);
            string strPost = HttpContext.Current.Server.HtmlEncode(userInfo.Post);
            string strBussiness = HttpContext.Current.Server.HtmlEncode(userInfo.Bussiness);
            string strMoney = HttpContext.Current.Server.HtmlEncode(userInfo.Money.ToString());

            using (SqlConnection conn = SqlDbAccess.GetSqlConnection())
            {
                conn.Open();//建立数据库联接并打开
                using (SqlTransaction trans = conn.BeginTransaction())//使用事务
                {
                    try
                    {
                        SqlParameter[] parms;
                        string updateManager;
                        if (string.IsNullOrEmpty(strPwd))//若没写密码，则不修改密码
                        {
                            updateManager = " update TbUserInfo SET User_Email=@User_Email,Use_Name=@Use_Name,User_Number=@User_Number,User_Tel=@User_Tel, "
                                          + " Province=@Province,City=@User_City,County=@User_County,User_QQ=@User_QQ,User_Post=@User_Post,"
                                          + " Sex=@User_Sex,Company=@User_Company,Bussiness=@User_Bussiness,Money=@Money "
                                          + " where User_logname=@User_logname ";
                            parms = new SqlParameter[] {
                                new SqlParameter("@User_logname",strLogName),
                                new SqlParameter("@User_Tel",strTel),
                                new SqlParameter("@User_Email",strEmail),
                                new SqlParameter("@Use_Name",strName),
                                new SqlParameter("@User_Number",strNumber),
                                new SqlParameter("@Province",strPro),
                                new SqlParameter("@User_City",strCity),
                                new SqlParameter("@User_County",strCounty),
                                new SqlParameter("@User_QQ",strQQ),
                                new SqlParameter("@User_Post",strPost),
                                new SqlParameter("@User_Sex",strSex),
                                new SqlParameter("@User_Company",strCompany),
                                new SqlParameter("@User_Bussiness",strBussiness),
                                new SqlParameter("@Money",strMoney)
                              };
                        }
                        else//若修改密码，则更新
                        {
                            updateManager = "update TbUserInfo SET PSD=@PSD , User_Email=@User_Email,Use_Name=@Use_Name,User_Number=@User_Number,User_Tel=@User_Tel, "
                                          + " Province=@Province,City=@User_City,County=@User_County,User_QQ=@User_QQ,User_Post=@User_Post,"
                                          + " Sex=@User_Sex,Company=@User_Company,Bussiness=@User_Bussiness,Money=@Money "
                                          + " where User_logname=@User_logname ";
                            parms = new SqlParameter[] {
                                new SqlParameter("@User_logname",strLogName),
                                new SqlParameter("@User_Tel",strTel),
                                new SqlParameter("@User_Email",strEmail),
                                new SqlParameter("@Use_Name",strName),
                                new SqlParameter("@User_Number",strNumber),
                                new SqlParameter("@PSD",strPwd),
                                new SqlParameter("@Province",strPro),
                                new SqlParameter("@User_City",strCity),
                                new SqlParameter("@User_County",strCounty),
                                new SqlParameter("@User_QQ",strQQ),
                                new SqlParameter("@User_Post",strPost),
                                new SqlParameter("@User_Sex",strSex),
                                new SqlParameter("@User_Company",strCompany),
                                new SqlParameter("@User_Bussiness",strBussiness),
                                new SqlParameter("@Money",strMoney)
                              };
                        }
                        //更新用户信息
                        SqlDbAccess.ExecNoQuery(trans, CommandType.Text, updateManager, parms);
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
        /// 更新用户信息及用户的权限信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="_strRoleCodes"></param>
        /// <param name="_isRoleUpdate">True更新；false不更新权限</param>
        /// <returns></returns>
        public static bool UpdateUserInfo(TbUserInfoInfo userInfo, string _strRoleCodes, bool _isRoleUpdate)
        {
            string strLogName = HttpContext.Current.Server.HtmlEncode(userInfo.User_logname);
            string strPwd = HttpContext.Current.Server.HtmlEncode(userInfo.PSD);
            string strName = HttpContext.Current.Server.HtmlEncode(userInfo.Use_Name);
            string strEmail = HttpContext.Current.Server.HtmlEncode(userInfo.User_Email);
            string strNumber = HttpContext.Current.Server.HtmlEncode(userInfo.User_Number);
            string strTel = HttpContext.Current.Server.HtmlEncode(userInfo.User_Tel);

            SqlParameter[] parms;
            using (SqlConnection conn = SqlDbAccess.GetSqlConnection())
            {
                conn.Open();//建立数据库联接并打开
                using (SqlTransaction trans = conn.BeginTransaction())//使用事务
                {
                    try
                    {
                        //判断是否要进行权限更新
                        if (true == _isRoleUpdate)
                        {
                            //2.删除以前所有的权限配置
                            string sqlDelRoleRight = "Delete TbUserRole Where User_logname=@UserName";
                            parms = new SqlParameter[1]{
                            new System.Data.SqlClient.SqlParameter("@UserName", SqlDbType.NVarChar, 20)};
                            parms[0].Value = strLogName;
                            SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sqlDelRoleRight, parms);


                            //3.添加现在最新的权限配置 添加用户角色关系                        
                            string sqlAddRole = "Insert Into TbUserRole values(@User_logname,@RoleCode)";
                            string[] roles = _strRoleCodes.Split(";".ToCharArray()[0]);
                            foreach (string r in roles)
                            {
                                parms = new SqlParameter[2]{
                                                    new System.Data.SqlClient.SqlParameter("@User_logname", SqlDbType.NVarChar, 10),
                                                    new System.Data.SqlClient.SqlParameter("@RoleCode", SqlDbType.NVarChar, 4)};
                                parms[0].Value = strLogName;
                                parms[1].Value = r.Trim();
                                SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sqlAddRole, parms);
                            }
                        }


                        string updateManager;
                        if (string.IsNullOrEmpty(strPwd))//若没写密码，则不修改密码
                        {
                            updateManager = "update TbUserInfo SET User_Email=@User_Email,Use_Name=@Use_Name,User_Number=@User_Number,User_Tel=@User_Tel where User_logname=@User_logname ";
                            parms = new SqlParameter[5]{
                                new SqlParameter("@User_logname",strLogName),
                                new SqlParameter("@User_Tel",strTel),
                                new SqlParameter("@User_Email",strEmail),
                                new SqlParameter("@Use_Name",strName),
                                new SqlParameter("@User_Number",strNumber)
                              };
                        }
                        else//若修改密码，则更新
                        {
                            updateManager = "update TbUserInfo SET PSD=@PSD , User_Email=@User_Email,Use_Name=@Use_Name,User_Number=@User_Number,User_Tel=@User_Tel where User_logname=@User_logname ";
                            parms = new SqlParameter[6]{
                                new SqlParameter("@User_logname",strLogName),
                                new SqlParameter("@User_Tel",strTel),
                                new SqlParameter("@User_Email",strEmail),
                                new SqlParameter("@Use_Name",strName),
                                new SqlParameter("@User_Number",strNumber),
                                new SqlParameter("@PSD",strPwd)
                              };
                        }
                        //更新用户信息
                        SqlDbAccess.ExecNoQuery(trans, CommandType.Text, updateManager, parms);
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
        /// 更新用户信息 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="ShowName"></param>
        /// <param name="Psd"></param>
        /// <param name="Tel"></param>
        /// <param name="RoleList"></param>
        /// <returns></returns>
        public static bool UpdateUser(string UserName, string ShowName, string Psd, string Tel, string RoleList, int ChuShiDaiMa)
        {
            UserName = HttpContext.Current.Server.HtmlEncode(UserName);
            ShowName = HttpContext.Current.Server.HtmlEncode(ShowName);
            Psd = HttpContext.Current.Server.HtmlEncode(Psd);
            Tel = HttpContext.Current.Server.HtmlEncode(Tel);

            SqlParameter[] parms;
            using (SqlConnection conn = SqlDbAccess.GetSqlConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //2.删除以前所有的权限配置
                        string sqlDelRoleRight = "Delete TbUserRole Where User_logname=@UserName";
                        parms = new SqlParameter[1]{
                            new System.Data.SqlClient.SqlParameter("@UserName", SqlDbType.NVarChar, 20)};
                        parms[0].Value = UserName;
                        SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sqlDelRoleRight, parms);


                        //3.添加现在最新的权限配置
                        string[] right = RoleList.Split(";".ToCharArray()[0]);
                        foreach (string r in right)
                        {
                            string sql = "Insert Into TbUserRole values(@UserName,@RoleCode)";
                            parms = new SqlParameter[2]{
                                                    new System.Data.SqlClient.SqlParameter("@UserName", SqlDbType.NVarChar, 20),
                                                    new System.Data.SqlClient.SqlParameter("@RoleCode", SqlDbType.NVarChar, 4)};
                            parms[0].Value = UserName;
                            parms[1].Value = r.Trim();
                            SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sql, parms);
                        }
                        string updateManager;
                        if (string.IsNullOrEmpty(Psd))
                        {
                            updateManager = "update TbUserInfo SET Use_name=@ShowName,User_Tel=@Tel where User_logname=@UserName ";
                            parms = new SqlParameter[3]{
                                new SqlParameter("@ShowName",ShowName),
                                new SqlParameter("@Tel",Tel),                                
                                new SqlParameter("@UserName",UserName)
                              };
                        }
                        else
                        {
                            updateManager = "update TbUserInfo SET Psd=@Psd,Use_name=@ShowName,User_Tel=@Tel  where User_logname=@UserName ";
                            parms = new SqlParameter[4]{
                                new SqlParameter("@Psd",Psd),
                                new SqlParameter("@ShowName",ShowName),
                                new SqlParameter("@Tel",Tel),
                                //new SqlParameter("@ChuShiDaiMa",ChuShiDaiMa),
                                new SqlParameter("@UserName",UserName)
                              };
                        }
                        //4.跟新用户信息
                        SqlDbAccess.ExecNoQuery(trans, CommandType.Text, updateManager, parms);
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
        /// 删除用户
        /// </summary>
        public static bool DelUser(string UserName)
        {
            UserName = HttpContext.Current.Server.HtmlEncode(UserName);
            string sqlDelUser = "Delete TbUserInfo WHERE User_logname=@UserName";
            SqlParameter parms = new SqlParameter("@UserName", UserName);
            SqlParameter parms1 = new SqlParameter("@UserName", UserName);
            if (SqlDbAccess.ExecNoQuery(CommandType.Text, sqlDelUser, parms) > 0 && SqlDbAccess.ExecNoQuery(CommandType.Text, "Delete TbUserRole WHERE User_logname=@UserName", parms1) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据用户名以及也面名称判断用户是否有权限访问这个也面
        /// </summary>
        /// <param name="PageFileName">页面名称</param>
        /// <returns></returns>
        public static bool LogInCheck(string PageFileName)
        {
            string UserName;
            UserName = HttpContext.Current.Session["UserName"].ToString();
            string sqlCheck = "select count(*)  from TbUserInfo as a,TbUserRole as b, TbRoleRight as c ,TbRight as d "
                               + " where a.User_logname = b.User_logname and b.RoleCode =c.RoleCode and  c.RightCode = d.RightCode "
                               + " and a.User_logname =@username and upper(d.PageFileName)=upper(@PageFileName)";
            //"select count(*)  from TbUserInfo as a,tbuserrole as b, tbroleright as c ,tbright as d "
            //                + " where a.username = b.username and b.rolecode =c.rolecode and  c.rightcode = d.rightcode "
            //                + " and a.username =@username and upper(d.PageFileName)=upper(@PageFileName)";



            SqlParameter[] parms = new SqlParameter[2]{
                new SqlParameter("@username",UserName),
                new SqlParameter("@PageFileName",PageFileName)
            };
            if (Convert.ToInt32(SqlDbAccess.ExecuteScalar(CommandType.Text, sqlCheck, parms)) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        public static bool LogIn(string UserName, string PassWord)
        {
            UserName = HttpContext.Current.Server.HtmlEncode(UserName);
            PassWord = HttpContext.Current.Server.HtmlEncode(PassWord);
            DataTable dt;
            string sql = "SELECT * FROM TbUserInfo WHERE User_logname=@User_logname";
            SqlParameter parms = new SqlParameter("@User_logname", UserName);
            dt = SqlDbAccess.GetDataTable(CommandType.Text, sql, parms);
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                if (PassWord.ToUpper() == dt.Rows[0]["psd"].ToString().Trim().ToUpper())
                {
                    //把数据填充到实体中
                    User userInfo = new User();
                    userInfo.ID = (int)dt.Rows[0]["ID"];
                    userInfo.User_logname = dt.Rows[0]["User_logname"].ToString();
                    userInfo.PSD = dt.Rows[0]["PSD"].ToString();
                    userInfo.Use_Name = dt.Rows[0]["Use_Name"].ToString();
                    userInfo.User_Number = dt.Rows[0]["User_Number"].ToString();
                    userInfo.User_Email = dt.Rows[0]["User_Email"].ToString();
                    userInfo.User_Tel = dt.Rows[0]["User_Tel"].ToString();
                    //userInfo.PackageCode = dt.Rows[0]["PackageCode"].ToString();
                    //userInfo.PackageBDate = dt.Rows[0]["PackageBDate"].ToString();
                    //userInfo.PackageEDate = dt.Rows[0]["PackageEDate"].ToString();
                    HttpContext.Current.Session["UserInfo"] = userInfo;
                    HttpContext.Current.Session["UserName"] = userInfo.User_logname;


                    LoginManager.User_Login();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 检测密码是否正确
        /// </summary>
        /// <param name="psd">密码</param>
        /// <returns></returns>
        public static bool CheckPassWord(string psd)
        {
            string UserName;
            string password;
            UserName = HttpContext.Current.Session["UserName"].ToString();
            string sql = "SELECT psd FROM TBUserInfo WHERE User_logname=@UserName";
            SqlParameter parms = new SqlParameter("@UserName", UserName);
            password = SqlDbAccess.ExecuteScalar(CommandType.Text, sql, parms).ToString().Trim();

            if (password.ToUpper() == psd.ToUpper())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="NewPassWord">新的密码</param>
        /// <returns></returns>
        public static bool UpdatePassWord(string NewPassWord)
        {
            string UserName;
            UserName = HttpContext.Current.Session["UserName"].ToString();
            string sql = "update TBUserInfo set psd=@psd  WHERE User_logname=@UserName";
            SqlParameter[] parms = new SqlParameter[2]{
                new SqlParameter("@psd",NewPassWord),
                new SqlParameter("@UserName",UserName)};
            if (Convert.ToInt32(SqlDbAccess.ExecNoQuery(CommandType.Text, sql, parms)) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到所有的处室
        /// </summary>
        /// <returns></returns>
        public static DataTable getChuShiList()
        {
            string sql = "select id,MingCheng from ChuShiConfig";
            return SqlDbAccess.GetDataTable(CommandType.Text, sql);

        }

        #endregion

        #region 游客相关

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            FreeGuestId = new Queue<int>();
            UsedGuestId = new Dictionary<string, int>();
            for (int i = 1; i <= 999999; i++)
            {
                FreeGuestId.Enqueue(i);
            }
        }

        /// <summary>
        /// 清空游客用户查询结果
        /// </summary>
        public static void Dispose()
        {
            foreach (var gustuse in UsedGuestId)
            {
                freeUsedGuestId(gustuse.Key);
            }
        }

        /// <summary>
        /// 已经使用的临时用户ID 字典
        /// </summary>
        private static Dictionary<string, int> UsedGuestId;
        /// <summary>
        /// 未使用的临时用户ID 队列
        /// </summary>
        private static Queue<int> FreeGuestId;


        /// <summary>
        /// 得到一个游客用户
        /// </summary>
        public static User getGuestUser(string SesssionId)
        {
            if (UsedGuestId.ContainsKey(SesssionId))
            {
                int id = UsedGuestId[SesssionId];
                return new User() { ID = id, Use_Name = "游客", User_logname = "游客" };
            }
            lock (FreeGuestId)
            {
                int id = FreeGuestId.Dequeue();
                UsedGuestId.Add(SesssionId, id);
                return new User() { ID = id, Use_Name = "游客", User_logname = "游客" };
            }


        }



        /// <summary>
        /// 是否某一个游客使用的用户ID、以及目录
        /// </summary>
        /// <param name="SessionId"></param>
        /// <returns></returns>
        public static bool freeUsedGuestId(string SessionId)
        {
            lock (FreeGuestId)
            {
                //只有游客释放的时候
                if (UsedGuestId.ContainsKey(SessionId))
                {
                    int id = UsedGuestId[SessionId];
                    //清除游客用户目录下所有的文件（检索历史、检索结果文件）
                    foreach (string s in Enum.GetNames(typeof(Search.SearchDbType)))
                    {
                        clearSearchHis(id, (Search.SearchDbType)Enum.Parse(typeof(Search.SearchDbType), s));
                        clearSearchHisData(id, (Search.SearchDbType)Enum.Parse(typeof(Search.SearchDbType), s));
                    }
                    FreeGuestId.Enqueue(id);
                    UsedGuestId.Remove(SessionId);
                }
                return true;
            }

        }


        /// <summary>
        /// 清楚删除用户检索历史
        /// </summary>
        /// <returns></returns>
        public static bool clearSearchHis(int UserId, Search.SearchDbType DBType)
        {
            string UserPath = CprsConfig.GetUserPath(UserId, "");
            string hisFilePath = UserPath + "\\" + DBType;
            string hisFile = hisFilePath + "\\SearchHistory.lst";
            try
            {
                System.IO.File.Delete(hisFile);
            }
            catch (Exception ex)
            {
                //todo:nothing;
            }
            return true;
        }

        /// <summary>
        /// 删除所有检索结果文件
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="DBType"></param>
        /// <returns></returns>
        public static bool clearSearchHisData(int UserId, Search.SearchDbType DBType)
        {
            string UserPath = CprsConfig.GetUserPath(UserId, "");
            string hisFilePath = UserPath + "\\" + DBType;

            if (System.IO.Directory.Exists(hisFilePath))
            {
                foreach (string f in System.IO.Directory.GetFiles(hisFilePath))
                {
                    //不删.lst 文件
                    if (f.EndsWith(".lst"))
                    {
                        continue;
                    }
                    //删除文件
                    System.IO.File.Delete(f);
                }
            }
            return true;
        }
        #endregion
    }



}
