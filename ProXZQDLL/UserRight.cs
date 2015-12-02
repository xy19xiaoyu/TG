using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Data.Linq;
using System.Collections.Generic;
using Cpic.Cprs2010.User;

namespace ProXZQDLL
{
    /// <summary>
    ///UserRight 的摘要说明。 xxt制作，2012-02
    /// </summary>
    public class UserRight
    {
        public UserRight()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 取得用户详细信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static TbUser getUserInfo(string UserID)
        {

            DataClasses1DataContext db = new DataClasses1DataContext();
            Table<TbUser> tb = db.TbUser;

            var result = from item in tb
                         where item.ID == Convert.ToInt32(UserID)
                         select item;


            TbUser TbUser = result.First();

            if (TbUser.DepartMentID == 0)
            {
                TbUser.QiYeID = TbUser.ID;
            }
            else
            {
                TbUser.QiYeID = TbUser.DepartMentID;
            }
            return TbUser;
        }
        /// <summary>
        /// 取得用户类型
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static string getUserLeiXing(string UserID)
        {
            TbUser tbUserInfo = getUserInfo(UserID);
            return tbUserInfo.YongHuLeiXing;

        }

        /// <summary>
        /// 取得企业用户的ID
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static string getDepartMentID(string UserID)
        {
            TbUser tbUserInfo = getUserInfo(UserID);
            return tbUserInfo.DepartMentID.ToString(); ;
        }

        /// <summary>
        /// 验证用户是否有当前权限
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static bool Check(Page page)
        {
            if (page.Session["UserID"] == null)
            {
                page.Session["ReturnUrl"] = page.ToString().Substring(4, page.ToString().Length - 9).Replace("details", "").Replace("_", "/") + ".aspx";

                MSG.AlertReturn(page, "您的登陆已超时，请重新登陆！", "../default.aspx");
                //page.Response.Write("<script language='javascript'>");
                //page.Response.Write("parent.location.href='../default.aspx';");
                //page.Response.Write("</script>");
                return false;
            }

            string UserID = page.Session["UserID"].ToString().Trim();
            DataSet ds = new DataSet();
            //ASP._1_default_aspx
            string sql = "Select Distinct a.ID From UserRole a,RoleRight b, TbRight c Where a.UserID='" + UserID + "' "
            + "And a.RoleID=b.RoleID And b.RightID=c.ID And 'ASP.'+REPLACE(REPLACE(c.PageName,'/','_'),'.','_')='" + page.ToString().Replace("details", "") + "'";

            ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //page.Response.Redirect("default.aspx");
                page.Response.Write("<script language='javascript'>");
                page.Response.Write("parent.location.href='../default.aspx';");
                page.Response.Write("</script>");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 取得用户的权限列表
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static DataSet getUserRight(string UserID)
        {
            DataSet ds = new DataSet();
            string sql = "Select PageName,PageDes From TbRight Where ID IN (Select RightID From RoleRight Where RoleID IN (Select RoleID From UserRole Where UserID='" + UserID + "'))";
            ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);
            return ds;
        }

        /// <summary>
        /// 取得用户的权限列表
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static DataSet getUserRightDs(string UserID, string _strLeixing)
        {
            DataSet ds = new DataSet();
            string sql = "Select PageName,PageDes From TbRight Where ID IN (Select RightID From RoleRight Where RoleID IN (Select RoleID From UserRole Where UserID='" + UserID + "')) and Nodelevel In (select ID from TbRight where PageName='" + _strLeixing + "')";
            ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);
            return ds;
        }

