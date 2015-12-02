using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Text;
using DBA;
using System.Web;

namespace Cpic.Cprs2010.User
{
    public class MenuConfig
    {
        /// <summary>
        /// TabMenu
        /// </summary>
        private static readonly string TabMenu = "<li id='NtTab{0}' name='NtTab' ><div id='NtDiv{0}' name='NtDiv'  Class='Currenttab{1}'><a href='#' id='NtA{0}'name ='NtA' onclick=\"{3}\" class='CurrentHoverTab{1}' onfocus='this.blur();'>{2}</a></div></li>";
        /// <summary>
        /// 主菜单
        /// </summary>
        private static readonly string mainMenuItem = "<li Class=\"CurrentItem\" forid=\"{0}\"><div class=\"current\" onmousedown=\"gomenu(event)\"> <cite>&nbsp;&nbsp;&nbsp;<a href=\"#\" style=\"font-weight:bold;\" onfocus=\"this.blur();\">{1}</a></cite> <span class=\"title\" id=\"top\" ><img src=\"../images/dropdown.gif\" class=\"arrow\" style=\"z-Index:-1;\"/></span> </div></li>";
        private static readonly string mainMenuItem1 = "<li Class=\"CurrentItem\" forid=\"{0}\"><div class=\"current\" onmousedown=\"gomenu(event)\"> <cite>&nbsp;&nbsp;&nbsp;<a href=\"#\" style=\"font-weight:bold;\" onfocus=\"this.blur();\">{1}</a></cite> <span class=\"title\" id=\"top\" ><img src=\"../admin/images/dropdown.gif\" class=\"arrow\" style=\"z-Index:-1;\"/></span> </div></li>";
        /// <summary>
        /// 子菜单头部
        /// </summary>
        private static readonly string TopSubMenu = "<li class=\"Submenu\"><div class=\"Submenu1\"><table><tbody>";
        /// <summary>
        /// 子菜单中间
        /// </summary>
        private static readonly string MidSubMenu = "<tr><td><a id=\"menuitem{0}\" name=\"menuitem{1}\" href=\"javascript:void(0);\" {2} onclick=\"{3}\" onfocus=\"this.blur();\">{4}</a></td></tr>";
        /// <summary>
        /// 子菜单底部
        /// </summary>
        private static readonly string BotSubMenu = "</tbody></table></div></li>";

        /// <summary>
        /// 得到所有的Tab
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTabMenus()
        {
            RequestFilter.CheckLogIn();
            string reporter;
            reporter = HttpContext.Current.Session["UserName"].ToString();
            string sql = "select distinct a.title,a.defaulturl,a.id from TabMenus as a,MainMenus as b where b.TabMenuId = a.id and b.id in(" +
                        " select MainMenuid from TbUserInfo as a,TbUserRole as b,TbRoleRight as c,TbRight as d,SubMenus as e " +
                        " where a.User_logname = b.User_logname  and b.RoleCode = c.RoleCode and c.rightCode = d.rightCode and d.PageFileName =e.PageFileName " +
                        " and e.isMenu =1 and a.User_logname = @UserName)  Or a.title='后台登陆'";
            SqlParameter parms = new SqlParameter("@UserName", reporter);
            return SqlDbAccess.GetDataTable(CommandType.Text, sql, parms);
        }
        public static DataTable GetPublicMainMenus(int TabMenuId)
        {
            string sql = @"select distinct title,id from MainMenus 
                            where TabMenuId = @TabMenuId ";
            SqlParameter[] parm = new SqlParameter[]{
                                new SqlParameter("@TabMenuId", TabMenuId)
            };
            return SqlDbAccess.GetDataTable(CommandType.Text, sql, parm);
        }
        /// <summary>
        /// 得到Tab下的主菜单
        /// </summary>
        /// <param name="TabMenuId"></param>
        /// <returns></returns>
        public static DataTable GetMainMenus(int TabMenuId)
        {
            if (TabMenuId == 4 || TabMenuId == 5)
            {
                return GetPublicMainMenus(TabMenuId);
            }
            RequestFilter.CheckLogIn();
            string reporter;
            reporter = HttpContext.Current.Session["UserName"].ToString();
            string sql = @"select distinct title,id from MainMenus 
                            where TabMenuId = @TabMenuId and id in(
                                select MainMenuid from TbUserInfo as a,TbUserRole as b,TbRoleRight as c,TbRight as d,SubMenus as e
                                where a.User_logname = b.User_logname  and b.RoleCode = c.RoleCode and c.rightCode = d.rightCode and d.PageFileName =e.PageFileName
                                and e.isMenu =1 and a.User_logname =@UserName) ";
            SqlParameter[] parm = new SqlParameter[]{
                                new SqlParameter("@TabMenuId", TabMenuId),
                                new SqlParameter("@UserName",reporter)
            };
            return SqlDbAccess.GetDataTable(CommandType.Text, sql, parm);

        }

