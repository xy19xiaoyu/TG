using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cpic.Cprs2010.Cfg;

namespace Patentquery.My
{
    public partial class frmFLZT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string AppNo = Request.QueryString["AppNo"];
                AppNo = UrlParameterCode_DE.DecryptionAll(AppNo);
                AppNo += CnAppLicationNo.getValidCode(AppNo);
                getFLZT(AppNo);
            }
        }

        private void getFLZT(string AppNo)
        {
            string status = string.Empty;
            try
            {

                WSFLZT.Service ss = new WSFLZT.Service();
                 status = ss.getFaLvZhuangTaiSimple(AppNo);
            }
            catch (Exception ex)
            {
                Response.Redirect("../newimg/timeout.jpg");
            }
            int flag = 1;

            switch (status)
            {
                case "有效":
                    flag = 1;
                    break;
                case "审中":
                    flag = 2;
                    break;
                case "失效":
                    flag = 3;
                    break;
            }
            Response.Redirect("../newimg/" + flag.ToString() + ".jpg");


        }
    }
}