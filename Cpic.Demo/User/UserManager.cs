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

        #region ע���¼���


        /// <summary>
        /// add by lidonglei(20100419)��������û�
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllUsers()
        {
            DataTable dt;
            string sql = "select distinct '' as ���,a.ID ID , a.User_logname �ʺ�,a.Use_Name ����,a.User_Tel �绰,C.RoleName ��ɫ�б�,b.RoleCode ��ɫ���� "
                       + "from TbUserInfo as a left join TbUserRole as b on a.User_logname = b.User_logname  "
                       + "left join TbRole as c on b.RoleCode = c.RoleCode   Order By a.Use_Name";
            dt = SqlDbAccess.GetDataTable(CommandType.Text, sql);

            for (int i = dt.Rows.Count - 1; i >= 1; i--)
            {
                if (dt.Rows.Count > 1)
                {

                    if (dt.Rows[i]["�ʺ�"].ToString() == dt.Rows[i - 1]["�ʺ�"].ToString())
                    {

                        dt.Rows[i - 1]["��ɫ�б�"] = dt.Rows[i - 1]["��ɫ�б�"].ToString() + ";" + dt.Rows[i]["��ɫ�б�"].ToString();
                        dt.Rows[i - 1]["��ɫ����"] = dt.Rows[i - 1]["��ɫ����"].ToString() + ";" + dt.Rows[i]["��ɫ����"].ToString();
                        dt.Rows.RemoveAt(i);
                    }

                }
            }

            //��ʼ���
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                dt.Rows[i]["���"] = i + 1;
            }
            return dt;
        }
        /// <summary>
        /// �����û�����ȡ�û���Ϣ
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static DataTable GetUsersByName(string Name)
        {
            DataTable dt;
            string sql = "select distinct '' as ���,a.ID ID , a.User_logname �ʺ�,a.Use_Name ����,a.User_Tel �绰,C.RoleName ��ɫ�б�,b.RoleCode ��ɫ���� "
                       + "from TbUserInfo as a left join TbUserRole as b on a.User_logname = b.User_logname  "
                       + "left join TbRole as c on b.RoleCode = c.RoleCode where a.Use_Name='" + Name + "'  Order By a.Use_Name ";
            dt = SqlDbAccess.GetDataTable(CommandType.Text, sql);

            for (int i = dt.Rows.Count - 1; i >= 1; i--)
            {
                if (dt.Rows.Count > 1)
                {

                    if (dt.Rows[i]["�ʺ�"].ToString() == dt.Rows[i - 1]["�ʺ�"].ToString())
                    {

                        dt.Rows[i - 1]["��ɫ�б�"] = dt.Rows[i - 1]["��ɫ�б�"].ToString() + ";" + dt.Rows[i]["��ɫ�б�"].ToString();
                        dt.Rows[i - 1]["��ɫ����"] = dt.Rows[i - 1]["��ɫ����"].ToString() + ";" + dt.Rows[i]["��ɫ����"].ToString();
                        dt.Rows.RemoveAt(i);
                    }

                }
            }

            //��ʼ���
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                dt.Rows[i]["���"] = i + 1;
            }
            return dt;
        }
        /// <summary>
        /// �����ʺŻ�ȡ�û���Ϣ
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static DataTable GetUsersByAccount(string account)
        {
            DataTable dt;
            string sql = "select distinct '' as ���,a.ID ID , a.User_logname �ʺ�,a.Use_Name ����,a.User_Tel �绰,C.RoleName ��ɫ�б�,b.RoleCode ��ɫ���� "
                       + "from TbUserInfo as a left join TbUserRole as b on a.User_logname = b.User_logname  "
                       + "left join TbRole as c on b.RoleCode = c.RoleCode where a.User_logname='" + account + "'  Order By a.Use_Name ";
            dt = SqlDbAccess.GetDataTable(CommandType.Text, sql);

            for (int i = dt.Rows.Count - 1; i >= 1; i--)
            {
                if (dt.Rows.Count > 1)
                {

                    if (dt.Rows[i]["�ʺ�"].ToString() == dt.Rows[i - 1]["�ʺ�"].ToString())
                    {

                        dt.Rows[i - 1]["��ɫ�б�"] = dt.Rows[i - 1]["��ɫ�б�"].ToString() + ";" + dt.Rows[i]["��ɫ�б�"].ToString();
                        dt.Rows[i - 1]["��ɫ����"] = dt.Rows[i - 1]["��ɫ����"].ToString() + ";" + dt.Rows[i]["��ɫ����"].ToString();
                        dt.Rows.RemoveAt(i);
                    }

                }
            }

            //��ʼ���
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                dt.Rows[i]["���"] = i + 1;
            }
            return dt;
        }
        /// <summary>
        /// �����û�ID��ȡ�û���Ϣ
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public static TbUserInfoInfo GetUserInfoByID(string strID)
        {
            strID = HttpContext.Current.Server.HtmlEncode(strID);
            //ƴsql���
            string strSql = "select * from TbUserInfo where ID = @ID";
            //���ò���
            SqlParameter parms = new SqlParameter("@ID", strID);
            //ִ�в�������ݱ�
            DataTable dt = SqlDbAccess.GetDataTable(CommandType.Text, strSql, parms);
            //��������䵽ʵ����
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
        /// ����rolecode��������û���Ϣ
        /// </summary>
        /// <param name="strRoleCode"></param>
        /// <returns></returns>
        public static DataTable GetAllUserByRoleCode(string strRoleCode)
        {
            DataTable dt;
            string sql = "select distinct '' as ���,a.ID ID,a.User_logname �˺�,a.Use_Name ���� from TbUserInfo a,TbUserRole b,TbRole c "
                        + "where a.User_logname = b.User_logname and b.RoleCode = c.RoleCode and c.RoleCode = '" + strRoleCode + "'";
            dt = SqlDbAccess.GetDataTable(CommandType.Text, sql);


            //��ʼ���
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                dt.Rows[i]["���"] = i + 1;
            }
            return dt;
        }

        /// <summary>
        /// �ж��û����Ƿ����
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
        /// ����û�
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
                        throw new Exception("�������ݿ�ʧ��,�����Ѿ��ع�", ex);
                    }
                };
            };
            return true;

        }

        /// <summary>
        /// ע���û�������add by lidonglei/2010/04/15��
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="strRoleCodes">��ɫ����</param>
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
                conn.Open();//������
                //ʹ������֤һ����
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        ////���ݽ�ɫ��������RoleCode
                        //string sqlSelectRoleCode = "select RoleCode from TbRole where RoleName = @RoleName";
                        //parms = new SqlParameter[1]{
                        //                        new SqlParameter("@RoleName",strRoleName)};
                        //string strRoleCode = SqlDbAccess.ExecuteScalar(CommandType.Text, sqlSelectRoleCode, parms).ToString();                       

                        //����û���ɫ��ϵ                        
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

                        //����û�
                        //����û�sql���
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
                        throw new Exception("�������ݿ�ʧ��,�����Ѿ��ع�", ex);
                    }
                };
            };
            return true;
        }

        /// <summary>
        /// ����RoleName���RoleCode
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
                conn.Open();//�����ݿ�����
                try
                {
                    //�����ݿ��л��RoleCode
                    string sqlSelectRoleCode = "select RoleCode from TbRole where RoleName = @RoleName";
                    paras = new SqlParameter[1]{
                                                new SqlParameter("@RoleName",strRoleName)};
                    strRoleCode = SqlDbAccess.ExecuteScalar(CommandType.Text, sqlSelectRoleCode, paras).ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception("���RoleCodeʧ��", ex);
                }
                finally
                {
                    //�ر�����
                    conn.Close();
                }
            }
            return strRoleCode;
        }

        /// <summary>
        /// ����RoleCode���RoleName
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
                conn.Open();//�����ݿ�����
                try
                {
                    //�����ݿ��л��RoleCode
                    string sqlSelectRoleName = "select RoleName from TbRole where RoleCode = @RoleCode";
                    paras = new SqlParameter[1]{
                                                new SqlParameter("@RoleCode",strRoleCode)};
                    strRoleName = SqlDbAccess.ExecuteScalar(CommandType.Text, sqlSelectRoleName, paras).ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception("���RoleNameʧ��", ex);
                }
                finally
                {
                    //�ر�����
                    conn.Close();
                }
            }
            return strRoleName;
        }

        /// <summary>
        /// ���ݵ�¼������û���Ϣ
        /// </summary>
        /// <param name="strLogName"></param>
        /// <returns></returns>
        public static TbUserInfoInfo GetUserInfoByLogName(string strLogName)
        {
            strLogName = HttpContext.Current.Server.HtmlEncode(strLogName);
            //ƴsql���
            string strSql = "select * from TbUserInfo where User_logname = @UserName";
            //���ò���
            SqlParameter parms = new SqlParameter("@UserName", strLogName);
            //ִ�в�������ݱ�
            DataTable dt = SqlDbAccess.GetDataTable(CommandType.Text, strSql, parms);
            //��������䵽ʵ����
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
        /// �����û���Ϣ
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
                conn.Open();//�������ݿ����Ӳ���
                using (SqlTransaction trans = conn.BeginTransaction())//ʹ������
                {
                    try
                    {
                        SqlParameter[] parms;
                        string updateManager;
                        if (string.IsNullOrEmpty(strPwd))//��ûд���룬���޸�����
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
                        else//���޸����룬�����
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
                        //�����û���Ϣ
                        SqlDbAccess.ExecNoQuery(trans, CommandType.Text, updateManager, parms);
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception("�������ݿ�ʧ��,�����Ѿ��ع�", ex);
                    }
                };
            };
            return true;

        }

        /// <summary>
        /// �����û���Ϣ���û���Ȩ����Ϣ
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="_strRoleCodes"></param>
        /// <param name="_isRoleUpdate">True���£�false������Ȩ��</param>
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
                conn.Open();//�������ݿ����Ӳ���
                using (SqlTransaction trans = conn.BeginTransaction())//ʹ������
                {
                    try
                    {
                        //�ж��Ƿ�Ҫ����Ȩ�޸���
                        if (true == _isRoleUpdate)
                        {
                            //2.ɾ����ǰ���е�Ȩ������
                            string sqlDelRoleRight = "Delete TbUserRole Where User_logname=@UserName";
                            parms = new SqlParameter[1]{
                            new System.Data.SqlClient.SqlParameter("@UserName", SqlDbType.NVarChar, 20)};
                            parms[0].Value = strLogName;
                            SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sqlDelRoleRight, parms);


                            //3.����������µ�Ȩ������ ����û���ɫ��ϵ                        
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
                        if (string.IsNullOrEmpty(strPwd))//��ûд���룬���޸�����
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
                        else//���޸����룬�����
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
                        //�����û���Ϣ
                        SqlDbAccess.ExecNoQuery(trans, CommandType.Text, updateManager, parms);
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception("�������ݿ�ʧ��,�����Ѿ��ع�", ex);
                    }
                };
            };
            return true;

        }

        /// <summary>
        /// �����û���Ϣ 
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
                        //2.ɾ����ǰ���е�Ȩ������
                        string sqlDelRoleRight = "Delete TbUserRole Where User_logname=@UserName";
                        parms = new SqlParameter[1]{
                            new System.Data.SqlClient.SqlParameter("@UserName", SqlDbType.NVarChar, 20)};
                        parms[0].Value = UserName;
                        SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sqlDelRoleRight, parms);


                        //3.����������µ�Ȩ������
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
                        //4.�����û���Ϣ
                        SqlDbAccess.ExecNoQuery(trans, CommandType.Text, updateManager, parms);
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception("�������ݿ�ʧ��,�����Ѿ��ع�", ex);
                    }
                };
            };
            return true;


        }

        /// <summary>
        /// ɾ���û�
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
        /// �����û����Լ�Ҳ�������ж��û��Ƿ���Ȩ�޷������Ҳ��
        /// </summary>
        /// <param name="PageFileName">ҳ������</param>
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
        /// �û���½
        /// </summary>
        /// <param name="UserName">�û���</param>
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
                    //��������䵽ʵ����
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
        /// ��������Ƿ���ȷ
        /// </summary>
        /// <param name="psd">����</param>
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
        /// ��������
        /// </summary>
        /// <param name="NewPassWord">�µ�����</param>
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
        /// �õ����еĴ���
        /// </summary>
        /// <returns></returns>
        public static DataTable getChuShiList()
        {
            string sql = "select id,MingCheng from ChuShiConfig";
            return SqlDbAccess.GetDataTable(CommandType.Text, sql);

        }

        #endregion

        #region �ο����

        /// <summary>
        /// ��ʼ��
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
        /// ����ο��û���ѯ���
        /// </summary>
        public static void Dispose()
        {
            foreach (var gustuse in UsedGuestId)
            {
                freeUsedGuestId(gustuse.Key);
            }
        }

        /// <summary>
        /// �Ѿ�ʹ�õ���ʱ�û�ID �ֵ�
        /// </summary>
        private static Dictionary<string, int> UsedGuestId;
        /// <summary>
        /// δʹ�õ���ʱ�û�ID ����
        /// </summary>
        private static Queue<int> FreeGuestId;


        /// <summary>
        /// �õ�һ���ο��û�
        /// </summary>
        public static User getGuestUser(string SesssionId)
        {
            if (UsedGuestId.ContainsKey(SesssionId))
            {
                int id = UsedGuestId[SesssionId];
                return new User() { ID = id, Use_Name = "�ο�", User_logname = "�ο�" };
            }
            lock (FreeGuestId)
            {
                int id = FreeGuestId.Dequeue();
                UsedGuestId.Add(SesssionId, id);
                return new User() { ID = id, Use_Name = "�ο�", User_logname = "�ο�" };
            }


        }



        /// <summary>
        /// �Ƿ�ĳһ���ο�ʹ�õ��û�ID���Լ�Ŀ¼
        /// </summary>
        /// <param name="SessionId"></param>
        /// <returns></returns>
        public static bool freeUsedGuestId(string SessionId)
        {
            lock (FreeGuestId)
            {
                //ֻ���ο��ͷŵ�ʱ��
                if (UsedGuestId.ContainsKey(SessionId))
                {
                    int id = UsedGuestId[SessionId];
                    //����ο��û�Ŀ¼�����е��ļ���������ʷ����������ļ���
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
        /// ���ɾ���û�������ʷ
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
        /// ɾ�����м�������ļ�
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
                    //��ɾ.lst �ļ�
                    if (f.EndsWith(".lst"))
                    {
                        continue;
                    }
                    //ɾ���ļ�
                    System.IO.File.Delete(f);
                }
            }
            return true;
        }
        #endregion
    }



}