        public static DataTable GetPublicSubMenus(int MainMenuId)
        {
            string sql = @"select id, Title ,Url from SubMenus 
                            where MainMenuId = @MainMenuId ";
            SqlParameter[] parm = new SqlParameter[]{
                                new SqlParameter("@MainMenuId", MainMenuId)
            };
            return SqlDbAccess.GetDataTable(CommandType.Text, sql, parm);
        }

        /// <summary>
        /// 得到主菜单下的所有子菜单
        /// </summary>
        /// <param name="TabMenuId"></param>
        /// <returns></returns>
        private static DataTable GetSubMenus(int MainMenuId)
        {
            if (MainMenuId == 14 || MainMenuId == 15 || MainMenuId == 16)
            {
                return   GetPublicSubMenus(MainMenuId);
            }
            RequestFilter.CheckLogIn();
            string reporter;
            reporter = HttpContext.Current.Session["UserName"].ToString();
            string sql = @"select e.id, e.Title ,e.Url from TbUserInfo as a,TbUserRole as b,TbRoleRight as c,TbRight as d,SubMenus as e
                            where a.User_logname = b.User_logname  and b.RoleCode = c.RoleCode and c.rightCode = d.rightCode and d.PageFileName =e.PageFileName
                            and e.isMenu =1 and a.User_logname = @UserName and e.MainMenuId=@MainMenuId order by Sort";
            SqlParameter[] parm = new SqlParameter[]{
                                new SqlParameter("@MainMenuId", MainMenuId),
                                new SqlParameter("@UserName",reporter)
            };
            return SqlDbAccess.GetDataTable(CommandType.Text, sql, parm);
        }
        /// <summary>
        /// 创建Tab菜单
        /// </summary>
        /// <returns></returns>
        public static string CreateTabMenu()
        {
            DataTable dt = MenuConfig.GetTabMenus();
            string menuText = "";
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                int mod = (i % 7) + 1;
                menuText += string.Format(TabMenu + Environment.NewLine, i + 1, mod, dt.Rows[i]["title"].ToString(), CreateTabOnclick(dt.Rows[i]["id"].ToString(),(i+1).ToString(), dt.Rows[i]["DefaultUrl"].ToString()));
            }

            return menuText;
        }

        /// <summary>
        /// 得到Tab菜单的菜单
        /// </summary>
        /// <param name="TabMenuId"></param>
        /// <returns></returns>
        public static string createMenuItems(int TabMenuId)
        {
            string menuText = string.Empty;
            DataTable tbMainMenus = GetMainMenus(TabMenuId);
            if (tbMainMenus.Rows.Count == 0)
            {
                menuText = string.Empty;
            }
            else
            {
                menuText += "<ul>";
                for (int j = 0; j < tbMainMenus.Rows.Count; j++)
                {
                    //增加菜单大项
                    menuText += createMainMenu((int)tbMainMenus.Rows[j]["id"], tbMainMenus.Rows[j]["title"].ToString());
                    //增加菜单子项
                    menuText += createSubMenu((int)tbMainMenus.Rows[j]["id"]);
                }
                menuText += "</ul>";                
            }
            return menuText;
        }
        /// <summary>
        /// 得到Tab菜单的菜单
        /// </summary>
        /// <param name="TabMenuId"></param>
        /// <returns></returns>
        public static string createMenuItems1(int TabMenuId)
        {
            string menuText = string.Empty;
            DataTable tbMainMenus = GetMainMenus(TabMenuId);
            if (tbMainMenus.Rows.Count == 0)
            {
                menuText = string.Empty;
            }
            else
            {
                menuText += "<ul>";
                for (int j = 0; j < tbMainMenus.Rows.Count; j++)
                {
                    //增加菜单大项
                    menuText += createMainMenu1((int)tbMainMenus.Rows[j]["id"], tbMainMenus.Rows[j]["title"].ToString());
                    //增加菜单子项
                    menuText += createSubMenu((int)tbMainMenus.Rows[j]["id"]);
                }
                menuText += "</ul>";
            }
            return menuText;
        }

        /// <summary>
        /// 创建主菜单
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="menuText"></param>
        /// <returns></returns>
        public static string createMainMenu(int Id, string menuText)
        {
            return string.Format(mainMenuItem, Id, menuText);
        }
        /// <summary>
        /// 创建子菜单
        /// </summary>
        /// <param name="mainMenuId"></param>
        /// <returns></returns>
        public static string createSubMenu(int mainMenuId)
        {
            DataTable tbSubMenus = GetSubMenus(mainMenuId);
            string subMenuItem="";
            subMenuItem += TopSubMenu;
            for (int i = 0; i <= tbSubMenus.Rows.Count - 1; i++)
            {
                subMenuItem += string.Format(MidSubMenu, tbSubMenus.Rows[i]["id"].ToString(), mainMenuId, "", CreateOnclick(tbSubMenus.Rows[i]["Url"].ToString(), mainMenuId), tbSubMenus.Rows[i]["Title"].ToString());
            }
            subMenuItem += BotSubMenu;

          
            return subMenuItem;
        }

