using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using ProXZQDLL;
using System.Xml;

namespace mobileAppWebService
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class CprsMobileWebSvc : System.Web.Services.WebService
    {
        private static string appVersion = "";
        private static string downloadURL = "";
        [WebMethod(Description = "检查更新")]
        public AppInfo VersionCheck(string currentVer)
        {
            AppInfo appInfo = new AppInfo();
            appInfo.needUpdate = false;
            appInfo.appVersion = appVersion;
            appInfo.downloadURL = "";

            try
            {
                if (appVersion == null || appVersion.Equals(""))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(HttpContext.Current.Server.MapPath("~/Svc/" + "AppConfig.xml"));
                    XmlNode node = doc.SelectSingleNode("/root/android");
                    XmlNodeList nodes = node.ChildNodes;
                    if (nodes.Count == 2)
                    {
                        appVersion = nodes[0].InnerText;
                        appInfo.appVersion = appVersion;
                        downloadURL = nodes[1].InnerText;
                    }
                    else
                    {
                        return appInfo;
                    }
                }
                if (currentVer != null && !currentVer.Equals(""))
                {
                    Version v1 = new Version(appVersion);
                    Version v2 = new Version(currentVer);
                    if (v1 > v2)
                    {
                        appInfo.needUpdate = true;
                        appInfo.downloadURL = downloadURL;
                    }
                }
            }
            catch (Exception e)
            {
                return appInfo;
            }

            return appInfo;
        }
        
        [WebMethod(Description = "登录校验")]
        public UserAccount LoginCheck(string userName, string passWord)
        {
            UserAccount userAccount = new UserAccount();
            userAccount.isLogin = false;
            userAccount.haveMsg = false;
            userAccount.msgContent = "";

            if (userName == null || userName.Trim().Equals(""))
            {
                userAccount.errorMsg = "请填写用户名！";
            }
            else if (passWord == null || passWord.Trim().Equals(""))
            {
                userAccount.errorMsg = "请填写密码！";
            }
            else
            {
                DataSet ds = new DataSet();
                string sql = "select * from TbUser Where UserName='" + userName.Trim() + "'";
                ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);
                
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    userAccount.errorMsg = "您输入的用户名不存在，请重新输入！";
                    return userAccount;
                }

                string PWD = ds.Tables[0].Rows[0]["UserPWD"].ToString().Trim();
                if (PWD.ToLower() != passWord.Trim().ToLower())
                {
                    userAccount.errorMsg = "您输入的用户名或密码错误，请重新输入！";
                    return userAccount;
                }
                
                userAccount.isLogin = true;
                userAccount.userId = ds.Tables[0].Rows[0]["ID"].ToString().Trim();
                userAccount.userName = ds.Tables[0].Rows[0]["RealName"].ToString().Trim();

                //添加专利预警信息更新提醒
                string YJ_sql = "Select a.c_id From C_EARLY_WARNING a Where a.user_id = '" + userAccount.userId + "' and a.isupdate = 1";
                DataSet YJ_ds = DBA.DbAccess.GetDataSet(CommandType.Text, YJ_sql);
                if (YJ_ds.Tables[0].Rows.Count > 0)
                {
                    userAccount.haveMsg = true;
                    userAccount.msgContent = "请注意，您关注的专利预警信息有更新！";

                    YJ_sql = "UPDATE C_EARLY_WARNING SET isupdate = 0 WHERE user_id = '" + userAccount.userId + "' and isupdate = 1";
                    DBA.DbAccess.ExecNoQuery(CommandType.Text, YJ_sql);
                }
            }

            return userAccount;
        }

        [WebMethod(Description = "注册用户")]
        public UserAccount Register(RegisterInf registerInf)
        {
            UserAccount userAccount = new UserAccount();
            userAccount.isLogin = false;
            userAccount.haveMsg = false;

            if (registerInf == null)
            {
                userAccount.errorMsg = "注册信息不完整,请查看！";
            }
            else
            {
                if (registerInf.txtUserName.Trim().Equals(""))
                {
                    userAccount.errorMsg = "请输入登录名称！";
                    return userAccount;
                }

                string sql = "Select * From TbUser Where UserName='" + registerInf.txtUserName.Trim() + "'";
                DataSet ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    userAccount.errorMsg = "您录入的登录名已存在，请重新输入！";
                    return userAccount;
                }
                if (registerInf.txtRealName.Trim() == "")
                {
                    userAccount.errorMsg = "请输入真实姓名！";
                    return userAccount;
                }
                if (registerInf.txtPWD.Trim() == "")
                {
                    userAccount.errorMsg = "请输入密码";
                    return userAccount;
                }
                if (registerInf.txtPWD.Trim().Length > 50)
                {
                    userAccount.errorMsg = "密码超长，请重新输入！";
                    return userAccount;
                }
                //if (txtPWD.Text.ToString().Trim() != txtQueRen.Text.ToString().Trim())
                //{
                //    return "您两次输入的密码不一致,请重新输入！";
                //}
                if (registerInf.txtYouXiang.Trim() == "")
                {
                    userAccount.errorMsg = "请输入您的邮箱地址！";
                    return userAccount;
                }
                
                TbUser user = new TbUser();

                user.UserName = registerInf.txtUserName.Trim();
                user.UserPWD = registerInf.txtPWD.Trim();
                user.RealName = registerInf.txtRealName.Trim();
                user.YongHuLeiXing = "个人";
                user.LianXiDianHua = registerInf.txtDianHua.Trim();
                user.ShouJi = registerInf.txtShouJi.Trim();
                user.TongXinDiZhi = registerInf.txtDiZhi.Trim();
                user.EMail = registerInf.txtYouXiang.Trim();
                user.DepartMentID = 0;
                user.SHFlag = 0;

                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    db.Log = Console.Out;
                    db.TbUser.InsertOnSubmit(user);
                    db.SubmitChanges();
                }
                
                string sqlShouCang = "insert into TLC_Albums (UserId,ParentId,Title,live,isdel,isparent) values('" + user.ID.ToString().Trim() + "',0,'收藏夹',0,0,0)";
                DBA.DbAccess.ExecNoQuery(CommandType.Text, sqlShouCang);

                userAccount.isLogin = true;
                userAccount.userId = user.ID.ToString().Trim();
                userAccount.userName = user.RealName.ToString().Trim();
            }

            return userAccount;
        }

        [WebMethod(Description = "获取查看预警权限")]
        public string GetAvailableWarningList(string userId)
        {
            //Random random = new Random();
            //string str = "";
            //for (int i = 0; i < 11; i++)
            //{
            //    str += random.Next(0, 2).ToString();
            //}
            //return str;

            List<string> l = new List<string>();
            l.Add("HYYJ_CN");
            l.Add("SQR_CN");
            l.Add("QYFB_CN");
            l.Add("FMRDX_CN");
            l.Add("LHZL_CN");
            l.Add("ZDY_CN");
            l.Add("HYYJ_EN");
            l.Add("SQR_EN");
            l.Add("QYYJ_EN");
            l.Add("FMR_EN");
            l.Add("ZDY_EN");

            string str = "";
            for (int i = 0; i < l.Count; i++)
            {
                if (UserRight.getUserRight(userId.ToString().Trim(), l[i]) == 1)
                    str += "1";
                else
                    str += "0";
            }
            return str;
        }

        [WebMethod(Description = "获取预警信息")]
        public List<WarningInf> GetWarningInf(string itemNo, string db, string userId)
        {
            string sql = "Select a.alias, b.w_Id, b.s_Name, b.changeDate, b.currentNum, b.changeNum From C_EARLY_WARNING a, C_W_SECARCH b Where a.c_id = b.c_id and a.c_type = '" + itemNo + "' and a.user_id = '" + userId + "' and a.dbsource = '" + db + "' and b.type = 1";
            DataSet ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);

            List<WarningInf> l = new List<WarningInf>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                WarningInf wi = new WarningInf();
                wi.warningName = ds.Tables[0].Rows[i]["alias"].ToString().Trim();
                wi.itemName = ds.Tables[0].Rows[i]["s_Name"].ToString().Trim();
                wi.itemId = ds.Tables[0].Rows[i]["w_Id"].ToString().Trim();
                //wi.totalSet = "";
                wi.totalSetNum = int.Parse(ds.Tables[0].Rows[i]["currentNum"].ToString().Trim());
                //wi.changeSet = "";
                wi.changeSetNum = int.Parse(ds.Tables[0].Rows[i]["changeNum"].ToString().Trim());
                l.Add(wi);
            }

            //List<WarningInf> l = new List<WarningInf>();
            //WarningInf wi_1 = new WarningInf();
            //wi_1.itemName = "预警1";
            //wi_1.totalSet = "";
            //wi_1.totalSetNum = 100;
            //wi_1.changeSet = "";
            //wi_1.changeSetNum = 0;
            //l.Add(wi_1);

            //WarningInf wi_2 = new WarningInf();
            //wi_2.itemName = "预警2";
            //wi_2.totalSet = "";
            //wi_2.totalSetNum = 10;
            //wi_2.changeSet = "";
            //wi_2.changeSetNum = 5;
            //l.Add(wi_2);

            return l;
        }
    }

    [Serializable]
    public class AppInfo
    {
        public bool needUpdate;
        public string appVersion;
        public string downloadURL;
    }

    [Serializable]
    public class UserAccount
    {
        public bool isLogin;
        public string errorCode;
        public string errorMsg;
        public string userId;
        public string userName;
        public bool haveMsg;
        public string msgContent;
    }

    [Serializable]
    public class RegisterInf
    {
        public string txtUserName;
        public string txtPWD;
        public string txtRealName;
        public string txtDianHua;
        public string txtShouJi;
        public string txtDiZhi;
        public string txtYouXiang;
    }

    [Serializable]
    public class WarningInf
    {
        public string warningName = "";
        public string itemName = "";
        public string itemId = "";
        public string totalSet = "";
        public int totalSetNum = 0;
        public string changeSet = "";
        public int changeSetNum = 0;
    }
}