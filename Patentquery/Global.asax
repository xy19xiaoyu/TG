<%@ Application Language="C#" %>
<%@ Import Namespace="Cpic.Cprs2010.Search.SearchManager" %>
<%@ Import Namespace="Cpic.Cprs2010.Cfg" %>
<%@ Import Namespace="Cpic.Cprs2010.User" %>
<%@ Import Namespace="Cpic.Cprs2010.Log" %>
<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        Cpic.Cprs2010.Log.Log.log.Error(string.Format("网站:{0}已经启动", this.Context.Server.MapPath(".")));
        Cpic.Cprs2010.Search.CprsConfig.Init();
        UserManager.Init();
        //SearchFactory.Ini(string.Format("网站:{0}已经启动", this.Context.Server.MapPath(".")));
        SearchFactory.Ini(SearchPoolType.SocketCn, SearchPoolType.SocketDocDB, SearchPoolType.SocketDwpi, string.Format("网站:{0}已经启动", this.Context.Server.MapPath(".")));

        //// 在应用程序启动时运行的代码
        //WebSiteStat.WebSiteStart();
        ////加载页面权限
        Right.IniPageConfig();
        RequestFilter.IniCheckList();
        //Stat.IniStatPage();
        //Stat.IniStatPageList();

    }

    void Application_PreRequestHandlerExecute()
    {
        //RequestFilter.Filter();
        ProXZQDLL.UserRight.Filter();
    }

    void Application_End(object sender, EventArgs e)
    {
        try
        {
            // Cpic.Cprs2010.Log.Log.log.Error(string.Format("网站:{0}已经关闭", this.Context.Server.MapPath(".")));
            //SearchFactory.Dispose();
            UserManager.Dispose();
        }
        catch (Exception ex)
        {
            //using (System.IO.StreamWriter sw = new System.IO.StreamWriter("E:\\Cprs2010\\VPath\\log\\xy.log", true))
            //{
            //    sw.WriteLine(DateTime.Now.ToString() + ex.ToString());
            //}
        }
    }

    void Application_Error(object sender, EventArgs e)
    {
        //在出现未处理的错误时运行的代码

    }

    void Session_Start(object sender, EventArgs e)
    {
        if (ProXZQDLL.IpFillterSvs.bIpFillter)
        {
            string IP = HttpContext.Current.Request.UserHostAddress.ToString();
            if (!ProXZQDLL.UserRight.getIPQuYu(IP))
            {
                Response.Redirect("frmXZQ.htm");
            }
        }
    }

    void Session_End(object sender, EventArgs e)
    {
        // 在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer 
        // 或 SQLServer，则不会引发该事件。
        //释放某用户的ID


    }

    
       
</script>