        /// <summary>
        /// 创建主菜单
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="menuText"></param>
        /// <returns></returns>
        public static string createMainMenu1(int Id, string menuText)
        {
            return string.Format(mainMenuItem1, Id, menuText);
        }
      
        public static string CreateOnclick(string Url, int menuitemId)
        {
            return string.Format("javascript:document.getElementById('main').src='{0}';SetMenuItemFocus(this,'menuitem{1}');", Url, menuitemId);
        }
        public static string CreateTabOnclick(string TabId,string rowid, string DefaultUrl)
        {
            string LocationUrlScript = "javascript:locationurl({0},{1},'{2}');";
            return string.Format(LocationUrlScript, TabId,rowid, DefaultUrl);
        }


        public static string CreateLocationUrlScript()
        {
            DataTable dt = MenuConfig.GetTabMenus();
            string LocationUrlScript = "<script>locationurl({0},1,'{1}');</script>";
            return string.Format(LocationUrlScript, dt.Rows[0]["id"].ToString(), dt.Rows[0]["DefaultUrl"].ToString());
           
        }

        /// <summary>
        /// 添加Tab菜单
        /// </summary>
        /// <param name="TabName"></param>
        /// <returns></returns>
        public static bool AddTabMenu(string TabName)
        {
            string sql = "Insert INTO TabMenus (title,sort) VALUES(@title,select max(sort)+1 from TabMenus)";
            SqlParameter parm = new SqlParameter("@title", TabName);
            if (SqlDbAccess.ExecNoQuery(CommandType.Text, sql, parm) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改Tab菜单名字
        /// </summary>
        /// <param name="NewTabName"></param>
        /// <param name="OldTabName"></param>
        /// <returns></returns>
        public static bool UpdateTabMenu(string NewTabName, string OldTabName)
        {
            string sql = "UPDATE TabMenus SET title=@NewTabName WHERE title=@OldTabName";
            SqlParameter[] parms = new SqlParameter[]{
                           new SqlParameter("@NewTabName", NewTabName),
                           new SqlParameter("@OldTabName",OldTabName)};
            if (SqlDbAccess.ExecNoQuery(CommandType.Text, sql, parms) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除Tab菜单
        /// </summary>
        /// <param name="TabName"></param>
        /// <returns></returns>
        public static bool DelTabMenu(string TabName)
        {
            string sql = "Delete TabMenus WHERE title=@title";
            SqlParameter parm = new SqlParameter("@title", TabName);
            if (SqlDbAccess.ExecNoQuery(CommandType.Text, sql, parm) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckTabMenuExisit(string TabName)
        {
            string sql = "SELECT COUNT(*) AS C FROM TabMenus WHERE title=@title";
            SqlParameter parm = new SqlParameter("@title", TabName);
            if (((int)SqlDbAccess.ExecuteScalar(CommandType.Text, sql, parm)) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 设置一个Tab菜单顺序向右移动一格
        /// </summary>
        /// <param name="TabName"></param>
        /// <returns></returns>
        public static bool MoveRight(string TabName)
        {
            string sql = "SELECT top 2 id,sort FROM TabMenus WHERE sort >= (select sort from tabmenus where title=@title) order by sort asc";
            SqlParameter parm = new SqlParameter("@title", TabName);
            DataTable dt = SqlDbAccess.GetDataTable(CommandType.Text, sql, parm);
            if (dt.Rows.Count == 2)
            {
                string sql1 = "update TabMenus set sort=" + dt.Rows[0]["sort"].ToString() + " where id=" + dt.Rows[1]["id"].ToString();
                string sql2 = "update TabMenus set sort=" + dt.Rows[1]["sort"].ToString() + " where id=" + dt.Rows[0]["id"].ToString();
                using (SqlConnection conn = SqlDbAccess.GetSqlConnection())
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sql1);
                        SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sql2);
                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 设置一个Tab菜单顺序向左移动一格
        /// </summary>
        /// <param name="TabName"></param>
        /// <returns></returns>
        public static bool MoveLeft(string TabName)
        {
            string sql = "SELECT top 2 id,sort FROM TabMenus WHERE sort <= (select sort from tabmenus where title=@title) order by sort desc";
            SqlParameter parm = new SqlParameter("@title", TabName);
            DataTable dt = SqlDbAccess.GetDataTable(CommandType.Text, sql, parm);
            if (dt.Rows.Count == 2)
            {
                string sql1 = "update TabMenus set sort=" + dt.Rows[0]["sort"].ToString() + " where id=" + dt.Rows[1]["id"].ToString();
                string sql2 = "update TabMenus set sort=" + dt.Rows[1]["sort"].ToString() + " where id=" + dt.Rows[0]["id"].ToString();
                using (SqlConnection conn = SqlDbAccess.GetSqlConnection())
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sql1);
                        SqlDbAccess.ExecNoQuery(trans, CommandType.Text, sql2);
                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

    }
}