        /// <summary>
        /// 根据权限代码用户编码返回权限
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="RightCode"></param>
        /// <returns>>1有权,-1无权</returns>
        public static int getUserRight(string UserID, string RightCode)
        {
            try
            {
                DataSet ds = new DataSet();
                string strGetRoleIdSql = "Select RoleID From UserRole Where UserID='" + UserID + "'";

                if (int.Parse(UserID) < 1000000)
                {
                    strGetRoleIdSql += " union select ID from TbRole where RoleName ='游客'";
                }

                string sql = "Select PageName,PageDes From TbRight Where ID IN (Select RightID From RoleRight Where RoleID IN (" + strGetRoleIdSql + ")) And PageName='" + RightCode + "'";

                ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    return 1;
                }
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static bool getVisibleRight(string UserID, string RightCode)
        {
            bool bRs = false;
            try
            {
                if (getUserRight(UserID, RightCode) > 0)
                {
                    bRs = true;
                }
            }
            catch (Exception ex)
            {
            }
            return bRs;
        }
        /// <summary>
        /// 记录用户登录信息
        /// </summary>
        /// <param name="strIP"></param>
        /// <param name="UserName"></param>
        /// <param name="UserPWD"></param>
        /// <returns></returns>
        public static bool WriteUserInfo(string strIP, string UserName, string UserPWD)
        {
            string sql = "Insert Into LoginInfo(IP,UserName,UserPWD) Values('" + strIP + "','" + UserName + "','" + UserPWD + "')";
            try
            {
                if (DBA.DbAccess.ExecNoQuery(CommandType.Text, sql) < 0)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获取当前IP的最后记录信息 
        /// </summary>
        /// <param name="strIP"></param>
        /// <returns></returns>
        public static ArrayList getLoginInfo(string strIP)
        {
            DataSet ds = new DataSet();
            string sql = "Select top 1 * From LoginInfo Where IP='" + strIP + "' Order By ID DESC";
            try
            {
                ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);
            }
            catch (Exception ex)
            {
                return null;
            }
            if (ds.Tables[0].Rows.Count <= 0)
            {
                return null;
            }

            ArrayList result = new ArrayList();

            result.Add(ds.Tables[0].Rows[0]["UserName"].ToString().Trim());
            result.Add(ds.Tables[0].Rows[0]["UserPWD"].ToString().Trim());

            return result;

        }
        /// <summary>
        /// 取得企业用户的上级
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int getDepID(int ID)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var result = from item in db.TbUser
                         where item.DepartMentID == ID
                         select item;
            if (result.Count() <= 0)
            {
                return 0;
            }

            return result.ToList()[0].ID;
        }


        /// <summary>
        /// 取得所有权限代码
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static string getstrRightCode(int UserID)
        {
            string result = ",";

            DataSet ds = new DataSet();
            ds = getUserRight(UserID.ToString().Trim());

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                result = result + ds.Tables[0].Rows[i]["PageName"].ToString().Trim() + ",";
            }

            return result;
        }
        /// <summary>
        /// 验证IP 是否在可浏览范围内
        /// </summary>
        /// <param name="strIP"></param>
        /// <returns></returns>
        public static bool getIPQuYu(string strIP)
        {
            List<TbIP> lst = new List<TbIP>();

            lst = getTbIP();

            foreach (var item in lst)
            {
                if (item.IP == strIP)
                {
                    if (item.flag == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            string[] subIP = strIP.Split('.');
            if (subIP.Length != 4)
            {
                return false;
            }
            string quYu = Stat.GetLocal(strIP);

            if (quYu.Contains("厦门") || quYu.Contains("漳州") || quYu.Contains("泉州") || quYu.Contains("北京"))
            {
                return true;
            }


            return false;
        }

        /// <summary>
        /// 过滤每次请求
        /// </summary>
        public static void Filter()
        {
            try
            {
                //得到请求的路径 
                string ReqPath = HttpContext.Current.Request.Url.AbsolutePath; // HttpContext.Current.Request.PhysicalPath;
                string strUrlExt = HttpContext.Current.Request.CurrentExecutionFilePathExtension;
                string strUrl = HttpContext.Current.Request.Url.Query.Trim().ToUpper();

                //得到请求页面类型      
                string strRegFileName = (ReqPath.IndexOf('/') > -1 ? ReqPath.Substring(ReqPath.LastIndexOf('/') + 1) : ReqPath).Trim().ToUpper();     // System.IO.Path.GetFileName(ReqPath).ToUpper();
                //如果请求的是aspx 页面
                if (!strUrlExt.ToUpper().Equals(".ASPX"))
                {
                    //不去做任何判断走了
                    return;
                }

                string strUrlPageAndQuery = strRegFileName + strUrl;

                //判断请求的页面 是否需要验证登陆
                //if (RequestFilter.CheckLogInList.Contains(strRegFileName))
                //{                
                //    //LoginManager.CheckUID_SessionID();
                //}

                ////判断请求的页面是否需要验证权限
                if (RequestFilter.CheckRightList.Contains(strUrlPageAndQuery))
                {
                    if (!UserRight.getVisibleRight(HttpContext.Current.Session["UserID"].ToString(), strUrlPageAndQuery))
                    {
                        HttpContext.Current.Response.Redirect("~/frmError.htm");
                    }
                }
                else if (RequestFilter.CheckRightList.Contains(strRegFileName))
                {
                    if (!UserRight.getVisibleRight(HttpContext.Current.Session["UserID"].ToString(), strRegFileName))
                    {
                        HttpContext.Current.Response.Redirect("~/frmError.htm");
                    }
                }
            }
            catch (Exception ex)
            {
                //HttpContext.Current.Response.Redirect("~/frmError.htm");
            }
        }

        public static List<TbIP> getTbIP()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var result = from item in db.TbIP
                         where item.IP != "glbalIpKey"
                         select item;
            return result.ToList();
        }

      

        public static void TbIpDel(int ID)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var tb = db.TbIP.Where(o => o.ID == ID);
                db.TbIP.DeleteAllOnSubmit(tb);
                db.SubmitChanges();
            }
        }


        public static void TbIpInsert(string IP, int flag)
        {
            TbIP tb = new TbIP();
            tb.IP = IP;
            tb.CreateDate = DateTime.Now;
            tb.flag = flag;

            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                db.TbIP.InsertOnSubmit(tb);
                db.SubmitChanges();
            }
        }       
    }
}