using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Reflection;
using System.Data;
using System.Web.Services;

namespace Patentquery.My
{
    public partial class frmCnTbSearch : System.Web.UI.Page
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }
            try
            {
                if (HttpContext.Current.Session["RealName"] == null)
                {
                    //UserLogin.GetYouKe();
                }

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
            }

            InitEntrances();
        }

        private static bool validateAn(string strSearchQuery)
        {
            bool res = true;
            return res;
        }

        private void InitEntrances()
        {
            try
            {
                //ConnectionString  Cn_Entrances  //select Cn_Entrances from TLC_Users where UserId={0}  //
                string strValues = ProXZQDLL.TbUserSvs.getEntrances(ProXZQDLL.TbUserSvs.EntrancesType.Cn, Session["UserID"].ToString());
                //DBA.SqlDbAccess.ExecuteScalar(CommandType.Text, string.Format("select Cn_Entrances from TbUser where ID={0}", Convert.ToInt32(Session["UserID"]))).ToString();
                if (strValues.Equals("NULL"))
                {
                    hfSelEntrances.Value = "";
                }
                else
                {
                    hfSelEntrances.Value = strValues;
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 更新表格检索项配置
        /// </summary>
        /// <param name="strEntrances"></param>
        /// <param name="_strCfgType"></param>
        /// <returns></returns>
        [WebMethod]
        public static string updateEntrancesCfg(string strEntrances, string _strCfgType)
        {
            string strRs = "";
            try
            {
                ProXZQDLL.TbUserSvs.EntrancesType entType = ProXZQDLL.TbUserSvs.EntrancesType.Cn;
                if (!_strCfgType.ToUpper().Equals("CN"))
                {
                    entType = ProXZQDLL.TbUserSvs.EntrancesType.En;
                }

                strEntrances = HttpUtility.UrlDecode(strEntrances).ToUpper().Trim();

                string strLoginUid = System.Web.HttpContext.Current.Session["UserID"].ToString();

                if (ProXZQDLL.TbUserSvs.updateEntrancesCfg(strEntrances, entType, strLoginUid))
                {
                    strRs = "succ";
                }
                else
                {
                    strRs = "error";
                }
            }
            catch (Exception ex)
            {
                strRs = "error";
            }
            return strRs;
        }
    }
}