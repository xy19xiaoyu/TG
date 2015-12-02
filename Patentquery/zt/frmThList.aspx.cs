using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using TLC;
using ProXZQDLL;


namespace Patentquery.zt
{
    public partial class frmThList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                spaCreateZtDb.Visible = getVisible("addzt");
                delzt.Value = getVisible("delzt").ToString();
                edzt.Value = getVisible("edzt").ToString();

                string IP = HttpContext.Current.Request.UserHostAddress.ToString(); ;//HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
                ProXZQDLL.TbUser tbSesionUser = (ProXZQDLL.TbUser)Session["UserInfo"];
                ProXZQDLL.ClsLog.LogInsert(IP, tbSesionUser.RealName, tbSesionUser.YongHuLeiXing, "专利专题数据库");
            }
        }

        public bool getVisible(string strRightCode)
        {
            bool bRs = false;
            try
            {
                bRs = UserRight.getVisibleRight(Session["UserID"].ToString(), strRightCode);
            }
            catch (Exception ex)
            {
            }
            return bRs;
        }

        [WebMethod]
        public static string GetPageList(int PageIndex, int PageSize)
        {
            int count = 0;
            List<ZtDbList> res = GetDatat(PageIndex, PageSize, out count);
            return JsonHelper.ListToJson<ZtDbList>(res, "rows", count.ToString());
        }
        [System.Web.Services.WebMethod]
        public static string addZtDb(string ZtDbname, int nType, string ztDbdes, string strImg)
        {
            return ztHelper.addZtDb(ZtDbname, nType, ztDbdes, strImg);
        }

        [System.Web.Services.WebMethod]
        public static string UpDateZtDb(string zid, string ZtDbname, string ztDbdes, string strImg)
        {
            return ztHelper.UpDateZtDb(zid, ZtDbname, ztDbdes, strImg);
        }
        [System.Web.Services.WebMethod]
        public static string DelZtDb(string zid)
        {
            return ztHelper.DelZtDb(zid);
        }
        public static List<ZtDbList> GetDatat(int PageIndex, int PageSize, out int count)
        {
            PQDataContext db = new PQDataContext();
            var rs = from item in db.ZtDbList
                     where item.dbType == 0 && item.IsDel == false
                     orderby item.CreateTime
                     select item;

            count = rs.Count();
            return rs.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList<ZtDbList>();
        }

    }
}