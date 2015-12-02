using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using ProXZQDLL;

namespace Patentquery.My
{
    /// <summary>
    /// UserService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class UserService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public string RegUserinfo(string userid,  string psd,string username,string email)
        {
            DataSet ds = new DataSet();

            
            TbUser user = new TbUser();
            string usertype = userid.Substring(4, 1);
            user.UserName = userid.Trim();
            user.UserPWD = psd.Trim();
            user.RealName = username;
            user.YongHuLeiXing = usertype;

            user.EMail = email.Trim();
            user.DepartMentID = 0;
            user.SHFlag = 0;
            DataTable dt = new DataTable();

            string sql = "select * from tbuser where username='" + user.UserName + "'";
            dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql, null);
            
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                db.Log = Console.Out;
                db.TbUser.InsertOnSubmit(user);
                db.SubmitChanges();
            }
            if (dt.Rows.Count > 0)
                return "1";

            string sqlShouCang = "insert into TLC_Albums (UserId,ParentId,Title,live,isdel,isparent) values('" + user.ID.ToString().Trim() + "',0,'收藏夹',0,0,0)";
            string cusertype = "";
            if (DBA.DbAccess.ExecNoQuery(CommandType.Text, sqlShouCang) < 0)
            {
                return "-1";
            }
            switch (usertype)
            {
                case "0":
                    cusertype = "个人";
                    break;
                case "1":
                    cusertype = "企业";
                    break;
                case "2":
                    cusertype = "事业单位";
                    break;
                case "3":
                    cusertype = "政府机关";
                    break;
                case "4":
                    cusertype = "社会团体";
                    break;
                case "5":
                    cusertype = "其他类型单位";
                    break;
                case "9":
                    cusertype = "简易账户";
                    break;
            }
            sqlShouCang = "select * from TbRole where rolename='" + cusertype + "'";
            dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, sqlShouCang, null);
            if (dt.Rows.Count == 0)
            {
                return "-1";
            }
            string roleid = dt.Rows[0]["ID"].ToString();
            sqlShouCang = "insert into UserRole (roleid,userid) values ('" + roleid + "','" + user.ID.ToString() + "') ";
            if (DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, sqlShouCang, null) < 0)
            {
                return "-1";
            }
            return "1";            
            
        }

        
    }
}
