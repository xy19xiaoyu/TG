using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Patentquery.My
{
    public partial class frmWdTbSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    InitEntrances();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void InitEntrances()
        {
            //ConnectionString  Cn_Entrances
            string strValues = ProXZQDLL.TbUserSvs.getEntrances(ProXZQDLL.TbUserSvs.EntrancesType.En, Session["UserID"].ToString());
            //DBA.SqlDbAccess.ExecuteScalar(CommandType.Text, string.Format("select En_Entrances from TLC_Users where UserId={0}", Convert.ToInt32(Session["UserID"]))).ToString();
            if (strValues.Equals("NULL"))
            {
                hfSelEntrances.Value = "";
            }
            else
            {
                hfSelEntrances.Value = strValues;
            }
        }
    }
}