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
    /// 权限
    /// </summary>
    public class Right
    {
        private static string WebSiteRootPaht = HttpContext.Current.Server.MapPath("~/");
        private static log4net.ILog log = log4net.LogManager.GetLogger("RequestFilter");
        private static DataTable _AllPagesConfig = new DataTable("PagesConfig");

        /// <summary>
        /// 配置
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
        /// 构造函数
        /// </summary>
        public static void IniPageConfig()
        {
            //1.从数据库中读取配置
            GetWebSitePage();
        }

        /// <summary>
        /// 添加一个权限
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
        /// 添加一个权限
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
        /// 删除一个权限
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
        /// 更新一个权限
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
                HttpContext.Current.Session.Add("ERROR_MESSAGE", "数据库操作错误!");
                HttpContext.Current.Session.Add("ERROR_Exception", ex);
                HttpContext.Current.Response.Redirect("~/frmErrInfor.aspx");
                return false;
            }
        }

        /// <summary>
        /// 更新一个权限
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
        /// 保存权限配置
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
                        //如果没有权限代码 就是还没插入到数据库中
                        if (row.IsNull("RightCode"))
                        {
                            AddRight(row["页面路径"].ToString(), row["页面标题"].ToString(), Convert.ToBoolean(row["是否验证登陆"].ToString()), Convert.ToBoolean(row["是否验证权限"].ToString()));
                        }
                        else
                        {
                            UpdateRight(row["页面路径"].ToString(), row["页面标题"].ToString(), row["RightCode"].ToString(), Convert.ToBoolean(row["是否验证登陆"].ToString()), Convert.ToBoolean(row["是否验证权限"].ToString()));
                        }
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    HttpContext.Current.Session.Add("ERROR_MESSAGE", "数据库操作错误!");
                    HttpContext.Current.Session.Add("ERROR_Exception", ex);
                    HttpContext.Current.Response.Redirect("~/frmErrInfor.aspx");
                    return false;
                }
                trans.Commit();
            }
            return true;
        }

        /// <summary>
        /// 得到网站所有的页面
        /// </summary>
        private static void GetWebSitePage()
        {
            string sqlSelectRight = "SELECT ROW_NUMBER() OVER(ORDER BY PageFileName) as 序号 ,PageFileName as 页面路径, RightName as 页面标题,CheckLogIn as 是否验证登陆 ,CheckRight as 是否验证权限,RightCode FROM TbRight Order By PageFileName";
            sqlSelectRight = "SELECT id as 序号 ,PageName as 页面路径, PageDes as 页面标题,'true' as 是否验证登陆 ,'true' as 是否验证权限,PageName as RightCode  FROM TbRight where PageName like '%.aspx'  Order By ID";
            try
            {
                _AllPagesConfig = SqlDbAccess.GetDataTable(CommandType.Text, sqlSelectRight);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Session.Add("ERROR_MESSAGE", "数据库操作错误!");
                HttpContext.Current.Session.Add("ERROR_Exception", ex);
                HttpContext.Current.Response.Redirect("~/frmErrInfor.aspx");
            }
            if (_AllPagesConfig.Rows.Count == 0)
            {
                //GetPages(new DirectoryInfo(WebSiteRootPaht), false);
                for (int i = 0; i <= _AllPagesConfig.Rows.Count - 1; i++)
                {
                    _AllPagesConfig.Rows[i]["序号"] = i + 1;
                }
            }

        }

        /// <summary>
        /// 刷新网站的页面  有可能页面已经不在了
        /// </summary>
        public static void reFreshWebSitPage()
        {
            string strUrlTemp = "";
            //删除不存在的页面
            for (int i = _AllPagesConfig.Rows.Count - 1; i >= 0; i--)
            {
                if (_AllPagesConfig.Rows[i]["页面路径"].ToString() == "审批")
                {
                    continue;
                }
                strUrlTemp = _AllPagesConfig.Rows[i]["页面路径"].ToString();
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
                _AllPagesConfig.Rows[i]["序号"] = i + 1;
            }
        }

        /// <summary>
        /// 得到目录下所有的aspx,asp,htm,html 文件
        /// </summary>
        /// <returns></returns>
        private static void GetPages(System.IO.DirectoryInfo dir, bool CheckExists)
        {
            //得到当前目录下的文件
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
                                    if (dr["页面路径"].ToString().IndexOf("?") > 0)
                                    {
                                        filePath = dr["页面路径"].ToString().Substring(0, dr["页面路径"].ToString().IndexOf("?"));
                                    }
                                    else
                                    {
                                        filePath = dr["页面路径"].ToString();
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
                                dr["页面路径"] = file.FullName.Replace(WebSiteRootPaht, "~/").Replace("\\","/");
                                dr["页面标题"] = string.Empty;
                                dr["是否验证登陆"] = false;
                                dr["是否验证权限"] = false;

                                _AllPagesConfig.Rows.Add(dr);
                            }
                        }
                        break;
                    default:
                        break;
                }

            }

            //得到子目录下的文件
            foreach (DirectoryInfo childdir in dir.GetDirectories())
            {
                GetPages(childdir, CheckExists);
            }
        }

        /// <summary>
        /// 根据子系统得到权限列表
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
