using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Patentquery.My
{
    public partial class frmLawInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["idx"] == null)
                {
                    return;
                }
                RefGrvFL();
            }
        }

        /// <summary>
        /// 中国专利法律状态检索
        /// </summary>
        private void RefGrvFL()
        {
            SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
            SearchInterface.WSFLZT.CnLegalStatus[] currentDataSet = search.getFalvZhuangTai(Request.QueryString["idx"]);


            GridView1.DataSource = currentDataSet;
            GridView1.DataBind();
        }
    }
}