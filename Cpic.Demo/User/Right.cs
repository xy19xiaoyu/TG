using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using DBA;
using System.Data.Sql;
namespace Cpic.Cprs2010.User
{
    /// <summary>
    /// Ȩ��
    /// </summary>
    public class Right
    {
        private static string WebSiteRootPaht = HttpContext.Current.Server.MapPath("~/");
        private static log4net.ILog log = log4net.LogManager.GetLogger("RequestFilter");
        private static DataTable _AllPagesConfig = new DataTable("PagesConfig");

        /// <summary>
        /// ����
        /// </summary>
        public static DataTable AllPagesConfig
        {
            get
            {
                if ((_AllPagesConfig == null) || _AllPagesConfig.Rows.Count <= 0)
                {
                    IniPageConfig();
                }
                return _AllPagesConfig;
            }
            set { _AllPagesConfig = value; }
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        public static void IniPageConfig()
        {
            //1.�����ݿ��ж�ȡ����
            GetWebSitePage();
        }

        /// <summary>
        /// ���һ��Ȩ��
        /// </summary>
        /// <param name="PageFileName"></param>
        /// <param name="RightName"></param>
        /// <param name="CheckLogIn"></param>
        /// <param name="CheckRight"></param>
        /// <returns></returns>
        public static bool AddRight(string PageFileName, string RightName, bool CheckLogIn, bool CheckRight)
        {
            int MaxId;
            object objMax;
            string sqlSelectRight = "select max(id) from TbRight";
            objMax = SqlDbAccess.ExecuteScalar(CommandType.Text, sqlSelectRight);
            if (DBNull.Value == objMax)
            {
                MaxId = 0;
            }
            else
            {
                MaxId = Convert.ToInt32(objMax);
            }


            string RightCode = (MaxId + 1).ToString();

            do
            {
                RightCode = "0" + RightCode;

            } while (RightCode.Length < 4);
            string sqlAddRight = "INSERT INTO TbRight (PageFileName,RightCode,RightName,CheckLogIn,CheckRight) values (@PageFileName,@RightCode,@RightName,@CheckLogIn,@CheckRight)";
            SqlParameter[] parms = {
                new SqlParameter("@PageFileName",PageFileName),
                new SqlParameter("@RightCode",RightCode),
                new SqlParameter("@RightName",RightName),
                new SqlParameter("@CheckLogIn",CheckLogIn),
                new SqlParameter("@CheckRight",CheckRight)};
            if (DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, sqlAddRight, parms) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ���һ��Ȩ��
        /// </summary>
        /// <param name="PageFileName"></param>
        /// <param name="RightName"></param>
        /// <param name="CheckLogIn"></param>
        /// <param name="CheckRight"></param>
        /// <returns></returns>
        public static bool AddRight(SqlTransaction trans, string PageFileName, string RightName, bool CheckLogIn, bool CheckRight)
        {
            int MaxId;
            object objMax;
            string sqlSelectRight = "select max(id) from TbRight";
            objMax = SqlDbAccess.ExecuteScalar(CommandType.Text, sqlSelectRight);
            if (DBNull.Value == objMax)
            {
                MaxId = 0;
            }
            else
            {
                MaxId = Convert.ToInt32(objMax);
            }


            string RightCode = (MaxId + 1).ToString();

            do
            {
                RightCode = "0" + RightCode;

            } while (RightCode.Length < 4);

            string sqlAddRight = "INSERT INTO TbRight (PageFileName,RightCode,RightName,CheckLogIn,CheckRight) values (@PageFileName,@RightCode,@RightName,@CheckLogIn,@CheckRight)";
            SqlParameter[] parms = {
                new SqlParameter("@PageFileName",PageFileName),
                new SqlParameter("@RightCode",RightCode),
                new SqlParameter("@RightName",RightName),
                new SqlParameter("@CheckLogIn",CheckLogIn),
                new SqlParameter("@CheckRight",CheckRight)};
            if (DBA.SqlDbAccess.ExecNoQuery(trans,CommandType.Text, sqlAddRight, parms) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ɾ��һ��Ȩ��
        /// </summary>
        /// <param name="RightCode"></param>
        /// <returns></returns>
        public static bool DeleteRight(string RightCode)
        {
            string sqlDelRight = "Delete TbRight WHERE RightCode =@RightCode";
            SqlParameter[] parms = {              
                new SqlParameter("@RightCode",RightCode)
            };
            if (DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, sqlDelRight, parms) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ����һ��Ȩ��
        /// </summary>
        /// <param name="PageFileName"></param>
        /// <param name="RightName"></param>
        /// <param name="CheckLogIn"></param>
        /// <param name="CheckRight"></param>
        /// <returns></returns>
        public static bool UpdateRight(string PageFileName, string RightName, string RightCode, bool CheckLogIn, bool CheckRight)
        {
            string sqlUpdateRight = "UPdate TbRight SET PageFileName=@PageFileName,RightName=@RightName,CheckLogIn=@CheckLogIn,CheckRight=@CheckRight  WHERE RightCode =@RightCode";
            SqlParameter[] parms = {
                new SqlParameter("@PageFileName",PageFileName),
                new SqlParameter("@RightCode",RightCode),
                new SqlParameter("@RightName",RightName),
                new SqlParameter("@CheckLogIn",CheckLogIn),
                new SqlParameter("@CheckRight",CheckRight)};
            try
            {

                if (DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, sqlUpdateRight, parms) > 0)
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
                HttpContext.Current.Session.Add("ERROR_MESSAGE", "���ݿ��������!");
                HttpContext.Current.Session.Add("ERROR_Exception", ex);
                HttpContext.Current.Response.Redirect("~/frmErrInfor.aspx");
                return false;
            }
        }

        /// <summary>
        /// ����һ��Ȩ��
        /// </summary>
        /// <param name="PageFileName"></param>
        /// <param name="RightName"></param>
        /// <param name="CheckLogIn"></param>
        /// <param name="CheckRight"></param>
        /// <returns></returns>
        public static bool UpdateRight(SqlTransaction trans, string PageFileName, string RightName, string RightCode, bool CheckLogIn, bool CheckRight)
        {
            string sqlUpdateRight = "UPdate TbRight SET PageFileName=@PageFileName,RightName=@RightName,CheckLogIn=@CheckLogIn,CheckRight=@CheckRight  WHERE RightCode =@RightCode";
            SqlParameter[] parms = {
                new SqlParameter("@PageFileName",PageFileName),
                new SqlParameter("@RightCode",RightCode),
                new SqlParameter("@RightName",RightName),
                new SqlParameter("@CheckLogIn",CheckLogIn),
                new SqlParameter("@CheckRight",CheckRight)};

            if (DBA.SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sqlUpdateRight, parms) > 0)
            {
                return true;
            }
            else
            {
                return false;
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
                    foreach (DataRow row in _AllPagesConfig.Rows)
                    {
                        //���û��Ȩ�޴��� ���ǻ�û���뵽���ݿ���
                        if (row.IsNull("RightCode"))
                        {
                            AddRight(row["ҳ��·��"].ToString(), row["ҳ�����"].ToString(), Convert.ToBoolean(row["�Ƿ���֤��½"].ToString()), Convert.ToBoolean(row["�Ƿ���֤Ȩ��"].ToString()));
                        }
                        else
                        {
                            UpdateRight(row["ҳ��·��"].ToString(), row["ҳ�����"].ToString(), row["RightCode"].ToString(), Convert.ToBoolean(row["�Ƿ���֤��½"].ToString()), Convert.ToBoolean(row["�Ƿ���֤Ȩ��"].ToString()));
                        }
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
        /// �õ���վ���е�ҳ��
        /// </summary>
        private static void GetWebSitePage()
        {
            string sqlSelectRight = "SELECT ROW_NUMBER() OVER(ORDER BY PageFileName) as ��� ,PageFileName as ҳ��·��, RightName as ҳ�����,CheckLogIn as �Ƿ���֤��½ ,CheckRight as �Ƿ���֤Ȩ��,RightCode FROM TbRight Order By PageFileName";
            sqlSelectRight = "SELECT id as ��� ,PageName as ҳ��·��, PageDes as ҳ�����,'true' as �Ƿ���֤��½ ,'true' as �Ƿ���֤Ȩ��,PageName as RightCode  FROM TbRight where PageName like '%.aspx'  Order By ID";
            try
            {
                _AllPagesConfig = SqlDbAccess.GetDataTable(CommandType.Text, sqlSelectRight);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Session.Add("ERROR_MESSAGE", "���ݿ��������!");
                HttpContext.Current.Session.Add("ERROR_Exception", ex);
                HttpContext.Current.Response.Redirect("~/frmErrInfor.aspx");
            }
            if (_AllPagesConfig.Rows.Count == 0)
            {
                //GetPages(new DirectoryInfo(WebSiteRootPaht), false);
                for (int i = 0; i <= _AllPagesConfig.Rows.Count - 1; i++)
                {
                    _AllPagesConfig.Rows[i]["���"] = i + 1;
                }
            }

        }

        /// <summary>
        /// ˢ����վ��ҳ��  �п���ҳ���Ѿ�������
        /// </summary>
        public static void reFreshWebSitPage()
        {
            string strUrlTemp = "";
            //ɾ�������ڵ�ҳ��
            for (int i = _AllPagesConfig.Rows.Count - 1; i >= 0; i--)
            {
                if (_AllPagesConfig.Rows[i]["ҳ��·��"].ToString() == "����")
                {
                    continue;
                }
                strUrlTemp = _AllPagesConfig.Rows[i]["ҳ��·��"].ToString();
                strUrlTemp = strUrlTemp.Contains("?") ? strUrlTemp.Substring(0, strUrlTemp.IndexOf("?")) : strUrlTemp;

                if (!File.Exists(HttpContext.Current.Server.MapPath(strUrlTemp)))
                {
                    DeleteRight(_AllPagesConfig.Rows[i]["RightCode"].ToString());
                    _AllPagesConfig.Rows.RemoveAt(i);

                }
            }
            GetPages(new DirectoryInfo(HttpContext.Current.Server.MapPath("~/")), true);
            for (int i = 0; i <= _AllPagesConfig.Rows.Count - 1; i++)
            {
                _AllPagesConfig.Rows[i]["���"] = i + 1;
            }
        }

        /// <summary>
        /// �õ�Ŀ¼�����е�aspx,asp,htm,html �ļ�
        /// </summary>
        /// <returns></returns>
        private static void GetPages(System.IO.DirectoryInfo dir, bool CheckExists)
        {
            //�õ���ǰĿ¼�µ��ļ�
            foreach (FileInfo file in dir.GetFiles("*.*"))
            {
                bool IsExists = false;
                switch (file.Extension.ToUpper())
                {
                    case ".ASPX":
                    case ".HTML":
                    case ".HTM":
                    case ".ASP":
                        if (file.FullName.ToString().IndexOf("fckeditor") < 0)
                        {
                            if (CheckExists)
                            {

                                foreach (DataRow dr in _AllPagesConfig.Rows)
                                {

                                    string filePath;
                                    if (dr["ҳ��·��"].ToString().IndexOf("?") > 0)
                                    {
                                        filePath = dr["ҳ��·��"].ToString().Substring(0, dr["ҳ��·��"].ToString().IndexOf("?"));
                                    }
                                    else
                                    {
                                        filePath = dr["ҳ��·��"].ToString();
                                    }

                                    if (HttpContext.Current.Server.MapPath(filePath) == file.FullName)
                                    {
                                        IsExists = true;
                                        continue;
                                    }
                                }
                            }
                            if ((CheckExists == true && IsExists == false) || CheckExists == false)
                            {
                                DataRow dr = _AllPagesConfig.NewRow();
                                dr["ҳ��·��"] = file.FullName.Replace(WebSiteRootPaht, "~/").Replace("\\","/");
                                dr["ҳ�����"] = string.Empty;
                                dr["�Ƿ���֤��½"] = false;
                                dr["�Ƿ���֤Ȩ��"] = false;

                                _AllPagesConfig.Rows.Add(dr);
                            }
                        }
                        break;
                    default:
                        break;
                }

            }

            //�õ���Ŀ¼�µ��ļ�
            foreach (DirectoryInfo childdir in dir.GetDirectories())
            {
                GetPages(childdir, CheckExists);
            }
        }

        /// <summary>
        /// ������ϵͳ�õ�Ȩ���б�
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static DataTable GetRightsList()
        {
            string sql = "SELECT RightName,RightCode From TbRight WHERE CheckRight=1";
            return SqlDbAccess.GetDataTable(CommandType.Text, sql);
        }
    }
}
